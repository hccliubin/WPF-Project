using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserControl6C_Right2C.Model
{
  public  class _2C_Config
    {
        #region 不同的播放速度，每一秒根据参数设置来确定，每一秒播放多少张图片
        //比如设置默认Second1X一秒播放5张图片
        //Second2X一秒播放10张图片
        //Second3X一秒播放10张图片
        private int _Second1X;
        /// <summary>
        /// 1X，是正常播放速度
        /// </summary>
        public int Second1X
        {
            get { return _Second1X; }
            set { _Second1X = value; }
        }

        private int _Second2X;
        /// <summary>
        /// 2X
        /// </summary>
        public int Second2X
        {
            get { return _Second2X; }
            set { _Second2X = value; }
        }

        private int _Second4X;
        /// <summary>
        /// 4X
        /// </summary>
        public int Second4X
        {
            get { return Second4X; }
            set { Second4X = value; }
        }

        private int _Second8X;
        /// <summary>
        /// 8X
        /// </summary>
        public int Second8X
        {
            get { return _Second8X; }
            set { _Second8X = value; }
        }


        private int _SecondLose2X;
        /// <summary>
        /// -2X
        /// </summary>
        public int SecondLose2X
        {
            get { return SecondLose2X; }
            set { SecondLose2X = value; }
        }


        private int _SecondLose4X;
        /// <summary>
        /// -4X
        /// </summary>
        public int SecondLose4X
        {
            get { return _SecondLose4X; }
            set { _SecondLose4X = value; }
        }
        #endregion
        private int _CacheTime;
        /// <summary>
        /// 缓存时间，单位是：秒；当加载数据的提示出现后，应该是把当前位置往后CacheTime秒的数据都缓存下来后才能继续播放；这样能保证加载数据完成后，往后的CacheTime秒在播放时都是流畅的，不会经常卡顿。
        /// </summary>
        public int CacheTime
        {
            get { return _CacheTime; }
            set { _CacheTime = value; }
        }
        private int _TotalCacheTime;
        /// <summary>
        /// 总缓存时间，单位是：秒；当用户暂停后或者正常播放过程中，也应做后续缓存操作，最多做TotalCacheTime秒钟的缓存，而不是无限制的把整个数据源都缓存下来。最多做TotalCacheTime分钟的缓存。
        /// </summary>
        public int TotalCacheTime
        {
            get { return TotalCacheTime; }
            set { TotalCacheTime = value; }
        }
    }
}
