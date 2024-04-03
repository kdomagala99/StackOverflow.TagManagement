FROM mcr.microsoft.com/dotnet/sdk:8.0-jammy AS build
WORKDIR /app

COPY . ./
RUN dotnet restore
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:8.0-jammy-chiseled AS runtime
WORKDIR /app

ENV ASPNETCORE_URLS=http://+:5000
EXPOSE 5000

COPY --from=build /app/out .
ENTRYPOINT ["dotnet", "StackOverflow.TagManagement.Api.dll"]