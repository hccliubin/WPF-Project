using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Globalization;
using System.Xml;

namespace LTO.Base.Product.Provider
{
    public class AppSystemInfo : IAppSystemInfo
    {
        CultureInfo _culture = null;

        AppSystemPath _systemPath = null;

        public IAppSystemPath SystemPath { get { return _systemPath; } }
        AppRegistries _regisries = null;

        public AppRegistries Regisries
        {
            get { return _regisries; }
            set { _regisries = value; }
        }
        public void Init(string binDirectoryPath,string exeName)
        {
            string rootPath = Path.Combine(binDirectoryPath, @"..\");

            _culture = CultureInfo.CurrentCulture;

            //  设置路径信息
            _systemPath = new AppSystemPath(binDirectoryPath, _culture, exeName);

            //  加载注册信息
            _regisries = new AppRegistries(_systemPath);
        }

        static AppSystemInfo s_instance = new AppSystemInfo();
        public static AppSystemInfo Instance { get { return s_instance; } }

        public CultureInfo DefaultCultureInfo
        {
            get { return _culture; }
        }
        
    }
}
