# 🥗 NutriTrack API

API para gerenciamento nutricional de usuários, permitindo registrar alimentos, acompanhar ingestão diária e controlar metas de calorias.

## 🚀 Tecnologias

- .NET 8
- ASP.NET Core
- PostgreSQL
- Docker
- JWT Authentication
- Clean Architecture
- DDD

## 📦 Funcionalidades

- Cadastro de usuários
- Autenticação com JWT
- Registro de alimentos
- Registro de refeições
- Cálculo de calorias diárias
- Histórico alimentar

## 🏗 Arquitetura

O projeto segue os princípios de **Clean Architecture**:

API  
Application  
Domain  
Infrastructure  

## ▶️ Como rodar o projeto

```bash
git clone https://github.com/Lucca-shizuru/Nutritrack_Backend
cd Nutritrack_Backend
docker compose up

🔐 Autenticação

A API utiliza JWT para autenticação.

Fluxo:
	1.	Registrar usuário
	2.	Fazer login
	3.	Receber token
	4.	Enviar token no header
    
📷 Demonstração

Swagger da API:



Arquitetura do projeto:



