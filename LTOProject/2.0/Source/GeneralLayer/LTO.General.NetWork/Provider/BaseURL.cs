using LTO.Base.Product.Provider;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace LTO.General.NetWork
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

            string port = StringResourceService.Instance.GetStringByID("Port");

            return string.Format(urlFormat, ip, port, serviceTypeString);
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
            FieldInfo field = urlEnum.GetType().GetField(urlEnum.ToString());
            var desc = Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute)) as DescriptionAttribute;

            return desc.Description;

        }
    }
    public enum URLEnum
    {
        /// <summary> 挂号接口 </summary>
        [Description("/register/registerDefend.do")]
        registerDefend = 0,
        /// <summary> 可签退列表数据接口 </summary>
        [Description("/register/queryObservHz.do")]
        queryObservHz,
        /// <summary> 人数统计接口 </summary>
        [Description("/register/countNum.do")]
        countNum,
        /// <summary> 签退接口 </summary>
        [Description("/register/updateObservByMykh.do")]
        updateObservByMykh
    }
}
