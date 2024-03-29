﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Controls;

namespace Ty.Base.WpfBase
{

    /// <summary> 附加事件 </summary>
    public static class ControlAttachEvent
    {

        #region - 双击事件 -


        public static readonly DependencyProperty PreviewMouseDoubleClickProperty =
            DependencyProperty.RegisterAttached("PreviewMouseDoubleClick", typeof(ICommand), typeof(ControlAttachEvent), new FrameworkPropertyMetadata(OnCommandChanged));

        public static ICommand GetPreviewMouseDoubleClick(Control target)
        {
            return (ICommand)target.GetValue(PreviewMouseDoubleClickProperty);
        }

        public static void SetPreviewMouseDoubleClick(Control target, ICommand value)
        {
            target.SetValue(PreviewMouseDoubleClickProperty, value);
        }

        private static void Element_PreviewMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Control control = sender as Control;

            ICommand command = GetPreviewMouseDoubleClick(control);
            
            if (command.CanExecute(sender))
            {
                command.Execute(sender);
                e.Handled = true;
            }
        }

        private static void OnCommandChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            Control control = d as Control;

            control.PreviewMouseDoubleClick += new MouseButtonEventHandler(Element_PreviewMouseDoubleClick);
        }
        #endregion

        #region  - 鼠标左键按下- 


        public static DependencyProperty PreviewMouseLeftButtonDownCommandProperty = DependencyProperty.RegisterAttached("PreviewMouseLeftButtonDown",typeof(ICommand),typeof(ControlAttachEvent),new FrameworkPropertyMetadata(null, new PropertyChangedCallback(PreviewMouseLeftButtonDownChanged)));

        public static void SetPreviewMouseLeftButtonDown(DependencyObject target, ICommand value)
        {
            target.SetValue(PreviewMouseLeftButtonDownCommandProperty, value);
        }

        public static ICommand GetPreviewMouseLeftButtonDown(DependencyObject target)
        {
            return (ICommand)target.GetValue(PreviewMouseLeftButtonDownCommandProperty);
        }

        private static void PreviewMouseLeftButtonDownChanged(DependencyObject target, DependencyPropertyChangedEventArgs e)
        {
            FrameworkElement element = target as FrameworkElement;
            if (element != null)
            {
                if ((e.NewValue != null) && (e.OldValue == null))
                {
                    element.PreviewMouseLeftButtonDown += element_PreviewMouseLeftButtonDown;
                }
                else if ((e.NewValue == null) && (e.OldValue != null))
                {
                    element.PreviewMouseLeftButtonDown -= element_PreviewMouseLeftButtonDown;
                }
            }
        }

        private static void element_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            FrameworkElement element = (FrameworkElement)sender;
            ICommand command = (ICommand)element.GetValue(PreviewMouseLeftButtonDownCommandProperty);
            command.Execute(sender);
        }

        #endregion
    }
}
