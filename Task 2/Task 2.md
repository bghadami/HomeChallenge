## Task 2- Automated tests for Monefy App

I proposed automated E2E Core test cases which are meant to reflect app core features. 
E2E testing is meant to ensure that user interactions always work and can complete a workflow successfully.Unit and API testing should test business logic but QA engineer can propose tests that should stop users from encountering bugs when performing high-value actions. 

I would like to write e2e scenarios to cover Monefy Key features: 

- Add new records quickly with the intuitive and easy-to-use interface
- See your spending distribution on an easy-to-read chart, or get detailed          information from the records list
- Safely synchronize using your own Google Drive or Dropbox account
- Take control of recurring payments
- Track in multi-currencies
- Access your spending tracker easily with handy widgets
- Manage custom or default categories
- Backup and export personal finance data in one click
- Save money with budget tracker
- Stay secure with passcode protection
- Use multiple accounts
- Crunch numbers with the built-in calculator
 

## _Test Senarios_
1-Create an income and expense and show the balance with a specific currency (High)
2-Create and manage multiple accounts(High)
3-Create and manage multi currencies(High)
4-Export application data to file(low)
5-Check widgets values  (low)
6-Create custom category(low)
7-Check the impact of date interval in balance(High)
8-Create and manage passcode(low)


## _Test Cases_
I only write down a test case for first Senarios :

| Test cases ID:| TC-01                                              |
| ------        | ------                                             |
| Senario       | Create an income and expence and show the balance with a specific currency                                                    |
| Priority      | High                                               |
| Pre-Conditions|                                                    |
| Steps         | 1-open application 2-Press kebab menu 3-press setting 4-click on currency 5-enter [Currency_value] 6-select [Currency_value] 7-click on main screen 8-press income 9-enter [Income_value] and [Income _Note_Value] 10-press choose category 11-select expense category 12-press expense 13-enter [Expense_value] and [Expense_note] 14-press choose category 15-choose [expense_category_value] 16-press balance |
| Test Data     |Currency_Value="euro",Income_Value=500,Income_ Note_Value="myNoteI",Income_Category_Value="salary", Expense_value=20 , Expense_Note_Value="myNoteE", Expense_Category_Value="Car", Balance_Value="euro480.00" ,Persentage_Value="4%", Pie_Caption_Value="euro500.00 euro20.00"    |
|Expected Result| 1-pie chart shows with minimum 1 expense 2-right menu shows 3-setting menu shows 4-currency shows 5-check filter list 6-eur shows as selected currency 7-menu close 8-income screen shows 10-income categories shows 11-notify shows,pie number changes 12-new expense screen shows 14-expense categories shows 15-notify shows with [Expense_Category_Value] and [Expense_Value] ,pie number equal to [Pie_Caption_Value], balance number equal to Balance_Value and expence category persentage equal to [Persentage _value] 16-balance detail shows with [Balance_Value] and [Income_value] and [Income Note_Value] and [Expense_Category_Value] and [Expense_Value] and [Expense_note_Value] and [Expense_Category_Value] and [Balance_Value] 16-validate results within widgets |

## _Test Automation_
My proposed solution would be a UI test implemented by Appium or Webdriver.io but unfortunately due to my current employment, I don't have time to implement my solution.

