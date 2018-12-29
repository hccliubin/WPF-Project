using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Ty.Component.SignsControl
{
    /// <summary>
    /// 按照步骤 1a 或 1b 操作，然后执行步骤 2 以在 XAML 文件中使用此自定义控件。
    ///
    /// 步骤 1a) 在当前项目中存在的 XAML 文件中使用该自定义控件。
    /// 将此 XmlNamespace 特性添加到要使用该特性的标记文件的根 
    /// 元素中: 
    ///
    ///     xmlns:MyNamespace="clr-namespace:Ty.Component.SignControl"
    ///
    ///
    /// 步骤 1b) 在其他项目中存在的 XAML 文件中使用该自定义控件。
    /// 将此 XmlNamespace 特性添加到要使用该特性的标记文件的根 
    /// 元素中: 
    ///
    ///     xmlns:MyNamespace="clr-namespace:Ty.Component.SignControl;assembly=Ty.Component.SignControl"
    ///
    /// 您还需要添加一个从 XAML 文件所在的项目到此项目的项目引用，
    /// 并重新生成以避免编译错误: 
    ///
    ///     在解决方案资源管理器中右击目标项目，然后依次单击
    ///     “添加引用”->“项目”->[浏览查找并选择此项目]
    ///
    ///
    /// 步骤 2)
    /// 继续操作并在 XAML 文件中使用控件。
    ///
    ///     <MyNamespace:SearchCombobox/>
    ///
    /// </summary>
    public class SearchCombobox : ComboBox
    {
        private bool t = true;//首次获取焦点标志位
        private ObservableCollection<object> bindingList = new ObservableCollection<object>();//数据源绑定List
        private string editText = "";//编辑文本内容

        /// <summary>
        /// 注册依赖事件
        /// </summary>
        public static readonly DependencyProperty ItemsSourcePropertyNew = DependencyProperty.Register("MyItemsSource", typeof(IEnumerable), typeof(SearchCombobox), new FrameworkPropertyMetadata(new PropertyChangedCallback(ValueChanged)));
        /// <summary>
        /// 数据源改变，添加数据源到绑定数据源
        /// </summary>
        /// <param name="d"></param>
        /// <param name="e"></param>
        private static void ValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            SearchCombobox ecb = d as SearchCombobox;
            ecb.bindingList.Clear();
            //遍历循环操作
            foreach (var item in ecb.MyItemsSource)
            {
                ecb.bindingList.Add(item);
            }
        }
        /// <summary>
        /// 设置或获取ComboBox的数据源
        /// </summary>
        public IEnumerable MyItemsSource
        {
            get
            {
                return (IEnumerable)GetValue(ItemsSourcePropertyNew);
            }

            set
            {
                if (value == null)
                    ClearValue(ItemsSourcePropertyNew);
                else
                    SetValue(ItemsSourcePropertyNew, value);
            }
        }
        /// <summary>
        /// 重写初始化
        /// </summary>
        /// <param name="e"></param>
        protected override void OnInitialized(EventArgs e)
        {
            base.OnInitialized(e);
            this.IsEditable = true;
            this.IsTextSearchEnabled = false;
            this.ItemsSource = bindingList;
        }
        /// <summary>
        /// 下拉框获取焦点，首次搜索文本编辑框
        /// </summary>
        /// <param name="e"></param>
        protected override void OnGotFocus(RoutedEventArgs e)
        {
            if (t)
                FindTextBox(this);
            else
                t = false;
        }
        /// <summary>
        /// 搜索编辑文本框，添加文本改变事件
        /// </summary>
        /// <param name="obj"></param>
        private void FindTextBox(DependencyObject obj)
        {
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(obj); i++)
            {
                DependencyObject child = VisualTreeHelper.GetChild(obj, i);
                if (child != null && child is TextBox)
                {
                    //注册文本改变事件
                    (child as TextBox).TextChanged += EditComboBox_TextChanged;
                }
                else
                {
                    FindTextBox(child);
                }
            }
        }
        /// <summary>
        /// 文本改变，动态控制下拉条数据源
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EditComboBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox tb = sender as TextBox;
            if (tb.IsFocused)
            {
                tb.Focus();
                this.IsDropDownOpen = true;
                if (editText == this.Text)
                    return;
                editText = this.Text;
                SetList(editText);
            }
        }
        /// <summary>
        /// 组合框关闭，数据源恢复
        /// </summary>
        /// <param name="e"></param>
        protected override void OnDropDownClosed(EventArgs e)
        {
            base.OnDropDownClosed(e);
            if (MyItemsSource == null)
                return;
            foreach (var item in MyItemsSource)
            {
                if (!bindingList.Contains(item))
                    bindingList.Add(item);
            }
        }



        /// <summary>
        /// 过滤符合条件的数据项，添加到数据源项中
        /// </summary>
        /// <param name="txt"></param>
        private void SetList(string txt)
        {
            string temp1 = "";

            string temp2 = "";

            if (MyItemsSource == null) return;

            foreach (var item in MyItemsSource)
            {
                temp1 = item.GetType().GetProperty(this.DisplayMemberPath).GetValue(item, null).ToString();

                if (string.IsNullOrEmpty(this.SelectedValuePath))
                {
                    temp2 = "";
                }
                else
                {
                    temp2 = item.GetType().GetProperty(this.SelectedValuePath).GetValue(item, null).ToString();
                }

                if (this.FilterMatch == null)
                {
                    if (temp1.Contains(txt) || temp2.StartsWith(txt))
                    {
                        if (!bindingList.Contains(item))
                            bindingList.Add(item);
                    }
                    else if (bindingList.Contains(item))
                    {
                        bindingList.Remove(item);
                    }
                }
                else
                {
                    if (this.FilterMatch(item, txt))
                    {
                        if (!bindingList.Contains(item))
                            bindingList.Add(item);
                    }
                    else
                    {
                        bindingList.Remove(item);
                    }
                }

            }
        }

        public Func<object,string,bool> FilterMatch
        {
            get { return (Func<object, string, bool>)GetValue(FilterMatchProperty); }
            set { SetValue(FilterMatchProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty FilterMatchProperty =
            DependencyProperty.Register("FilterMatch", typeof(Func<object, string, bool>), typeof(SearchCombobox), new PropertyMetadata());

    }
}
