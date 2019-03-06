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
using Ty.Component.MediaControl;

namespace SureDream.Appliaction.Demo.MediaControl
{
    /// <summary>
    /// MulMediaPlayerWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MulMediaPlayerWindow : Window
    {
        public MulMediaPlayerWindow()
        {
            InitializeComponent();
        }

        List<IVdeioImagePlayerService> vdeioImagePlayerServices = new List<IVdeioImagePlayerService>();

        private void Btn_load_Click(object sender, RoutedEventArgs e)
        {
            vdeioImagePlayerServices.Clear();

            //  Do：根据数量初始化控件
            int c = int.Parse(this.txt_count.Text);

            for (int i = 0; i < c; i++)
            
            {
                IVdeioImagePlayerService vedioImagePlayerControl = new VedioImagePlayerControl();

                IImgOperate _imgOperate = vedioImagePlayerControl.ImagePlayerService.GetImgOperate();

                _imgOperate.SetMarkType(MarkType.Defect);

                List<ImgMarkEntity> temp = new List<ImgMarkEntity>();

                vedioImagePlayerControl.ImagePlayerService.ImgPlayModeChanged += l =>
                {
                    Debug.WriteLine("ImgPlayModeChanged:" + vedioImagePlayerControl.ImagePlayerService.ImgPlayMode);
                    Debug.WriteLine("ImgPlayModeChanged:" + l);
                };

                vedioImagePlayerControl.FullScreenHandle += () =>
                {
                    Debug.WriteLine("FullScreenHandle");
                };


                vedioImagePlayerControl.ImagePlayerService.ImageIndexChanged += (k, j) =>
                {
                    Debug.WriteLine("ImageIndexChanged:" + k);
                    Debug.WriteLine("ImgSliderMode:" + j);


                    //  Message：加载Mark 20190105050908[2019-01-06-01-58-42].mark

                    //string current1 = _imgOperate.BuildEntity().Current.Value;

                    string current = k;

                    var tuple = vedioImagePlayerControl.ImagePlayerService.GetIndexWithTotal();

                    string fileName = System.IO.Path.GetFileNameWithoutExtension(current);

                    var foder = Directory.CreateDirectory(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory + "\\Marks"));

                    var collection = foder.GetFiles().Where(l => l.Name.StartsWith(fileName)).Select(l => l.FullName);

                    foreach (var item in collection)
                    {
                        string marks = File.ReadAllText(item);

                        var list = JsonConvert.DeserializeObject<List<ImgMarkEntity>>(marks);

                        //foreach (var c in list)
                        //{
                        //    c.Code = Guid.NewGuid().ToString();
                        //}

                        _imgOperate.LoadMarkEntitys(list);
                    }
                };

                //  Do：1、注册编辑标定事件 包括新增、删除
                _imgOperate.ImgMarkOperateEvent += l =>
                {
                    temp.Clear();

                    string fn = System.IO.Path.GetFileNameWithoutExtension(_imgOperate.GetCurrentUrl());

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

                _imgOperate.MarkEntitySelectChanged += l =>
                {
                    Debug.WriteLine("MarkEntitySelectChanged:" + l.DetectDate);
                };

                vdeioImagePlayerServices.Add(vedioImagePlayerControl);
            }

            this.control_mulMedia.MediaSources = vdeioImagePlayerServices;
        }

        /// <summary> 获取标定信息存放路径 </summary>
        string GetMarkFileName(string imgName)
        {
            string file = DateTime.Now.ToString("yyyy-MM-dd-hh-mm-ss");

            string tempFiles = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory + "\\Marks", imgName + "[" + file + "].mark");

            if (!File.Exists(tempFiles)) File.WriteAllText(tempFiles, string.Empty);

            return tempFiles;
        }

        private void Btn_loadImages_Click(object sender, RoutedEventArgs e)
        {
            foreach (var item in vdeioImagePlayerServices)
            {
                string filePath1 = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "images");
                string filePath2 = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "images1");
                List<string> folders = new List<string>();

                folders.Add(filePath1);
                folders.Add(filePath2);

                item.LoadImageFolder(folders, filePath1);
            }
        }
    }
}
