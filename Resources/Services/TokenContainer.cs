using Batsy.Resources.Interfaces;
using System.Windows;

namespace Batsy.Resources.Services
{
    public class TokenContainer : ITokenContainer
    {
        public string GetToken()
        {
            try
            {
                return App.AccessToken;

            }catch (Exception ex)
            {
                MessageBox.Show(ex.Message,"Get token",MessageBoxButton.OK,MessageBoxImage.Error);
                return string.Empty;
            }
        }

        public string RefreshToken()
        {
            throw new NotImplementedException();
        }

        public void SetToken(string token)
        {
            try
            {
                App.AccessToken = token;
            }catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Set token", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
