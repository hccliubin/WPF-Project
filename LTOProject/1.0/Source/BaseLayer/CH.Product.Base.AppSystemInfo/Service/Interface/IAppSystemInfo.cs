using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CH.Product.Base.AppSystemInfo
{
    public interface IAppSystemInfo
    {
        IAppSystemPath SystemPath { get; }
    }
}
