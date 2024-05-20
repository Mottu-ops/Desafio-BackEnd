# Projeto: Motorcycle Rental
## Este projeto, foi desenvolvido em uma arquitetuar de micro serviço.

Ao total, foram desenvolvidos 11 projetos, são eles:

- 2 - Serviços principais: 
    - AdminManagementService e DeliveryManagementService
        - Cada serviço principal são compostos por 2 projetos: Api e Service.
- 2 - Serviços para consumir filas com o RabbitMq:
    - MotorcycleConsumer - utilizado para consumir as mensagens enviadas pelo serviço: AdminManagementService
                           Após processar a mensagem, o serviço guardará a informação em uma base de dados MongoDb, para consultas futuras e enviará uma mensagem para a fila de notificação "MailServiceConsumer"
    - MailServiceConsumer - utilizado para consumir fila Notify e disparar emails, para 1 ou um lista de emails, previamente cadastrados. 
- 1 - Serviço de Authentication - Responsável por criar os usuarios que terão acesso a aplicação.
- 1 - Projeto do Domain - Responsável por armazenar as informações principais do negocio, tais como Entitidades que serão utilizadas pelo projeto e interfaces, 
que ditarão o comportamento dos projetos principais
- 1 - Infraestructure - Responsável por gerenciar conexões com os principais serviços utilizado pela aplicação, como por ex: bancos e serviços externos.
- 1 - Shared/Api.Core - Serviço responsável por guardar configurações que poderão ser utilizadas pelos demais projetos.
- 1 - Shared/RabbitMqMessage - Serviço responsável, por disponibillizar uma interface, para que os outros projetos, possam publicar mensagens para as filas do rabbitMq.
    
## Instruções para execução dos Projetos.
### OBS: Para a exucução de projeto, será necessário a instlação prévia do Docker Desktop, para a utilização do Docker-Compose

### Para executar, você poderá optar em executar o projeto que desejar, ou executar todos os projetos.

### Obs: Para executar o serviço: AdminManagementService, obrigatoriamente, você deverá executar os 2 serviços Consumer, para que possa verificar o funcionamento da coreografia.
#### Lembrando para receber notificação, basta cadatrar o seu email, dentro o appSettings (EmailList) do serviço: MotorcycleRental.MotorcycleConsumer.

##### Let's bora!

##### Para executar todos os serviços:
- Antes de qualquer configuração, serão necessário primeiro buildar a aplicação.
- Em seguida, deverá clicar com o botão direito no projeto, selecionar a opção: Propriedades -> Multiplie Start Projects -> Selecionar os seguintes projetos para iniciar:
    - AdminManagementService.Api
    - DeliveryManagementService.Api
    - Authentication
    - MotorcycleConsumer
    - MailServiceConsumer
Depois, basta clicar em Aplicar, para que todos os serviçoes possam executar. 
- Feito isso
- Agora, você precisará abrir um pronpt de comando: 
    - Deverá clicar com o botão direito no projeto, selecionar a opção: Open In terminal
    - Agora, basta digitar o comando: docker-compose up e apertar "Enter"

### Obs: Todas as bases serão criadas no momento em que os 2 principais serviçoes forem executados. Por precaução, as migrations, serão aplicadas nas 2 aplicações.

### No serviço de Authentication, poderá registrar 2 tipos de usuario: Admin e Entregadores. Ao cadastrar um novo Entregador, o Authenticario, enviar uma mensagem para a fila de NewUserRegister, que por sua vez, um serviço que tá rodando em BackgroundService no serviço: AdminManagementService, que receberá a mensagem e efetuará o cadastro do novo entregador.

#### Qualquer dúvida, meus contatos: 
##### What's: 11-95925-0776
##### Email: cleber.trindade.net@gmail.com




#### *** Detalhes sobre o desafio ***


# Desafio backend Mottu.
Seja muito bem-vindo ao desafio backend da Mottu, obrigado pelo interesse em fazer parte do nosso time e ajudar a melhorar a vida de milhares de pessoas.

## Instruções
- O desafio é válido para diversos níveis, portanto não se preocupe se não conseguir resolver por completo.
- A aplicação só será avaliada se estiver rodando, se necessário crie um passo a passo para isso.
- Faça um clone do repositório em seu git pessoal para iniciar o desenvolvimento e não cite nada relacionado a Mottu.
- Após finalização envie um e-mail para o recrutador informando o repositório para análise.
  
## Requisitos não funcionais 
- A aplicação deverá ser construida com .Net utilizando C#.
- Utilizar apenas os seguintes bancos de dados (Postgress, MongoDB)
    - Não utilizar PL/pgSQL
- Escolha o sistema de mensageria de sua preferencia( RabbitMq, Sqs/Sns , Kafka, Gooogle Pub/Sub ou qualquer outro)

## Aplicação a ser desenvolvida
Seu objetivo é criar uma aplicação para gerenciar aluguel de motos e entregadores. Quando um entregador estiver registrado e com uma locação ativa poderá também efetuar entregas de pedidos disponíveis na plataforma.
### Casos de uso
- Eu como usuário admin quero cadastrar uma nova moto.
  - Os dados obrigatórios da moto são Identificador, Ano, Modelo e Placa
  - A placa é um dado único e não pode se repetir.
  - Quando a moto for cadastrada a aplicação deverá gerar um evento de moto cadastrada
    - A notificação deverá ser publicada por mensageria.
    - Criar um consumidor para notificar quando o ano da moto for "2024"
    - Assim que a mensagem for recebida, deverá ser armazenada no banco de dados para consulta futura.
- Eu como usuário admin quero consultar as motos existentes na plataforma e conseguir filtrar pela placa.
- Eu como usuário admin quero modificar uma moto alterando apenas sua placa que foi cadastrado indevidamente
- Eu como usuário admin quero remover uma moto que foi cadastrado incorretamente, desde que não tenha registro de locações.
- Eu como usuário entregador quero me cadastrar na plataforma para alugar motos.
    - Os dados do entregador são( identificador, nome, cnpj, data de nascimento, número da CNHh, tipo da CNH, imagemCNH)
    - Os tipos de cnh válidos são A, B ou ambas A+B.
    - O cnpj é único e não pode se repetir.
    - O número da CNH é único e não pode se repetir.
- Eu como entregador quero enviar a foto de minha cnh para atualizar meu cadastro.
    - O formato do arquivo deve ser png ou bmp.
    - A foto não poderá ser armazenada no banco de dados, você pode utilizar um serviço de storage( disco local, amazon s3, minIO ou outros).
- Eu como entregador quero alugar uma moto por um período.
    - Os planos disponíveis para locação são:
        - 7 dias com um custo de R$30,00 por dia
        - 15 dias com um custo de R$28,00 por dia
        - 30 dias com um custo de R$22,00 por dia
        - 45 dias com um custo de R$20,00 por dia
        - 50 dias com um custo de R$18,00 por dia
    - A locação obrigatóriamente tem que ter uma data de inicio e uma data de término e outra data de previsão de término.
    - O inicio da locação obrigatóriamente é o primeiro dia após a data de criação.
    - Somente entregadores habilitados na categoria A podem efetuar uma locação
- Eu como entregador quero informar a data que irei devolver a moto e consultar o valor total da locação.
    - Quando a data informada for inferior a data prevista do término, será cobrado o valor das diárias e uma multa adicional
        - Para plano de 7 dias o valor da multa é de 20% sobre o valor das diárias não efetivadas.
        - Para plano de 15 dias o valor da multa é de 40% sobre o valor das diárias não efetivadas.
    - Quando a data informada for superior a data prevista do término, será cobrado um valor adicional de R$50,00 por diária adicional.
    

## Diferenciais 🚀
- Testes unitários
- Testes de integração
- EntityFramework e/ou Dapper
- Docker e Docker Compose
- Design Patterns
- Documentação
- Tratamento de erros
- Arquitetura e modelagem de dados
- Código escrito em língua inglesa
- Código limpo e organizado
- Logs bem estruturados
- Seguir convenções utilizadas pela comunidade
  

