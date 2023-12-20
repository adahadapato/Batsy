

using Batsy.Resources.Interfaces;
using Microsoft.Win32;
using Newtonsoft.Json.Linq;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;

namespace Batsy.Resources.Services;

public class RegistryService : RegistryToken
{
    public RegistryService()
    {
       
    }

    public bool FirstStart
    {
        get { return Convert.ToBoolean(GetValue("first")); }
        set { SetValue("first", value.ToString()); }
    }

    public bool LogOut
    {
        get { return Convert.ToBoolean(GetValue("Logout")); }
        set { SetValue("Logout", value.ToString()); }
    }

    public string ApiToken
    {
        get { return GetValue("ApiToken").ToString(); }
        set { SetValue("ApiToken", value); }
    }
    public string ExamType
    {
        get { return GetValue("examType")?.ToString(); }
        set { SetValue("examType", value); }
    }

    public string Examination
    {
        get { return GetValue("examination")?.ToString(); }
        set { SetValue("examination", value); }
    }
    public string ExamYear
    {
        get { return GetValue("examYear")?.ToString(); }
        set { SetValue("examYear", value); }
    }
    public string ExamsDetails
    {
        get { return GetValue("examsDetails")?.ToString(); }
        set { SetValue("examsDetails", value); }
    }

    public string FullName
    {
        get { return GetValue("fullName")?.ToString(); }
        set { SetValue("fullName", value); }
    }

    public string PersonnelNo
    {
        get { return GetValue("personnelNo")?.ToString(); }
        set { SetValue("personnelNo", value); }
    }

    public override string BaseKey()
    {
        throw new NotImplementedException();
    }
}
