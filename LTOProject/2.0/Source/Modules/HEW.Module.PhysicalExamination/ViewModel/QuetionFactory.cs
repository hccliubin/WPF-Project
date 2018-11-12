
using HEW.UserControls.Reports;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace HEW.Module.PhysicalExamination
{
    /// <summary> 从配置文件中加载问题 </summary>
    internal class QuetionFactory
    {

        public static QuetionFactory Instance = new QuetionFactory();
        internal QuetionListViewModel Create()
        {

            QuetionListViewModel q = new QuetionListViewModel();

            XmlElement modulesNode = XmlTools.xmlDoc.DocumentElement.SelectSingleNode("Modules") as XmlElement;

            if (modulesNode == null || modulesNode.ChildNodes == null) return q;

            foreach (XmlElement item in modulesNode.ChildNodes)
            {
                QuetionViewModelcs model = new QuetionViewModelcs();

                model.Index = item.GetAttribute("id");

                model.Detail = string.IsNullOrEmpty(item.GetAttribute("detial")) ? string.Empty : "(" + item.GetAttribute("detial") + ")";

                model.Quetion = string.Format("({0}) {1}{2}", model.Index, item.GetAttribute("quetion"), model.Detail);

                string[] items = item.GetAttribute("select").Split('|');

                model.Collection.Add(new AnswerViewModel(items[0], 1));
                model.Collection.Add(new AnswerViewModel(items[1], 2));
                model.Collection.Add(new AnswerViewModel(items[2], 3));
                model.Collection.Add(new AnswerViewModel(items[3], 4));
                model.Collection.Add(new AnswerViewModel(items[4], 5));

                q.Models.Add(model);
            }

            return q;
        }
    }
}
