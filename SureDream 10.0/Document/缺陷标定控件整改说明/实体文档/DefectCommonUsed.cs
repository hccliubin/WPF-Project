 /// <summary>
    /// 常用缺陷
    /// <summary>
    public class DefectCommonUsed : TyeEncodeDeviceEntity
    {
        /// <summary>
        /// 描述
        /// </summary>
        public string Describletion { get; set; }

        /// <summary>
        /// 使用次数
        /// </summary>
        public int CountUse { get; set; }

        /// <summary>
        /// 缺陷类型1预估缺陷，2最近缺陷，3历史缺陷
        /// </summary>
        public int DefectType { get; set; }
    }