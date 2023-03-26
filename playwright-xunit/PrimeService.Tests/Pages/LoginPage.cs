using Microsoft.Playwright;
using System.Threading.Tasks;

namespace Outsource.Tests.Pages
{
    public class LoginPage
    {
        private IPage _page;
        private readonly ILocator _loginlink;
        private readonly ILocator _email;
        private readonly ILocator _password;
        private readonly ILocator _loginbutton;
        public LoginPage(IPage page) 
        {
            _page = page;
            _loginlink = _page.Locator(".header-links a[href]").GetByText("Log in");
            _email = _page.Locator("input.email");
            _password = _page.Locator("input.password");
            _loginbutton = _page.Locator("input.login-button");
        }

        public async Task ClickLogin() => await _loginlink.ClickAsync();

        public async Task LoginAction(string email, string password)
        {
            await _email.FillAsync(email);
            await _password.FillAsync(password);
        }

        public async Task ClickSignIn() => await _loginbutton.ClickAsync();
    }
}
