🌐 CDE-UTC-Microservice

🚀 Welcome to the CDE-UTC Microservice Project!This repository is dedicated to the development and deployment of a highly scalable and modular web application, leveraging the power of Microservices Architecture.

📖 Overview

Microservices architecture enables this project to be broken into smaller, independent services, each focusing on a specific business capability. This design ensures:

✅ Scalability: Services can be scaled individually.

✅ Flexibility: Developers can work on different services simultaneously.

✅ Resilience: Failure in one service doesn't bring down the entire system.

📂 Project Structure

├── Services
│   ├── Auth.Service
│   ├── User.Service
│   ├── JobTitle.Service
│   └── Notification.Service
├── Gateway
├── SharedLibraries
├── Infrastructure
└── Documentation

Key Folders:

Services: Contains all microservices (e.g., authentication, user management, etc.).

Gateway: API gateway for routing requests to the appropriate services.

SharedLibraries: Reusable libraries for common functionalities.

Infrastructure: Deployment scripts and infrastructure configuration.

Documentation: All related docs (API specs, architecture diagrams, etc.).

⚙️ Technologies Used

Tech

Description

ASP.NET Core

Backend framework for microservices

SQL Server

Database solution

Docker

Containerization

Kubernetes

Orchestration and scaling

RabbitMQ

Messaging between services

Swagger

API Documentation

Next.js

Frontend Framework

NGINX

Reverse Proxy for routing

🖇️ How to Get Started

1. Clone the repository

git clone https://github.com/PhucLa1/CDE-UTC-Microservice.git
cd CDE-UTC-Microservice

2. Run services locally

Make sure you have Docker and Docker Compose installed.

docker-compose up --build

3. Access the application

API Gateway: http://localhost:8000

Swagger Documentation: http://localhost:8000/swagger

🚧 Development Guidelines

Code Style: Follow the shared coding conventions and guidelines.

Branching Strategy: Use feature/, bugfix/, and hotfix/ prefixes.

Pull Requests: Ensure PRs are reviewed by at least one team member before merging.

Testing: Add unit and integration tests for all new features.

📊 Microservices Overview

Service

Description

Port

Auth.Service

Handles authentication and authorization

5050

Event.Service

Sends email and in-app notifications

5003

Project.Service


📈 System Architecture



💬 Contributing

We welcome contributions from everyone! Please follow these steps to contribute:

Fork the repository.

Create a new branch.

Commit your changes.

Open a pull request.

🛡️ License

This project is licensed under the MIT License. See the LICENSE file for details.

📞 Contact

For any questions or support, please contact:

Email: phucminhbeos@gmail.com

GitHub Issues: Create an Issue

✨ Happy Coding!


