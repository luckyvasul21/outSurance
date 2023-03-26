using Microsoft.Playwright;
using System;
using System.Threading.Tasks;


namespace Outsource.Tests.Drivers
{
    public class Driver : IDisposable
    {
        private readonly Task<IPage> _page;
        private IBrowser? _browser;

        public Driver() => _page = InitializePlaywright();
        public IPage Page => _page.Result;

        public void Dispose() => _browser.CloseAsync();

        public async Task<IPage> InitializePlaywright()
        {
            var playwright = await Playwright.CreateAsync();
            _browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions { Headless = false });
            return await _browser.NewPageAsync();
        }
    }
}
