using SureDream.Base.WpfBase;
using SureDream.Component.MenuBar;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SureDream.Appliaction.DemoApp
{
    /// <summary>
    /// SecondUserControl.xaml 的交互逻辑
    /// </summary>
    public partial class SecondUserControl : UserControl
    {

        SecondNotifyClass _vm = new SecondNotifyClass();

        public SecondUserControl()
        {
            InitializeComponent();

            this.DataContext = _vm;
        }

        private void MenuBar_MenuClicked(object sender, MenuRoutedEventArgs Args)
        {
            MessageBox.Show(Args.MenuSource.Content?.ToString());
        }

        private void MenuBar_CheckedChanged(object sender, MenuCheckedRoutedEventArgs e)
        {
            MessageBox.Show("名称:" + e.MenuSource.Content?.ToString() + " 状态:" + e.MenuSource.IsChecked?.ToString());
        }
    }



    partial class SecondNotifyClass
    {

        private ObservableCollection<MenuButton> _collection1 = new ObservableCollection<MenuButton>();
        /// <summary> 说明  </summary>
        public ObservableCollection<MenuButton> Collection1
        {
            get { return _collection1; }
            set
            {
                _collection1 = value;
                RaisePropertyChanged("Collection1");
            }
        }

        private ObservableCollection<MenuButton> _collection2 = new ObservableCollection<MenuButton>();
        /// <summary> 说明  </summary>
        public ObservableCollection<MenuButton> Collection2
        {
            get { return _collection2; }
            set
            {
                _collection2 = value;
                RaisePropertyChanged("Collection2");
            }
        }


        private MenuButton _bindAddButton;
        /// <summary> 说明  </summary>
        public MenuButton BindAddButton
        {
            get { return _bindAddButton; }
            set
            {
                _bindAddButton = value;
                RaisePropertyChanged("BindAddButton");
            }
        }



        Random r = new Random();

        public void RelayMethod(object obj)
        {
            string command = obj.ToString();

            //  Do：应用
            if (command == "Init")
            {
                string str = "\xe652;\xe653;\xe654;\xe655;\xe656;\xe657;\xe658;\xe65a;\xe65d;\xe65e;\xe65f;\xe662;\xe663;\xe668;\xe669;\xe66a;";

                var collection = str.Split(';').ToList();

                collection.RemoveAll(l => l == "");

                foreach (var item in collection)
                {

                    MenuButton btn = new MenuButton();
                    btn.IconFont = item;
                    btn.Content = "添加";

                    int result = r.Next(8);

                    if (r.Next(8) == 1)
                    {
                        btn.LeftRightAlignment = LeftRightAlignment.Right;
                        btn.MenuButtonStyle = MenuButtonStyle.Default;
                    }
                    else
                    {
                        btn.LeftRightAlignment = LeftRightAlignment.Left;

                        result = r.Next(3);

                        btn.MenuButtonStyle = r.Next(3) == 1 ? MenuButtonStyle.ToggleButton : MenuButtonStyle.IconButton;
                    }

                    this.Collection1.Add(btn);
                }

            }
            //  Do：取消
            else if (command == "sumit")
            {
                this.Collection2.Add(BindAddButton);
            }
            //  Do：取消
            else if (command == "Delete")
            {
                this.Collection2.Remove(BindAddButton);
            }
        }
    }

    partial class SecondNotifyClass : INotifyPropertyChanged
    {
        public RelayCommand RelayCommand { get; set; }

        public SecondNotifyClass()
        {
            RelayCommand = new RelayCommand(RelayMethod);

            RelayMethod("Init");

        }
        #region - MVVM -

        public event PropertyChangedEventHandler PropertyChanged;

        public void RaisePropertyChanged([CallerMemberName] string propertyName = "")
        {
            if (PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }

}
