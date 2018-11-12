//using BHM.General.Algorithms;
//using BHM.General.Data.Network.Base;
//using BHM.General.Data.Network.DataFilter;
//using BHM.General.Model.Json;
//using Newtonsoft.Json;
//using Newtonsoft.Json.Linq;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Net;
//using System.Text;
//using System.Threading;
//using System.Threading.Tasks;

//namespace CH.Product.General.NetWork
//{
//    /// <summary>
//    /// 网络通信请求数据类
//    /// </summary>
//    public class NetwokrRequestData
//    {
//        private HttpPostHelper httpPostHelper = new HttpPostHelper();
//        private BaseURL baseHealthyURL = new BaseURL();
//        //private HealthExamDataFilter healthExamDataFilter = new HealthExamDataFilter();

//        #region 医生账号登陆
//        /// <summary>
//        /// 妥妥医账号登陆 
//        /// </summary>
//        /// <param name="userID">妥妥医账号</param>
//        /// <param name="userPsd">妥妥医密码</param>
//        /// <param name="mac">本机 mac 地址全部大写且要有“-”间隔 测试统一使用（01-01-02-03-04） </param>
//        /// <param name="ErrorInfor">错误信息</param>
//        /// <returns></returns>
//        public LoginJsonModel TuoTuoYiLogin(string userID, string userPsd, string mac, out string ErrorInfor)
//        {
//            Dictionary<string, string> dic = new Dictionary<string, string>();
//            EncryptionArithmetic ea = new EncryptionArithmetic();
//            dic.Add("username", userID);
//            dic.Add("password", ea.Md5Encrypt(userPsd));
//            JContainer jsonResult;
//            httpPostHelper.BaseHealthyHutLogin(baseHealthyURL.GetURL(URLEnum.login_other_do), dic, out jsonResult);
//            ErrorInfor = "";
//            var item = JsonDeserializeObject<LoginJsonModel>.getT(jsonResult, ref ErrorInfor);

//            if (jsonResult == null)
//            {
//                ErrorInfor = "服务器挂掉了";
//                return null;
//            }

//            if (item == null)
//            {
//                ErrorInfor = jsonResult["message"].ToString() + " [在线]";
//                return null;
//            }
//            else
//            {
//                ErrorInfor = "";

//                return item;
//            }
//        }
//        #endregion

//        #region 区划信息查询
//        /// <summary>
//        /// 获取省份的PID值level=1四川省
//        /// </summary>
//        public string GetProvincePID(out string errorInfor)
//        {
//            Dictionary<string, string> dic = new Dictionary<string, string>();
//            dic.Add("level", "1");
//            JContainer jsonResult;
//            httpPostHelper.PostData(baseHealthyURL.GetURL(URLEnum.getRegionByLevel), dic, out jsonResult, out errorInfor);

//            if (jsonResult != null)
//            {
//                if (jsonResult["code"].ToString().Equals("0"))
//                {
//                    try
//                    {
//                        string datt = jsonResult["data"].ToString().Substring(1, jsonResult["data"].ToString().Length - 2);
//                        JObject jobject = JObject.Parse(datt);
//                        return jobject["id"].ToString();
//                    }
//                    catch (Exception ex)
//                    {
//                        errorInfor = ex.Message.ToString();
//                    }
//                }
//            }
//            return null;
//        }

//        /// <summary>
//        /// 获取省份的PID值level=1四川省 列表
//        /// </summary>
//        /// <param name="ID">若查询城市，则输入省的PID；若查询区县，则输入城市的ID值</param>
//        public List<AreaQueryJsonModel> GetProvinceList(out string errorInfor)
//        {
//            Dictionary<string, string> dic = new Dictionary<string, string>();
//            dic.Add("level", "1");
//            JContainer jsonResult;
//            httpPostHelper.PostData(baseHealthyURL.GetURL(URLEnum.getRegionByLevel), dic, out jsonResult, out errorInfor);
//            return JsonDeserializeObject<AreaQueryJsonModel>.getListT(jsonResult, ref errorInfor);
//        }

//        /// <summary>
//        /// 获取省下的城市列表以及城市下面的区县列表
//        /// </summary>
//        /// <param name="ID">若查询城市，则输入省的PID；若查询区县，则输入城市的ID值</param>
//        public List<AreaQueryJsonModel> GetCityList(string ID, out string errorInfor)
//        {
//            Dictionary<string, string> dic = new Dictionary<string, string>();
//            dic.Add("pid", ID);
//            JContainer jsonResult;
//            httpPostHelper.PostData(baseHealthyURL.GetURL(URLEnum.getChildRegions_do), dic, out jsonResult, out errorInfor);
//            return JsonDeserializeObject<AreaQueryJsonModel>.getListT(jsonResult, ref errorInfor);
//        }

//        #endregion

//        #region 查询居民档案
//        /// <summary>
//        ///  居民列表查询接口,可查询多个，也可查询单个
//        /// </summary>
//        /// <param name="keyCode">关键字代码，1-名字，2-身份证，3-档案号，4-自 定义编码，5-电话，0 所有列表 （此种情况 keyValue 可不填写） </param>
//        /// <param name="keyValue">查询关键字 </param>
//        /// <param name="regionCode">区划代码 </param>
//        /// <param name="ErrorInfor">错误信息，当返回值为null</param>
//        /// <param name="total">总共查询到的数量</param>
//        /// <param name="limit">每页条数 </param>
//        /// <param name="offset">起始页 </param>
//        /// <param name="isStatus">居民状态 ，0 活动，1 迁出，2 死亡，99 已删除，3 其他</param>
//        public List<ResidentFilesJsonModel> GetPeopleList(uint keyCode, string keyValue, string regionCode, out string ErrorInfor, out uint total, uint limit = 10, uint offset = 0, uint isStatus = 0)
//        {
//            ErrorInfor = "";
//            Dictionary<string, string> dic = new Dictionary<string, string>();

//            dic.Add("keyCode", keyCode.ToString());
//            dic.Add("keyValue", keyValue);
//            dic.Add("regionCode", regionCode);
//            dic.Add("limit", limit.ToString());
//            dic.Add("offset", offset.ToString());
//            dic.Add("isStatus", isStatus.ToString());
//            JContainer jsonResult;
//            httpPostHelper.PostData(baseHealthyURL.GetURL(URLEnum.getPeopleList_do), dic, out jsonResult, out ErrorInfor);
//            try
//            {
//                if (jsonResult != null) total = uint.Parse(jsonResult["total"].ToString());
//                else total = 0;
//            }
//            catch { total = 0; }
//            return JsonDeserializeObject<ResidentFilesJsonModel>.getListT(jsonResult, ref ErrorInfor);
//        }

//        /// <summary>
//        /// 获取居民健康档案详情
//        /// </summary>
//        /// <param name="cardID">居民档案号码</param>
//        /// <returns>详细数据</returns>
//        public HealthCareRecordsJsonModel GetHealthCareRecords(string ResidentID, string regionCode, out string errorInfor)
//        {
//            Dictionary<string, string> dic = new Dictionary<string, string>();
//            dic.Add("ID", ResidentID);
//            JContainer jsonresult;
//            httpPostHelper.PostData(baseHealthyURL.GetURL(URLEnum.find), dic, out jsonresult, out errorInfor);
//            if (jsonresult != null)
//            {
//                if (jsonresult["code"].ToString().Equals("0"))
//                {
//                    JObject jsonObj = JObject.Parse(jsonresult.ToString());

//                    if (jsonObj.Property("data") != null)
//                    {
//                        JObject jvalue = JObject.Parse(jsonObj["data"].ToString());
//                        if (jvalue != null)
//                        {
//                            JObject jpeople = (JObject)jvalue["people"];
//                            if (jpeople != null)
//                            {
//                                var item = JsonConvert.DeserializeObject<HealthCareRecordsJsonModel>(jpeople.ToString());

//                                JArray jfamilyDiseaselHistory = (JArray)jvalue["familyDiseaselHistoryList"];

//                                if (jfamilyDiseaselHistory != null) item.FamilyDiseaselHistoryList = JsonConvert.DeserializeObject<List<FamilyDiseaselHistory>>(jfamilyDiseaselHistory.ToString());

//                                JArray jpersonalDiseaseHistory = (JArray)jvalue["personalDiseaseHistoryList"];
//                                if (jpersonalDiseaseHistory != null) item.PersonalDiseaseHistoryList = JsonConvert.DeserializeObject<List<PersonalDiseaseHistory>>(jpersonalDiseaseHistory.ToString());

//                                JArray jpersonalChronicDiseaseFile = (JArray)jvalue["personalChronicDiseaseFileList"];
//                                if (jpersonalChronicDiseaseFile != null) item.PersonalChronicDiseaseFileList = JsonConvert.DeserializeObject<List<PersonalChronicDiseaseFile>>(jpersonalChronicDiseaseFile.ToString());

//                                return healthExamDataFilter.HealthCareRecordsDataFilterFunc(item);
//                            }
//                        }
//                    }
//                }
//            }
//            return null;
//        }
//        #endregion

//        #region 居民体检报告
//        /// <summary>
//        /// 查询指定居民的体检列表
//        /// </summary>
//        /// <param name="cardID">身份证</param>
//        /// <param name="startTime">开始时间</param>
//        /// <param name="endTime">结束时间</param>
//        /// <param name="regionCode">区划信息</param>
//        /// <param name="errorInfor">错误信息</param>
//        /// <param name="pageIndex">序号</param>
//        /// <returns></returns>
//        public List<PhysicalExaminationJsonModel> GetPeopleExamList(string cardID, string startTime, string endTime, string regionCode, out uint total, out string errorInfor, uint pageIndex = 0)
//        {
//            JContainer jsonresult;
//            Dictionary<string, string> dic = new Dictionary<string, string>();

//            List<ResidentFilesJsonModel> HealthCareRecords = GetPeopleList(2, cardID, regionCode, out errorInfor, out total);
//            if (HealthCareRecords == null)
//            {
//                errorInfor = "居民未建档";
//                return null;
//            }

//            dic.Add("personId", HealthCareRecords[0].id);
//            dic.Add("type", "0");
//            dic.Add("pageSize", "10");
//            dic.Add("pageIndex", pageIndex.ToString());
//            httpPostHelper.PostData(baseHealthyURL.GetURL(URLEnum.getPeopleExamList_do), dic, out jsonresult, out errorInfor);

//            List<PeopleExamListJsonModel> peopleExamListJsonModelList = JsonDeserializeObject<PeopleExamListJsonModel>.getListT(jsonresult, out total, ref errorInfor);

//            if (peopleExamListJsonModelList == null)
//            {
//                errorInfor = "居民没有体检表";
//                return null;
//            }

//            List<PhysicalExaminationJsonModel> PhysicalExaminationJsonModelList = new List<PhysicalExaminationJsonModel>();

//            foreach (PeopleExamListJsonModel item in peopleExamListJsonModelList)
//            {
//                PhysicalExaminationJsonModelList.Add(new PhysicalExaminationJsonModel()
//                {
//                    Age = item.age,
//                    CardID = item.card_ID,
//                    GenderCode = item.gender,
//                    Lasthldate = item.follow_UP_DATE,
//                    Name = item.name,
//                    PersonCode = item.person_CODE,
//                    HlSophisticationID = item.oldpeopleid,
//                });
//            }

//            return PhysicalExaminationJsonModelList;
//        }
//        /// <summary>
//        /// 查询居民体检列表
//        /// </summary>
//        /// <param name="keyValue">查询关键字</param>
//        /// <param name="startTime"></param>
//        /// <param name="endTime"></param>
//        /// <param name="regionCode">区划代码</param>
//        /// <param name="errorInfor">错误信息</param>
//        /// <param name="keyCode">默认为2，1-名字，2-身份证，3-档案号，4-自 定义编码，5-电话，0 所有列表 （此种情况 keyValue 可不填写）</param>
//        /// <param name="pageIndex">起始页，默认为0</param>
//        /// <returns></returns>
//        public List<PhysicalExaminationJsonModel> GetPhysicalExaminationList(string keyValue, string startTime, string endTime, string regionCode, out uint total, out string errorInfor, uint keyCode = 2, uint pageIndex = 0)
//        {
//            JContainer jContainer;

//            Dictionary<string, string> dic = new Dictionary<string, string>();
//            dic.Add("regionCode", regionCode);
//            dic.Add("keyValueType", keyCode.ToString());
//            dic.Add("keyValue", keyValue);
//            dic.Add("followUpdeS", startTime);
//            dic.Add("followUpdeE", endTime);
//            dic.Add("isStandard", "2");
//            dic.Add("pageSize", "10");
//            dic.Add("pageIndex", pageIndex.ToString());
//            httpPostHelper.PostData(baseHealthyURL.GetURL(URLEnum.getExamList), dic, out jContainer, out errorInfor);
//            return JsonDeserializeObject<PhysicalExaminationJsonModel>.getListT(jContainer, out total, ref errorInfor);
//        }
//        /// <summary>
//        /// 根据随访ID查询体检详细信息
//        /// </summary>
//        /// <param name="mtId"></param>
//        /// <param name="regionCode">区划信息</param>
//        /// <returns></returns>
//        public HealthExamInfoJsonModel GetPhysicalExaminationDetails(string mtId, string regionCode, out string errorInfor)
//        {
//            Dictionary<string, string> dic = new Dictionary<string, string>();
//            dic.Add("mtId", mtId);
//            JContainer jContainer;

//            httpPostHelper.PostData(baseHealthyURL.GetURL(URLEnum.getHealthExamInfo), dic, out jContainer, out errorInfor);

//            HealthExamInfoJsonModel healthExamInfoJsonModel = healthExamDataFilter.HealthExamDataFilterFunc(JsonDeserializeObject<HealthExamInfoJsonModel>.getT(jContainer, ref errorInfor));

//            if (healthExamInfoJsonModel == null) return null;

//            healthExamInfoJsonModel.HealthCareRecords = GetHealthCareRecords(healthExamInfoJsonModel.person_id, regionCode, out errorInfor);

//            if (healthExamInfoJsonModel.HealthCareRecords == null) return null;

//            return healthExamInfoJsonModel;
//        }

//        #endregion

//        #region 居民体检反馈单
//        /// <summary>
//        /// 获取居民体检反馈单详情
//        /// </summary>
//        /// <param name="mtId">体检标号</param>
//        /// <param name="regionCode">区划code</param>
//        /// <param name="errorInfor">错误信息</param>
//        /// <returns></returns>
//        public BaseHealthyReport GetPEFeedbackDetail(string mtId, string regionCode, out string errorInfor)
//        {
//            PEFeedbackDataFilter pEFeedbackDataFilter = new PEFeedbackDataFilter();

//            return pEFeedbackDataFilter.PEFeedbackDataFilterFunc(GetPhysicalExaminationDetails(mtId, regionCode, out errorInfor));
//        }

//        #endregion

//        #region 导诊指引单

//        /// <summary>
//        /// 查询导诊单模板详情
//        /// </summary>
//        /// <param name="id">不填表示获取当前医生所在机构最新的模板信息</param>
//        /// <param name="errorInfor">若发生错误，则返回错误信息</param>
//        /// <returns>导诊单模板详情</returns>
//        public TemplateJsonModel CheckguideTemplateFind(out string errorInfor, string id = "")
//        {
//            Dictionary<string, string> dic = new Dictionary<string, string>();
//            dic.Add("id", id);
//            JContainer jsonResult;
//            httpPostHelper.PostData(baseHealthyURL.GetURL(URLEnum.find_do), dic, out jsonResult, out errorInfor);

//            return JsonDeserializeObject<TemplateJsonModel>.getT(jsonResult, ref errorInfor);
//        }

//        /// <summary>
//        /// 查询用户导诊单列表
//        /// </summary>
//        /// <param name="regionCode">区划code</param>
//        /// <param name="errorInfor">若发生错误，则返回错误信息</param>
//        /// <param name="total">总共数量 </param>
//        /// <param name="startTime">开始时间’yyyy-MM-dd’</param>
//        /// <param name="endTime">结束时间 格式 ‘yyyy-MM-dd’</param>
//        /// <param name="page">请求页，默认从第一页开始，错误或不传都请求第一页</param>
//        /// <param name="size">每页个数，错误或不传默认请求10条数据</param>
//        /// <returns>用户导诊单列表</returns>
//        public List<ExaminationGuideListJsonModel> CheckguideList(string regionCode, out string errorInfor, out uint total, string startTime = "", string endTime = "", uint page = 0, uint size = 10)
//        {
//            Dictionary<string, string> dic = new Dictionary<string, string>();
//            dic.Add("regionCode", regionCode);
//            dic.Add("from", startTime);
//            dic.Add("to", endTime);
//            dic.Add("page", page.ToString());
//            dic.Add("size", size.ToString());
//            JContainer jsonResult;
//            httpPostHelper.PostData(baseHealthyURL.GetURL(URLEnum.list_do), dic, out jsonResult, out errorInfor);
//            if (jsonResult != null) total = uint.Parse(jsonResult["total"].ToString());
//            else total = 0;
//            return JsonDeserializeObject<ExaminationGuideListJsonModel>.getListT(jsonResult, ref errorInfor);
//        }

//        /// <summary>
//        /// 查询居民导诊单详情
//        /// </summary>
//        /// <param name="id">居民导诊单id</param>
//        /// <param name="errorInfor">若发生错误，则返回错误信息</param>
//        /// <returns>居民导诊单详情</returns>
//        public ExaminationGuideDetailJsonModel CheckguideDetail(string id, out string errorInfor)
//        {
//            Dictionary<string, string> dic = new Dictionary<string, string>();
//            dic.Add("id", id);
//            JContainer jsonResult;
//            httpPostHelper.PostData(baseHealthyURL.GetURL(URLEnum.detail_do), dic, out jsonResult, out errorInfor);
//            return JsonDeserializeObject<ExaminationGuideDetailJsonModel>.getT(jsonResult, ref errorInfor);
//        }

//        /// <summary>
//        /// 修改导诊单模板
//        /// </summary>
//        /// <param name="templateJsonModel">POST数据模板</param>
//        /// <param name="errorInfor">若发生错误，则返回错误信息</param>
//        /// <returns>true-成功，false-失败</returns>
//        public bool CheckguideTemplateUpdate(TemplateJsonModel templateJsonModel, out string errorInfor)
//        {
//            var item = httpPostHelper.Post(baseHealthyURL.GetURL(URLEnum.update_do), JsonConvert.SerializeObject(templateJsonModel));

//            if (item == null)
//            {
//                errorInfor = "网络异常！";
//                return false;
//            }

//            if (item["code"].ToString() == "0")
//            {
//                errorInfor = "";
//                return true;
//            }
//            else
//            {
//                errorInfor = item["message"].ToString();
//                return false;
//            }
//        }
//        /// <summary>
//        /// 新增居民导诊单
//        /// </summary>
//        /// <param name="data">POST数据</param>
//        /// <param name="errorInfor">若发生错误，则返回错误信息<</param>
//        /// <returns>true-成功，false-失败</returns>
//        public bool CheckguideSave(AddPhyExaGuiPostJsonModel addPhyExaGuiPostJsonModel, out string errorInfor)
//        {
//            var item = httpPostHelper.Post(baseHealthyURL.GetURL(URLEnum.save_do), JsonConvert.SerializeObject(addPhyExaGuiPostJsonModel));

//            if (item == null)
//            {
//                errorInfor = "网络异常！";
//                return false;
//            }

//            if (item["code"].ToString() == "0")
//            {
//                errorInfor = "";
//                return true;
//            }
//            else
//            {
//                errorInfor = item["message"].ToString();
//                return false;
//            }
//        }

//        #endregion

//        #region 签约协议
//        /// <summary>
//        /// 获取居民签约协议的列表，包括已签约、未签约
//        /// </summary>
//        /// <param name="regionCode">区划代码</param>
//        /// <param name="nameOrIdcard">姓名或者身份号码</param>
//        /// <param name="pageSize">一页数量</param>
//        /// <param name="pageIndex">页码</param>
//        /// <param name="errorInfor">错误信息</param>
//        public SigningList GetSigningList(string regionCode, string nameOrIdcard, uint pageSize, uint pageIndex, out string errorInfor)
//        {
//            Dictionary<string, string> dic = new Dictionary<string, string>();
//            dic.Add("regionCode", regionCode);
//            dic.Add("search", nameOrIdcard);
//            dic.Add("pageSize", pageSize.ToString());
//            dic.Add("pageIndex", pageIndex.ToString());
//            dic.Add("doctorRange", "1");
//            JContainer jContainer;
//            httpPostHelper.PostData(baseHealthyURL.GetURL(URLEnum.search_signed_person_do), dic, out jContainer, out errorInfor);

//            List<SigningModelJson> signingModelJsonList = JsonDeserializeObject<SigningModelJson>.getListT(jContainer, ref errorInfor);

//            if (signingModelJsonList == null)
//            {
//                if (errorInfor == "")
//                {
//                    if (pageIndex == 0) errorInfor = "未查询到用户数据！";
//                    else errorInfor = "没有查询到更多的数据！";
//                }
//                else if (errorInfor == "系统错误") errorInfor = "该账号未开通相关权限！";
//                return null;
//            }

//            for (int i = 0; i < signingModelJsonList.Count; i++)
//            {
//                signingModelJsonList[i].signingDetailJsonModel = GetFamilySigningDetail(signingModelJsonList[i].contractId, signingModelJsonList[i].id, out errorInfor);
//            }

//            return new SigningList()
//            {
//                total = uint.Parse(jContainer["total"].ToString()),
//                SigningModelJsonList = signingModelJsonList,
//            };
//        }
//        /// <summary>
//        /// 获取家庭签约协议详情
//        /// </summary>
//        /// <param name="contractId">协议合同id</param>
//        /// <param name="errorInfor">错误信息</param>
//        private SigningDetailJsonModel GetFamilySigningDetail(int contractId, int personId, out string errorInfor)
//        {
//            Dictionary<string, string> dic = new Dictionary<string, string>();
//            dic.Add("contractId", contractId.ToString());
//            dic.Add("personId", personId.ToString());
//            JContainer jContainer;
//            httpPostHelper.PostData(baseHealthyURL.GetURL(URLEnum.print_signed_contract_detail_do), dic, out jContainer, out errorInfor);
//            return JsonDeserializeObject<SigningDetailJsonModel>.getT(jContainer, ref errorInfor);
//        }

//        /// <summary>
//        /// 签约协议打印完毕后标记
//        /// </summary>
//        /// <param name="contractId">协议合同id</param>
//        /// <param name="errorInfor">错误信息</param>
//        public void SignPrintMark(int contractId, out string errorInfor)
//        {
//            Dictionary<string, string> dic = new Dictionary<string, string>();
//            dic.Add("contractId", contractId.ToString());
//            JContainer jContainer;
//            httpPostHelper.PostData(baseHealthyURL.GetURL(URLEnum.print_mark_do), dic, out jContainer, out errorInfor);
//        }

//        #endregion

//        #region - 离线下载 -
//        /// <summary>
//        /// 获取离线下载文件名的接口
//        /// </summary>
//        /// <param name="regionId"> 区划ID </param>
//        /// <param name="errorInfor"> 错误信息 </param>
//        /// <returns> "code":0,"message":"调用成功","data":"aca3966efe52407288d984101a674c30" errinfo </returns>
//        public Tuple<string, string, string, string> GetRegionFile(string regionId)
//        {
//            string errorInfor;
//            Dictionary<string, string> dic = new Dictionary<string, string>();
//            dic.Add("regionIds", regionId);
//            JContainer jsonResult;
//            httpPostHelper.PostData(baseHealthyURL.GetURL(URLEnum.offline_do), dic, out jsonResult, out errorInfor);

//            Func<object, string> func = l => l == null ? null : l.ToString();

//            if (jsonResult != null)
//            {
//                Tuple<string, string, string, string> result = new Tuple<string, string, string, string>(func(jsonResult["code"]),
//                    func(jsonResult["message"]), func(jsonResult["data"]), errorInfor);

//                return result;
//            }
//            else
//            {
//                return null;
//            }
//        }


//        /// <summary>
//        /// 检查文件是否可以下载
//        /// </summary>
//        /// <param name="fileName"> 要检查的文件名称 </param>
//        /// <returns> code":3024,"message":"离线文件努力准备中,请稍后","data":{"current":10,"total":100,"msg":"从基卫获取医生列表信息..."}} </returns>
//        /// <remarks>
//        /// code 3023 离线文件准备失败
//        /// code 3024 离线文件准备中
//        /// code 0 准备成功
//        /// </remarks>
//        public Tuple<string, string, string, string> CheckFile(string fileName)
//        {
//            Func<JObject, string> convertData = l =>
//            {
//                string total = l.Property("total").Value.ToString();
//                string current = l.Property("current").Value.ToString();

//                string data = Math.Round((double.Parse(current) * 100) / double.Parse(total), 0).ToString() + "%";

//                return data;
//            };

//            return CheckFile(fileName, convertData);
//        }

//        /// <summary>
//        /// 检查文件是否可以下载
//        /// </summary>
//        /// <param name="fileName"> 要检查的文件名称 </param>
//        /// <param name="convertData"> 转换状态数据的方法  </param>
//        /// <returns> code":3024,"message":"离线文件努力准备中,请稍后","data":{"current":10,"total":100,"msg":"从基卫获取医生列表信息..."}} </returns>
//        private Tuple<string, string, string, string> CheckFile(string fileName, Func<JObject, string> convertData)
//        {
//            string errorInfor;

//            JContainer jsonResult = this.CheckFile(fileName, out errorInfor);

//            Func<object, string> func = l => l == null ? null : l.ToString();

//            if (jsonResult != null)
//            {
//                string data = string.Empty;

//                if (jsonResult["code"].ToString() == "0")
//                {
//                    data = jsonResult["data"].ToString();
//                }
//                else
//                {
//                    if (jsonResult["data"] != null)
//                    {
//                        try
//                        {
//                            JObject jsonObj = JObject.Parse(jsonResult["data"].ToString());
//                            data = convertData(jsonObj);
//                        }
//                        catch (Exception ex)
//                        {
//                            errorInfor = ex.Message;
//                        }
//                    }

//                }

//                Tuple<string, string, string, string> result = new Tuple<string, string, string, string>(func(jsonResult["code"]),
//                    func(jsonResult["message"]), data, errorInfor);

//                return result;
//            }
//            else
//            {
//                return null;
//            }
//        }

//        /// <summary>
//        /// 检查文件是否可以下载
//        /// </summary>
//        /// <param name="fileName"> 要检查的文件名称 </param>
//        private JContainer CheckFile(string fileName, out string errorInfor)
//        {
//            Dictionary<string, string> dic = new Dictionary<string, string>();
//            dic.Add("fileName", fileName);
//            JContainer jsonResult;
//            httpPostHelper.PostData(baseHealthyURL.GetURL(URLEnum.checkFile_do), dic, out jsonResult, out errorInfor);

//            return jsonResult;
//        }


//        /// <summary> 下载文件 </summary>        
//        /// <param name="URL">下载文件地址</param>  
//        /// <param name="Filename">下载后的存放地址</param>        
//        /// <param name="Prog">用于显示的进度条</param>        
//        /// 
//        public void DownloadFile(string URL, string filename, Action<string, string> percentAction = null, int refreshTime = 1000)
//        {
//            float percent = 0;
//            int total = 0;
//            int current = 0;
//            HttpWebRequest Myrq = HttpWebRequest.Create(URL) as HttpWebRequest;
//            //Myrq.Method = "POST";
//            //Myrq.ContentType = "application/x-www-form-urlencoded;charset=utf-8";
//            Myrq.UserAgent = "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.1; .NET CLR 1.0.3705;)";
//            //Myrq.Accept = "*/*";
//            //Myrq.KeepAlive = true;
//            //Myrq.ProtocolVersion = HttpVersion.Version10;
//            //Myrq.Timeout = 30000;


//            HttpWebResponse myrp = (HttpWebResponse)Myrq.GetResponse();

//            long totalBytes = myrp.ContentLength;
//            total = (int)totalBytes;
//            System.IO.Stream st = myrp.GetResponseStream();
//            System.IO.Stream so = new System.IO.FileStream(filename, System.IO.FileMode.Create);

//            long totalDownloadedByte = 0;
//            byte[] by = new byte[1024];

//            int osize = st.Read(by, 0, (int)by.Length);


//            // Todo ：定时刷新进度 
//            if (percentAction != null)
//            {
//                Action action = () =>
//                {
//                    while (true)
//                    {
//                        Thread.Sleep(refreshTime);

//                        // Todo ：返回进度 
//                        percentAction(current.ToString(), total.ToString());
//                    }
//                };

//                Task task = new Task(action);
//                task.Start();
//            }


//            while (osize > 0)
//            {
//                totalDownloadedByte = osize + totalDownloadedByte;
//                so.Write(by, 0, osize);
//                current = (int)totalDownloadedByte;

//                osize = st.Read(by, 0, (int)by.Length);

//                percent = (float)totalDownloadedByte / (float)totalBytes * 100;
//            }
//            so.Close();
//            st.Close();




//        }


//        #endregion

//        #region 软件升级
//        /// <summary>
//        /// 检查更新版本
//        /// </summary>
//        /// <param name="fileVersion">如2.5.0</param>
//        /// <param name="mac">格式：XX-XX-XX-XX-XX-XX</param>
//        /// <returns></returns>
//        public SoftwareVersion CheckVersion(string fileVersion, string mac, out string errorInfor)
//        {
//            Dictionary<string, string> dic = new Dictionary<string, string>();
//            dic.Add("version", fileVersion);
//            dic.Add("os", "windows");
//            dic.Add("pkg", "PhyExaGuiSysPKG");
//            dic.Add("mac", mac);
//            JContainer jContainer;
//            httpPostHelper.PostData(baseHealthyURL.GetURL(URLEnum.upgrade_do), dic, out jContainer, out errorInfor);
//            return JsonDeserializeObject<SoftwareVersion>.getT(jContainer, ref errorInfor);
//        }
//        #endregion

//    }
//}
