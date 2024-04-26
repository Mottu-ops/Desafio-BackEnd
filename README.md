# Introdução

Olá,

Gostaria de salientar, antes de prosseguir com a análise do meu código, que este foi inteiramente escrito na quinta-feira (24/05). Conforme acordado com Brian Ripper, o prazo para entrega do projeto era de dois dias, portanto, peço que isso seja levado em consideração.

No tempo disponível, tentei entregar o melhor trabalho possível, priorizando a implementação de funcionalidades e realização de testes extensivos, sempre mantendo a organização do código.

Estarei implementando alguns testes de integração até às 14 horas do dia 25/04, cumprindo, assim, parte dos requisitos. Estou à disposição para discutirmos mais sobre o código e tecnologias envolvidas, caso seja necessário.

Agradeço a oportunidade e aguardo feedback.

## Intruções para execução

Para executar o projeto, basta clonar o repositório e executar o comando `docker compose up -d` para subir o container do banco de dados e da aplicação.
Abra o navegador e acesse `http://lcalhost:5001/swagger/index.html` para visualizar a aplicação.

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