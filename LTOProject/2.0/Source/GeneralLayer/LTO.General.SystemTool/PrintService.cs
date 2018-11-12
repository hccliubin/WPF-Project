using System;
using System.Collections.Generic;
using System.Linq;
using System.Printing;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace LTO.General.SystemTool
{
    /// <summary> 打印支持类 </summary>
    public class PrintService
    {

        public static PrintService Instance = new PrintService();

        /// <summary> 打印表格 </summary>
        public void PrintGrid(Grid grid, bool isShowForm = true)
        {
            Action action = () =>
            {
                PrintDialog dialog = new PrintDialog();
                dialog.PrintTicket.PageOrientation = System.Printing.PageOrientation.Portrait;
                dialog.PrintTicket.PageMediaSize = new PageMediaSize(PageMediaSizeName.ISOA4);
                dialog.ShowDialog();
                if (isShowForm)
                {

                    dialog.PrintVisual(grid, "Print Test");
                }
                else
                {
                    //// Do ：批量打印用
                    //UserControl c = grid.Parent as UserControl;


                    //if (c != null)
                    //{
                    //    if (c.Parent is WrapPanel)
                    //    {
                    //        PrintListWindow.Instance.Show();
                    //        PrintListWindow.Instance.PrintControl = c;
                    //        PrintListWindow.Instance.Hide();
                    //    }
                    //}


                    //    //Application.Current.Dispatcher.Invoke(() => dialog.PrintVisual(grid, "Print Test")
                    dialog.PrintVisual(grid, "Print Test");
                }
            };

            //action.Invoke();

            Application.Current.Dispatcher.BeginInvoke(action, DispatcherPriority.SystemIdle, null);


        }

        bool _isFirst = true;

        // Todo ：设置信号量 
        Semaphore semaphore = new Semaphore(1, 1);

        /// <summary> 第一次调用会弹出打印首选项 </summary>
        public bool CheckPrintShow()
        {
            if (_isFirst)
            {
                _isFirst = false;

                PrintDialog dialog = new PrintDialog();
                dialog.PrintTicket.PageOrientation = System.Printing.PageOrientation.Portrait;
                dialog.PrintTicket.PageMediaSize = new PageMediaSize(PageMediaSizeName.ISOA4Extra);
                var result = dialog.ShowDialog();

                if (result.HasValue && result.Value) return true;

                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
