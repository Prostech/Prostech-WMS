FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src

# Copy and restore BLL and DAL and API projects
COPY Prostech.WMS.BLL/Prostech.WMS.BLL.csproj Prostech.WMS.BLL/
COPY Prostech.WMS.DAL/Prostech.WMS.DAL.csproj Prostech.WMS.DAL/
COPY Prostech.WMS.API/Prostech.WMS.API.csproj Prostech.WMS.API/

RUN dotnet restore Prostech.WMS.BLL/Prostech.WMS.BLL.csproj
RUN dotnet restore Prostech.WMS.DAL/Prostech.WMS.DAL.csproj
RUN dotnet restore Prostech.WMS.API/Prostech.WMS.API.csproj

# Copy the entire solution
COPY . .

# Build the solution
WORKDIR /src
RUN dotnet build Prostech.WMS.API/Prostech.WMS.API.csproj -c Release -o /app/build

FROM build AS publish
RUN dotnet publish Prostech.WMS.API/Prostech.WMS.API.csproj -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Prostech.WMS.API.dll"]
