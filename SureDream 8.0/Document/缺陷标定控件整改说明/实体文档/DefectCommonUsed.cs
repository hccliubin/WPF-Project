 /// <summary>
    /// ����ȱ��
    /// <summary>
    public class DefectCommonUsed : TyeEncodeDeviceEntity
    {
        /// <summary>
        /// ����
        /// </summary>
        public string Describletion { get; set; }

        /// <summary>
        /// ʹ�ô���
        /// </summary>
        public int CountUse { get; set; }

        /// <summary>
        /// ȱ������1Ԥ��ȱ�ݣ�2���ȱ�ݣ�3��ʷȱ��
        /// </summary>
        public int DefectType { get; set; }
    }