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
"server=127.0.0.1;uid=root;pwd=**********;database=HTM;port=3300"

Therefore the whole command that I should execute at step 5 of the above tutorial was:
 dotnet ef dbcontext scaffold "server=127.0.0.1;uid=root;pwd=*********;database=HTM;port=3300" MySql.EntityFrameworkCore -o HTM -f


After running this script, I could see that the folder HTM is made in the project tree.
