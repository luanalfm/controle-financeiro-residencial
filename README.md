# Controle de Gastos Residenciais

Backend: API .NET 8 com **Clean Architecture**, **Entity Framework Core 8**, **PostgreSQL**, **FluentValidation**, 
**AutoMapper** e **Swagger**.

Frontend: React com **Vite**, **TypeScript**, **TailwindCSS**, **Zustand**, 
**Zod**, **React Router DOM** e **React Hook Form**.

Banco de dados: **PostgreSQL** 

#

# Backend

## Estrutura

- `src/Api` — ASP.NET Core Web API, Swagger, middleware de erro, `Program.cs`
- `src/Application` — casos de uso, DTOs, validadores, interfaces de repositório, AutoMapper
- `src/Domain` — entidades, enums, exceções de domínio
- `src/Infrastructure` — `DbContext`, repositórios, migrations, PostgreSQL

## Pré-requisitos

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [Docker](https://docs.docker.com/get-docker/) (para PostgreSQL)
- Opcional: `dotnet-ef` global (`dotnet tool install --global dotnet-ef --version 8.0.11`)

## Passo a passo para rodar

1. **Subir o PostgreSQL**

   Na pasta "backend", onde está o arquivo docker-compose.yml:

   ```bash
   docker compose up -d
   ```

2. **Restaurar e compilar**

   ```bash
   dotnet restore
   dotnet build
   ```

3. **Executar a API** (ambiente Development aplica migrations automaticamente)

   ```bash
   cd src/Api
   dotnet run
   ```

4. **Abrir o Swagger**

    A porta geralmente é `https://localhost:7000/swagger` ou `https://localhost:7050/swagger` 
   (confirme no console após `dotnet run`).

### Migrations (manual)

Se preferir aplicar migrations sem subir a API:

```bash
dotnet ef database update --project src/Infrastructure/ControleGastos.Infrastructure.csproj --startup-project src/Api/ControleGastos.Api.csproj
```

Nova migration:

```bash
dotnet ef migrations add NomeDaMigration --project src/Infrastructure/ControleGastos.Infrastructure.csproj --startup-project src/Api/ControleGastos.Api.csproj --output-dir Persistence/Migrations
```

## Connection string

Padrão em `src/Api/appsettings.json`:

`Host=localhost;Port=5433;Database=controle_gastos;Username=postgres;Password=1234` (No arquivo docker-compose está sendo utilizada a porta 5433 ao invés da padrão 5432)


## Exemplos de requisição

Resumo:

- **POST** `/api/pessoas` — `{ "nome": "Luana", "idade": 25 }`
- **POST** `/api/categorias` — `{ "descricao": "Alimentação", "finalidade": 0 }` (0=Despesa, 1=Receita, 2=Ambas)
- **POST** `/api/transacoes` — `{ "descricao": "Compras", "valor": 150.50, "tipo": 0, "categoriaId": "...", "pessoaId": "..." }` (tipo: 0=Despesa, 1=Receita)
- **GET** `/api/consultas/totais-por-pessoa`
- **GET** `/api/consultas/totais-por-categoria`

#

# Frontend

## Estrutura

- `src/api` — instância Axios centralizada, interceptors de erro
- `src/services` — camada de comunicação com a API (personService, categoryService, transactionService, searchService)
- `src/types` — tipagens globais (entidades, payloads, enums)
- `src/store` — uso de estados globais com Zustand
- `src/utils` — schemas de validação do Zod integrados ao React Hook Form
- `src/pages` — páginas da aplicação (Dashboard, Persons, Categories, Transactions)
- `src/components` — componentes genéricos e reutilizáveis (Modal, FormFields, StatCard, Layout)
- `src/routes` — configuração centralizada do React Router DOM

## Pré-requisitos

- [Node.js 20](https://nodejs.org/) 
- WebApi + docker rodando (ver README do backend)

## Passo a passo para rodar

1. **Instalar dependências**

   ```bash
   npm install
   ```

2. **Configurar variável de ambiente**

   Crie um arquivo `.env` na raiz do frontend:

   ```env
   VITE_API_BASE_URL=https://localhost:7xxx/api
   ```

   Substitua a porta pela que aparece no console do backend (`launchSettings.json`).

3. **Rodar em desenvolvimento**

   ```bash
   npm run dev
   ```

4. **Acessar a aplicação**

   Navegue para `http://localhost:5173` (porta padrão do Vite) ou `http://localhost:8080/`.

## Stack

- React 18 + TypeScript
- Vite 5
- TailwindCSS
- Axios
- React Router DOM
- React Hook Form + Zod
- Zustand
- Sonner (toasts)
- shadcn/ui

## Funcionalidades

- **Dashboard** — resumo financeiro geral, por pessoa e por categoria
- **Pessoas** — CRUD completo com validações
- **Categorias** — cadastro de categorias com finalidade (despesa, receita, ambas)
- **Transações** — cadastro com regras de negócio:
  - Menores de 18 anos só podem registrar despesas
  - Filtro de categorias por tipo de transação
  - Validação completa via Zod

## Integração com o Backend

Services em `src/services/` que consomem os endpoints REST:

- **Pessoas** → `POST /api/pessoas`, `GET /api/pessoas`, `PUT /api/pessoas/:id`, `DELETE /api/pessoas/:id`
- **Categorias** → `POST /api/categorias`, `GET /api/categorias`
- **Transações** → `POST /api/transacoes`, `GET /api/transacoes`
- **Consultas** → `GET /api/consultas/totais-por-pessoa`, `GET /api/consultas/totais-por-categoria`
