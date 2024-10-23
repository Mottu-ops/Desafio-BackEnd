# README - Instruções para instalar e rodar RabbitMQ usando Docker

### Passo 1: Instalar Docker
Certifique-se de que o Docker está instalado e rodando em seu sistema. Se ainda não estiver instalado, siga as instruções de instalação disponíveis em [https://docs.docker.com/get-docker/](https://docs.docker.com/get-docker/).

### Passo 2: arquivo docker-compose.yml
Na pasta [raiz-projeto]/scripts/sonar-local, você encontrará um arquivo chamado `docker-compose.yml`.

### Passo 3: Rodar o Docker Compose
Abra o terminal, navegue até o diretório onde o arquivo `docker-compose.yml` está salvo e execute o comando abaixo para iniciar o RabbitMQ:

```
docker-compose up -d
```

Esse comando vai baixar a imagem do RabbitMQ e iniciar um container com base nas configurações especificadas no arquivo `docker-compose.yml`.

### Passo 4: Acessar o RabbitMQ
Após iniciar o container, você pode acessar a interface de gerenciamento do RabbitMQ através do navegador, utilizando o seguinte endereço:

- **URL:** [http://localhost:15672](http://localhost:15672)

- **Usuário padrão:** myuser
- **Senha padrão:** mypassword

### Passo 5: Parar o Container
Para parar o container, execute o seguinte comando:

```
docker-compose down
```

Isso encerrará o container e liberará os recursos utilizados.
