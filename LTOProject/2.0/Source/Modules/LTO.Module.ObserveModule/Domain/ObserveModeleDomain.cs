using LTO.Domain.DataService;
using LTO.General.ModuleManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LTO.Module.ObserveModule
{
    class ObserveModeleDomain
    {
        public static ObserveModeleDomain Instance = new ObserveModeleDomain();

        public List<ChildObserveModel> GetChildObserveModel(out string err)
        {

            var result = ServiceManager.DataService.PostQueryObservHz(out err);

            if (result == null) return null;

            List<ChildObserveModel> collection = new List<ChildObserveModel>();

            foreach (var item in result)
            {
                ChildObserveModel c = new ChildObserveModel();


                c.Name = item.hzxm == null || item.hzxm.Length < 5 ? item.hzxm : item.hzxm.ToArray().Take(4).Select(l => l.ToString()).Aggregate((l, k) =>
                    {
                        return l + k;
                    }) + "...";

                c.Number = item.ghnum;
                collection.Add(c);
            }

            return collection;


            //var result = r.Next(5);

            //err = string.Empty;

            //if (result == 1)
            //{
            //    err = "刷新数据错误，请检查网络通信信息！";
            //    return null;
            //}

            //List<ChildObserveModel> collection = new List<ChildObserveModel>();

            //int count = r.Next(99);

            //for (int i = 0; i < count; i++)
            //{
            //    ChildObserveModel c = new ChildObserveModel();

            //     string item= "张小小小小小";

            //    c.Name = item.Length < 5 ? item : item.ToArray().Take(4).Select(l => l.ToString()).Aggregate((l, k) =>
            //    {
            //        return l + k;
            //    })+"...";
            //    c.Number = "B" + r.Next(90).ToString().PadLeft(3, '0');
            //    collection.Add(c);

            //}

            //return collection;
        }



        Random r = new Random();
        public void PostGetChildInfo(string id, Action<string, string, string> action)
        {
            string err;

            var result = ServiceManager.DataService.PostUpdateObservByMykh(id, out err);

            if (result == null)
            {
                action(string.Empty, "签退失败！请联系【预检台】", err);
                return;
            }

            if (result.code == "400")
            {
                action(result.code, "签退失败！", result.msg);

            }

            else if (result.code == "200")
            {
                action(result.code, result.hzxm + "【" + result.rowNum + "】签退成功", "接种已完成，请注意下次接种时间");
            }

            else if (result.code == "201")
            {
                action(result.code, result.hzxm + "【" + result.rowNum + "】不可签退", "请留观30分钟结束后再进行签退");
            }

            else if (result.code == "202")
            {
                action(result.code, "未查询到留观信息", "请先完成取号、预检、登记与接种");
            }

            else
            {
                action(result.code, "签退失败！请联系【预检台】", result.msg);
            }
        }


        public void BegionGetChildID(Action<string, string> action)
        {
            ServiceManager.ToolService.StartScanEngine(action);
            //var result = r.Next(5);

            //if (result == 1)
            //    return null;

            //return "729818173";
        }

        public Tuple<string, string, string, string> PostGetTotal(out string err)
        {
            //var result=  r.Next(5);

            // err = string.Empty;

            // if (result==1)
            // {
            //     err = "刷新数据错误，请检查网络通信信息！";
            //     return null;
            // }

            // return new Tuple<string, string, string, string>(r.Next(10, 300).ToString(), r.Next(10, 300).ToString(), r.Next(10, 300).ToString(), r.Next(10, 300).ToString());         

            var result = ServiceManager.DataService.GetCountEntity(out err);

            if (result == null) return null;

            return new Tuple<string, string, string, string>(result.countZswc.ToString(), result.countLgzt0.ToString(), result.countLgzt1.ToString(), result.countLgzt9.ToString());



        }

        /// <summary> 根据当前操作系统返回指定页面 </summary>
        public IModulePage GetModulePage()
        {
            var result = ServiceManager.ToolService.IsWin7();
            //result = true;

            if (result)
            {
                return new ObserveModuleControl_Santai();
            }
            return new ObserveModuleControl();
        }
    }
}
