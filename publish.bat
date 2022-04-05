rmdir webapiPublish; 
dotnet publish Challenge.WebApi/Challenge.WebApi/Challenge.WebApi.csproj -f net6.0 -r win10-x64 --self-contained false -o webapiPublish
pause

rmdir botPublish
dotnet publish Challenge.WebApi/Challenge.Bot/Challenge.Bot.csproj -f net6.0 -r win10-x64 --self-contained false -o botPublish
pause