using System;

namespace CDTY.DataAnalysis.Entity
{
    /// <summary>
    /// 原始数据表信息表实体类
    /// <summary>
    [Serializable]
    public class TyeEncodeDeviceEntity 
    {
        private string _id;
        /// <summary>
        /// 
        /// <summary>
        public string ID { get => _id; set { _id = value; } }

        private string _parentid;
        /// <summary>
        /// 父节点ID
        /// <summary>
        public string ParentID { get => _parentid; set { _parentid = value;  } }

        private string _name;
        /// <summary>
        /// 名称
        /// <summary>
        public string Name { get => _name; set { _name = value; } }

        private string _namepy;
        /// <summary>
        /// 中文名称拼音首字母，用于快速检索
        /// <summary>
        public string NamePY { get => _namepy; set { _namepy = value;  } }

        private string _rootcode;
        /// <summary>
        /// 根节点编码
        /// <summary>
        public string RootCode { get => _rootcode; set { _rootcode = value; } }

        private string _code;
        /// <summary>
        /// 节点编码
        /// <summary>
        public string Code { get => _code; set { _code = value;  } }

        private int _paramstate;
        /// <summary>
        /// 1： 枚举值 2：评判指标
        /// <summary>
        public int ParamState { get => _paramstate; set { _paramstate = value;  } }

        private string _defaultrate;
        /// <summary>
        /// 对于标称属性而言，取值范围00-99。对于二元属性和数值属性而言，取值范围00-02，其中00表示正常状态，01表示二级故障，02表示一级故障。
        /// <summary>
        public string DefaultRate { get => _defaultrate; set { _defaultrate = value;  } }

        private int _valuetype;
        /// <summary>
        /// 1：标称  2：二元  3：数值
        /// <summary>
        public int ValueType { get => _valuetype; set { _valuetype = value;} }

        private string _unit;
        /// <summary>
        /// 属性单位
        /// <summary>
        public string Unit { get => _unit; set { _unit = value;  } }

        private int _orderno;
        /// <summary>
        /// 组内的代码序号
        /// <summary>
        public int OrderNo { get => _orderno; set { _orderno = value;  } }

        private int _isdelete;
        /// <summary>
        /// 0：否   1：是
        /// <summary>
        public int IsDelete { get => _isdelete; set { _isdelete = value;  } }

        private int _usedin1c;
        /// <summary>
        /// 是否使用于1C，1：是  0：否
        /// <summary>
        public int UsedIn1C { get => _usedin1c; set { _usedin1c = value; } }

        private int _usedin2c;
        /// <summary>
        /// 是否使用于2C，1：是  0：否
        /// <summary>
        public int UsedIn2C { get => _usedin2c; set { _usedin2c = value;} }

        private int _usedin3c;
        /// <summary>
        /// 是否使用于3C，1：是  0：否
        /// <summary>
        public int UsedIn3C { get => _usedin3c; set { _usedin3c = value; } }

        private int _usedin4c;
        /// <summary>
        /// 是否使用于4C，1：是  0：否
        /// <summary>
        public int UsedIn4C { get => _usedin4c; set { _usedin4c = value; } }

        private int _usedin5c;
        /// <summary>
        /// 是否使用于5C，1：是  0：否
        /// <summary>
        public int UsedIn5C { get => _usedin5c; set { _usedin5c = value; } }

        private int _usedin6c;
        /// <summary>
        /// 是否使用于6C，1：是  0：否
        /// <summary>
        public int UsedIn6C { get => _usedin6c; set { _usedin6c = value; } }

    }
}
