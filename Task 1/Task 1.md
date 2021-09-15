
## Task 1 - Exploratory Testing

Dillinger is currently extended with the following plugins.
Instructions on how to use them in your own application are linked below.

| Charter Name: | CH-01-Permission                                              |
| ------        | ------                                                        |
| Priority      | High                                                          |
| Time          | Short(10-15min)                                               |
| Explore       | Items requering purchase                                      |
| with          | a normal(default) user                                        |
| To discover   | if user gain access to pro features without purchase an ultimate account  |
| Findings      |  |

| Charter Name: | CH-02-Numeric Type Attack                         |
| ------        | ------                                            |
| Priority      | High                                              |
| Time          | Short(10-15min)                                   |
| Explore       | All numeric inputs (enter-search-update)          |
| with          | negative-floats-comma-dot-blank value             |
| To discover   | possibility of any break in a numeric data type   |
| Findings      | **Issue#1**: In expense screen,last mathematical operation will apply on the input value eventhough = operation haven't pressed.**Issue#2**: In expense screen,Choosing category before entering the amount- blinks the numeric data and does not give a proper message to user to fill amount first. |

| Charter Name: | CH-03-String Type Attack                                  |
| ------        | ------                                                    |
| Priority      | High                                                      |
| Time          | Short(10-15min)                                           |
| Explore       | All string inputs (enter-search-update)                   |
| with          | long-Special characters-single/multiple space-blank value |
| To discover   | possibility of any break in a string data type            |
| Findings      |**Issue#1**: In expense screen,note input allow special characters without showing error message **Issue#2**: In expense screen,numeric input will lock in case of entering special characters in Note **Issue#3**: In expense screen,Note input allow multiple spaces **Issue#4**: In expense screen,after typing a note by tapping on any point of screen else than Note, Onscreen character keyboard should closed and "Add note" input should be inactive.**Issue#5**:on main screen if you enter C charachter in search, it couldnt find Car item |


| Charter Name: | CH-04-Follow the Data                         |
| ------        | ------                                        |
| Priority      | High                                          |
| Time          | Short(10-15min)                               |
| Explore       | expense and incomes                           |
| with          | Perform a sequence of actions(create-update-delete) involving data|
| To discover   | interactions between expense and incomes     |
| Findings      | **Issue#1**:There is no confirmation message asked from user While deleting an existing record.**Issue#2**:on main screen, the expense distribution strategy used does not indicate ratio of income used in a particular expenditure. suppose income = 2000 ; rent = 500 ; ratio should be = 500/2000 * 100 = 25% , but it shown 100%. **Issue#3**: on Balance screen, when sorting pressed, the collapse detailed will close  |


| Charter Name: | CH-05-Boundaries                                   |
| ------        | ------                                             |
| Priority      | low                                                |
| Time          | Short(10-15min)                                    |
| Explore       | categories (expences/incomes/accounts)             |
| with          | too long/short amount of items in each             |
| To discover   | possibility of a break in shapes or user interface |
| Findings      | **Issue#1**: on main screen, in case of many expenses, the expense distribution lacks some pointing Pie chart pointing error |

| Charter Name: | CH-06-Configuration                                   |
| ------        | ------                                                |
| Priority      | low                                                   |
| Time          | Short(10-15min)                                       |   
| Explore       | setting menu                                          |
| with          | Varying the variables related to configuration        |
| To discover   | problem in configuration                              |
| Findings      |**Issue#1**: locked features are not easily recognisable **Issue#2**:The amount in USD is not convertible to any other currency and digits remain same.**Issue#3**: The budget amount can not be empty once initialized.                                                                                |



## Risks to mitigate : 
 1-Security Vulnerabilities
    (https://appsweep.guardsquare.com/builds/c3a62797-8d23-4868-a65f-3bede23b2a26)
 2-User Data may be lost because there is no account created by a user to manage them.
 3-Poor User Experience :
   - Missing message and confirmation
   - Pro features should be recognisable 
   - Missing help or user manual guides
   - Inconsistencies in font style 


