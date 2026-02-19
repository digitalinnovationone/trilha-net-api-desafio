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

## Resumo do Guerreiro
🚀 Desafio: API de Sistema Gerenciador de Tarefas
Esta API foi desenvolvida como parte de um desafio técnico para praticar conceitos de CRUD com ASP.NET Core 6.0, Entity Framework Core e SQL Server.

🛠️ Tecnologias Utilizadas
.NET 6.0 SDK

Entity Framework Core 6.0

SQL Server / LocalDB

Swagger (OpenAPI) para documentação e testes

⚠️ Guia de Sobrevivência: O Conflito de Versões (Feature Fix)
Se você estiver tentando rodar este projeto em uma máquina que possui o SDK do .NET 10 (ou superior), poderá encontrar o erro:

A compatible .NET SDK was not found. Requested SDK version: 6.0.x

Como resolvi esse problema:
Para que o projeto compile e as Migrations funcionem corretamente, apliquei os seguintes passos:

Versões de Pacotes: Garanti que todos os pacotes do EF Core no arquivo .csproj estivessem travados na versão 6.0.0.

Global.json: Foi necessário criar um arquivo global.json na raiz do projeto para instruir o CLI do .NET a utilizar o SDK 6.

Runtime: Além do SDK, é fundamental ter o ASP.NET Core Runtime 6.0 instalado para que o comando dotnet ef database update consiga executar as ferramentas de design.

🏗️ Como Rodar o Projeto
Clonar o repositório:

Bash
git clone https://github.com/Gisele-lab/trilha-net-api-desafio.git
Configurar o Banco de Dados:
No arquivo appsettings.json, ajuste a sua Connection String:

JSON
"ConexaoPadrao": "Server=localhost\\SQLEXPRESS;Database=TarefasDB;Trusted_Connection=True;TrustServerCertificate=True;"
Aplicar Migrations:

Bash
dotnet ef database update
Executar a API:

Bash
dotnet watch run
Acesse: https://localhost:7295/swagger

📝 Funcionalidades (Endpoints)
POST /Tarefa: Cria uma nova tarefa.

GET /Tarefa/{id}: Busca uma tarefa por ID.

GET /Tarefa/ObterTodos: Lista todas as tarefas do banco.

PUT /Tarefa/{id}: Atualiza os dados de uma tarefa existente.

DELETE /Tarefa/{id}: Remove uma tarefa do banco.