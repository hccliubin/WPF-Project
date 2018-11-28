//using System;
//using System.Collections.Generic;
//using System.Diagnostics;
//using System.IO;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.Windows.Input;
//using System.Windows.Media.Imaging;

//namespace Ty.Component.ImageControl
//{
//    public partial class ImgOperate : IImgOperate
//    {

//        public event ImgMarkHandler ImgMarkOperateEvent;

//        public event ImgProcessHandler ImgProcessEvent;


//        ImageOprateCtrEntity _imageOprateCtrEntity = new ImageOprateCtrEntity();

//        //LinkedList<string> _collection = new LinkedList<string>();

//        //LinkedListNode<string> current;

//        public void AddImgFigure(Dictionary<string, string> imgFigures)
//        {
//            if (this._imageOprateCtrEntity.ViewModel == null)
//            {
//                Debug.WriteLine("请先加载图片数据，在添加标定信息");
//                return;
//            }

//            this._imageOprateCtrEntity.ViewModel.FigureCollection = imgFigures;
//        }

//        public ImageOprateCtrEntity BuildEntity()
//        {
//            return _imageOprateCtrEntity;
//        }

//        public void ImgPlaySpeedDown()
//        {
//            _imageOprateCtrEntity.Speed = 2 * _imageOprateCtrEntity.Speed;
//        }

//        public void ImgPlaySpeedUp()
//        {
//            _imageOprateCtrEntity.Speed = _imageOprateCtrEntity.Speed / 2;
//        }

//        public void LoadCodes(Dictionary<string, string> codeDic)
//        {
//            if (this._imageOprateCtrEntity.ViewModel == null)
//            {
//                Debug.WriteLine("请先加载图片数据，在添加标定信息");
//                return;
//            }

//            this._imageOprateCtrEntity.ViewModel.CodeCollection = codeDic;
//        }

//        public void LoadImg(string imgPath)
//        {
//            if (imgPath == null) return;

//            if (!File.Exists(imgPath)) return;

//            ImageControlViewModel viewModel = new ImageControlViewModel();

//            viewModel.ImageSource = new BitmapImage(new Uri(imgPath, UriKind.Absolute));

//            viewModel.ImgMarkOperateEvent += this.ImgMarkOperateEvent;

//            this._imageOprateCtrEntity.ViewModel = viewModel;
//        }

//        public void LoadImg(List<string> imgPathes)
//        {
//            this._imageOprateCtrEntity.ImagePaths = imgPathes;
//        }

//        public void LoadMarkEntitys(List<ImgMarkEntity> markEntityList)
//        {
//            if (markEntityList == null)
//            {
//                Debug.WriteLine("加载标定数据为空");
//                return;
//            }

//            if (this._imageOprateCtrEntity.ViewModel == null)
//            {
//                Debug.WriteLine("请先加载图片数据，在添加标定信息");
//                return;
//            }

//            foreach (var item in markEntityList)
//            {
//                SampleVieModel vm = new SampleVieModel(item);

//                this._imageOprateCtrEntity.ViewModel.SampleCollection.Add(vm);
//            }

//            this._imageOprateCtrEntity.RefreshAll();
//        }

//        public void NextImg()
//        {
//            this._imageOprateCtrEntity.OnNextClick();
//        }

//        public void PreviousImg()
//        {
//            this._imageOprateCtrEntity.OnLastClicked();
//        }

//        public void SetFullScreen(bool isFullScreen)
//        {
//            if (isFullScreen)
//            {
//                ImageViewCommands.FullScreen.Execute(null, this._imageOprateCtrEntity);
//            }
//            else
//            {
//                ApplicationCommands.Close.Execute(null, this._imageOprateCtrEntity.FullWindow);
//            }
//        }

//        public void SetImgPlay(ImgPlayMode imgPlayMode)
//        {
//            _imageOprateCtrEntity.ImgPlayMode = imgPlayMode;
//        }

//        public void ShowDefects()
//        {
//            foreach (var item in _imageOprateCtrEntity.ViewModel.SampleCollection)
//            {
//                item.Visible = item.Type == "0";
//            }
//        }

//        public void ShowLocates()
//        {
//            foreach (var item in _imageOprateCtrEntity.ViewModel.SampleCollection)
//            {
//                item.Visible = item.Type == "1";
//            }
//        }

//        public void ShowMarks()
//        {
//            foreach (var item in _imageOprateCtrEntity.ViewModel.SampleCollection)
//            {
//                item.Visible = true;
//            }
//        }

//        public void ShowMarks(List<string> markCodes)
//        {
//            foreach (var item in _imageOprateCtrEntity.ViewModel.SampleCollection)
//            {
//                item.Visible = markCodes.Exists(l => l == item.Code);
//            }
//        }
//    }
//}
