import random
import string

from faker import Faker

fake = Faker()

print(''.join(random.choices(string.ascii_lowercase, k=5)))

print(fake.first_name())

print(f'{Faker().first_name()}.{Faker().last_name()}@test.com'.lower())

print(Faker().ascii_company_email())

print(fake.lexify(text='??????').lower())
