using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CH.Product.Base.AppSystemInfo
{
    /// <summary> 系统配置文件接口 </summary>
    public interface IAppSystemPath
    {
  
        string DocumentPath { get; }

        /// <summary> bin所在目录 </summary>
        string BinPath { get; }

        /// <summary> 配置文件所在目录 </summary>
        string ConfigPath { get; }

        string GlobalConfigPath { get; }

        string CultureConfigPath { get; }

        string ProjectsPath { get; }

        /// <summary> 模块注册文件所在目录 </summary>
        string RegistriesConfigPath { get; set; }

        string ModulesDllPath { get; set; }

        string ApplicationsConfigPath { get; set; }
    }

}
