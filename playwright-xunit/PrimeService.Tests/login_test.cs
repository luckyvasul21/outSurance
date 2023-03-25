using Xunit;
using Microsoft.Playwright;
using System.Threading.Tasks;
using System;
using Xunit.Abstractions;
using System.Text;
using FluentAssertions.Execution;

namespace Prime.UnitTests.Services
{
    public class login_test
    {

        private readonly ITestOutputHelper testOutputHelper;

        public login_test(ITestOutputHelper testOutputHelper)
        {
            this.testOutputHelper = testOutputHelper;
        }

        [Theory]
        [InlineData("jedaj85013@necktai.com", "jedaj85013password", 302)]
        [InlineData("test@test.com", "testpassword", 200)]
        public async Task postdatatest(String username, String password, int statusCode)
        {
            using var playwright = await Playwright.CreateAsync();
            var chrome = playwright.Chromium;
            var browser = await chrome.LaunchAsync(new BrowserTypeLaunchOptions { Headless = false });
            var page = await browser.NewPageAsync();


            await page.GotoAsync("https://demowebshop.tricentis.com/");


            await page.Locator(".header-links a[href]").GetByText("Log in").ClickAsync();

            var formLocator = page.Locator("form[action = '/login']");

            if (await formLocator.IsVisibleAsync())
            {
                await page.FillAsync("input.email", username);
                await page.FillAsync("input.password", password);
                
            }

            var waitForResponseTask = await page.RunAndWaitForResponseAsync(async () =>
             {
                 await page.Locator("input.login-button").ClickAsync();
             }, response => response.Url.Contains("https://demowebshop.tricentis.com/login"));

            Console.WriteLine(System.Text.Encoding.UTF8);

            //byte[] bytes = Encoding.Default.GetBytes(username);
            //username = Encoding.UTF8.GetString(bytes);

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
            using var playwright = await Playwright.CreateAsync();
            var chrome = playwright.Chromium;
            var browser = await chrome.LaunchAsync(new BrowserTypeLaunchOptions { Headless = false });
            var page = await browser.NewPageAsync();


            await page.GotoAsync("https://demowebshop.tricentis.com/");


            await page.Locator(".header-links a[href]").GetByText("Log in").ClickAsync();

            var formLocator = page.Locator("form[action = '/login']");

            if (await formLocator.IsVisibleAsync())
            {
                await page.FillAsync("input.email", username);
                await page.FillAsync("input.password", password);
                await page.Locator("input.login-button").ClickAsync();
            }

            await page.WaitForLoadStateAsync(LoadState.NetworkIdle);

            var userloggedin = await page.Locator(".header-links .account").IsVisibleAsync();

            using (new AssertionScope())
            {
                Assert.Equal(isvisible, userloggedin);
            }

        }


        [Fact]
        public async Task BuyaProduct()
        {
            using var playwright = await Playwright.CreateAsync();
            var chrome = playwright.Chromium;
            var browser = await chrome.LaunchAsync(new BrowserTypeLaunchOptions { Headless = false });
            var page = await browser.NewPageAsync();

            await page.GotoAsync("https://demowebshop.tricentis.com/books");

            var itemBoxes = await page.Locator(".item-box").CountAsync();
            var addToCartbuttons = await page.Locator(".item-box input").CountAsync();
            

            using (new AssertionScope())
            {
                Assert.Equal(itemBoxes, addToCartbuttons);
            }
        }
    }
}