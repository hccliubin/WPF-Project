using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LTO.Domain.DataService
{
    public class ServiceManager
    {
        public static DataService DataService = new DataService();
        public static ToolService ToolService = new ToolService();

        public static void Init()
        {
            DataService.Init();
            ToolService.Init();
        }

        public static void Dispose()
        {
            DataService.Dispose();
            ToolService.Dispose();
        }
    }
}
