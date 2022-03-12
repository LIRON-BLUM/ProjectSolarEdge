using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectSolarEdge.Server.Configuration
{
    public class ApiResult
    {
        public bool Success { get; set; }
        public List<string> Errors { get; set; }
    }
}
