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

Later I added two new tables, ```user``` and ```organization```. I had to also update HtmContext.cs file and register these two tables ther too, otherwise they only be known as class. 
To update the database based on the code in asp.net, I should first run "dotnet ef migrations add <theName>". this command makes a migration file with DateTime stamp + <theName>. (Remember that you need to delete this file after your migration is done.) Then I had to run the command ```dotnet ef database update```. This will create the new tables in the database.

### Backend
For ASP.NET backened, I used https://freeasphosting.net/. I just learned that they offer free database hosting too! I had to right click on the project, choose "publish" and choose folder as publish profile. then the conetet of that folder should be zipped and uploaded on the hosting website and unzipped. Just this!

So now I have my database and backend hosted for feee. Go F*** your self Microsoft. I am angry at Microsoft because I did not truely ever got the chance to use Azure free 12 months. But I understand Microsoft trying to closing the possebility of abusing their system. 

## 8.Authentication
To implement authentication, two tables user and organization is implemented. also a procedure is added to MySQL server,
```
CREATE DEFINER=`root`@`localhost` PROCEDURE `user_Register`(IN iFirstName char(100), IN iLastName char(100), IN iPassword char(100), IN iOrganizationId int, IN iUsername char(100))
BEGIN
	INSERT INTO user (FirstName, LastName, Password, OrganizationId, Username) values (iFirstName, iLastName, iPassword, iOrganizationId, iUsername);
END
```

###Update1:
It turned out that I do not need to make these tables here in my app. Using Azure AD B2C, it take care of all of the user credentioals. So far, I have follwed this tutorial (to section 2.5) and have added both my backend and front end to the AD tenent on Azure.
https://learn.microsoft.com/en-us/azure/active-directory-b2c/configure-authentication-sample-react-spa-app
Currently, the user will be redirected to the page ```api/Register``` of my bacakend after the authentication. The scopes are defined, Read, Write, and Export. The problem at the moment is that I cannot read the authorization token from the get request header. It is most likely because the header is not there.
I just printed all of the header contents and now I am sure there is nothing in the header with the key "Authorization". The get request is sent to the server in the form of 
```
// https://localhost:7085/api/Register?code=eyJraW....
```
I suspect that the token is hiden in that!

### Update2:
I finally decided to write my authentication by myself to avoid all of the complication that is followed by Active Directory. So, now I am using these tables: User, Organization and Roles. The user credentioals must be sent to the server in the header of the get request. To have a Get request that includes header, I faced a problem.
The Dotnet server was sending CORS error while header was included in the Get request. So I had to add the following lines to Program.cs file.

```
app.addcors()  //line 18
```
and in line 26 to 30 I added these commands.
```
(x => x
                .AllowAnyMethod()
                .AllowAnyHeader()
                .SetIsOriginAllowed(origin => true) // allow any origin
                .AllowCredentials()); // allow credentials

```
I learned this solution from this page: https://jasonwatmore.com/post/2020/05/20/aspnet-core-api-allow-cors-requests-from-any-origin-and-with-credentials 

### Authorization based on tokn
This video very well described JWT. Now I know I need it. https://youtu.be/7Q17ubqLfaM?si. 
Now I should follow this to implement the JWT: https://medium.com/bina-nusantara-it-division/implementing-jwt-in-asp-net-core-6-0-web-api-c-3a396fa8cfeb#:~:text=If%20you%20are%20using%20postman,NET%20Core%206.0%20Web%20API.
Followed the page and I could implement the JWT authentication. Now ```API/risks``` is protected and to see the list of the risks, the get request header must have a field with the name ```Authorization``` and the value of ``` Bearer <token>````.

## 9. More work around the model
I added a new model ````Project``` and user, risk, RCMs, and organizations are connected to this entity.

## 10.Deploy to Heroku, using docker
I had a problem with FreeAspHosting.net So I decided to find another free host. Apparantly using Docker, one can deploy the app on Heroku.
I used this guide to make the docker image and deploy it on Heroku.
https://faun.pub/deploy-dotnet-core-api-docker-container-with-mysql-on-heroku-ed387eab4222
However, I had to change a couple of things.
First line of the Dockerfile should be ```FROM mcr.microsoft.com/dotnet/sdk:6.0``` because I am using dotnet 6.
The "D" in the name of the file ```Dockerfile``` must be capital.

### 10.1 It did not work
The turorial did not work. So I had to start Docker from the scratch. I am watching this to learn Docker A-Z
```https://www.youtube.com/watch?v=pTFZFxd4hOI```

First thing first, I had to turn on Hyper-V and Containers in Settings/Optional Features/More Windows Features.