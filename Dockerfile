FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY ["Web/Web.csproj", "Web/"]
RUN dotnet restore "Web/Web.csproj"
COPY . .
WORKDIR /src/Web
RUN dotnet ef migrations add InitialCreate
RUN dotnet ef database update InitialCreate
RUN dotnet build "Web.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "Web.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
RUN mkdir AudioFiles
COPY ./Web/AudioFiles ./AudioFiles
RUN mkdir AudioFiles
COPY --from=publish /app .
COPY --from=build /src/Web/dubbing.db .
ENTRYPOINT ["dotnet", "Web.dll"]