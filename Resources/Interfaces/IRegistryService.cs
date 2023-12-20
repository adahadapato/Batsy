

using Microsoft.Win32;
using System.Windows;

namespace Batsy.Resources.Interfaces;

//public interface IRegistryService
//{
    
//}

public abstract class RegistryToken
{
    public virtual string GetValue(string key)
    {
        try
        {
            RegistryKey mICParam = Registry.CurrentUser;
            var mICParams = mICParam.OpenSubKey(@"software\AOS\batsy", true);
            if (mICParams == null)
            {
                mICParam.CreateSubKey($"{mICParams}", true);
                //SetValue("first","true");
                
            }
            return mICParams?.GetValue(key)?.ToString();

        }
        catch (Exception e)
        {
            MessageBox.Show(e.Message);
        }
        return string.Empty;
    }
    public virtual void SetValue(string key, string value)
    {
        try
        {
            RegistryKey mICParam = Registry.CurrentUser;
            var mICParams = mICParam.OpenSubKey(@"software\AOS\batsy", true);
            if (mICParams == null)
            {
                mICParam.CreateSubKey($"{mICParams}", true);
            }
            mICParams?.SetValue(key, value);
            mICParams?.Close();
        }
        catch (Exception e)
        {
            MessageBox.Show(e.Message);
        }
    }

    public abstract string BaseKey();
}
