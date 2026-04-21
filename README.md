# DIO - Trilha .NET - API e Entity Framework
www.dio.me

## Desafio de projeto
Para este desafio, você precisará usar seus conhecimentos adquiridos no módulo de API e Entity Framework, da trilha .NET da DIO.

## Contexto
Você precisa construir um sistema gerenciador de tarefas, onde você poderá cadastrar uma lista de tarefas que permitirá organizar melhor a sua rotina.

Essa lista de tarefas precisa ter um CRUD, ou seja, deverá permitir a você obter os registros, criar, salvar e deletar esses registros.

A sua aplicação deverá ser do tipo Web API ou MVC, fique a vontade para implementar a solução que achar mais adequado.

A sua classe principal, a classe de tarefa, deve ser a seguinte:

![Diagrama da classe Tarefa](diagrama.png)

Não se esqueça de gerar a sua migration para atualização no banco de dados.

## Métodos esperados
É esperado que você crie o seus métodos conforme a seguir:


**Swagger**


![Métodos Swagger](swagger.png)


**Endpoints**


| Verbo  | Endpoint                | Parâmetro | Body          |
|--------|-------------------------|-----------|---------------|
| GET    | /Tarefa/{id}            | id        | N/A           |
| PUT    | /Tarefa/{id}            | id        | Schema Tarefa |
| DELETE | /Tarefa/{id}            | id        | N/A           |
| GET    | /Tarefa/ObterTodos      | N/A       | N/A           |
| GET    | /Tarefa/ObterPorTitulo  | titulo    | N/A           |
| GET    | /Tarefa/ObterPorData    | data      | N/A           |
| GET    | /Tarefa/ObterPorStatus  | status    | N/A           |
| POST   | /Tarefa                 | N/A       | Schema Tarefa |

Esse é o schema (model) de Tarefa, utilizado para passar para os métodos que exigirem

```json
{
  "id": 0,
  "titulo": "string",
  "descricao": "string",
  "data": "2022-06-08T01:31:07.056Z",
  "status": "Pendente"
}
```


## Solução
O código está pela metade, e você deverá dar continuidade obedecendo as regras descritas acima, para que no final, tenhamos um programa funcional. Procure pela palavra comentada "TODO" no código, em seguida, implemente conforme as regras acima.

## Stack utilizada

- .NET 8 (ASP.NET Core Web API)
- Entity Framework Core 8 (SQL Server)
- Swashbuckle (Swagger UI)
- Deploy no Azure App Service + Azure SQL Database

## Como rodar localmente

Pré-requisitos: SDK do .NET 8 instalado.

```powershell
dotnet restore
dotnet build
```

Antes do primeiro `dotnet run`, configure a connection string via User Secrets (não versionada):

```powershell
dotnet user-secrets init
dotnet user-secrets set "ConnectionStrings:ConexaoPadrao" "Server=SEU_SERVIDOR,1433;Initial Catalog=Organizador;User ID=SEU_USUARIO;Password=SUA_SENHA;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"
```

Gere a migration inicial e atualize o banco:

```powershell
dotnet tool install --global dotnet-ef
dotnet ef migrations add InitialCreate
dotnet ef database update
```

Execute a aplicação:

```powershell
dotnet run
```

O Swagger estará disponível em `https://localhost:<porta>/swagger`.

## Segurança de credenciais

- Nunca suba `User ID` e `Password` reais em `appsettings.json`.
- Em desenvolvimento: use `dotnet user-secrets` (os valores ficam fora do repositório, em `%APPDATA%\Microsoft\UserSecrets`).
- Em produção (Azure App Service), defina em **Configuration > Application settings**:
  - `ConnectionStrings__ConexaoPadrao` com a string real da Azure SQL Database.
- O `appsettings.json` neste repositório contém apenas placeholders (`SEU_SERVIDOR`, `SEU_USUARIO`, `SUA_SENHA`).

## Deploy no Azure

Recomendações:

- SQL Server (logical server): pode reaproveitar um existente.
- Azure SQL Database: crie uma nova database (ex.: `Organizador`) no mesmo logical server.
- App Service: crie um novo Web App (ex.: `aulaorganizadorapi`) apontando runtime `.NET 8 (LTS)`.
- App Service Plan: pode reaproveitar um já existente para reduzir custo.
- Connection string: configure em **Application settings** a chave `ConnectionStrings__ConexaoPadrao`.
- O `Program.cs` executa `Database.Migrate()` no startup, então as migrations presentes no repositório serão aplicadas automaticamente à base Azure SQL durante o primeiro boot.

Publicação:

- Via Visual Studio: botão direito no projeto > Publish > Azure App Service.
- Via CLI: `dotnet publish -c Release` + Zip Deploy para o App Service.
- Via GitHub Actions: workflow de build/publish direcionando ao App Service.