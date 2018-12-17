using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ty.Base.WpfBase.Service;

namespace Ty.Component.TaskAssignment
{
    /// <summary>
    /// viewmodel（仅供参考） TaskDivisionViewModel 使用到的接口
    /// </summary>
    public interface ITaskItem
    {
        void SetTaskModelList(ObservableCollection<TaskModel> modelList);

        /// <summary>
        /// 设置分析员列表
        /// </summary>
        /// <param name="users"></param>
        void SetTyeAdminUserEntity(ObservableCollection<TyeAdminUserEntity> users);

        /// <summary>
        /// 设置战区列表
        /// </summary>
        /// <param name="sites"></param>
        void SetTyeBaseSiteEntity(ObservableCollection<TyeBaseSiteEntity> sites);

        /// <summary>
        /// 设置杆号列表
        /// </summary>
        /// <param name="Pillars"></param>
        void SetTyeBasePillarEntity(ObservableCollection<TyeBasePillarEntity> Pillars);

        /// <summary>
        /// 保存时触发的事件
        /// </summary>
        event Action<ObservableCollection<TaskModel>> SaveEvent;

        /// <summary>
        /// 当选中一个站区触发的事件
        /// </summary>
        event Action<TyeBaseSiteEntity> SeletctSameSiteEvent;
    }
}
