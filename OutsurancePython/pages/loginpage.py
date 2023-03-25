from playwright.sync_api import Page

class LoginPage:
    def __init__(self, page: Page) -> None:
        self.page = page
        self.login_button = page.locator(".header-links a[href]").get_by_text("Log in")
        self.email_input = page.locator("input.email")
        self.password_input = page.locator("input.password")
        self.signin_button = page.locator("input.login-button")

    def load(self) -> None:
        self.page.goto('https://demowebshop.tricentis.com/')

    def loginaction(self, username: str, password: str) -> None:
        self.login_button.click()
        self.email_input.fill(username)
        self.password_input.fill(password)
        self.signin_button.click()

