# CleanArch - Projeto de Catálogo de Produtos

## Escopo do projeto

### Objetivo 
O projeto CleanArc tem como objetivo principal aplicar os conceitos de uma arquitetura limpa em um projeto ASP.NET a partir de um uma aplicação web utilizando ASP.NET Core MVC para o gerenciamento de produtos e categorias, proporcionando a criação de um catálogo de produtos para vendas.

#### Ferramentas utilizadas
- Visual Studio Community (ou VS Code)
- ASP.NET Core MVC
- Entity Framework Core
- SQL Server

## Estrutura do Projeto
O projeto CleanArch segue a abordagem da Clean Architecture, com os seguintes componentes distribuídos por camadas:

### 1. CleanArch.Domain:
- Entities: Contém as classes que representam o modelo de domínio (Product e Category).
- Interfaces: Contém as interfaces (ICategoryRepository, IProductRepository).
- Validation: Contém a classe DomainExceptionValidation para validar o modelo de domínio.

### 2. CleanArch.Application:
- Services: Contém os serviços (ProductService, CategoryService).
- Interfaces: Contém as interfaces (IProductService, ICategoryService).
- DTOs: Contém os Data Transfer Objects (ProductDTO, CategoryDTO).
- Mappings: Contém mapeamentos entre domínio e visão (DomainViewModel, ViewModelDomain).
- CQRS: Implementação do padrão Command-Query Responsibility Segregation para os Produtos.

### 3. CleanArch.Infra.Data:
- Repositories: Implementação concreta dos repositórios (ProductRepository, CategoryRepository).
- Context: Implementação do DbContext (ApplicationDbContext).
- Migrations: Migrations para versionamento do banco de dados.
- Identity: Configuração para autenticação e autorização com Identity.
- EntitiesConfiguration: Configuração das entidades utilizando o Entity Framework Core

### 4. CleanArch.Infra.IoC:
- DependencyInjection: Configurações das injeções de dependências e registros de serviços.

### 5. CleanArch.WebUI:
- Controllers: Controladores da aplicação (AccountController, HomeController, CategoriesController, ProductsController).
- Views: Arquivos de visualização da aplicação.
- ViewModels: Modelos de visão utilizados pela camada de apresentação.

### 6. CleanArch.API:
- Controllers: Controladores da aplicação (TokenController, CategoriesController, ProductsController).
- Models: Modelos de visão utilizados pela camada da api


## Regras de Negócio

### Regras para os Produtos
1. Exibir produtos.
2. Criar um novo produto.
3. Alterar propriedades de um produto existente (o Id do produto não pode ser alterado).
4. Excluir um produto existente pelo seu Id.
5. Relacionamento entre produto e categoria (propriedade de navegação).
6. Restrições para criação de produtos:
   - Construtor parametrizado para evitar estado inconsistente.
   - Atributos Id, Stock e Price não podem ser negativos.
   - Atributos Name e Description não podem ser nulos ou vazios.
   - Atributo Image pode ser nulo.
   - Atributo Name não pode ter menos que 3 caracteres.
   - Atributo Description não pode ter menos que 5 caracteres.
   - Atributo Image não pode ter mais que 250 caracteres.
   - Imagem será armazenada como uma string com arquivo separado em uma pasta do projeto.

### Regras para as Categorias
1. Exibir categorias.
2. Criar uma nova categoria.
3. Alterar propriedades de uma categoria existente (o Id da categoria não pode ser alterado).
4. Excluir uma categoria existente pelo seu Id.
5. Relacionamento entre categoria e produto (propriedade de navegação).
6. Restrições para criação de categorias:
   - Construtor parametrizado para evitar estado inconsistente.
   - Atributo CategoryId não pode ter valor negativo.
   - Atributo Name não pode ser nulo ou vazio.
   - Atributo Name não pode ter menos que 3 caracteres.

## Persistência dos Dados
- Banco de dados relacional: SQL Server.
- ORM: Entity Framework Core.
- Abordagem Code-First.
- Provedor do banco de dados: Microsoft.EntityFrameworkCore.SqlServer.
- Ferramenta para aplicar Migrations: Microsoft.EntityFrameworkCore.Tools.
- Desacoplamento da camada de acesso a dados do ORM usando o padrão Repository.

## Estrutura do Projeto
- CleanArch.Domain: Sem dependências.
- CleanArch.Application: Dependência com CleanArch.Domain.
- CleanArch.Infra.Data: Dependência com CleanArch.Domain.
- CleanArch.Infra.IoC: Dependência com CleanArch.Domain, CleanArch.Application, CleanArch.Infra.Data.
- CleanArch.WebUI: Dependência com CleanArch.Infra.IoC.

## CQRS - Command Query Responsibility Separation
O CQRS (Command Query Responsibility Separation) leva o conceito do CQS adiante, determinando a separação dos comandos e das consultas em objetos diferentes. A ideia principal do CQRS é permitir que um aplicativo funcione com diferentes modelos: um modelo para comandos e outro para consultas.

Principais pontos do CQRS:

- Separação de comandos e consultas em modelos distintos.
- Flexibilidade com diferentes modelos para diferentes operações.
- Modelos específicos para comando e consulta.
- Não dependência de apenas um DTO para todas as operações CRUD.
- Implementação do padrão Mediator para otimizar e favorecer o desacoplamento.

### Implementação do Padrão CQRS no Projeto (Entidade: Product) 
#### 1. Modelos para Comandos:
- ProductCommand
- ProductCreateCommand
- ProductUpdateCommand
- ProductRemoveCommand

#### 2. Modelos para Consultas:
- GetProductByIdQuery
- GetProductsQuery

Essas classes representam os comandos e consultas e devem implementar a interface IRequest da MediatR, que representa um request com um response. A interface IRequest aceita o tipo que o respectivo handler deverá retornar para o componente chamador. Um Request contém propriedades que são usadas para fazer o input dos dados para os Handlers.

#### 3. Handlers para Processar Comandos e Consultas:
- GetProductByIdQueryHandler
- GetProductsQueryHandler
- ProductCreateCommandHandler
- ProductUpdateCommandHandler
- ProductRemoveCommandHandler

Essas classes devem implementar a interface IRequestHandler<Request, Response>, que trata o respectivo comando. No método Handle, definimos o comando a ser processado e o retorno esperado.

#### 4. Serviço ProductService:
- Injetar a instância do Mediatr (IMediator). O padrão Mediator receberá um request e invocará o handler associado a ele.

#### 5. Mapeamento de DTOs:
- Definir um novo mapeamento de DTOs para os DTOs dos comandos e consultas criados.

Ao seguir esses passos, implementamos o padrão CQRS no projeto, proporcionando uma separação clara entre comandos e consultas, aumentando a flexibilidade e facilitando o tratamento de diferentes operações. O uso do padrão Mediator contribui para o desacoplamento e a organização do código.