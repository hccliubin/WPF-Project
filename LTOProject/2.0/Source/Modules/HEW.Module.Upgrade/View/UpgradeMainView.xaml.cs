using System;
using System.Collections.Generic;
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

namespace HEW.Module.Upgrade
{
    /// <summary>
    /// UpgradeMainView.xaml 的交互逻辑
    /// </summary>
    public partial class UpgradeMainView : Window
    {

        //public UpgradeMainView()
        //{
        //    InitializeComponent();
        //}

        public UpgradeMainView(string infor, string url)
        {
            InitializeComponent();
            this.DataContext = new ViewModel.UpgradeMainViewModel();
            (this.DataContext as ViewModel.UpgradeMainViewModel).UpgradeMainModel.URL = url;
            (this.DataContext as ViewModel.UpgradeMainViewModel).UpgradeMainModel.UpgradeInfor = infor;
            this.Loaded += (o, args) => ((ViewModel.UpgradeMainViewModel)DataContext).LoadDelegateCommand.Execute(args);
            this.Closing += UpgradeMainView_Closing;
        }

        private void UpgradeMainView_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (MessageBox.Show("是否退出软件升级？", "提示", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)

            {
                Environment.Exit(0);
            }
            else e.Cancel = true;
        }
    }
}
