Habit Tracker Project



How to use the application:

This project used .NET 10.0

The following libraries are needed to run the project: Spectre Console, Microsoft.Data.Sqlite.

To install go to the terminal and the file where the project is based and type

dotnet add package Spectre.Console

dotnet add package Microsoft.Data.Sqlite



Press the run button to start the console application. You will be prompted to select an option from a menu. These options include: 'View Habits', 'AddHabit', 'UpdateHabit', 'DeleteHabit' and 'Exit'.



On selecting 'ViewHabits' you will see a table that includes 100 habits. Each habit has an 'Id', a 'Date', a 'Name', a 'Unit' and a 'Quantity'. To return to the main menu press enter as prompted.



On selecting 'AddHabit' you will be prompted to enter the date of the habit(this will need to be in the correct format shown "yyyy-MM-dd"). If you do not enter anything and press Enter the current date will be entered by default. You will then be prompted to enter the name of the habit you wish to track. This cannot be empty and an error will show if Enter is pressed before text is entered. After this you will be prompted to select a unit of measurement from a predefined list. These include 'Glasses', 'Times', 'Pages' and 'Kilometers'. Press up and down on the arrow keys to cycle through the options and press Enter when you with to select one. You will then be prompted to Enter the quantity that corresponds to the unit you selected. Enter a number and press enter. You cannot enter a negative number. If you press Enter without entering a number 0 will be entered by default. Finally you will be prompted to confirm that you would like to enter the habit into the tracker. Press 'y' or 'n' and then press Enter. Attempting to enter any other value will result in an error and you will be prompted to re-enter a correct input. If you input 'y' you will receive a message confirming that the entry was successful(or an error message if it was not), else if you input 'n' you will be prompted to press Enter to return to the main menu.



On selecting 'UpdateHabit' the table will be shown and you will be prompted to enter the Id of the habit you wish to update. This cannot be null, and must match the id of an entry in the table. If Enter is pressed without entering a number, a negative number or a number that does not match an Id in the table an error will be displayed. The next prompts follows the order from 'AddHabit'. After the information is entered, you will be prompted to confirm that you would like to add this entry to the table. If you select 'y', a message will appear confirming that it was successfully updated(or an error if not) and you will be prompted to return to the main menu by pressing Enter. If you select 'n' you will be prompted to return to the main menu.



On selecting 'DeleteHabit' the table will be shown and you will be prompted to enter the Id of the habit you wish to delete. This cannot be null, and must match the id of an entry in the table. If Enter is pressed without entering a number, a negative number or a number that does not match an Id in the table an error will be displayed. You will then be prompted to confirm if you wish to delete the selected entry. If you select 'y', a message will appear confirming that it was successfully deleted(or an error if not) and then you will be prompted to return to the main menu by pressing Enter. If you select 'n', the habit is not deleted from the table and you will be prompted to return to the main menu.



On selecting 'Exit' the application will close completely.



Process of creating the application:

* I created a new project file in Visual Studio.
* The first thing I decided to implement was the selection menu, which is the first thing the user sees and controls the execution flow of the app. This is done using a simple switch statement. The UI prompt for this was handled inside the UserInterface file inside the UI folder in order to separate the project into different files each with their own separate responsibilities. This keeps code clean, reusable and more readable.
* Next I created a method to connect to the database(inside the Program.cs file for now). This uses ADO.Net commands to create a table called DrinkingWater. Each row has an Id, a date, and a quantity.
* I then created a new file inside a Helpers folder called DatabaseOperations. This file connects to the database using a string containing its source. It contains a method for creating a database if one does not exist and calling a method from the TableSeeder file. This file contains code that is responsible for creating 100 entries in the database. It creates a random date as well as a random quantity for amount of water glasses the user has drunk. This file is only used if a table with entries does not exist. The GetData reads the database(if one exists) and returns a list of habits(objects based on the Habit class) created while the database is being read.
* I created a new file inside the UI folder called TablePrinter. This is responsible for printing a table based on the table inside the database. A list of habits is created using the GetData method from DatabaseOperations and for every habit in that list a row is created containing its Id, date and quantity. A return to menu prompt is created in a method as this is used repeatedly.
* I then implemented the AddHabit method inside the DatabaseOperations file. This connects to the database and inserts data into the DrinkingWater table. This method takes a date and quantity parameter(the id is generated automatically). Inside the UserInterface file, I created a new method called AddHabitUi. This method prompts the user for a date and a quantity and returns those as a datatype. The date and quantity returned from this method is passed to the AddHabit method inside DatabaseOperations.
* Next I added a string parameter to the bool method UserVerifyData in my UserInterface file. This now takes a string as the prompt message and returns true or false based on if the user selects 'y' or 'n'.
* I created a method called DeleteHabitUi in my UserInterface file which prompts the user for an Id, validates that it is valid input and returns it. In DatabaseOperations, a new method is called DeleteHabit takes this Id as a parameter and using SQL commands. The user is asked to verify that they wish to delete the Habit with the Id they entered in the Program.cs file(as with all other operations).
* I then created a method called UpdateHabitUi which returns an an id, a date, and a quantity. It creates a list of habits by calling the GetData method from DatabaseOperations. It prompts the user for an Id, a date, and quantity and validates each to ensure that they are valid input. In DatabaseOperations I created a method called UpdateHabit which takes these values as parameters. It executes SQL commands to update the row with the Id passed as a parameter. The Program.cs file asks the user to verify that they would like to update the habit corresponding to the id.
* I corrected a minor error with the TableSeeder in which 101 rows were created instead of 100.
* Next I decided to clean up Program.cs by creating an Enum(StateEnum) which contains the operations that the user can select on the applications main menu. This reduces the use of magic strings and as a result errors caused by misspelling.
* To further clean up my code I decided it would be more clean to pass a Habit instance to my DatabaseOperation methods(AddHabit and UpdateHabit) instead of variables that loosely connect to Habit properties.
* I ensured that my DatabaseOperation methdos correctly formatted dates to comply with the format of the database, preventing errors. Another way of handling errors I created was to add a universal Try-Catch statement in my program.cs file. If any unhandled error occurs that would crash my program this acts as a shield to close the application gracefully.
* Instead of creating lists of habits inside my UserInterface methods I pass a list of habits to them as parameters. This keeps the code clean and upholds single responsibility. I also refactored some of the validation handling to ensure that all data being passed to the DatabaseOperations methdos were in the correct format and were valid input.
* I tried implementing try catch statements over every code block in the swith statement inside Program.cs however this made things very untity.
* I decided to create a new file called HabitController inside a controllers folder. This took over the responsibility I was previously assigning to Program.cs of both handling exceptions and controlling the flow of UI interaction to DatabaseOperations. Instead Program.cs has been tidied up and calls a method from this file inside each corresponding case in the switch statement.
* Next I decided to take the challenge of allowing the user to decide what habit they would like to track. This involved refactoring the DatabaseOperations methods to create a new table called Habits(replacing DrinkingWater). I also added two new properties to the Habit class called Name and Unit. I added a new prompt to my UserInterface methods to prompt the user for both of these values. Name is a string that a user can input. It can be anything. Unit can be one of a selection of predefined values coming from an enum called UnitEnum. The SeedData method also needed to be refactored heavily. I added an array of anonymous objects which pairs the Name and Unit values and then randomised to add 100 rows of Habits.
* Finally I added some styling to the prompts, table and error messages using Spectre Console to make the app look more visually appealing and to make important information stand out. I also fixed some small remaining issues.



Project Experience:

For the most part I found this project quite straight forward but it still challenged me in a few ways.

The thing I most struggled with was deciding how to structure my project and separate my code into separate files each fulfilling different tasks. I was mostly able to remove spectre console from my Program.cs however it is still used to print out errors.

Something that became more clear the more I used it was ADO.NET. I learned that their was multiple different ways of opening and closing connections and ended up using the method that keeps the code as clean as possible.

I feel as though I have gained more experience in creating reusable code as opposed to repeating the same lines over and over in similar methods as well as learning a little more of how to structure my projects.





