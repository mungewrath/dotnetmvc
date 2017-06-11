FROM microsoft/dotnet:runtime

# Workaround to prevent weird errors when Docker launches in the root dir
WORKDIR /app

EXPOSE 5000

COPY out .

ENTRYPOINT ["dotnet", "dotnetmvctest.dll"]
