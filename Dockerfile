# Estágio 1: Build da Aplicação
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /app

# Copia o arquivo do projeto e restaura as dependências primeiro (cache para acelerar o build da imagem).
COPY *.csproj .
RUN dotnet restore

# Copia o resto do código-fonte.
COPY . .

# Publica a aplicação em modo Release
RUN dotnet publish -c Release -o /app/publish

# Estágio 2: Imagem Final
FROM mcr.microsoft.com/dotnet/aspnet:9.0
WORKDIR /app

# Copia apenas os arquivos publicados do estágio de build.
COPY --from=build /app/publish .

# Define o entrypoint para rodar a aplicação.
ENTRYPOINT ["dotnet", "TrilhaApiDesafio.dll"]
