using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApplicationDBManager.ViewModels
{
    public class ErrorResponse
    {
        public string Message{ get; set; } = string.Empty;
        public int Code { get; set; }
    }
}
