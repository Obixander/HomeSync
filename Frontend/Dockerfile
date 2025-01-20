FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

COPY *.csproj ./
RUN dotnet restore

COPY . ./
RUN dotnet publish -c Release -o out

FROM nginx:alpine
WORKDIR /usr/share/nginx/html

COPY --from=build /app/out/wwwroot .

EXPOSE 80