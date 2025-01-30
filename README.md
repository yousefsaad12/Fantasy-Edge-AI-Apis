
**# Fantasy Premier Edge - Backend**

## Overview

The backend of **Fantasy Premier Edge** is responsible for managing data processing, handling API requests, and integrating with the AI model. It is built using **.NET 8** and follows a clean architecture approach.

## Features

- **User Authentication**: Secure login and registration system using JWT authentication.
- **Fantasy Data Management**: Fetching, storing, and updating player statistics, team data, and gameweek information.
- **AI Integration**: Communicates with the AI model to fetch player performance predictions.
- **Gameweek Data Processing**: Fetches and updates player data weekly to maintain accuracy.
- **Database Optimization**: Utilizes indexing and caching strategies for better performance.

## API Endpoints

### **Authentication**

- `POST /api/auth/register` → User registration
- `POST /api/auth/login` → User login

### **Players**

- `GET /api/players` → Fetch all players
- `GET /api/players/names` → Get player names
- `GET /api/players/predictions` → Fetch AI-predicted player performances


## Tech Stack

- **.NET 8** (Minimal API)
- **Entity Framework Core** (Database ORM)
- **SQL Database** (Data storage)
- **JWT Authentication** (User security)
- **Redis** (Caching layer for faster responses)

## Installation

```sh
git clone https://github.com/YousefSaad25/FantasyPremierEdge-Backend.git
cd FantasyPremierEdge-Backend
dotnet build
dotnet run
```

---
