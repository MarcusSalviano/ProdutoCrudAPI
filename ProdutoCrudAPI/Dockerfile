# syntax=docker/dockerfile:1

ARG DOTNET_RUNTIME=mcr.microsoft.com/dotnet/aspnet:8.0
ARG DOTNET_SDK=mcr.microsoft.com/dotnet/sdk:8.0

# Build stage: .NET SDK for building the application
FROM ${DOTNET_SDK} AS build

COPY . /source
WORKDIR /source

# Restore dependencies
RUN dotnet restore

# Build the application
RUN dotnet publish --runtime linux-x64 --self-contained false -o /app

################################################################################

# Runtime stage: ASP.NET Core runtime for running the application
FROM ${DOTNET_RUNTIME} AS final
WORKDIR /app

# Copie o aplicativo do estágio de build
COPY --from=build /app .

# Copie as ferramentas dotnet-ef do estágio de build para o runtime
COPY --from=build /root/.dotnet /root/.dotnet

ENTRYPOINT ["dotnet", "ProdutoCrudAPI.dll"]
