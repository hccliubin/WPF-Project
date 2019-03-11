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
        public int CapacityBack { get; set; } = 10;

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



            this.SetStartFile(startFile);
        }


        public void RefreshCapacity(int count)
        {
            //  Do：可播放队列设置15s
            this.Capacity = count * 5;

            //  Do：后台缓存最多队列设置成5分钟
            this.CapacityBack = count * 5 * 60 * 1000;
        }

        CancellationTokenSource cts;

        public void Start()
        {
            if (cts != null)
            {
                cts.Cancel();
            }

            cts = new CancellationTokenSource();

            Task.Run(() =>
            {
                while (true)
                {
                    if (cts.IsCancellationRequested) break;

                    int startIndex = _fileCollection.FindIndex(l => l.FilePath == this._startFile);

                    this._cache = _fileCollection.Skip(startIndex).Take(this.Capacity).ToList();

                    //  Message：优先下载等待部分
                    foreach (var item in this._cache)
                    {
                        if (item.IsLoaded == 1 || item.IsLoaded == 2) continue;

                        //  Message：最多执行5个下载任务
                        if (_tasks.Count >= TaskCount) continue;

                        //  Message：启动下
                        Task current = Task.Run(() =>
                        {
                            item.Start();

                            //  Message：下载完成输出任务 
                            _tasks.Dequeue();
                        });

                        //  Message：添加一个下载任务
                        _tasks.Enqueue(current);
                    }

                    //  Message：后台下缓存部分
                    int playIndex = _fileCollection.FindIndex(l => l.FilePath == this._playFile);

                    this._cacheBack = _fileCollection.Skip(playIndex).Take(this.CapacityBack).ToList();

                    //  Message：优先下载等待部分
                    foreach (var item in this._cacheBack)
                    {
                        if (item.IsLoaded == 1 || item.IsLoaded == 2) continue;

                        //  Message：最多执行5个下载任务
                        if (_taskBacks.Count >= TaskCount) continue;

                        //  Message：启动下
                        Task current = Task.Run(() =>
                        {
                            item.Start();

                            //  Message：下载完成输出任务 
                            _taskBacks.Dequeue();
                        });

                        //  Message：添加一个下载任务
                        _taskBacks.Enqueue(current);
                    }

                    Thread.Sleep(10);
                }
            }, cts.Token);

        }

        public void Stop()
        {
            cts.Cancel();
        }

        /// <summary> 是否存在下好的文件 </summary>
        public bool IsContain()
        {
            return _cache.Count > 0;
        }


        //  Message：用于检查可播放部分节点
        string _startFile;

        //  Message：用于当前播放到的节点
        string _playFile;

        /// <summary> 用于跳到指定位置 </summary>
        public void SetStartFile(string file)
        {
            _startFile = file;

            _playFile = file;

            this._cache.Clear();
        }

        /// <summary> 获取下好的文件 返回null则需要等待 </summary>
        public string BeginPlay(string file)
        {
            var result = this._cache.Find(l => l.FilePath == file);

            if (result == null)
            {
                _startFile = file;
                return null;
            }

            //if (result.IsLoaded == 2) return result.LocalPath;

            //  Message：检查所有cache列表都可以播放时才播放

            if (this._cache.TrueForAll(l => l.IsLoaded == 2))
            {
                this._playFile = file;

                return result.LocalPath;
            }

            return null;
        }

        public string PlayWait(string file)
        {
            while (true)
            {
                var result = this.BeginPlay(file);

                if (!string.IsNullOrEmpty(result))
                {
                    return result;
                }

                Thread.Sleep(1000);
            }
        }

        /// <summary> 清理缓存数据 </summary>
        public void Clear()
        {
            Directory.Delete(this.LocalFolder);
        }
    }

    class ImageCacheEntity
    {

        string _userName;

        string _passWord;

        string _ip;


        public ImageCacheEntity(string path, string localPath, string user, string pw, string ip)
        {
            this.CacheType = ip == "" ? 0 : 1;
            this.FilePath = path;
            this.LocalPath = localPath;

        }

        /// <summary> 下载状态 0 未开始 1 正在下载 2 已经下载完成 </summary>
        public int IsLoaded { get; set; } = 0;

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

                    //Thread.Sleep(2000);

                    Debug.WriteLine("下载完成:" + this.FilePath);

                }

            }
            else
            {
                File.Copy(this.FilePath, this.LocalPath, false);
            }

            Thread.Sleep(2000);

            this.IsLoaded = 2;

            Debug.WriteLine("下载完成:" + this.FilePath);
        }

        public void Stop()
        {

        }
    }
}
