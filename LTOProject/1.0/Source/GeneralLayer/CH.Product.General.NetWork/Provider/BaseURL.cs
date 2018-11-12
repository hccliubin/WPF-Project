using CH.Product.Base.AppSystemInfo.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CH.Product.General.NetWork
{
    /// <summary>
    /// 网络请求中所需要用到的URL
    /// </summary>
    public class BaseURL
    {
        const string urlFormat = " http://{0}:{1}{2}";
        string GetServiceUrl(string serviceTypeString)
        {
            string ip = StringResourceService.Instance.GetStringByID("IP");

            string port= StringResourceService.Instance.GetStringByID("Port");

            return string.Format(urlFormat,ip, port,serviceTypeString);
        }

        public string GetServiceUrl(URLEnum urlEnum)
        {
           return this.GetServiceUrl(this.GetURL(urlEnum));
        }

        /// <summary>
        /// 根据相应的Enum类型以及服务器环境返回相应的URL
        /// </summary>
        /// <param name="urlEnum"></param>
        /// <returns></returns>
         string GetURL(URLEnum urlEnum)
        {
            string url = "";
            switch (urlEnum)
            {
                case URLEnum.registerDefend:
                    url = "/child/registerDefend.do"; 
                    break;
                case URLEnum.registerDefendList:
                    url = "/child/queryObservHz.do";
                    break;
                case URLEnum.UpdateState:
                    url = "/child/updateLgjl.do";
                    break;
                case URLEnum.UpdateVerson:
                    url = "";
                    break;
                default:
                    break;
            }
            if (url == "") return "";

            return url;

        }
    }
    public enum URLEnum
    {
        registerDefend=0, registerDefendList,UpdateState,UpdateVerson
    }
}
