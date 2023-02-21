FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["Microsoft.Orleans.Course/Microsoft.Orleans.Course.csproj", "Microsoft.Orleans.Course/"]
RUN dotnet restore "Microsoft.Orleans.Course/Microsoft.Orleans.Course.csproj"
COPY . .
WORKDIR "/src/Microsoft.Orleans.Course"
RUN dotnet build "Microsoft.Orleans.Course.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Microsoft.Orleans.Course.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Microsoft.Orleans.Course.dll"]
