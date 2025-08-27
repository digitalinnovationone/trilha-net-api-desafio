# API de Gerenciamento de Tarefas com Deploy na Nuvem Azure
![.NET](https://img.shields.io/badge/.NET-512BD4?style=for-the-badge&logo=dotnet&logoColor=white)
![C#](https://img.shields.io/badge/C%23-239120?style=for-the-badge&logo=c-sharp&logoColor=white)
![PostgreSQL](https://img.shields.io/badge/PostgreSQL-4169E1?style=for-the-badge&logo=postgresql&logoColor=white)
![Swagger](https://img.shields.io/badge/Swagger-85EA2D?style=for-the-badge&logo=swagger&logoColor=black)
![Docker](https://img.shields.io/badge/Docker-2496ED?style=for-the-badge&logo=docker&logoColor=white)
![CI/CD](https://img.shields.io/badge/CI%2FCD-Automated-blue?style=for-the-badge&logo=githubactions&logoColor=white)
![Azure ACA](https://img.shields.io/badge/Deploy-Azure%20Container%20Apps-0078D4?style=for-the-badge&logo=azure&logoColor=white)


Este projeto é o motor por trás de um aplicativo de gerenciamento de tarefas. Trata-se de uma API de back-end robusta que oferece um CRUD completo, permitindo que uma interface de usuário (web ou mobile) possa facilmente criar, listar, atualizar e deletar tarefas. O sistema também inclui um controle de status para cada tarefa (`Pendente`, `Finalizado`), demonstrando a criação de um serviço web escalável, seguro e pronto para produção.

---
Nascido como um desafio do **Bootcamp .NET da DIO**, demonstra a **evolução de uma arquitetura de nuvem**, migrando de uma abordagem tradicional `IaaS (Máquina Virtual)` para uma solução moderna, serverless e otimizada com `PaaS (Azure Container Apps)`.

A versão inicial, utilizando uma VM, cumpriu os requisitos do desafio. No entanto, buscando otimização de custos, maior escalabilidade e alinhamento com as práticas de mercado, o projeto foi totalmente migrado para o Azure Container Apps. A nova arquitetura é inteiramente baseada em **Plataforma como Serviço (PaaS)**, tanto para a aplicação quanto para o banco de dados e o deploy é **automatizado** via GitHub Actions, integrando CI/CD diretamente ao Azure, refletindo um ambiente de produção mais robusto e eficiente.


## 🚀 API em Produção! (Arquitetura Atual com Azure Container Apps)
A nova versão da API está online, rodando em uma infraestrutura serverless, escalável e segura.
[Clique aqui para acessar a API na Azure (ACA)](https://aca-dio-tarefas-api.happypebble-f5b88307.centralus.azurecontainerapps.io/swagger/index.html#/Tarefa)
> Observação: A aplicação pode levar alguns segundos para responder na primeira requisição devido à "partida a frio" (cold start), onde o contêiner é iniciado sob demanda.

---

## 📦 CI/CD Pipeline
O fluxo de deploy foi modernizado e hoje segue as melhores práticas:

1. **Commit & Push (branch `main`)**  
   Qualquer alteração enviada para a branch principal dispara automaticamente o pipeline no GitHub Actions.
2. **Build da Imagem**  
   O pipeline cria uma nova imagem Docker da aplicação.
3. **Push para o Azure Container Registry (ACR)**  
   A imagem é versionada com o hash do commit e enviada para o ACR.
4. **Deploy Automático para o Azure Container Apps (ACA)**  
   O GitHub Actions aciona a ação de deploy, atualizando a aplicação em produção sem downtime.

📄 Workflow de CI/CD: [`deploy-aca.yml`](.github/workflows/deploy-aca.yml)

---

## 🏛️ Versão Histórica (Arquitetura Original com VM)
A versão original da API, hospedada em uma Máquina Virtual do Azure como parte do desafio inicial, foi desativada e arquivada para fins históricos.
[Acesse o snapshot da API original via Web Archive](https://web.archive.org/web/20250824193241/http://132.196.1.215/swagger/index.html#/Tarefa)

---

## 🛠️ Tech Stack
O projeto foi construído com as tecnologias mais modernas e requisitadas do ecossistema .NET e de Cloud Computing.

* 👨‍💻 **Linguagem e Framework**: .NET 9 (ASP.NET Core)
* 🐘 **Banco de Dados**: PostgreSQL
* 🗺️ **ORM**: Entity Framework Core 9
* 🐳 **Containerização**: Docker & Docker Compose
* 📖 **Documentação de API**: OpenAPI + NSwag (Swagger)
* ☁️ **Cloud (PaaS/Serverless)**: Azure Container Apps, Azure Database for PostgreSQL & Azure Virtual Network

--- 

## 🏗️ Evolução da Arquitetura na Nuvem

### Arquitetura Atual (PaaS - Azure Container Apps)

A arquitetura atual é otimizada para escalabilidade, segurança e baixo custo operacional, utilizando serviços gerenciados do Azure.

1. **Gateway de Entrada (`ACA Ingress`)**: O ponto de entrada é o Ingress gerenciado do próprio Azure Container Apps. Ele fornece um endpoint HTTPS público, terminação TLS e distribui o tráfego para os contêineres da aplicação.
2. **Ambiente Serverless (`Azure Container Apps`)**: A API .NET roda em um contêiner Docker dentro de um ambiente serverless. O ACA gerencia automaticamente o dimensionamento (inclusive para zero, economizando custos), o balanceamento de carga e a saúde das réplicas.
3. **Rede Privada Segura (`Azure VNet Integration`)**: O ambiente do Container Apps está integrado a uma Rede Virtual (VNet). Isso permite que a aplicação se comunique com o banco de dados de forma privada e segura, sem expor nenhum dos serviços à internet pública.
4. **Banco de Dados Gerenciado (`Azure DB for PostgreSQL - PaaS`)**: O banco de dados continua com a configuração de Acesso Privado, sendo totalmente inacessível pela internet. Ele só aceita conexões da subnet integrada ao ACA, garantindo a máxima segurança para a camada de dados.

### Arquitetura Original (`IaaS - VM`)
A solução inicial foi projetada seguindo o princípio de "defesa em profundidade", utilizando uma combinação de IaaS e PaaS.

1. **Firewall de Entrada (`Network Security Group`)**: A primeira camada de segurança era o NSG associado à rede da VM. Ele atuava como um firewall, permitindo o tráfego da Internet apenas na porta HTTP (80).
2. **Servidor de Aplicação (`Azure VM - IaaS`)**: Uma Máquina Virtual Linux (Ubuntu) recebia as requisições. Dentro dela, a API .NET rodava isolada em um contentor Docker.
3. **Rede Privada Segura (`Azure VNet`)**: A comunicação entre a API e o Banco de Dados acontecia através da mesma Rede Virtual (VNet).
4. **Banco de Dados Gerenciado (`Azure DB for PostgreSQL - PaaS`)**: O banco de dados foi configurado com Acesso Privado, aceitando apenas conexões originadas de dentro da VNet.

---

## 🚀 A Jornada para a Nuvem: O Processo de Deploy
### Novo Processo de Deploy (Azure Container Apps via Container Registry)
O deploy na nova arquitetura é automatizado e segue as melhores práticas de CI/CD:
1. **Containerização**: O Dockerfile é usado para construir a imagem da aplicação.
2. **Publicação da Imagem**: A imagem Docker é enviada para um Container Registry (como o Azure Container Registry ou Docker Hub).
3. **Deploy no ACA**: O Azure Container Apps é configurado para baixar a nova versão da imagem do registry e atualizar as réplicas da aplicação de forma transparente (zero downtime).

### Processo de Deploy Original (VM via SSH & Docker)

O deploy na arquitetura original foi um processo manual, dividido em três grandes etapas. A criação da infraestrutura foi **documentada em detalhes em repositórios dedicados**, servindo como guias completos do processo.

1. **Provisionando a Infraestrutura (VM - IaaS)**
A primeira etapa envolveu a criação de uma Máquina Virtual Ubuntu na Azure, incluindo a configuração de redes (VNet), regras de segurança (NSG) e acesso via chaves SSH.

> 📄 **Para um guia detalhado, com screenshots e explicações passo a passo, acesse o repositório dedicado: [Guia de Deploy - Azure VM](https://github.com/GustavoHerreira/azure-vm-deploy-dio-challenge)**

2. **Configurando o Banco de Dados Gerenciado (DB - PaaS)**
Em seguida, o Banco de Dados do Azure para PostgreSQL foi provisionado e configurado para acesso privado, garantindo a comunicação segura apenas de dentro da VNet.

> 🐘 **O passo a passo completo para a configuração do banco de dados está disponível em: [Guia de Setup - Azure PostgreSQL](https://github.com/GustavoHerreira/azure-db-deploy-dio-challenge)**

3. **Deploy Final da Aplicação (Docker)**
Com a infraestrutura pronta, o deploy final foi realizado conectando-se à VM via SSH, instalando Docker e iniciando a aplicação com `docker compose`, injetando a ConnectionString como variável de ambiente.

---

## 📝 Contexto do Desafio Original (DIO Bootcamp)
O objetivo inicial do projeto era construir um sistema gerenciador de tarefas com um CRUD (Criar, Ler, Atualizar, Deletar) funcional.

Requisitos da API:
A classe principal de `Tarefa` deveria seguir o diagrama abaixo, e a API deveria expor endpoints para todas as operações de CRUD, além de buscas por título, data e status.

![Diagrama da classe Tarefa](diagrama.png)

**Endpoints Esperados:**

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

---

## 🐳 Como Executar Localmente

Este projeto está preparado para rodar facilmente em um ambiente de desenvolvimento local usando Docker.

1. **Clone o repositório:**
```
git clone https://github.com/GustavoHerreira/todoapp-trilha-dotnet-api-desafio.git
cd todoapp-trilha-dotnet-api-desafio
```
2. **Crie o arquivo de ambiente:**
Crie um arquivo `.env` na raiz do projeto. Ele é essencial para configurar as credenciais do banco de dados local. Use o seguinte template:
```
# Credenciais para o banco de dados PostgreSQL local
POSTGRES_USER=root
POSTGRES_PASSWORD=root
POSTGRES_DB=TodoAppDb
```
3. **Execute o Docker Compose:**
Use o arquivo `docker-compose.dev.yml`, que subirá tanto a API quanto um contêiner com o banco de dados PostgreSQL.
```
docker compose -f docker-compose.dev.yml up --build
```
4. **Acesse a API:**
A documentação do Swagger estará disponível em `http://localhost:8080/swagger`.
