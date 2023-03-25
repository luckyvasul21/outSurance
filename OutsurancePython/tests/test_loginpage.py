from faker import Faker

import pytest
from playwright.sync_api import expect, Page

from pages.loginpage import LoginPage
from pages.registrationpage import RegistrationPage

login_parameters = [
    (f'{Faker().first_name()}', f'{Faker().last_name()}', f'{Faker().ascii_company_email()}',
     f'{Faker().lexify(text="??????").lower()}', True),
    ('', f'{Faker().last_name()}', f'{Faker().ascii_company_email()}', f'{Faker().lexify(text="??????").lower()}',
     False)]


@pytest.mark.dependency()
@pytest.mark.parametrize("firstname, lastname, email, password, isvisible", login_parameters)
def test_registration(page: Page, firstname, lastname, email, password, isvisible) -> None:
    registration_page = RegistrationPage(page)
    registration_page.load()
    registration_page.registration_action(firstname=firstname, lastname=lastname, email=email, password=password)

    assert page.locator("a.account").get_by_text(email).is_visible() == isvisible


@pytest.mark.dependency(depends=["test_registration"])
@pytest.mark.parametrize("firstname, lastname, email, password, isvisible", login_parameters)
def test_login(page: Page, firstname, lastname, email, password, isvisible) -> None:
    login_page = LoginPage(page)
    login_page.load()
    login_page.loginaction(email, password)
    page.wait_for_load_state('networkidle')

    assert page.locator(".header-links .account").is_visible() == isvisible
