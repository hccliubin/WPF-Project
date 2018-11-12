using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LTO.Base.Product.Provider
{
    public interface IAppSystemInfo
    {
        IAppSystemPath SystemPath { get; }
    }
}
