using Ty.Base.WpfBase;
using Ty.Component.MenuBar;
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
    /// 动态加载
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

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            List<IMenuIconButton> names = new List<IMenuIconButton>();

            foreach (var item in this.listbox.SelectedItems)
            {
                if (item is IMenuIconButton)
                {
                    names.Add(item as IMenuIconButton);
                }
            }


            Predicate<IMenuIconButton> match = l =>
              {
                  return !names.Exists(k => k.Content == l.Content);
              };

            _vm.RefreshFilter(match);

        }
    }



    partial class SecondNotifyClass
    {
        public void RefreshFilter(Predicate<IMenuIconButton> filter)
        {
            if (this.SelectIndex == 0)
            {
                this.Filter1 = filter;
            }
            else
            {
                this.Filter2 = filter;
            }
        }

        private Predicate<IMenuIconButton> _filter1;
        /// <summary> 说明  </summary>
        public Predicate<IMenuIconButton> Filter1
        {
            get { return _filter1; }
            set
            {
                _filter1 = value;
                RaisePropertyChanged("Filter1");
            }
        }


        private Predicate<IMenuIconButton> _filter2;
        /// <summary> 说明  </summary>
        public Predicate<IMenuIconButton> Filter2
        {
            get { return _filter2; }
            set
            {
                _filter2 = value;
                RaisePropertyChanged("Filter2");
            }
        }


        private ObservableCollection<MenuButton> _collection1 = new ObservableCollection<MenuButton>();
        /// <summary> 工具栏按钮列表  </summary>
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
        /// <summary> 菜单栏按钮列表  </summary>
        public ObservableCollection<MenuButton> Collection2
        {
            get { return _collection2; }
            set
            {
                _collection2 = value;
                RaisePropertyChanged("Collection2");
            }
        }

        private ObservableCollection<MenuButton> _collection = new ObservableCollection<MenuButton>();
        /// <summary> 菜单栏按钮列表  </summary>
        public ObservableCollection<MenuButton> Collection
        {
            get { return _collection; }
            set
            {
                _collection = value;
                RaisePropertyChanged("Collection");
            }
        }


        private MenuButton _bindAddButton;
        /// <summary> 当前要增加删除的按钮  </summary>
        public MenuButton BindAddButton
        {
            get { return _bindAddButton; }
            set
            {
                _bindAddButton = value;
                RaisePropertyChanged("BindAddButton");
            }
        }

        private int _selectIndex = 0;
        /// <summary> 说明  </summary>
        public int SelectIndex
        {
            get { return _selectIndex; }
            set
            {
                _selectIndex = value;

                RaisePropertyChanged("SelectIndex");


                if (this.SelectIndex == 0)
                {
                    this.Collection = this.Collection1;
                    this.Filter1 = l => true;
                }
                else
                {
                    this.Collection = this.Collection2;
                    this.Filter2 = l => true;
                }
            }
        }




        Random r = new Random();

        public void RelayMethod(object obj)
        {
            string command = obj.ToString();

            //  Do：初始化
            if (command == "Init")
            {
                string str = "\xe652;\xe653;\xe654;\xe655;\xe656;\xe657;\xe658;\xe65a;\xe65d;\xe65e;\xe65f;\xe662;\xe663;\xe668;\xe669;\xe66a;";

                var collection = str.Split(';').ToList();

                collection.RemoveAll(l => l == "");


                int index = 0;
                foreach (var item in collection)
                {

                    MenuButton btn = new MenuButton();
                    btn.IconFont = item;
                    btn.Content = "添加" + (index++).ToString();

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


                this.SelectIndex = 0;

            }
            //  Do：添加
            else if (command == "sumit")
            {
                if (!string.IsNullOrEmpty(BindAddButton.MenuKey.String))
                {
                    if (this.Collection.ToList().Exists(l => l.MenuKey != null && l.MenuKey.String == BindAddButton.MenuKey.String))
                    {
                        MessageBox.Show("该【快捷键】已经被其他按钮注册了" + BindAddButton.MenuKey.String);
                        return;
                    }
                }
                if (this.Collection.ToList().Exists(l => l.Content == BindAddButton.Content))
                {
                    MessageBox.Show("该【名称】已经被其他按钮注册了" + BindAddButton.Content);
                    return;
                }

                this.Collection.Add(BindAddButton);

            }
            //  Do：删除
            else if (command == "Delete")
            {

                this.Collection.Remove(BindAddButton);

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
