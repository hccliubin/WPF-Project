using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Ty.Component.ImageControl
{
    public class ImageCacheEngine
    {

        public string ID { get; set; }

        /// <summary> 容器量 </summary>
        public int Capacity { get; set; } = 10;

        /// <summary> 容器量 </summary>
        public int CapacityTotal { get; set; } = 10;

        /// <summary> 同时下载的任务数量 </summary>
        public int TaskCount { get; set; } = 5;

        public string LocalFolder { get; set; }

        //  Message：所有的文件列表
        List<ImageCacheEntity> _fileCollection = new List<ImageCacheEntity>();

        //  Message：下载完成等待播放的队列
        List<ImageCacheEntity> _cache = new List<ImageCacheEntity>();

        //  Message：后台一直下载的 默认下载五分钟的数据
        List<ImageCacheEntity> _cacheBack = new List<ImageCacheEntity>();

        Queue<Task> _tasks = new Queue<Task>();

        Queue<Task> _taskBacks = new Queue<Task>();

        public ImageCacheEngine(List<string> filePath, string localFolder, string startFile, string user, string password, string ip = "")
        {
            this.ID = Guid.NewGuid().ToString();

            this.LocalFolder = Path.Combine(localFolder, this.ID);

            if (!Directory.Exists(this.LocalFolder))
            {
                Directory.CreateDirectory(this.LocalFolder);

            }

            foreach (var item in filePath)
            {
                string localPath = Path.Combine(localFolder, this.ID, Path.GetFileName(item));

                ImageCacheEntity entity = new ImageCacheEntity(item, localPath, user, password, ip);

                _fileCollection.Add(entity);
            }

            _current = _fileCollection.Find(l => l.FilePath == startFile); ;
        }


        public void RefreshCapacity(int count)
        {
            //  Do：可播放队列设置15s
            //this.Capacity = count;

            this.Capacity = count * 5;

            ////Do：后台缓存最多队列设置成5分钟
            //this.CapacityTotal = count * 5 * 60;

            this.CapacityTotal = count * 1 * 10;
        }
 

        public void Start()
        {
            this.RefreshPosition(this._current.FilePath);
        }

        Queue<CancellationTokenSource> _control = new Queue<CancellationTokenSource>();

        /// <summary> 当位置改变时触发，停止上一个下载任务，开始新位置的下载任务 </summary>
        void RefreshPosition(string current)
        {
            if (_control.Count > 0)
            {
                _control.Dequeue().Cancel();
            }

            CancellationTokenSource cts1 = new CancellationTokenSource();

            _control.Enqueue(cts1);

            //  Message：启动当前位置的顺序下载任务
            Task.Run(() =>
            {
                while (true)
                {
                    //  Message：请求退出时退出
                    if (cts1.IsCancellationRequested)
                    {

                        Debug.WriteLine("任务取消:");

                        break;
                    }

                    int index = _fileCollection.FindIndex(l => l.FilePath == _current.FilePath);

                    this._cache = _fileCollection.Skip(index).ToList();

                    //  Message：并行运行
                    ParallelLoopResult result = Parallel.For(0, 5, k =>
                    {
                        for (int i = 0; i < this._cache.Count; i++)
                        {
                            //  Message：请求退出时退出
                            if (cts1.IsCancellationRequested)
                            {
                                Debug.WriteLine("任务取消:");
                                break;
                            }

                            index = this._cache.FindIndex(l => l.FilePath == _current.FilePath);

                            //  Message：检查是否操出当前最大缓冲索引
                            if (i - index > this.CapacityTotal) break;

                            //  Message：已经加载继续
                            if (this._cache[i].IsLoaded != 0) continue;

                            this._cache[i].Start();
                        }

                    });

                }

            }, cts1.Token);
        }

        public void Stop()
        {
            if (_control.Count > 0)
            {
                _control.Dequeue().Cancel();
            }


            flag = false;
        }

        //  Message：当前播放的节点
        ImageCacheEntity _current;

        Queue<Boolean> _taskControlTemp = new Queue<Boolean>();

        bool flag = true;

        /// <summary> 获取下好的文件 返回null则需要等待 </summary>
        public string GetWaitCurrent(string file, Action<bool, int, int> action)
        {
            //  Message：如果没下载完，则等待15s数据都下载完为止
            flag = false;

            var result = this._fileCollection.Find(l => l.FilePath == file);

            int last = this._fileCollection.FindIndex(l => l.FilePath == _current.FilePath);

            int now = this._fileCollection.FindIndex(l => l.FilePath == file);

            _current = result;

            //  Message：如果时间相差较多，重新分配下载区间
            if (now - last > 5 * 15)
            {
                this.RefreshPosition(_current.FilePath);
            }

            if (last - now > 0)
            {
                this.RefreshPosition(_current.FilePath);
            }

            if (result.IsLoaded == 2)
            {
                return result.LocalPath;
            }
            else
            {
                Thread.Sleep(1000);

                flag = true;

                var waitCache = _fileCollection.Skip(now).Take(this.Capacity).ToList();

                while (!waitCache.TrueForAll(l => l.IsLoaded == 2))
                {
                    if (!flag)
                    {
                        Debug.WriteLine("取消等待");
                        return null;
                    }
                    Thread.Sleep(500);

                    action(false, waitCache.FindAll(l => l.IsLoaded == 2).Count, waitCache.Count);
                }
                 
                action(true, waitCache.FindAll(l => l.IsLoaded == 2).Count, waitCache.Count);

                return result.LocalPath;
            }
        }

        /// <summary> 清理缓存数据 </summary>
        public void Clear()
        {
            Directory.Delete(this.LocalFolder);
        }

        //  Message：是否是向前播放
        bool _isForward = true;

        public void RefreshPlayMode(bool forward)
        {
            if (_isForward = forward) return;

            _isForward = forward;

            _fileCollection.Reverse();

            this.RefreshPosition(this._current.FilePath);
        }
    }

    class ImageCacheEntity
    {


        public ImageCacheEntity(string path, string localPath, string user, string pw, string ip)
        {
            this.CacheType = ip == "" ? 0 : 1;
            this.FilePath = path;
            this.LocalPath = localPath;

        }

        object locker = new object();

        int _isLoaded = 0;

        /// <summary> 下载状态 0 未开始 1 正在下载 2 已经下载完成 </summary>
        public int IsLoaded
        {
            get
            {
                //  Message：加锁
                lock (locker)
                {
                    return _isLoaded;
                };
            }
            set
            {
                lock (locker)
                {
                    _isLoaded = value;
                };
            }
        }

        public int CacheType { get; set; }

        public string FilePath { get; set; }

        public string LocalPath { get; set; }

        public void Start()
        {
            this.IsLoaded = 1;

            if (File.Exists(this.LocalPath))
            {
                this.IsLoaded = 2;
                return;
            }

            if (this.CacheType == 0)
            {
                //FTPHelper helper = new FTPHelper(this._userName, this._passWord);

                //helper.DownLoadFile(this.FilePath, this.LocalPath);

                if (!File.Exists(this.LocalPath))
                {

                    //Debug.WriteLine("正在下载:" + this.FilePath);

                    FtpHelper.DownLoadFile(this.FilePath, this.LocalPath);

                    //Thread.Sleep(1000);

                }

            }
            else
            {
                File.Copy(this.FilePath, this.LocalPath, false);
            }

            Thread.Sleep(1000);

            this.IsLoaded = 2;

            Debug.WriteLine("下载完成:" + this.FilePath);
        }

        public void Stop()
        {

        }
    }
}
