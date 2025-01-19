# Sistema de Gerenciamento de Pedidos

Uma aplicação .NET 8 para gerenciamento de pedidos de clientes, construída utilizando os princípios de Clean Architecture e padrão CQRS.

## Funcionalidades

- Operações CRUD para gerenciamento de pedidos
- API REST com documentação Swagger
- Banco de dados PostgreSQL para persistência de dados
- Suporte a Docker para fácil implantação
- Testes unitários usando xUnit
- Implementação CQRS usando MediatR
- Design baseado em Clean Architecture
- Validação de dados usando FluentValidation
- Mapeamento objeto-relacional com Entity Framework Core
- Documentação de API com Swagger/OpenAPI

## Stack Tecnológica

### Frameworks e Bibliotecas
- .NET 8.0
- Entity Framework Core 8.0.1
- PostgreSQL com Npgsql 8.0.0
- MediatR 12.2.0
- FluentValidation.AspNetCore 11.3.0
- Swagger/OpenAPI 6.5.0
- xUnit 2.7.0
- Moq 4.20.70

### Ferramentas de Desenvolvimento
- Visual Studio 2022 ou VS Code
- Docker Desktop
- Azure DevOps (para CI/CD)

### Padrões e Práticas
- Clean Architecture
- Domain-Driven Design (DDD)
- Command Query Responsibility Segregation (CQRS)
- Repository Pattern
- Dependency Injection
- Unit Testing
- SOLID Principles

## Estrutura do Projeto

```
OrderManagement/
├── src/
│   ├── OrderManagement.Api/              # Camada de API REST
│   │   ├── Controllers/                  # Controladores da API
│   │   ├── Program.cs                    # Configuração da aplicação
│   │   └── appsettings.json             # Configurações da aplicação
│   │
│   ├── OrderManagement.Application/      # Camada de Aplicação
│   │   ├── Commands/                     # Comandos CQRS
│   │   ├── Queries/                      # Consultas CQRS
│   │   ├── DTOs/                        # Objetos de Transferência de Dados
│   │   ├── Handlers/                     # Manipuladores de comandos e consultas
│   │   └── Validators/                   # Validadores FluentValidation
│   │
│   ├── OrderManagement.Domain/           # Camada de Domínio
│   │   ├── Entities/                     # Entidades de domínio
│   │   ├── Common/                       # Classes base e interfaces comuns
│   │   └── Repositories/                 # Interfaces de repositório
│   │
│   └── OrderManagement.Infrastructure/   # Camada de Infraestrutura
│       ├── Data/                         # Contexto e configurações do EF Core
│       └── Repositories/                 # Implementações de repositório
│
├── tests/
│   └── OrderManagement.Tests/            # Testes Unitários
│       ├── Domain/                       # Testes de domínio
│       └── Handlers/                     # Testes de handlers
│
├── docker-compose.yml                    # Configuração do Docker
└── README.md
```

## Detalhes de Implementação

### Domain Layer
- Entidades de domínio com encapsulamento
- Validações de regras de negócio
- Interfaces de repositório
- Classes base para auditoria

### Application Layer
- Implementação do padrão CQRS
- DTOs para transferência de dados
- Validadores usando FluentValidation
- Handlers para processamento de comandos e consultas

### Infrastructure Layer
- Implementação do Entity Framework Core
- Configurações de mapeamento objeto-relacional
- Implementações de repositório
- Migrações de banco de dados

### API Layer
- Controllers RESTful
- Documentação Swagger
- Configuração de Dependency Injection
- Middleware de tratamento de erros

## Como Começar

### Pré-requisitos

- .NET 8 SDK
- Docker Desktop
- Visual Studio 2022 ou VS Code
- PostgreSQL (se executando localmente)
- Entity Framework Core CLI

### Instalação do Entity Framework Core CLI

1. Verifique se o EF Core CLI já está instalado:
   ```bash
   dotnet ef --version
   ```

2. Se o comando acima retornar erro ou não for encontrado, instale o EF Core CLI globalmente:
   ```bash
   dotnet tool install --global dotnet-ef
   ```

3. Se você já tem o EF Core CLI instalado mas precisa atualizar para a versão mais recente:
   ```bash
   dotnet tool update --global dotnet-ef
   ```

4. Se encontrar erros durante a instalação, tente:
   ```bash
   # Desinstalar versão atual
   dotnet tool uninstall --global dotnet-ef
   
   # Limpar cache do NuGet
   dotnet nuget locals all --clear
   
   # Reinstalar EF Core CLI
   dotnet tool install --global dotnet-ef
   ```

5. Após a instalação, verifique se está funcionando:
   ```bash
   dotnet ef --version
   ```

### Configuração do Banco de Dados

1. Verifique se o PostgreSQL está em execução e acessível
2. Verifique as configurações de conexão em `src/OrderManagement.Api/appsettings.json`:
   ```json
   {
     "ConnectionStrings": {
       "DefaultConnection": "Host=localhost;Database=ordermanagement;Username=postgres;Password=postgres"
     }
   }
   ```

3. Copie o arquivo de configuração para o projeto de Infraestrutura:
   ```bash
   cd src/OrderManagement.Api
   cp appsettings.json ../OrderManagement.Infrastructure/
   ```

4. Execute as migrações do banco de dados:
   ```bash
   # Na pasta src/OrderManagement.Api
   dotnet ef database update --project ../OrderManagement.Infrastructure/OrderManagement.Infrastructure.csproj
   ```

#### Comandos úteis do Entity Framework

```bash
# Criar uma nova migração
dotnet ef migrations add NomeDaMigracao --project ../OrderManagement.Infrastructure/OrderManagement.Infrastructure.csproj

# Listar migrações
dotnet ef migrations list --project ../OrderManagement.Infrastructure/OrderManagement.Infrastructure.csproj

# Remover última migração
dotnet ef migrations remove --project ../OrderManagement.Infrastructure/OrderManagement.Infrastructure.csproj

# Atualizar banco de dados para uma migração específica
dotnet ef database update NomeDaMigracao --project ../OrderManagement.Infrastructure/OrderManagement.Infrastructure.csproj

# Gerar script SQL
dotnet ef migrations script --project ../OrderManagement.Infrastructure/OrderManagement.Infrastructure.csproj

# Reverter para uma migração específica
dotnet ef database update NomeDaMigracao --project ../OrderManagement.Infrastructure/OrderManagement.Infrastructure.csproj
```

### Executando Localmente

1. Clone o repositório:
```bash
git clone [repository-url]
cd OrderManagement
```

2. Execute com Docker Compose:
```bash
# Construir e iniciar os containers
docker-compose up -d --build

# Verificar se os containers estão rodando
docker-compose ps

# Ver logs da aplicação (opcional)
docker-compose logs -f api
```

3. Acesse a API:

#### Com Docker (Recomendado)
```
http://localhost:8080/swagger
http://localhost:8080/swagger/index.html
```

#### Localmente sem Docker
```
http://localhost:5000/swagger
https://localhost:5001/swagger
```

#### Troubleshooting de Acesso ao Swagger

Se não conseguir acessar o Swagger, verifique:

1. Status dos containers:
```bash
docker-compose ps
```

2. Logs da aplicação:
```bash
docker-compose logs api
```

3. Portas em uso:
```bash
# Windows
netstat -ano | findstr "80"
netstat -ano | findstr "443"

# Linux/MacOS
netstat -tulpn | grep "80"
netstat -tulpn | grep "443"
```

4. Soluções comuns:
   - Se a porta 80 estiver em uso:
     - Pare outros serviços usando a porta (ex: IIS, Apache)
     - Ou altere a porta no docker-compose.yml para outra (ex: 8080:80)
   - Se o container não iniciar:
     - Verifique se o Docker está rodando
     - Tente reconstruir a imagem: `docker-compose build --no-cache`
   - Se o Swagger não carregar:
     - Verifique se a aplicação está em modo Development
     - Limpe o cache do navegador
     - Tente outro navegador

### Usando o Swagger

1. A interface do Swagger fornece:
   - Documentação completa da API
   - Interface interativa para testar endpoints
   - Exemplos de requisições e respostas
   - Schemas dos modelos de dados

2. Para testar um endpoint:
   - Clique no endpoint desejado para expandir
   - Clique em "Try it out"
   - Preencha os parâmetros necessários
   - Clique em "Execute"
   - Veja a resposta com status code e corpo da resposta

3. Dicas úteis:
   - Use o botão "Schema" para ver a estrutura completa dos objetos
   - Copie exemplos de requisição clicando no ícone de cópia
   - Veja os códigos de resposta possíveis em cada endpoint
   - Utilize os filtros para encontrar endpoints específicos

### Endpoints da API

- POST /api/orders - Criar um novo pedido
- GET /api/orders - Obter todos os pedidos
- GET /api/orders/{id} - Obter pedido por ID
- PUT /api/orders/{id} - Atualizar um pedido
- DELETE /api/orders/{id} - Excluir um pedido

#### Exemplo de Requisição (Criar Pedido)

```json
POST /api/orders
{
  "customerName": "João Silva",
  "orderDate": "2024-03-20T10:00:00Z",
  "items": [
    {
      "name": "Produto 1",
      "quantity": 2,
      "unitPrice": 29.99
    }
  ]
}
```

## Testes

O projeto inclui testes unitários abrangentes usando xUnit:

```bash
# Executar todos os testes
dotnet test

# Executar testes com cobertura
dotnet test /p:CollectCoverage=true
```

### Cobertura de Testes
- Testes de domínio para entidades
- Testes de handlers CQRS
- Testes de validação
- Mocks usando Moq

## Implantação

A aplicação pode ser implantada de várias formas:

1. Azure App Service
   - Usando o Dockerfile fornecido
   - Através de pipelines do Azure DevOps

2. Kubernetes
   - Usando os manifestos fornecidos
   - Com Helm charts (em desenvolvimento)

3. Docker Compose
   - Para ambientes de desenvolvimento
   - Para implantações simples

## Contribuindo

1. Fork o projeto
2. Crie uma branch para sua feature (`git checkout -b feature/AmazingFeature`)
3. Commit suas mudanças (`git commit -m 'Add some AmazingFeature'`)
4. Push para a branch (`git push origin feature/AmazingFeature`)
5. Abra um Pull Request

## Licença

Este projeto está licenciado sob a Licença MIT. Veja o arquivo `LICENSE` para mais detalhes. 