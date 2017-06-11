#!/bin/bash

./generateGitInfo.sh
dotnet restore
dotnet publish -c Release -o out

docker build -t dotnetmvc .
