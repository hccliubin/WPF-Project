using CDTY.DataAnalysis.Entity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Ty.Component.ImageControl;

namespace SureDream.Appliaction.Demo.MediaControl
{
    /// <summary>
    /// ShellWindow.xaml 的交互逻辑
    /// </summary>
    public partial class ShellWindow : Window
    {
        public ShellWindow()
        {
            InitializeComponent();

            IImgOperate _imgOperate = this.media.ImagePlayerService.GetImgOperate();

            List<ImgMarkEntity> temp = new List<ImgMarkEntity>();

            //  Do：1、注册编辑标定事件 包括新增、删除
            _imgOperate.ImgMarkOperateEvent += l =>
            {
                temp.Clear();

                string fn = System.IO.Path.GetFileNameWithoutExtension(_imgOperate.BuildEntity().Current.Value);

                string file = this.GetMarkFileName(fn);

                string str = l.markOperateType.ToString();

                Debug.WriteLine(str + "：" + l.Name + "-" + l.Code + $"({l.X},{l.Y}) {l.Width}*{l.Height}");

                temp.Add(l);

                string result = JsonConvert.SerializeObject(temp);

                File.WriteAllText(file, result);

                MessageBox.Show(str + "：" + l.Name + "-" + l.Code + $"({l.X},{l.Y}) {l.Width}*{l.Height}", "保存成功");

            };

            //  Do：2、注册风格化处理事件
            _imgOperate.ImgProcessEvent += (l, k) =>
            {
                Debug.WriteLine("图片路径：" + l);

                Debug.WriteLine("操作参数：" + k);

                MessageBox.Show(k.ToString());
            };

            //  Do：3、注册上一页事件 需要在此处加载上一页的标定 ImgMarkEntity
            _imgOperate.PreviousImgEvent += () =>
            {
                Debug.WriteLine("PreviousImgEvent");

                //  Message：加载Mark 20190105050908[2019-01-06-01-58-42].mark

                string current = _imgOperate.BuildEntity().Current.Value;

                string fileName = System.IO.Path.GetFileNameWithoutExtension(current);

                var foder = Directory.CreateDirectory(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory + "\\Marks"));

                var collection = foder.GetFiles().Where(l => l.Name.StartsWith(fileName)).Select(l => l.FullName);

                foreach (var item in collection)
                {
                    string marks = File.ReadAllText(item);

                    var list = JsonConvert.DeserializeObject<List<ImgMarkEntity>>(marks);

                    _imgOperate.LoadMarkEntitys(list);
                }

            };

            //  Do：4、注册上一页事件 需要在此处加载下一页的标定 ImgMarkEntity
            _imgOperate.NextImgEvent += () =>
            {
                Debug.WriteLine("NextImgEvent");

                //  Message：加载Mark

                //  Message：加载Mark 20190105050908[2019-01-06-01-58-42].mark

                string current = _imgOperate.BuildEntity().Current.Value;

                string fileName = System.IO.Path.GetFileNameWithoutExtension(current);

                var foder = Directory.CreateDirectory(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory + "\\Marks"));

                var collection = foder.GetFiles().Where(l => l.Name.StartsWith(fileName)).Select(l => l.FullName);

                foreach (var item in collection)
                {
                    string marks = File.ReadAllText(item);

                    var list = JsonConvert.DeserializeObject<List<ImgMarkEntity>>(marks);

                    _imgOperate.LoadMarkEntitys(list);
                }


            };

            //  Do：5、注册绘制矩形框结束事件 需要在此处弹出缺陷管理控件，并设置如下参数
            _imgOperate.DrawMarkedMouseUp += (l, k) =>
            {
                Debug.WriteLine(l);
                Debug.WriteLine(k);

                //  Do：选择的责任工区
                l.SelectResponsibilityWorkArea = new TyeBaseDepartmentEntity();
                //  Do：选择的责任车间
                l.SelectResponsibilityWorkshop = new TyeBaseDepartmentEntity();
                //  Do：选择的单元
                l.SelectBasicUnit = new TyeBasePillarEntity();
                //  Do：选择的站
                l.SelectDedicatedStation = new TyeBaseSiteEntity();
                //  Do：选择的段
                l.SelectDedicatedLine = new TyeBaseLineEntity();
                //  Do：选择的铁路局顺序码
                l.SelectRailwaySsequence = new TyeBaseRailwaystationEntity();
                //  Do：选择的数据采集方式
                l.SelectDataAcquisitionMode = new TyeBaseDatacollecttypeEntity();
                //  Do：PHM编码（基本由界面属性组合而成）
                l.PHMCodes = "PHM编码（基本由界面属性组合而成）";
                //  Do：当前用户
                l.tyeAdminUserEntity = new TyeAdminUserEntity();
                //  Do：检测日期
                l.DetectDate = DateTime.Now;
                //  Do：公里标
                l.KmLog = "公里标";
                //  Do：检测车辆
                l.DetectionVehicles = "检测车辆";

                //  Do：选择的缺陷
                l.SelectDefectOrMarkCodes = new TyeEncodeDeviceEntity();

                //  Do：选择的历史信息
                l.SelectCommonHistoricalDefectsOrMark = new DefectCommonUsed();

                _imgOperate.AddMark(l);

                //_imgOperate.CancelAddMark();
            };
        }

        /// <summary> 获取标定信息存放路径 </summary>
        string GetMarkFileName(string imgName)
        {
            string file = DateTime.Now.ToString("yyyy-MM-dd-hh-mm-ss");

            string tempFiles = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory + "\\Marks", imgName + "[" + file + "].mark");

            if (!File.Exists(tempFiles)) File.WriteAllText(tempFiles, string.Empty);

            return tempFiles;
        }

        //  Message：播放avi
        private void Btn_play_avi_Click(object sender, RoutedEventArgs e)
        {
            string filePath = "http://download.blender.org/peach/bigbuckbunny_movies/big_buck_bunny_480p_surround-fix.avi";

            this.media.LoadVedio(filePath);
        }

        //  Message：播放mkv
        private void Btn_play_mkv_Click(object sender, RoutedEventArgs e)
        {
            string filePath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "media.mkv");

            this.media.LoadVedio(filePath);

        }
        //  Message：播放MP4
        private void Btn_play_mp4_Click(object sender, RoutedEventArgs e)
        {
            string filePath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "media.mp4");

            this.media.LoadVedio(filePath);
        }

        //  Message：播放本地
        private void Btn_play_local_Click(object sender, RoutedEventArgs e)
        {
            string filePath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "media.mp4");

            this.media.LoadVedio(filePath);
        }

        //  Message：播放局域网共享
        private void Btn_play_localarea_Click(object sender, RoutedEventArgs e)
        {
            string filePath = @"\\Desktop-bem7r0b\视频格式大全\6-9+有关梯度下降法的更多深入讨论.mp4";

            this.media.LoadVedio(filePath);
        }

        //  Message：播放http
        private void Btn_play_http_Click(object sender, RoutedEventArgs e)
        {
            string filePath = "http://download.blender.org/peach/bigbuckbunny_movies/big_buck_bunny_480p_surround-fix.avi";

            this.media.LoadVedio(filePath);
        }

        //  Message：设置视频位置
        private void Btn_play_setpostion_Click(object sender, RoutedEventArgs e)
        {
            this.media.MediaPlayerService.SetPositon(TimeSpan.FromSeconds(60));
        }

        //  Message：在指定区间重复播放
        private void Btn_play_repeat_Click(object sender, RoutedEventArgs e)
        {
            this.media.MediaPlayerService.RepeatFromTo(TimeSpan.FromSeconds(60), TimeSpan.FromSeconds(70));
        }

        //  Message：视频截屏
        private void Btn_play_screen_Click(object sender, RoutedEventArgs e)
        {
            string fileName = DateTime.Now.ToString("yyyyMMddhhmmss");

            string filePath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "images", fileName + ".jpg");

            this.media.MediaPlayerService.ScreenShot(TimeSpan.FromSeconds(60), filePath);

            Process.Start(filePath);
        }
        //  Message：视频获取当前路径
        private void Btn_play_currentUrl_Click(object sender, RoutedEventArgs e)
        {
            var result = this.media.MediaPlayerService.GetCurrentUrl();

            MessageBox.Show(result);
        }

        //  Message：视频获取当前帧和总帧
        private void Btn_play_currentframe_Click(object sender, RoutedEventArgs e)
        {
            var result = "当前：" + this.media.MediaPlayerService.GetCurrentFrame();

            result += " - 总计：" + this.media.MediaPlayerService.GetTotalFrame();

            MessageBox.Show(result);
        }

        //  Message：播放ftp视频
        private void Btn_play_localftp_Click(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine("ftp");

            string filePath = "ftp://192.168.0.104/media.mkv";

            this.media.MediaPlayerService.Load(filePath);
        }

        //  Message：播放图片文件夹
        private void btn_imageplay_imagefoder_Click(object sender, RoutedEventArgs e)
        {
            string filePath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "images");

            this.media.LoadImageFolder(filePath);
        }

        //  Message：获取图片列表当前帧
        private void btn_imageplay_currentframe_Click(object sender, RoutedEventArgs e)
        {
            var t = this.media.ImagePlayerService.GetIndexWithTotal();

            MessageBox.Show($"当前：{t.Item1} - 总数：{t.Item2}");

        }

        //  Message：获取图片当前路径
        private void btn_imageplay_currentUrl_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(this.media.ImagePlayerService.GetCurrentUrl());
        }

        //  Message：播放ftp图片文件夹路径
        private void btn_imageftpplay_imagefoder_Click(object sender, RoutedEventArgs e)
        {
            List<string> folders = new List<string>();

            string filePath = @"ftp://Healthy:870210lhj@127.0.0.1/images/";

            folders.Add(filePath);

            this.media.LoadFtpImageFolder(folders, "Healthy", "870210lhj");
        }

        //  Message：播放图片集合
        private void Btn_play_imagelist_Click(object sender, RoutedEventArgs e)
        {
            string filePath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "images");

            var forder = Directory.CreateDirectory(filePath);

            List<string> imgs = forder.GetFiles().Select(l => l.FullName).ToList();

            this.media.LoadImages(imgs);
        }
    }
}
