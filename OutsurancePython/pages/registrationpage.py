from playwright.sync_api import Page


class RegistrationPage:
    def __init__(self, page: Page) -> None:
        self.page = page
        self.register_link_button = page.locator(".header-links a[href]").get_by_text("Register", exact=True)
        # self.gender_radiobutton = page.locator("input+label")
        self.firstname_input = page.locator("input#FirstName")
        self.lastname_input = page.locator("input#LastName")
        self.email_input = page.locator("input#Email")
        self.password_input = page.locator("input#Password")
        self.confirmpassword_input = page.locator("input#ConfirmPassword")
        self.register_button = page.locator("input#register-button")

    def load(self) -> None:
        self.page.goto('https://demowebshop.tricentis.com/register')

    def registration_action(self, firstname: str, lastname: str, email: str, password: str) -> None:
        self.firstname_input.fill(firstname)
        self.lastname_input.fill(lastname)
        self.email_input.fill(email)
        self.password_input.fill(password)
        self.confirmpassword_input.fill(password)
        self.register_button.click()
