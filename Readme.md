# About this project
This is my first serious dotnet project.
In this project, I am going to make an app for making HTM. For more information about HTM contact me.
# This Readme file
In this file, I am going to write my notes, for different parts of the project.
The first serious challenge that I faced was to connect my backend to my currently made database. Since it is easier for me to make the database in the SQL environment, I have made the database there and I had a hard time to connect to it. I have used MySQL as my server.
# My Challenges
## 1. Scaffolding from a current MySQL database
To connect to the database, I had to follow the instruction in this page.
https://dev.mysql.com/doc/connector-net/en/connector-net-entityframework-core-scaffold-example.html

For the connection string, I had to keep this format.
```
"server=127.0.0.1;uid=root;pwd=**********;database=HTM;port=3300"
```
Therefore the whole command that I should execute at step 5 of the above tutorial was:
```
 dotnet ef dbcontext scaffold "server=127.0.0.1;uid=root;pwd=*********;database=HTM;port=3300" MySql.EntityFrameworkCore -o HTM -f
 ```


After running this script, I could see that the folder HTM is made in the project tree.
 
During scaffolding I had a minor issue, I could not remember my MySQL password. I was following this instruction but it was not effective.
https://dev.mysql.com/doc/refman/8.0/en/resetting-permissions.html
The problem was that I was runnign Command Prompt with normal previlage. I had to run it in administrator mode to get it to work.

After running this script, I could see that the folder HTM is made in the project tree.

## 2. Making APIs
I could make my first API for the risk table. I first had to right click on the folder "Controllers" > "Add" > "New Scaffolded Item". In the new window, I selected "API controller with action, using Entity Framework" and clicked on the button add in the lower rigtht corner.
In the next window, in the first line, I had to choose the table, (in this case Risk), for the second line, I had only one datacontext, which is HTMContext.cs and in the third line I had to give a name to the controller. By default it was offering RiskController.
After doing all these the controller was generated.
From reverse engineering step, (scaffolding the database, challenge Nu.1) the HtmContext.cs already have the ConnectionString. I did not change anything in that file.
After running the code, I got two errors. 
### A. Error for identifying the database, Program.cs
From the template, the database was referring to the old datacontext of the template. I had to change that. So, initializing db updated. using var 
```
db = new HTMbackend.HTM.HtmContext();
```
### B. The interface for the datacontext, Program.cs
I was getting a compile error, saying that while making the interface for the datacontext, it has no idea what type of data the constructor of this object should have. Therefore I had to add this line:
```
builder.Services.AddScoped<HTMbackend.HTM.HtmContext, HTMbackend.HTM.HtmContext>();
```