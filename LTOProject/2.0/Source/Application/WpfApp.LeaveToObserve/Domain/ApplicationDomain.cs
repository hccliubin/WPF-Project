
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using LTO.General.ModuleManager;
using LTO.Module.GetNumberModule;
using LTO.Module.ObserveModule;
using LTO.Domain.DataService;

namespace WpfApp.LeaveToObserve
{
    class ApplicationDomain
    {
        public static ApplicationDomain Instance = new ApplicationDomain();


        //public T GetReprotDetail<T>(string cardID, ReportTypeEnum reportType, out string err) where T : class
        //{
        //    object data = DataManager.DatasManager.GetReprotDetail(cardID, reportType, out err);

        //    return data as T;
        //}

        //public object GetReprotDetail(string cardID, ReportTypeEnum reportType, out string err)
        //{
        //    object data = DataManager.DatasManager.GetReprotDetail(cardID, reportType, out err);

        //    return data;
        //}


        //public ReportTypeEnum GetModuleConfigType(string moduleName)
        //{
        //    return ModuleManager.ModuleConfig[moduleName];
        //}


        //string GetCardID(out string err)
        //{
        //    var result = DataManager.DatasManager.GetCardID(out err);


        //    if (result.IsIDNumber())
        //        return result;

        //    return null;
        //}

        //public string GetCardID(Predicate<string> stopMatch)
        //{
        //    string cardid = null;

        //    int count = 10;

        //    while (count > 0)
        //    {
        //        count--;

        //        string err;

        //        try
        //        {
        //            string result = this.GetCardID(out err);

        //            if (stopMatch(err))
        //            {
        //                return cardid;
        //            }

        //            if (result != null)
        //            {
        //                cardid = result;
        //                break;
        //            }

        //        }
        //        catch (Exception ex)
        //        {
        //            DataManager.DatasManager.Error("读取身份证失败！", ex);
        //            return cardid;
        //        }



        //        DataManager.DatasManager.Info(err);


        //        Thread.Sleep(1000);

        //    }

        //    return cardid;
        //}


        public bool IsWin7()
        {
          return  ServiceManager.ToolService.IsWin7();

        }



        public Tuple<string, string> GetUIConfig()
        {
            string outInfo = ServiceManager.DataService.GetConfigByID("Address"); 

            string result=null;

            return new Tuple<string, string>(outInfo, result);

        }



        public List<ILTOModule> GetModules()
        {
            List<ILTOModule> collection = new List<ILTOModule>();
            collection.Add(new GetNumberModule());
            collection.Add(new ObserveModule());
            return collection;
        }

        /// <summary> 获取配置文件中配置的默认模块 </summary>
        public ILTOModule GetConfigDefaultModule(List<ILTOModule> modules)
        {
            string index = ServiceManager.DataService.GetConfigByID("StartPage");

            if (index == "0") return null;

            if (index == "1") return modules.Find(l=>l.GetType()==typeof(GetNumberModule));

            if (index == "2") return modules.Find(l => l.GetType() == typeof(ObserveModule));

            return null;
        }

    }
}
