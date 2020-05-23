FROM mcr.microsoft.com/dotnet/core/runtime:3.1-buster-slim AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["Presentation/MakiseSharp.Infrastructure.csproj", "Presentation/"]
COPY ["Application/MakiseSharp.Application.csproj", "Application/"]
COPY ["Domain/MakiseSharp.Domain.csproj", "Domain/"]
RUN dotnet restore "Presentation/MakiseSharp.Infrastructure.csproj"
COPY . .
WORKDIR "/src/Presentation"
RUN dotnet build "MakiseSharp.Infrastructure.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "MakiseSharp.Infrastructure.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "MakiseSharp.Infrastructure.dll"]