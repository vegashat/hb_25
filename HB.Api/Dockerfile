# https://hub.docker.com/_/microsoft-dotnet
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /source

# copy csproj and restore as distinct layers
COPY *.sln .
COPY HB.Api/*.csproj ./HB.Api/
RUN dotnet dev-certs https --clean
RUN dotnet dev-certs https -t
RUN dotnet restore

# copy everything else and build app
COPY HB.Api/. ./HB.Api/
WORKDIR /source/HB.Api
RUN dotnet build -c release -o /app/build 
#--no-restore
RUN dotnet publish -c release -o /app --no-restore

#install curl
RUN apt-get -y update; apt-get -y install curl

# final stage/image
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app ./

EXPOSE 5400
EXPOSE 5401

ENTRYPOINT ["dotnet", "HB.Api.dll"]