using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ty.Component.TaskAssignment
{
    public class TaskService
    {
        public static TaskService Instance = new TaskService();

        List<RawTaskViewModel> _collection = new List<RawTaskViewModel>();

        /// <summary>
        /// 分工 生成绑定模型
        /// </summary>
        /// <param name="task"></param>
        /// <returns></returns>
        public RawTaskViewModel CreateTaskViewModel(TaskAllocation task)
        {
            var find = _collection.Find(l => l.PacketId == task.PacketId);

            if(find==null)
            {
                RawTaskViewModel vm = new RawTaskViewModel();
                vm.RefreshConfig(task);
                this._collection.Add(vm);
                return vm;
            }
            else
            {
                return find;
            }
        }

        /// <summary>
        /// 查看  获取绑定模型
        /// </summary>
        /// <param name="packetId"></param>
        /// <returns></returns>
        public RawTaskViewModel GetTaskViewModel(string packetId)
        {
            var find = _collection.Find(l => l.PacketId == packetId);

            return find;
        }
    }
}
