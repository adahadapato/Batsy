using Batsy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Batsy.Resources.Interfaces
{
    public interface IResourceService
    {
        Task<(bool Success, string Message, LoginResponse Data)> Login(LoginRequest data);
        Task<(bool Success, string Message, StaffObject? Data)> GetStaffDetails(string _staffNo);
    }
}
