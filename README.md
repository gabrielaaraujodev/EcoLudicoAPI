
# Portuguese Version

<br/>
<br/>

# EcoLúdico - API

*A API responsável por gerenciar os dados e funcionalidades do projeto EcoLúdico.*

## 📑 Índice

- [Sobre o Projeto](#-sobre-o-projeto)
- [Funcionalidades](#-funcionalidades) 
- [Tecnologias Utilizadas](#-tecnologias-utilizadas)
- [Pré-requisitos](#-pré-requisitos)
- [Instalação](#-instalação)
- [Como Usar](#-como-usar)
- [Contato](#-contato)

## 📌 Sobre o Projeto

Este repositório contém a API desenvolvida em .NET 8 que fornece suporte às funcionalidades do projeto **EcoLúdico**. Ele gerencia os dados de usuários, projetos recicláveis, pontos de coleta e outras operações relacionadas, sendo consumido pelo frontend da plataforma.

## ✨ Funcionalidades

* **Cadastro e gerenciamento de usuários**
* **CRUD de projetos recicláveis**
* **CRUD de pontos de coleta**
* **Upload de imagens para projetos e perfis**
* **Integração com banco de dados MySQL**
* **Documentação da API via Swagger**

### 🚀 Tecnologias Utilizadas

[![.NET 8](https://img.shields.io/badge/.NET-512BD4?style=for-the-badge&logo=dotnet&logoColor=white)](https://dotnet.microsoft.com/)  
[![Entity Framework Core](https://img.shields.io/badge/Entity%20Framework%20Core-6DB33F?style=for-the-badge&logo=.net&logoColor=white)](https://docs.microsoft.com/ef/core/)  
[![Pomelo MySQL](https://img.shields.io/badge/Pomelo--MySQL-4479A1?style=for-the-badge&logo=mysql&logoColor=white)](https://github.com/PomeloFoundation/Pomelo.EntityFrameworkCore.MySql)  
[![AutoMapper](https://img.shields.io/badge/AutoMapper-FF6F00?style=for-the-badge&logo=automapper&logoColor=white)](https://automapper.org/)  
[![Swagger](https://img.shields.io/badge/Swagger-85EA2D?style=for-the-badge&logo=swagger&logoColor=black)](https://swagger.io/)  

### 🔧 Pré-requisitos

Antes de começar, você precisa ter instalado em sua máquina:

[![.NET SDK](https://img.shields.io/badge/.NET-512BD4?style=for-the-badge&logo=dotnet&logoColor=white)](https://dotnet.microsoft.com/)  
[![Git](https://img.shields.io/badge/Git-F05032?style=for-the-badge&logo=git&logoColor=white)](https://git-scm.com/)  
[![MySQL](https://img.shields.io/badge/MySQL-005C84?style=for-the-badge&logo=mysql&logoColor=white)](https://www.mysql.com/)  

### 📦 Instalação

```bash
git clone https://github.com/gabrielaaraujodev/EcoLudicoAPI.git
cd EcoLudicoAPI
```

Configure o banco `EcoLudicoDb` no MySQL e edite a connection string no `appsettings.json`.

```bash
dotnet ef database update
dotnet run
```

## 💡 Como Usar

### 1. Configurar a string de conexão com o banco Railway (ou outro banco MySQL na nuvem) via UserSecrets

> **Importante:** Eu utilizei o banco MySQL hospedado no Railway.

1. No terminal, dentro da pasta do projeto, execute:
   ```bash
   dotnet user-secrets init
   ```

2. Depois, adicione sua string de conexão:
   ```bash
   dotnet user-secrets set "ConnectionStrings:DefaultConnection" "Server=...;Database=...;User=...;Password=...;"
   ```
   *(Você pode encontrar essa string no painel do Railway → Connect → MySQL → connection string padrão)*

3. Certifique-se que no `appsettings.json` você tem:
   ```json
   "ConnectionStrings": {
     "DefaultConnection": ""
   }
   ```
   E no `Program.cs` algo como:
   ```csharp
   builder.Configuration.GetConnectionString("DefaultConnection");
   ```

### 2. Rodar a API localmente

```bash
dotnet run
```

O Swagger UI estará disponível em: [https://localhost:port/swagger](https://localhost:port/swagger)

## 📞 Contato

* **Gabriel Araujo** - [LinkedIn](https://www.linkedin.com/in/gabrielslaraujo/)  
* **Frontend do Projeto** - [EcoLudico](https://github.com/gabrielaaraujodev/EcoLudico)  
* **Email** - contatogabrielslaraujo@gmail.com  

---

<br/>
<br/>

# English Version

<br/>
<br/>

# EcoLúdico - API

*The API responsible for managing data and features of the EcoLúdico project.*

## 📑 Table of Contents

- [About the Project](#-about-the-project)
- [Features](#-features) 
- [Technologies Used](#-technologies-used)
- [Prerequisites](#-prerequisites)
- [Installation](#-installation)
- [How to Use](#-how-to-use)
- [Contact](#-contact)

## 📌 About the Project

This repository contains the backend API built with .NET 8 for the **EcoLúdico** platform. It handles user management, recyclable project data, collection points, authentication, and more, serving the frontend application.

## ✨ Features

* **User registration and management**
* **CRUD operations for recycling projects**
* **CRUD operations for collection points**
* **Image upload for projects and profiles**
* **MySQL database integration**
* **API documentation via Swagger**

### 🚀 Technologies Used

[![.NET 8](https://img.shields.io/badge/.NET-512BD4?style=for-the-badge&logo=dotnet&logoColor=white)](https://dotnet.microsoft.com/)  
[![Entity Framework Core](https://img.shields.io/badge/Entity%20Framework%20Core-6DB33F?style=for-the-badge&logo=.net&logoColor=white)](https://docs.microsoft.com/ef/core/)  
[![Pomelo MySQL](https://img.shields.io/badge/Pomelo--MySQL-4479A1?style=for-the-badge&logo=mysql&logoColor=white)](https://github.com/PomeloFoundation/Pomelo.EntityFrameworkCore.MySql)  
[![AutoMapper](https://img.shields.io/badge/AutoMapper-FF6F00?style=for-the-badge&logo=automapper&logoColor=white)](https://automapper.org/)  
[![Swagger](https://img.shields.io/badge/Swagger-85EA2D?style=for-the-badge&logo=swagger&logoColor=black)](https://swagger.io/)  

### 🔧 Prerequisites

Make sure you have installed:

[![.NET SDK](https://img.shields.io/badge/.NET-512BD4?style=for-the-badge&logo=dotnet&logoColor=white)](https://dotnet.microsoft.com/)  
[![Git](https://img.shields.io/badge/Git-F05032?style=for-the-badge&logo=git&logoColor=white)](https://git-scm.com/)  
[![MySQL](https://img.shields.io/badge/MySQL-005C84?style=for-the-badge&logo=mysql&logoColor=white)](https://www.mysql.com/)  

### 📦 Installation

```bash
git clone https://github.com/gabrielaaraujodev/EcoLudicoAPI.git
cd EcoLudicoAPI
```

Setup your MySQL with a database `EcoLudicoDb` and configure `appsettings.json`.

```bash
dotnet ef database update
dotnet run
```

## 💡 How to Use

### 1. Configure the Connection String with Railway (or another MySQL cloud database) via UserSecrets

> **Note:** I used the MySQL database hosted on Railway.

1. From the terminal, inside the project folder:
   ```bash
   dotnet user-secrets init
   ```

2. Add your connection string (from Railway dashboard → Connect):
   ```bash
   dotnet user-secrets set "ConnectionStrings:DefaultConnection" "Server=...;Database=...;User=...;Password=...;"
   ```

3. Ensure `appsettings.json` contains:
   ```json
   "ConnectionStrings": {
     "DefaultConnection": ""
   }
   ```

   And that your `Program.cs` reads it like:
   ```csharp
   builder.Configuration.GetConnectionString("DefaultConnection");
   ```

### 2. Run the API

```bash
dotnet run
```

Swagger UI will be available at: [https://localhost:port/swagger](https://localhost:port/swagger)

## 📞 Contact

* **Gabriel Araujo** - [LinkedIn](https://www.linkedin.com/in/gabrielslaraujo/)  
* **Frontend Repository** - [EcoLudico](https://github.com/gabrielaaraujodev/EcoLudico)  
* **Email** - contatogabrielslaraujo@gmail.com  
