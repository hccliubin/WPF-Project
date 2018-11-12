#region <版 本 注 释>
/*
 * ========================================================================
 * Copyright(c) 长虹智慧健康有限公司, All Rights Reserved.
 * ========================================================================
 *    
 * 作者：[李海军]   时间：2018/4/2 16:28:43 
 * 文件名：LeaveToObserveEngine 
 * 说明：
 * 
 * 
 * 修改者：           时间：               
 * 修改说明：
 * ========================================================================
*/
#endregion
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CH.Product.UserControls
{
    class LeaveToObserveEngine
    {

        public static LeaveToObserveEngine Instance = new LeaveToObserveEngine();

        List<LeaveToObserveItemViewModel> _collection = new List<LeaveToObserveItemViewModel>();

        internal List<LeaveToObserveItemViewModel> Collection { get => _collection; set => _collection = value; }


        public void RunDemo()
        {
            Random r = new Random();
            for (int i = 0; i < 6; i++)
            {
                LeaveToObserveItemViewModel model = new LeaveToObserveItemViewModel();
                model.Seq = i.ToString();
                model.Name = "托尼";
                model.StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                model.CreateTime = r.Next(60).ToString();
                model.VaccineName = "狂犬疫苗";
                model.Sex = "女";

                this.Collection.Add(model);
            }
        }


    }
}
