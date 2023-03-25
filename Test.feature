Feature: DemoWebShop Test Feature


  Description: 
  Test in Python (test function => test_login)
  Scenario Outline: Verify registration is successful, submitting mandatory fields required for registration
    Given I am on registration page
    When User enter details for registration
      | FirstName   | LastName   | Email   | Password   | ConfirmPassword   |
      | <firstName> | <lastName> | <email> | <password> | <confirmpassword> |
    And Click Register
    Then Verify user is registered succesfully with <Status>
      | Status                      |
      | Your registration completed |
    Examples:
      | firstName         | lastName          | email                                   | password          | confirmpassword   |
      | random.toString() | random.toString() | random.toString()@random.toString().com | random.toString() | random.toString() |
      |                   | random.toString() | random.toString()@random.toString().com | random.toString() | random.toString() |
	  
	Test in DotNet (test function => VerifyLoggedInUser)
	Test in Python (test function => test_login)
	Scenario Outline: Verify non-registered user cannot login
    Given I am on login page
    When User enter details for registration
      | FirstName   | LastName   | Email   | Password   | ConfirmPassword   |
      | <firstName> | <lastName> | <email> | <password> | <confirmpassword> |
    And Click Register
    Then Verify user is registered succesfully with <Status>
      | Status                      |
      | Your registration completed |
    Examples:
      | firstName         | lastName          | email                                   | password          | confirmpassword   |
      | random.toString() | random.toString() | random.toString()@random.toString().com | random.toString() | random.toString() |
      |                   | random.toString() | random.toString()@random.toString().com | random.toString() | random.toString() |  
	
	
  Scenario Outline: User should not log out from session accessing logout url
    Given I am on login page
    When User is logged in with correct credentials
    Then Verify User is logged in successfully
    And User access <URL>
    Then User should not logged out
    Examples:
      | URL                                      |
      | https://demowebshop.tricentis.com/logout |

  Scenario Outline: User should not enter login details again when already Logged-in (screenshot2.png)
    Given I am on login page
    When User is logged in with correct credentials
    Then Verify User is logged in successfully
    And User access <URL>
    Then User should not enter credential again to login
    Examples:
      | URL                                     |
      | https://demowebshop.tricentis.com/login |

   Test in DotNet (test function => postdatatest)
  Scenario Outline: login API request POST data should send personal details encrypted (screenshot1)
    Given I am on login page
    When User is logged in with correct credentials
    And Trace POST request POST data
    Then Is Password encrypted
    Examples:
      | URL                                     |
      | https://demowebshop.tricentis.com/login |

  Test in DotNet (test function => BuyaProduct)
  Scenario Outline: Verify Add to Cart button is enabled to all Item boxes
    Given User is logged in with correct credentials
    When  Select a category from Left menu <category>
    Then Verify 'Add to cart' button is visible on Itemboxes
    And Click on any item-box which do not have 'Add to cart'
    And user is on product-details-page
    Then Verify 'Add to cart' button is visible
    Examples:
      | category             |
      | Books                |
      | Computers -> Desktop |

  Scenario Outline: Verify different Payment Methods with Corrects payments details 
    Given User is logged in with correct credentials
    When  User selected <> item(s) into Shopping Cart
    And user navigates t0 shaopping cart page
    Then verify <> items added matches to shopping cart list
    And Check Agree Terms
    And Click Checkout
    And Enter Billing Address
    And Click Continue
    And Select Payment method <>
    And Click Continue
    Then verify payment information
    And Click Continue
    Then Verify Confirm order
    |Billing Address| Paymet method| Products|
    Then Verify Order is Processed with status 'Your order has been successfully processed!'
    
