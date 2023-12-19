using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Batsy.Models
{
    public class LoginResponse
    {
        public string PersonnelNo { get; set; }
        public string Name { get; set; }
        public string Picture { get; set; }
        public string Token { get; set; }
    }

    public class LoginRequest
    {
        public string PersonnelNo { get; set; }
        public string Password { get; set; }
    }
}
