
# MT Backend Challenge

## Informações Gerais
Este projeto é uma API desenvolvida com .NET 8 para resolver um desafio backend. A API utiliza RabbitMQ para filas, PostgreSQL como banco de dados, e Cloudinary para armazenar imagens. O projeto é configurado para rodar em contêineres Docker, facilitando a implantação e gerenciamento dos serviços.

## Tecnologias Utilizadas
- **.NET 8** – Desenvolvimento da API
- **RabbitMQ** – Sistema de filas para comunicação entre processos
- **PostgreSQL** – Banco de dados relacional
- **Cloudinary** – Armazenamento de imagens na nuvem
- **Docker/Docker Compose** – Contêinerização e orquestração de serviços
- **RabbitMQ Management** – Interface web de gerenciamento de filas

## Estrutura da API
- **MT.Backend.Challenge.Api**: Ponto de entrada e implementação dos endpoints
- **MT.Backend.Challenge.Application**: Camada de aplicação para regras de negócio
- **MT.Backend.Challenge.CrossCutting**: Configurações e dependências comuns
- **MT.Backend.Challenge.Domain**: Entidades e contratos de domínio
- **MT.Backend.Challenge.Infrastructure**: Implementações de acesso a dados e integração com serviços externos

## Estrutura dos Controllers
- **DeliveryDriverController.cs**: Gerenciamento de motoristas de entrega
- **MotorcycleController.cs**: Operações relacionadas a motocicletas
- **RentalController.cs**: Funcionalidades de aluguel de veículos

## Como Subir a Aplicação
### Pré-requisitos:
- Docker instalado na máquina


### Passos:
1. Clone o repositório em sua máquina:
   ```
   git clone https://github.com/bisslee/desafio-backend-mt
   cd MT.Backend.Challenge
   ```

2. Execute o Docker Compose para subir os serviços:
   ```
   docker-compose up -d
   ```

3. Acesse os serviços:
   - PostgreSQL: **localhost:5432**
   - RabbitMQ Management: **localhost:15672** (usuário: `guest`, senha: `guest`)

5. A API estará disponível em: **http://localhost:5000**

## Informações de Contato
Autor: Ivana Santos
Email: biss.lee@gmail.com  
