# README - Instruções para instalar e rodar PostgreSQL usando Docker

### Passo 1: Instalar Docker
Certifique-se de que o Docker está instalado e rodando em seu sistema. Se ainda não estiver instalado, siga as instruções de instalação disponíveis em [https://docs.docker.com/get-docker/](https://docs.docker.com/get-docker/).

### Passo 2: arquivo docker-compose.yml
Na pasta [raiz-projeto]/scripts/postgres, você encontrará um arquivo chamado `docker-compose.yml`. Abra esse arquivo em um editor de texto e verifique as configurações do serviço PostgreSQL. 


### Passo 3: Rodar o Docker Compose
Abra o terminal, navegue até o diretório [raiz-projeto]/scripts/postgres e execute o comando abaixo para iniciar o PostgreSQL:

```
docker-compose up -d
```

Esse comando vai baixar a imagem do PostgreSQL e iniciar um container com base nas configurações especificadas no arquivo `docker-compose.yml`.

### Passo 4: Acessar o PostgreSQL
Após iniciar o container, você pode acessar o banco de dados PostgreSQL utilizando qualquer ferramenta de cliente PostgreSQL, como `psql`, DBeaver ou PgAdmin. 

- **Host:** localhost
- **Porta:** 5432
- **Usuário:** myuser
- **Senha:** mypassword
- **Banco de Dados:** challenge_db

### Passo 5: Connection String para Entity Framework
Se você for utilizar o Entity Framework e o Migration para criar a base, a connection string deve ser configurada da seguinte maneira:

```
"ConnectionStrings": {
  "DefaultConnection": "Host=localhost;Port=5432;Database=challenge_db;Username=myuser;Password=mypassword"
}
```

### Passo 6: Parar o Container
Para parar o container, execute o seguinte comando:

```
docker-compose down
```

Isso encerrará o container e liberará os recursos utilizados.

