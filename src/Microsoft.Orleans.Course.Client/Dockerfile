FROM mcr.microsoft.com/dotnet/runtime:8.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["src/Microsoft.Orleans.Course.Client/Microsoft.Orleans.Course.Client.csproj", "src/Microsoft.Orleans.Course.Client/"]
RUN dotnet restore "src/Microsoft.Orleans.Course.Client/Microsoft.Orleans.Course.Client.csproj"
COPY . .
WORKDIR "/src/src/Microsoft.Orleans.Course.Client"
RUN dotnet build "Microsoft.Orleans.Course.Client.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Microsoft.Orleans.Course.Client.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Microsoft.Orleans.Course.Client.dll"]
