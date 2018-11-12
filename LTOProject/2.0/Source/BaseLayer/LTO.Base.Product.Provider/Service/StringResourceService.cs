using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace LTO.Base.Product.Provider
{
    public class StringResourceService
    {
        public static StringResourceService Instance = new StringResourceService();

        XmlDocument _xmlDoc = new XmlDocument();

        string path;
        public void Init()
        {
            path = Path.Combine(AppSystemInfo.Instance.SystemPath.DocumentPath, "Config.xml");

            if (!File.Exists(path))
            {
                _xmlDoc.AppendChild(_xmlDoc.CreateXmlDeclaration("1.0", "UTF-8", null));
                XmlElement root = _xmlDoc.CreateElement("ROOT");
                _xmlDoc.AppendChild(root);

                var theElem = _xmlDoc.CreateElement("GovernmentUnit");
                theElem.InnerText = "729818173";
                root.AppendChild(theElem);

                var ipElem = _xmlDoc.CreateElement("IP");
                ipElem.InnerText = "192.168.5.15";
                root.AppendChild(ipElem);

                var portElem = _xmlDoc.CreateElement("Port");
                portElem.InnerText = "8080";
                root.AppendChild(portElem);

                var downElem = _xmlDoc.CreateElement("CountDownTime");
                downElem.InnerText = "1800";
                root.AppendChild(downElem);

                var AutoLeaveElem = _xmlDoc.CreateElement("AutoLeaveDownTime");
                AutoLeaveElem.InnerText = "3600";
                root.AppendChild(AutoLeaveElem);

                var userNameElem = _xmlDoc.CreateElement("UserName");
                userNameElem.InnerText = "null";
                root.AppendChild(userNameElem);

                var passWordElem = _xmlDoc.CreateElement("PassWord");
                passWordElem.InnerText = "null";
                root.AppendChild(passWordElem);

                var tagElem = _xmlDoc.CreateElement("Tag");
                tagElem.InnerText = "null";
                root.AppendChild(tagElem);

                var tagStartPage = _xmlDoc.CreateElement("StartPage");
                tagStartPage.InnerText = "0";
                root.AppendChild(tagStartPage);

                var tagEnblePrint = _xmlDoc.CreateElement("EnblePrint");
                tagEnblePrint.InnerText = "0";
                root.AppendChild(tagEnblePrint);

                var Address = _xmlDoc.CreateElement("Address");
                Address.InnerText = "三台县潼川镇中心卫生院";
                root.AppendChild(Address);

                _xmlDoc.Save(path);
            }
            else
            {
                _xmlDoc.Load(path);
            }


        }

        public string GetStringByID(string id = "GovernmentUnit")
        {
            if (_xmlDoc == null) return id;

            //_xmlDoc.DocumentElement.GetElementsByTagName(id);
            XmlElement cultureNode = _xmlDoc.DocumentElement.SelectSingleNode(id) as XmlElement;

            return cultureNode.InnerText;
        }


        public void SetStringByID(string id, string value)
        {
            XmlElement cultureNode = _xmlDoc.DocumentElement.SelectSingleNode(id) as XmlElement;

            cultureNode.InnerText = value;

            _xmlDoc.Save(path);
        }
    }
}
