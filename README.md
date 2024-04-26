# Introdução

< Incluir introdução >

## Intruções para execução

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