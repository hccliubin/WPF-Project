﻿using HEW.Base.Theme.Style;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace HEW.Module.PhysicalExamination
{
    /// <summary>
    /// PhysicalReportControl.xaml 的交互逻辑
    /// </summary>
    public partial class PhysicalReportControl : BaseUserControl
    {
        public PhysicalReportControl()
        {
            InitializeComponent();
        }


        //声明和注册路由事件
        public static readonly RoutedEvent GoBackRoutedEvent =
            EventManager.RegisterRoutedEvent("GoBack", RoutingStrategy.Bubble, typeof(EventHandler<RoutedEventArgs>), typeof(PhysicalReportControl));
        //CLR事件包装
        public event RoutedEventHandler GoBack
        {
            add { this.AddHandler(GoBackRoutedEvent, value); }
            remove { this.RemoveHandler(GoBackRoutedEvent, value); }
        }

        //激发路由事件,借用Click事件的激发方法

        protected void OnGoBack()
        {
            RoutedEventArgs args = new RoutedEventArgs(GoBackRoutedEvent, this);
            this.RaiseEvent(args);
        }

        private void btn_goback_Click(object sender, RoutedEventArgs e)
        {
            this.OnGoBack();
        }


        //声明和注册路由事件
        public static readonly RoutedEvent ReWorkRoutedEvent =
            EventManager.RegisterRoutedEvent("ReWork", RoutingStrategy.Bubble, typeof(EventHandler<RoutedEventArgs>), typeof(PhysicalReportControl));
        //CLR事件包装
        public event RoutedEventHandler ReWork
        {
            add { this.AddHandler(ReWorkRoutedEvent, value); }
            remove { this.RemoveHandler(ReWorkRoutedEvent, value); }
        }

        //激发路由事件,借用Click事件的激发方法

        protected void OnReWork()
        {
            RoutedEventArgs args = new RoutedEventArgs(ReWorkRoutedEvent, this);
            this.RaiseEvent(args);
        }


        private void btn_rework_Click(object sender, RoutedEventArgs e)
        {
            this.OnReWork();
        }
        

        //private void btn_print_Click(object sender, RoutedEventArgs e)
        //{
        //    this.tpage_control.Print();
        //}
    }
}
