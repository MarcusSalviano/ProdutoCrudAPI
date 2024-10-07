

# ![](/assets/configuracao.png) Produto CRUD API

> Este é um projeto é uma API de um CRUD (Create, Read, Update, Delete) de produto desenvolvido em C# que se comunicam com o MySql para persistência dos dados.

## Tecnologias Utilizadas
### API
- .NET 8.0 (C#)
- AutoMapper 13.0.1  
Framework para mapeamento entre os modelos e DTOs
- Newtonsoft  
Framework para serialização de JSON
### Testes unitários
- xUnit 2.5.3  
Framework para os testes unitários
- Moq 4.20.272  
Framework para mocking nos testes unitários

## Requisitos para executar
- Docker

## Estrutura do Projeto
Projeto foi feito em arquitetura hexagonal e está organizado da seguinte forma:

```shell
  $ ProdutoCrudAPI
  .
  ├── Application
  │   ├── Dtos
  │   └── Services
  ├── Domain
  │   ├── Models
  │   └── Repositories
  ├── Infrastructure
  │   ├── Data
  │   │   └── Migrations
  │   ├── Filters
  │   ├── Mapping
  │   │   └── Repositories
  │   └── Repositories
  ├── Presentation
  │   └── Controllers

  $ ProdutoCrudAPI.Test
  .
```

- `ProdutoCrudAPI/`: Aplicação principal
    - `Application/Dtos`: Camada de aplicação da aplicação
    - `Application/Services`: Serviços da aplicação
    - `Domain/Models`: Entidades JPA
    - `Domain/Repositories`: Interfaces dos repositórios JPA
    - `Infrastructure/Data/Migrations`: Migrations para criação do banco
    - `Infrastructure/Filters`: Filtros da aplicação
    - `Infrastructure/Mapping/Profiles`: Arquivos para mapeamento entre os DTOs e as entidades
    - `Infrastructure/Repositories`: Classes concretas dos repositórios JPA
    - `Presentation/Controllers`: Controladores REST
- `ProdutoCrudAPI.Test/`: Testes de unidade e integração


## Configuração do Projeto
1. Para o correto funcionamento da aplicação é necessário configurar a seguinte variável de ambiente no sistema operacional:

- `JWT_SECRET_KEY` - Secret Key para geração do token JWT necessário para acessar os endpoints

2. Faça o clone do repositório para sua máquina local:
```bash
   git clone https://github.com/MarcusSalviano/ProdutoCrudAPI.git
```

## Executando a Aplicação
1. Para compilar e executar o projeto usando docker compose entre no diretório do projeto principal (ProdutoCrudAPI) e execute a seguinte linha de comando:

```bash
docker compose up --build
```

2. A aplicação estará disponível em http://localhost:5025.

3. É necessário usar o endpoint de login (http://localhost:5025/login) passando o usuário "root" para ter um token válido.

4. O token adquirido deve ser passado todos os endpoints do CRUD de Produto



## Funcionalidades
### LoginController
- Possui um endpoint de login que retorna um token JWT para validação nos demais endpoints.
- Login é o único endpoint que não precisa de um toke para validação

### **[POST]**
`/login` - endpoint de login de usuário  
    - Entrada: JSON de LoginDto  
    - Ação: Valida o valor de usuário recebido e gera um token JWT de acordo com a chave JWT_SECRET_KEY (Único usuário válido é o usuário *"root"*).  
    - Saída: Retorna um token JWT caso usuário seja válido, caso contrário, retorna status 401-Unauthorized.

### ProdutoControler
- Controller responsável pelo CRUD de Produto.
- Todos os endpoints desse controlador precisam de um token para validação do usuário.

### **[POST]**
`/api/produtos` - endpoint para cadastro de um produto  
- Entrada: JSON de Produto.  
- Ação: Cadastra o produto recebido, no banco, de acordo com os parêmetros passados.  
- Saída: JSON com todos o produtos cadastrado e status 200.  

### **[GET]**
`/api/produtos` - endpoint para buscar todos os produtos cadastrados no banco  
- Entrada:   
    - Parâmetros:
        - skip - quantos registros devem ser pulados na busca (default 0)
        - take - máximo de registros que será retornado (default 50)
        - nome - busca os nomes que contêm o valor passado
        - descricao - busca as descrições que contêm o valor passado
- Ação: Busca no banco os produtos cadastrados.  
- Saída: JSON com todos os produtos cadastrados.  

`/api/produtos/{id}` - endpoint para buscar por id um produto cadastrado no banco
    - Entrada: Parâmetro de ID na URI.  
    - Ação: Busca no banco o produto com o id passado.  
    - Saída: JSON com o produto buscado.  

### **[PUT]**
`/api/produtos/{id}` - endpoint para alterar um produto cadastrado a partir do seu id  
    - Entrada: Parâmetro de ID na URI e JSON com os dados a alterar.  
    - Ação: Altera os dados do produto especificado de acordo com os valores passados.  
    - Saída: JSON com o produto alterado. 

### **[DELETE]**
`/api/produtos/{id}` - endpoint para deletar um produto cadastrado a partir do seu id  
    - Entrada: Parâmetro de ID na URI.  
    - Ação: Deleta o produto com id informado.  
    - Saída: não possui.


## Exemplos de JSON

### LoginDto
```json
{
    "usuario": "root"
}
```

### Produto
```json
{
  "nome": "Garrafa Stanley",
  "descricao": "Garrafa Térmica",
  "preco": 250.00,
  "dataValidade": "2030-10-04",
  "imagem":"imagem1.jpg",
  "categoria": {
    "nome": "camping"
  }
}
```