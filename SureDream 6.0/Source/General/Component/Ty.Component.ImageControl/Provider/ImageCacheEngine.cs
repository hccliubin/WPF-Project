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
        /// <summary> 容器量 </summary>
        public int Capacity { get; set; } = 3;

        public string LocalFolder { get; set; }

        //  Message：所有的文件列表
        List<ImageCacheEntity> _fileCollection = new List<ImageCacheEntity>();

        //  Message：下载完成等待播放的队列
        List<ImageCacheEntity> _cache = new List<ImageCacheEntity>();

        Queue<Task> _tasks = new Queue<Task>();

        public ImageCacheEngine(List<string> filePath, string localFolder, string startFile, string user, string password, string ip = "")
        {
            foreach (var item in filePath)
            {
                string localPath = Path.Combine(localFolder, Path.GetFileName(item));

                ImageCacheEntity entity = new ImageCacheEntity(item, localPath, user, password, ip);

                _fileCollection.Add(entity);
            }

            this.LocalFolder = localFolder;

            this.SetStartFile(startFile);
        }

        public void Start()
        {
            Task.Run(() =>
            {
                while (true)
                {
                    int index = _fileCollection.FindIndex(l => l.FilePath == this._startFile);

                    this._cache = _fileCollection.Skip(index).Take(this.Capacity).ToList();

                    foreach (var item in this._cache)
                    {
                        if (item.IsLoaded == 1 || item.IsLoaded == 2) continue;

                        //  Message：最多执行5个下载任务
                        if (_tasks.Count >= 2) continue;

                        //  Message：启动下
                        Task current = Task.Run(() =>
                        {

                            Debug.WriteLine($"开始下载任务:{item.FilePath}-当前任务数量{_tasks.Count}");

                            item.Start();
                        });

                        //  Message：添加一个下载任务
                        _tasks.Enqueue(current);

                        //  Do ：下载完成保存到队列中
                        current.ContinueWith(l =>
                        {  
                            //  Message：下载完成输出任务 
                            _tasks.Dequeue();

                            Debug.WriteLine($"完成下载任务:{item.LocalPath}-当前任务数量{_tasks.Count}");

                        });
                    }
                     

                    //foreach (var item in collection)
                    //{
                    //    if (item.IsLoaded == 2)
                    //    {
                    //        _cache.Enqueue(item);
                    //    }
                    //    else
                    //    {

                    //    }
                    //    //  Message：最多执行5个下载任务
                    //    if (_tasks.Count >= 2) continue;

                    //    //  Message：如果待播放列表已满则不下载
                    //    if (_cache.Count + _tasks.Count >= this.Capacity) continue;

                    //    //  Message：启动下
                    //    Task current = Task.Run(() =>
                    //    {
                    //        item.Start();
                    //    });

                    //    //  Message：添加一个下载任务
                    //    _tasks.Enqueue(current);

                    //    //  Do ：下载完成保存到队列中
                    //    current.ContinueWith(l =>
                    //    {
                    //        _cache.Enqueue(item);

                    //        Debug.WriteLine($"缓存完成:当前缓存数量-{_cache.Count}");

                    //        //  Message：下载完成输出任务 
                    //        _tasks.Dequeue();

                    //        Debug.WriteLine($"任务完成:当前任务数量-{_tasks.Count}");

                    //    });
                    //}

                    Thread.Sleep(100);
                }
            });

        }

        public void Stop()
        {

        }

        /// <summary> 是否存在下好的文件 </summary>
        public bool IsContain()
        {
            return _cache.Count > 0;
        }


        string _startFile;

        /// <summary> 用于跳到指定位置 </summary>
        public void SetStartFile(string file)
        {
            _startFile = file;
        }

        /// <summary> 获取下好的文件 返回null则需要等待 </summary>
        public string Play(string file)
        {
            this.SetStartFile(file);

            var result= this._cache.Find(l => l.FilePath == file);

            if (result == null) return null;

            if (result.IsLoaded == 2) return result.LocalPath;

            return null; 
        }

        public string PlayWait(string file)
        {
            while(true)
            {
              var result= this.Play(file);

                if(!string.IsNullOrEmpty(result))
                {
                    return result;
                }

                Thread.Sleep(1000);
            }
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
            if (File.Exists(this.LocalPath))
            {
                this.IsLoaded = 2;
                return;
            }

            this.IsLoaded = 1;

            Debug.WriteLine("开始下载:" + this.FilePath);

            if (this.CacheType == 0)
            {
                //FTPHelper helper = new FTPHelper(this._userName, this._passWord);

                //helper.DownLoadFile(this.FilePath, this.LocalPath);
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
