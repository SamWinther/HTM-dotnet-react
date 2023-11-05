FROM mcr.microsoft.com/dotnet/sdk:6.0
WORKDIR /app
COPY /app /app
CMD ASPNETCORE_URLS=http://*:$PORT dotnet DomainTeamWebsite.dll
ENTRYPOINT ["dotnet", "HTMbackend.dll"]