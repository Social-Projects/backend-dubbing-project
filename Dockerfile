FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY ["backend-dubbing-project/backend-dubbing-project.csproj", "backend-dubbing-project/"]
RUN dotnet restore "backend-dubbing-project/backend-dubbing-project.csproj"
COPY . .
WORKDIR /src/backend-dubbing-project
RUN dotnet ef database update InitialCreate
RUN dotnet build "backend-dubbing-project.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "backend-dubbing-project.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
RUN mkdir AudioFiles
COPY ./backend-dubbing-project/AudioFiles ./AudioFiles
COPY --from=publish /app .
COPY --from=build /src/backend-dubbing-projecttree/dubbing.db .
ENTRYPOINT ["dotnet", "backend-dubbing-project.dll"]