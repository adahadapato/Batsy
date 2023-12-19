using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Batsy.Resources.Interfaces
{
    public interface ITokenContainer
    {
        string GetToken();
        void SetToken(string token);
        string RefreshToken();
    }
}
