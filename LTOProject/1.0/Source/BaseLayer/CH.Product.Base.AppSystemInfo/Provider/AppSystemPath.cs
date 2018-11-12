
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace CH.Product.Base.AppSystemInfo
{
    /// <summary> 系统配置文件接口 </summary>
    public class AppSystemPath : IAppSystemPath
    {
        public AppSystemPath()
        {
           
        }
        public AppSystemPath(string binDirectoryPath, CultureInfo culture, string exeName)
        {

            BinPath = binDirectoryPath;

            ModulesDllPath = Path.Combine(BinPath, @"..\Modules");

            ConfigPath = Path.Combine(binDirectoryPath, @"..\Config");

            GlobalConfigPath = Path.Combine(ConfigPath, @"Global");

            CultureConfigPath = Path.Combine(ConfigPath, culture.Name);

            RegistriesConfigPath = Path.Combine(CultureConfigPath, "Registries");

            ApplicationsConfigPath = Path.Combine(CultureConfigPath, "Applications");

            ProjectsPath = Path.Combine(binDirectoryPath, @"..\Projects\");

            this.DocumentPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), exeName);

            if(!Directory.Exists(this.DocumentPath))
            {
                Directory.CreateDirectory(this.DocumentPath);
            }

        }

        public string Documents { get; set; }
        public string BinPath { get; set; }
        public string ModulesDllPath { get; set; }
        public string ConfigPath { get; set; }
        public string GlobalConfigPath { get; set; }
        public string CultureConfigPath { get; set; }
        public string RegistriesConfigPath { get; set; }
        public string ApplicationsConfigPath { get; set; }
        public string ProjectsPath { get; set; }
        public string DocumentPath { get; set; }
    }
}
