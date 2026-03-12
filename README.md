# Task Manager - Full-Stack Blazor App

## Overview

Full-stack Task Manager with .NET 9 Blazor Server, MongoDB, TailwindCSS.

## Features

- CRUD operations for tasks
- Filter by status & priority
- Search by title/description
- Responsive design
- Repository pattern, DI, async

## Setup

1. MongoDB on `localhost:27017`
2. `dotnet restore`
3. `dotnet build`
4. `dotnet run --project TaskManager`

## Structure

```
TaskManager/
├── Models/Task.cs
├── Data/ (MongoDbContext, TaskRepository)
├── Services/TaskService
├── Program.cs (DI)
├── Components/Pages/Home.razor (Dashboard)
└── Components/Shared/TaskCard.razor
```

## Tech

- .NET 9 Blazor Server
- MongoDB.Driver
- FluentValidation
- TailwindCSS

UI ready for tasks (needs MongoDB), extend with CRUD forms.
