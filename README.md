# 🍎 NutriTrack Backend: Engenharia de Software com .NET 8

[![.NET 8](https://img.shields.io/badge/.NET-8.0-512bd4?logo=dotnet)](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)
[![Clean Architecture](https://img.shields.io/badge/Architecture-Clean-green)](https://github.com/jasontaylordev/CleanArchitecture)
[![CQRS](https://img.shields.io/badge/Pattern-CQRS-blue)](https://learn.microsoft.com/en-us/azure/architecture/patterns/cqrs)

O **NutriTrack** é um ecossistema backend robusto projetado para o rastreamento nutricional inteligente. O foco central deste projeto não é apenas a funcionalidade, mas a aplicação rigorosa de conceitos avançados de **Engenharia de Software**, **Arquitetura Limpa** e **SOLID**. Este projeto demonstra a capacidade de isolar regras de negócio complexas de ferramentas externas, garantindo manutenibilidade e escalabilidade.

## 🏛️ Arquitetura Macro

O sistema utiliza a **Onion Architecture** (Arquitetura em Cebola), garantindo que o núcleo do sistema (Domínio) seja totalmente independente de frameworks e drivers externos.



### Divisão de Responsabilidades:
* **Domain Layer**: Contém o "coração" do negócio, incluindo **Entities**, **Value Objects** (ex: `NutritionalInfo`) e interfaces de abstração.
* **Application Layer**: Responsável pela orquestração dos casos de uso via **CQRS** (Command Query Responsibility Segregation) com suporte do **MediatR**. Implementa **Pipeline Behaviors** para validação e logging.
* **Infrastructure Layer**: Contém as implementações de persistência com **EF Core (PostgreSQL)**, integrações de APIs externas (Edamam) e mensageria.
* **Presentation Layer**: Web API documentada com **Swagger** e protegida via **JWT Authentication**.

---

## 🚀 Fluxo de Processamento (Case: Meal Creation)

Abaixo, o fluxo detalhado de como o sistema processa uma requisição, demonstrando o uso de padrões de design e resiliência:



1.  **Validation**: A requisição é interceptada pelo **FluentValidation** via MediatR Pipeline.
2.  **Caching (Hints)**: Antes de consultas externas, o sistema utiliza um mecanismo de **Hints (In-Memory Cache)** para otimizar a performance.
3.  **External Fallback**: Em caso de cache miss, a integração com a **API Edamam** é realizada de forma resiliente.
4.  **Persistence**: O estado final é persistido no **PostgreSQL** seguindo o padrão Repository.
5.  **Event-Driven**: Um evento de domínio (`MealCreatedEvent`) é publicado no **RabbitMQ** via **MassTransit** para processamento assíncrono.

---

## 🛠️ Tech Stack

* **Linguagem**: C# 12 / .NET 8
* **ORM**: Entity Framework Core
* **Banco de Dados**: PostgreSQL
* **Mensageria**: RabbitMQ & MassTransit
* **Design Patterns**: CQRS, MediatR, Repository Pattern, Strategy Pattern
* **Bibliotecas Auxiliares**: AutoMapper, FluentValidation, Serilog

---

## ⚖️ Decisões Arquiteturais

* **Por que Clean Architecture?** Para permitir a evolução do software sem que mudanças em tecnologias externas (como o banco de dados) quebrem as regras de negócio centrais.
* **Por que CQRS?** Para separar fluxos de leitura e escrita, facilitando otimizações específicas para cada operação e reduzindo a complexidade dos Handlers.
* **Por que Hints (In-Memory) vs Redis?** Optou-se por cache em memória para este projeto para minimizar a latência e simplificar a infraestrutura, mantendo alta performance para dados nutricionais estáticos.

---

## 👨‍💻 Autor

**Shizuru**
Estudante de **Sistemas de Informação** na **Universidade São Judas Tadeu (USJT)** (Conclusão em 2026).
Foco em desenvolvimento backend escalável e arquitetura de software.

---
