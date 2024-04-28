# Introdução

Projetos feitos para o processo seletivo, com o objetivo de criar uma aplicação para gerenciamento de aluguel de motos.

## Introdução para execução

### Variáveis de ambiente

Ajustar as variáveis de ambiente `ASPNETCORE_ENVIRONMENT` e `DOTNET_ENVIRONMENT` com o valor `Development` no sistema operacional.

No Windows, para criar a variável de ambiente para o usuário conectado, ou alterar o valor, com CMD:

```bash
setx ASPNETCORE_ENVIRONMENT "Development"
setx DOTNET_ENVIRONMENT "Development"
```

No MacOS, para criar as variáveis de ambiente permanentemente, é necessário editar o arquivo `~/.bash_profile` como root e acrescentar as seguintes linhas no final do arquivo:

```bash
export ASPNETCORE_ENVIRONMENT="Development"
export DOTNET_ENVIRONMENT="Development"
```

### Ferramentas necessárias

- Docker/Docker Compose

Para executar o projeto, basta clonar o repositório e executar o comando `docker compose up -d` para subir o container do banco de dados e da aplicação.
Abra o navegador e acesse `http://localhost:5001/swagger/index.html` para visualizar a aplicação.

Ao executar o comando `docker compose up -d` o banco de dados será populado com dados de teste.

### Autenticação ADMIN

Para acessar a aplicação como administrador, utilize o seguinte usuário:

``` json
{
  "email": "job@job.com",
  "password": "mudar@123"
}
```
Para autenticar como motoboy e necessário fazer o cadastro e utilizar o CNPJ no login.

## Características do projeto

- .NET 8
- Banco de dados PostgreSQL
- CQS (Command Query Separation)
- ORM: **EntityFramework Core**
- Framework de Testes: **XUnit**
- Framework de Assertions: **FluentAssertions**
- Framework de Mock: **Moq**
- Code Analyzer: **Microsoft.CodeAnalysis.NetAnalyzers**
- Projeto para testes de Unidade
- Projeto para testes de Integração
- Tratamento de Warning como Error