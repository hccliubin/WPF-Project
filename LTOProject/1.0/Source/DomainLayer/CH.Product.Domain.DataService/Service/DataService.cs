using CH.Product.Base.AppSystemInfo.Service;
using CH.Product.Base.Model;
using CH.Product.General.Logger;
using CH.Product.General.NetWork.Service;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CH.Product.Domain.DataService.Service
{
    public class DataService
    {
        public static DataService Instance = new DataService();

        NetWorkService _netWorkService = new NetWorkService();

        public ChildInfo GetChild(string code,out string message)
        {

            string unitstr= this.GetUnit();

           var tp=  _netWorkService.GetChild(code, unitstr,out message);

            if (tp == null) return null;

            if(tp.Item1 == "200")
            {
                message = tp.Item2;
            }

            else if(tp.Item1 == "403")
            {
                message = tp.Item2;
            }

            // Todo ：提示已经挂号
            else if (tp.Item1 == "201")
            {
                message = tp.Item2;
            }
            else if (tp.Item1 == "202")
            {
                message = tp.Item2;
            }
            else
            {
                message = tp.Item2;
                return null;
            }

           return  tp.Item3.JsonDeserialize<ChildInfo>();
        }

        public List<ChildInfo> GetList(out string message)
        {
            string unitstr = this.GetUnit();

            var tp = _netWorkService.GetList(unitstr, out message);

            if (tp == null)
            {
                return null;
            }

            var collection = tp.JsonDeserialize<List<ChildInfo>>();

            if (collection == null || collection.Count == 0)
            {
                message = "没有查询到数据,请检查机关单位是否配置正确！";
            }
            return collection;

        }


        public bool SetChildState(string code, out string err)
        {
            var tp = _netWorkService.SetChildState(code, out err);

            if(tp==null)
            {
                return false;
            }
            else
            {
                if(tp.Item1== "success")
                {
                    return true;
                }
                else
                {
                    err = tp.Item3;
                    return false;
                }
            }
        }

        public string GetUnit()
        {
            return StringResourceService.Instance.GetStringByID("GovernmentUnit").Trim();
        }

        public long GetDownCount()
        {
           return StringResourceService.Instance.GetStringByID("CountDownTime").ToLong();
        }

        public long GetDownCount(string timestr)
        {
          DateTime time=  timestr.ToDateTime();

            var span = DateTime.Now - time;

            long count = this.GetDownCount();
           
            return count - (long)span.TotalSeconds;
        }

        public long GetAutoLeaveDownCount()
        {
            return StringResourceService.Instance.GetStringByID("AutoLeaveDownTime").ToLong();
        }

        public void LogWithSpeech(string message)
        {
            SpeechService.Instance.Speek(message);

            Log4Servcie.Instance.Info(message);
        }

        public string CheckVersion(string version,out string message)
        {
          return  _netWorkService.CheckVersion(version,out message);
        }
    }
}
