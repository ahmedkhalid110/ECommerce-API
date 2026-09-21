# 🛒 E-Commerce RESTful API (.NET 8)

An E-Commerce backend RESTful API built with **ASP.NET Core** following **Clean Architecture** principles.

## 🛠 Tech Stack & Architecture
- **Framework:** .NET 8 / ASP.NET Core Web API
- **Architecture:** Clean Architecture (Core, Application, Infrastructure, API)
- **Database:** Entity Framework Core (SQL Server)
- **Caching & Basket:** Redis
- **Authentication & Security:** ASP.NET Core Identity with JWT (JSON Web Tokens)
- **API Documentation:** Swagger / OpenAPI

## 📂 Solution Structure
- `Core`: Domain entities, interfaces, and specifications.
- `Application`: Business logic, DTOs, Mapping profiles, and Service contracts.
- `Infrastructure`: Data persistence (EF Core, SQL Server) and Caching (Redis).
- `API`: Controllers, Middlewares, Dependency Injection setup, and Swagger config.

## 🚀 Getting Started

### Prerequisites
- .NET 8 SDK
- SQL Server
- Redis Server (or Docker)

### Run Locally
1. Clone the repository:
   ```bash
   git clone [https://github.com/ahmedkhalid110/ECommerce-API.git](https://github.com/ahmedkhalid110/ECommerce-API.git)