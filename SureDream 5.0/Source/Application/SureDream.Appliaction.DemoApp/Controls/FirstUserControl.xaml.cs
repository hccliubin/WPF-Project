﻿using System;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SureDream.Appliaction.DemoApp
{
    /// <summary>
    /// 静态加载
    /// </summary>
    public partial class FirstUserControl : UserControl
    {
        public FirstUserControl()
        {
            InitializeComponent();
        }

        private void MenuToggleButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("eee");
        }
    }
}
