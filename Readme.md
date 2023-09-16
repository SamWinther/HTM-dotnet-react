# About this project
This is my first serious dotnet project.
In this project, I am going to make an app for making HTM. For more information about HTM contact me.
# This Readme file
In this file, I am going to write my notes, for different parts of the project.
The notes are usually described as a challnge that I have faced, something that I wanted to deploy but I did not know how. After I solve the puzzle, I make a note to describe the question that U have faced, and the solution that I could find.
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

## 3. Moving the database information to the AppSettings.json file
I could finally move the database credentials to the appsettings.json file. The implementation is in file HtmContext.cs.
Here, I first had to add a property _configuration with the type of interface IConfiguration
```
protected readonly IConfiguration _configuration;
```
Then in the constructor, I had to initialize it. The constructor method is overloaded in this class. (different constructors exist for different cases of initialization)
I had to add two overload constructor.
case1, when only config file is present.
```
    public HtmContext(IConfiguration configuration) : this()
    {
        _configuration = configuration;
    }
```
case2, when there is an option parameter together with the configuration.
```
    public HtmContext(DbContextOptions<HtmContext> options, IConfiguration configuration)
        : this(options)
    {
        _configuration = configuration;
    }
```

As you can see, I have repeated the section ```_configuration = configuration;``` and have violated the DRY principle but I was not sure how can I handle such a case.
And at the end, I had to add ```using System.Configuration;``` at the header.

## 4. Making a custom query based on a condition or from a joint table
In MySQL, it is easy to make a query like this.
```
SELECT * FROM risk WHERE scenario like "%user%";
```
I wanted to make a similar query in one of my API controllers. The problem was type conversion. I had to make sure the return type of the family is right. So when I define the function, I should state that this function returns a list of Risk.
And in order to convert the query results to a list, I needed to use ```ToList()``` at the end of the query. Therefore, the whole command for getting the data is:
```var riskWithWord2 = _context.Risks.Where(r=>r.Scenario.Contains("user")).ToList();```
For the next practice, I will try to get queries from a joint table.

## 5. Joining two tables and driving results from the joint table
This was an annoying challenge. I could use Linq query to join two tables of RCM and RCMtype. The annoying part was to find a format for the output because now the result is not neither RCM nor RcmType. It is a combination of both.
The solution was to define a new class, RcmDTO. DTO stands for Data Transfer Object. This object is used for combining the data from the two table and removing unnacessary parts like RCMid.

## 6. Deploy (migrate) the app MySQL database on Azure
The database is transferred to Azure. I used Azure mySQL service. The currnet data from the currnet database is migrated to the Azure mySQL server using mysqldump. To conduct this transfer, I had to:
1. Make sure I am running CMD in administrative mode.
2. To use mysqldump, I should be in the folder, C:\Program Files\MySQL\MySQL Server 8.0\bin>
3. To backup the database, I needed to use the attrebute -P (capital case) to indentify the port. The final format of the command was like this.

```
mysqldump -P 3300 -u root -p***************** htm > htmbackup.sql
```

4. Before deploing the database from the backup to Azure, I needed to make a database on Azure. I needed the name of the database to include in the command. The name of the database on Azure is htm_dev.
At the end, the final format of the command that is used to transfer the database to Azure was like this.

```
mysql -h htmmysqlserver.mysql.database.azure.com -u [username] -p[***password****] htm_dev < htmbackup.sql
```

5. The connection String in the appsettings.json should be updated.

```
"ConnectionString": {
    "myDB1": "server=127.0.0.1;uid=root;pwd=****p assword****;database=HTM;port=3300",
    "azure": "server=??????.mysql.database.azure.com;uid=**username**;pwd=***password*****;database=htm_dev;port=3306"
```

## 7 Deploy (migrate) the app MySQL database on Azure
I followed this tutorial: https://learn.microsoft.com/en-us/aspnet/core/tutorials/publish-to-azure-api-management-using-vs?view=aspnetcore-7.0
Therefore, First step was to move ```app.UseSwagger();``` out of the if condition in program.cs.
First I skipped changing ```[Route("[controller]")]``` to ```[Route("/")]``` cause I wanted to know what difference it can cause. It seems that it causes no problem.
I published the app, according to the instruction that was given in the tutorial. The only hick up was the error massage regarding Swagger. at the end I noticed that I have to comment all of the lines that includes Swagger in program.cs. In this situation no error will be shown on publish.
After successful publish the app was tested. All works, I can get the results from the API. I only need to copy the "Default domain" from the web app (```****webAppServiceName****.azurewebsites.net```) and add for example ```/api/risks``` to get the data.
The only issue at the current publish is that the API is not authenticated. Anyone can access the API. I need to secure it.

## Microsoft is expensive for an unprofessional developer
### Database
When I transferred my database and backend to Azure, I believed I am on the first year free account plan, but soon I saw I am being charged. It turned out that earlier I was registered as a user on Microsoft. Azure free first year offer is only for new users.
So I had to find a new hosting service. Heruko has stopped offering free MySQL server but freemysqlhosting.net kindly offers free mysql hosting. There I made a new database. To make the tables, I first added 

```db.Database.EnsureCreated();```

to ```program.cs```. I also used the command ```dotnet ef database update``` a couple of time. It seems that ```EnsureCreated()``` was throwing an error and I had to comment that line. but then it seems that, SOMEHOW, after turning that line to comment, and runnign the app again, the tables were made in the new database.
### Backend
For ASP.NET backened, I used https://freeasphosting.net/. I just learned that they offer free database hosting too! I had to right click on the project, choose "publish" and choose folder as publish profile. then the conetet of that folder should be zipped and uploaded on the hosting website and unzipped. Just this!

So now I have my database and backend hosted for feee. Go F*** your self Microsoft. I am angry at Microsoft because I did not truely ever got the chance to use Azure free 12 months. But I understand Microsoft trying to closing the possebility of abusing their system. 