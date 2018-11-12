using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace LTO.General.ModuleManager
{
    public interface ILTOModule
    {
        string ModuleName { get; }


        ImageSource Image { get; }

        IModulePage ModulePage { get; }

    }

    public interface IModulePage
    {
        Action ImageClick { get; set; }
    }

}
