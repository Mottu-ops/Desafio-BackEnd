# Desafio backend Mottu.
Seja muito bem-vindo ao desafio backend da Mottu, obrigado pelo interesse em fazer parte do nosso time e ajudar a melhorar a vida de milhares de pessoas.
## Instruções
- A vaga é presencial em São Paulo no bairro do Butantã.
- O desafio é válido para diversos níveis, portanto não se preocupe se não conseguir resolver por completo.
- A aplicação só será avaliada se estiver rodando, se necessário crie um passo a passo para isso.
- Faça um clone do repositório em seu git pessoal para iniciar o desenvolvimento e não cite nada relacionado a Mottu.
- Após finalização envie um e-mail para o recrutador informando o repositório para análise.
  
## Requisitos não funcionais 
- A aplicação deverá ser construida com .Net utilizando C#.
- Utilizar apenas os seguintes bancos de dados ( Postgress, MongoDB)
- Escolha o sistema de mensageria de sua preferencia( RabbitMq, Sqs/Sns , Kafka, Gooogle Pub/Sub ou qualquer outro)

## Aplicação a ser desenvolvida
Seu objetivo é criar uma aplicação para gerenciar aluguel de motos e entregadores. Quando um entregador estiver registrado e com uma locação ativa poderá também efetuar entregas de pedidos disponíveis na plataforma.
### Casos de uso
- Eu como usuário admin quero cadastrar uma nova moto.
  - Os dados obrigatórios da moto são Identificador, Ano, Modelo e Placa
  - A placa é um dado único e não pode se repetir.
    
- Eu como usuário admin quero consultar as motos existentes na plataforma e conseguir filtrar pela placa.
- Eu como usuário admin quero modificar uma moto alterando apenas sua placa que foi cadastrado indevidamente.
- Eu como usuário admin quero remover uma moto que foi cadastrado incorretamente, desde que não tenha registro de locações.
- Eu como usuário entregador quero me cadastrar na plataforma para alugar motos.
  - Os dados do entregador são( identificador, nome, cnpj, data de nascimento, numero da cnh, tipo da cnh, imagemCnh)
  - Os tipos de cnh válidos são A, B ou ambas A+B.
  - O cnpj é único e não pode se repetir.
  - O número da CNH é único e não pode se repetir.
- Eu como entregador quero enviar a foto de minha cnh para atualizar meu cadastro.
  - O formato do arquivo deve ser png ou bmp.
  - A foto não poderá ser armazenada no banco de dados, você pode utilizar um storage( disco local, amazon s3, minIO ou outros).
- Eu como entregador quero alugar uma moto por um período.
  - Os planos disponíveis para locação são:
    - 7 dias com um custo de R$30,00 por dia
    - 15 dias com um custo de R$28,00 por dia
    - 30 dias com um custo de R$22,00 por dia
  - A locação obrigatóriamente tem que ter uma data de inicio e uma data de término e outra data de previsão de término.
  - O inicio da locação obrigatóriamente é o primeiro dia após a data de criação.
  - O entregador só conseguirá concluir na locação caso exista motos disponíveis.
  - Somente entregadores habilitados na categoria A podem efetuar uma locação
- Eu como entregador quero informar a data que irei devolver a moto e consultar o valor total da locação.
  - Quando a data informada for inferior a data prevista do término, será cobrado o valor das diárias e uma multa adicional
    - Para plano de 7 dias o valor da multa é de 20% sobre o valor das diárias não efetivadas.
    - Para plano de 15 dias o valor da multa é de 40% sobre o valor das diárias não efetivadas.
    - Para plano de 30 dias o valor da multa é de 60% sobre o valor das diárias não efetivadas.
  - Quando a data informada for superior a data prevista do término, será cobrado um valor adicional de R$50,00 por diária adicional.
- Eu como admin quero cadastrar um pedido na plataforma e disponibilizar para os entregadores aptos efetuarem a entrega.
  - Os dados obrigatórios do pedido são: identificador, data de criacao, valor da corrida, situacao.
  - As situações válidas são disponivel, aceito e entregue.
  - Quando o pedido entrar na plataforma a aplicação deverá notificar os entregadores sobre a existencia desse pedido.
    - A notificação deverá ser publicada por mensageria.
    - Somente entregadores com locação ativa e que não estejam com um pedido já aceito deverão ser notificados.
  - Criar um consumidor para notificação de pedido disponível.
    - Assim que a mensagem for recebida, deverá ser armazenada no banco de dados para consulta futura.
- Eu como admin quero consultar todos entregadoeres que foram notificados de um pedido.
- Eu como entregador quero aceitar um pedido.
  - Somente entregadores que tenham sido notificados podem aceitar o pedido.
- Eu como entregador quero efetuar a entrega do pedido.
      

## Diferenciais 🚀
- Testes unitários
- Testes de integração
- EntityFramework e/ou Dapper
- Docker e Docker Compose
- Design Patterns
- Documentação
- Tratamento de erros
- Arquitetura e modelagem de dados
- Código escrito em linga inglesa
- Código limpo e organizado
- Logs bem estruturados
- Seguir convenções utilizadas pela comunidade
  

