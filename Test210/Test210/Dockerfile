FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 5000
EXPOSE 5001

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
#COPY Test210/Test210.csproj Test210/
#RUN dotnet restore Test210/Test210.csproj
COPY *.csproj ./
RUN dotnet restore
COPY . ./
WORKDIR /src
RUN dotnet build Test210.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish Test210.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Test210.dll"]
