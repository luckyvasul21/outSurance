using Xunit;
using Microsoft.Playwright;
using System.Threading.Tasks;
using System;
using FluentAssertions.Execution;
using Outsource.Tests.Drivers;
using Outsource.Tests.Pages;

namespace Prime.UnitTests.Services
{
    public class Login_test
    {
        private readonly Driver _driver;
        private readonly LoginPage _loginPage;

        //Driver _driver;
        public Login_test()
        {
            _driver = new Driver();
            _loginPage = new LoginPage(_driver.Page);
        }

        [Theory]
        [InlineData("jedaj85013@necktai.com", "jedaj85013password", 302)]
        [InlineData("test@test.com", "testpassword", 200)]
        public async Task Postdatatest(String username, String password, int statusCode)
        {
            await  _driver.Page.GotoAsync("https://demowebshop.tricentis.com/");

            await _loginPage.ClickLogin();

            var formLocator = _driver.Page.Locator("form[action = '/login']");

            if (await formLocator.IsVisibleAsync())
            {   
                await _loginPage.LoginAction(username, password); 
            }

            var waitForResponseTask = await _driver.Page.RunAndWaitForResponseAsync(async () =>
             {
                 await _loginPage.ClickSignIn();
             }, response => response.Url.Contains("https://demowebshop.tricentis.com/login"));

            using (new AssertionScope())
            {
                Assert.Equal(statusCode, waitForResponseTask.Status);
                Assert.Contains(password, waitForResponseTask.Request.PostData);
            }
        }


        [Theory]
        [InlineData("jedaj85013@necktai.com", "jedaj85013password", true)]
        [InlineData("test@test.vom", "testpassword", false)]
        public async Task VerifyLoggedInUser(String username, String password, Boolean isvisible)
        {
            await _driver.Page.GotoAsync("https://demowebshop.tricentis.com/");

            await _loginPage.ClickLogin();

            var formLocator = _driver.Page.Locator("form[action = '/login']");

            if (await formLocator.IsVisibleAsync())
            {
                await _loginPage.LoginAction(username, password);
                await _loginPage.ClickSignIn();
            }

            await _driver.Page.WaitForLoadStateAsync(LoadState.NetworkIdle);

            var userloggedin = await _driver.Page.Locator(".header-links .account").IsVisibleAsync();

            using (new AssertionScope())
            {
                Assert.Equal(isvisible, userloggedin);
            }

        }

        [Fact]
        public async Task BuyaProduct()
        {
            await _driver.Page.GotoAsync("https://demowebshop.tricentis.com/books");

            var itemBoxes = await _driver.Page.Locator(".item-box").CountAsync();
            var addToCartbuttons = await _driver.Page.Locator(".item-box input").CountAsync();

            using (new AssertionScope())
            {
                Assert.Equal(itemBoxes, addToCartbuttons);
            }
        }
    }
}