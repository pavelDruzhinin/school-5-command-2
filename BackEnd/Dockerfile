FROM microsoft/dotnet:2.2-aspnetcore-runtime
EXPOSE 80/tcp

COPY /bin/Release/netcoreapp2.2/linux-x64/publish .

ENTRYPOINT ["dotnet", "ChatsConstructor.WebApi.dll"]
