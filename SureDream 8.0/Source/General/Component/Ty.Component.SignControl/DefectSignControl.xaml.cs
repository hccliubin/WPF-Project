using System;
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

namespace Ty.Component.SignControl
{
    /// <summary>
    /// DefectSignControl.xaml 的交互逻辑
    /// </summary>
    public partial class DefectSignControl : UserControl
    {
        public DefectSignControl()
        {
            InitializeComponent();

            this.Loaded += DefectSignControl_Loaded;
        }

        private void DefectSignControl_Loaded(object sender, RoutedEventArgs e)
        {
            ObservableCollection<TestModel> list = new ObservableCollection<TestModel>();

            list.Add(new TestModel() {ID="0001",Name= "A001" } );
            list.Add(new TestModel() { ID = "0002", Name = "A002" });
            list.Add(new TestModel() { ID = "0003", Name = "A003" });
            list.Add(new TestModel() { ID = "0004", Name = "A004" });
            list.Add(new TestModel() { ID = "0005", Name = "A005" });
            list.Add(new TestModel() { ID = "0006", Name = "A006" });
            list.Add(new TestModel() { ID = "0007", Name = "A007" });
            list.Add(new TestModel() { ID = "0008", Name = "A008" });

            this.com.MyItemsSource = list;  
        } 
    }

    public class TestModel
    {
        public string ID { get; set; }

        public string Name { get; set; }
    }
}
