# FinRegSeguros

Enterprise Distributed Transactions

## Tecnologias

- .NET 10
- RabbitMQ
- MassTransit
- EF Core
- SQL Server
- DDD
- CQRS
- Saga
- Outbox
- Inbox
- Docker

## Arquitetura

```mermaid
flowchart LR

API --> RabbitMQ

RabbitMQ --> Saga

Saga --> Regulatory

Regulatory --> RabbitMQ

RabbitMQ --> Saga
```