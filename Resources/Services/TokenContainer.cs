using Batsy.Resources.Interfaces;
using System.Windows;

namespace Batsy.Resources.Services
{
    public class TokenContainer : ITokenContainer
    {
        private readonly RegistryService _registryService;
        public TokenContainer(RegistryService registryService)
        {

            _registryService = registryService;

        }
        public string GetToken()
        {
            try
            {
                return _registryService.ApiToken;

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
                _registryService.ApiToken = token;
            }catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Set token", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
