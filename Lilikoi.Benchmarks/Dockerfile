FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Lilikoi.Benchmarks/Lilikoi.Benchmarks.csproj", "Lilikoi.Benchmarks/"]
RUN dotnet restore "Lilikoi.Benchmarks/Lilikoi.Benchmarks.csproj"
COPY . .
WORKDIR "/src/Lilikoi.Benchmarks"
RUN dotnet build "Lilikoi.Benchmarks.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Lilikoi.Benchmarks.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Lilikoi.Benchmarks.dll"]
