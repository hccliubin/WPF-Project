using CDTY.DataAnalysis.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ty.Component.SignsControl
{
    interface IDefectSign
    {
        /// <summary>
        /// 加载
        /// </summary>
        /// <param name="entity"></param>
        void Load(DefectMenuEntity entity);

        /// <summary>
        /// 保存
        /// </summary>
        event Action<string> ConfirmData;

        /// <summary>
        /// 清空所有设置
        /// </summary>
        void Reset();

        /// <summary>
        /// 设置光标位置
        /// </summary>
        void SetTabIndex(int index);
    }
}
