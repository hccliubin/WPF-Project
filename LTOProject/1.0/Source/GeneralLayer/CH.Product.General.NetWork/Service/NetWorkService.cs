
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CH.Product.General.NetWork.Service
{
    public class NetWorkService
    {
        HttpPostHelper _httpPostHelper = new HttpPostHelper();

        BaseURL _base = new BaseURL();

        public Tuple<string, string, string, string> GetChild(string code, string unitstr, out string err)
        {
            IDictionary<string, string> dic = new Dictionary<string, string>();

            dic.Add("code",code);
            dic.Add("yljgdm", unitstr);

            string url = _base.GetServiceUrl(URLEnum.registerDefend);

            JContainer jsonResult;

            _httpPostHelper.PostData(url, dic,out jsonResult,out err);

            if (jsonResult == null)
            {
                return null;
            }

           return  this.ConvertJContainer(jsonResult,err);
        }

        Tuple<string, string, string, string> ConvertJContainer(JContainer jsonResult,string err)
        {
            if (jsonResult != null)
            {
                Func<object, string> func = l => l == null ? null : l.ToString();

                Tuple<string, string, string, string> result = new Tuple<string, string, string, string>(func(jsonResult["code"]),
                    func(jsonResult["msg"]), func(jsonResult["data"]), err);

                return result;
            }
            else
            {
                return null;
            }
        }

        public Tuple<string, string, string, string> SetChildState(string code, out string err)
        {
            IDictionary<string, string> dic = new Dictionary<string, string>();

            dic.Add("code", code);

            string url = _base.GetServiceUrl(URLEnum.UpdateState);

            JContainer jsonResult;

            _httpPostHelper.PostData(url, dic, out jsonResult, out err);

            if (jsonResult == null)
            {
                return null;
            }

            return this.ConvertJContainer(jsonResult, err);
        }

        string ConvertToDataJContainer(JContainer jsonResult, string err)
        {
            if (jsonResult != null)
            {
                return jsonResult.ToString();
            }
            else
            {
                return null;
            }
        }


        public string GetList(string unit,out string err)
        {
            IDictionary<string, string> dic = new Dictionary<string, string>();

            dic.Add("yljgdm", unit);

            JContainer jsonResult;

            string url = _base.GetServiceUrl(URLEnum.registerDefendList);

            _httpPostHelper.PostData(url, dic, out jsonResult, out err);

            if (jsonResult == null)
            {
                return null;
            }

            return this.ConvertToDataJContainer(jsonResult, err);
        }


        /// <summary>
        /// 检查版本号
        /// </summary>
        /// <param name="fileVersion">如2.5.0</param>
        /// <param name="mac">格式：XX-XX-XX-XX-XX-XX</param>
        /// <returns></returns>
        public string CheckVersion(string fileVersion, out string errorInfor)
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("version", fileVersion);

            JContainer jContainer;

            string url = _base.GetServiceUrl(URLEnum.registerDefendList);

            _httpPostHelper.PostData(url, dic, out jContainer, out errorInfor);

            if(jContainer==null)
            {
                return null;
            }

            return this.ConvertToDataJContainer(jContainer, errorInfor);
        }
    }
}
