
using LTO.General.Model;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LTO.Base.Tool;

namespace LTO.General.NetWork
{
    public class NetWorkService
    {
        HttpPostHelper _httpPostHelper = new HttpPostHelper();

        BaseURL _base = new BaseURL();

        //public Tuple<string, string, string, string> GetChild(string code, string unitstr, out string err)
        //{
        //    IDictionary<string, string> dic = new Dictionary<string, string>();

        //    dic.Add("code",code);
        //    dic.Add("yljgdm", unitstr);

        //    string url = _base.GetServiceUrl(URLEnum.registerDefend);

        //    JContainer jsonResult;

        //    _httpPostHelper.PostData(url, dic,out jsonResult,out err);

        //    if (jsonResult == null)
        //    {
        //        return null;
        //    }

        //   return  this.ConvertJContainer(jsonResult,err);
        //}

        //public Tuple<string, string, string, string> SetChildState(string code, out string err)
        //{
        //    IDictionary<string, string> dic = new Dictionary<string, string>();

        //    dic.Add("code", code);

        //    string url = _base.GetServiceUrl(URLEnum.UpdateState);

        //    JContainer jsonResult;

        //    _httpPostHelper.PostData(url, dic, out jsonResult, out err);

        //    if (jsonResult == null)
        //    {
        //        return null;
        //    }

        //    return this.ConvertJContainer(jsonResult, err);
        //}




        //public string GetList(string unit,out string err)
        //{
        //    IDictionary<string, string> dic = new Dictionary<string, string>();

        //    dic.Add("yljgdm", unit);

        //    JContainer jsonResult;

        //    string url = _base.GetServiceUrl(URLEnum.registerDefendList);

        //    _httpPostHelper.PostData(url, dic, out jsonResult, out err);

        //    if (jsonResult == null)
        //    {
        //        return null;
        //    }

        //    return this.ConvertToDataJContainer(jsonResult, err);
        //}


        ///// <summary>
        ///// 检查版本号
        ///// </summary>
        ///// <param name="fileVersion">如2.5.0</param>
        ///// <param name="mac">格式：XX-XX-XX-XX-XX-XX</param>
        ///// <returns></returns>
        //public string CheckVersion(string fileVersion, out string errorInfor)
        //{
        //    Dictionary<string, string> dic = new Dictionary<string, string>();
        //    dic.Add("version", fileVersion);

        //    JContainer jContainer;

        //    string url = _base.GetServiceUrl(URLEnum.registerDefendList);

        //    _httpPostHelper.PostData(url, dic, out jContainer, out errorInfor);

        //    if(jContainer==null)
        //    {
        //        return null;
        //    }

        //    return this.ConvertToDataJContainer(jContainer, errorInfor);
        //}

        /// <summary> 人数统计接口 </summary>
        /// <param name="jgdm"> {"countLgzt0":0,"countLgzt1":0,"countLgzt9":0,"countZswc":0} </param>
        /// <param name="err">  错误信息 </param>
        /// <returns></returns>
        //public object GetCount(string jgdm, out string err)
        //{
        //    Dictionary<string, string> dic = new Dictionary<string, string>();

        //    dic.Add("jgdm", jgdm);

        //    JContainer jContainer;

        //    string url = _base.GetServiceUrl(URLEnum.countNum);

        //    _httpPostHelper.PostData(url, dic, out jContainer, out err);

        //    if (jContainer == null)
        //    {
        //        return null;
        //    }

        //    var result = this.ConvertJContainer(jContainer, err);

        //    if (result == null) return null;

        //    //Tuple<string, string, string> tuple = new Tuple<string, string, string>();
        //}


        Tuple<string, string, string, string> ConvertJContainer(JContainer jsonResult, string err)
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

        public string Post(string url, Dictionary<string, string> dic, out string err)
        {

            JContainer jContainer;

            //string url = _base.GetServiceUrl(type);

            _httpPostHelper.PostData(url, dic, out jContainer, out err);

            if (jContainer == null)
            {
                return null;
            }

            return this.ConvertToDataJContainer(jContainer, err);
        }


        public string Post(URLEnum type, Dictionary<string, string> dic, out string err)
        {

            JContainer jContainer;

            string url = _base.GetServiceUrl(type);

            _httpPostHelper.PostData(url, dic, out jContainer, out err);

            if (jContainer == null)
            {
                return null;
            }

            return this.ConvertToDataJContainer(jContainer, err);
        }


        /// <summary> 人数统计接口 </summary>
        public CountEntity GetCountEntity(string jgdm, out string err)
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();

            dic.Add("jgdm", jgdm);

            string str = this.Post(URLEnum.countNum, dic, out err);

            var result = str.JsonDeserialize<CountEntity>();

            return result;

        }

        /// <summary> 挂号接口 </summary>
        public RegisterEntity PostRegisterDefend(string code, string jgdm,string type,string idx, out string err)
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();

            dic.Add("code", code);
            dic.Add("jgdm", jgdm);
            dic.Add("type", type);
            dic.Add("idx", idx);

            string str = this.Post(URLEnum.registerDefend, dic, out err);

            if (str == null) return null;

            var result = str.JsonDeserialize<RegisterEntity>();

            return result;

        }

        /// <summary> 签退接口 </summary>
        public RegisterEntity PostUpdateObservByMykh(string code, out string err)
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();

            dic.Add("code", code);

            string str = this.Post(URLEnum.updateObservByMykh, dic, out err);

            var result = str.JsonDeserialize<RegisterEntity>();

            return result;

        }

        /// <summary> 可签退列表数据接口 </summary>
        public List<LeaveEntity> PostQueryObservHz(string jgdm, out string err)
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();

            dic.Add("jgdm", jgdm);

            string str = this.Post(URLEnum.queryObservHz, dic, out err);

            var result = str.JsonDeserialize<List<LeaveEntity>>();

            return result;

        }

        
    }
}
