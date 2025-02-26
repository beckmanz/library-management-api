Este repositório contém a implementação do sistema de gerenciamento de biblioteca desenvolvido como parte de um projeto acadêmico para a faculdade. O projeto tem como objetivo oferecer funcionalidades para o gerenciamento de livros, usuários e empréstimos dentro de uma biblioteca, utilizando o framework ASP.NET Core para o back-end.

# 📌 Instruções para Rodar a API Localmente

## 🛠 Requisitos

Antes de iniciar, certifique-se de que sua máquina atende aos seguintes requisitos:

- [.NET 9 SDK](https://dotnet.microsoft.com/download/dotnet/9.0) instalado
- [SQL Server](https://www.microsoft.com/pt-br/sql-server/sql-server-downloads) instalado ou rodando em um container Docker
- [Postman](https://www.postman.com/) ou outra ferramenta para testar a API (opcional)
- [Git](https://git-scm.com/) instalado (obrigatorio para clonar o repositório e contribuir)

## 🚀 Passo a Passo para Executar a API

### 1️⃣ **Clonar o repositório** (caso ainda não tenha)
   ```bash
   git clone https://github.com/beckmanz/library-management-api.git
   cd library-management-api
   ```

### 2️⃣ **Configurar o banco de dados**

1. Abra o arquivo `appsettings.Development.json` e configure a string de conexão:
   ```json
   "ConnectionStrings": {
       "DefaultConnection": "Server=localhost;Database=LibraryManagementDb;Trusted_Connection=True;"
   }
   ```
2. Se estiver usando autenticação por usuário e senha, ajuste conforme necessário:
   ```json
   "ConnectionStrings": {
       "DefaultConnection": "Server=localhost;Database=LibraryManagementDb;User Id=seu_user;Password=sua_senha;TrustServerCertificate=true;"
   }
   ```

### 3️⃣ **Restaurar dependências**

```bash
dotnet restore
```

### 4️⃣ **Aplicar as migrations no banco de dados**

```bash
dotnet ef database update
```

Se não houver migrations criadas, gere uma nova com:

```bash
dotnet ef migrations add InitialCreate
```

### 5️⃣ **Executar a API**

```bash
dotnet run
```

Se quiser rodar em modo de desenvolvimento, use:

```bash
dotnet watch run
```

### 6️⃣ **Acessar a documentação da API**

Após rodar a API, acesse:

- **Scalar HTTPS:** [`https://localhost:8080/scalar/v1`](https://localhost:8080/scalar/v1)
- **Scalar HTTP:** [`http://localhost:5094/scalar/v1`](http://localhost:5094/scalar/v1)

Caso tenha alterado a porta no `launchSettings.json`, ajuste a URL conforme necessário.

---

## 📌 Observações

- Certifique-se de que nenhuma outra aplicação está utilizando a mesma porta.
- Se houver erros de banco de dados, revise a string de conexão e a execução das migrations.
- Para testar os endpoints, você pode usar o Postman, Insomnia ou outra ferramenta de requisição HTTP.

Se precisar de mais detalhes, entre em contato! 🚀

