FROM microsoft/dotnet:runtime

WORKDIR /app

EXPOSE 5000

COPY out .

ENTRYPOINT ["dotnet", "dotnetmvctest.dll"]
