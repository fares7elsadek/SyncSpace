# SyncSpace Project

## Table of Contents

- [Overview](#overview)
- [Features](#features)
- [Architecture](#architecture)
- [Technologies Used](#technologies-used)
- [Controllers and API Functionality](#controllers-and-api-functionality)
- [Testing](#testing)
- [Authentication and Security](#authentication-and-security)
- [SignalR Streaming and Chatting](#signalr-streaming-and-chatting)
- [Setup and Deployment](#setup-and-deployment)
- [Contribution Guidelines](#contribution-guidelines)

## Overview

SyncSpace is a robust social media platform developed using .NET, following Clean Architecture principles, and incorporating advanced features such as CQRS, MediatR, JWT authentication with refresh tokens, real-time communication using SignalR, and Serilog for structured logging.

This project demonstrates my proficiency in designing scalable, maintainable systems with a focus on performance and security.

## Features

- **User Authentication**: Includes registration, login, and refresh token support.
- **Chat Rooms**: Real-time messaging within rooms powered by SignalR.
- **Streaming**: Real-time video streaming in rooms using SignalR.
- **Clean Code**: Follows Clean Architecture principles for separation of concerns.
- **Extensive Logging**: Implements Serilog for logging and debugging.
- **Testing**: Comprehensive unit testing with xUnit.

## Architecture

This project adopts the Clean Architecture pattern:

- **Core Layer**: Contains business logic and domain entities.
- **Application Layer**: Implements CQRS pattern with MediatR and includes validation using Fluent Validation.
- **Infrastructure Layer**: Provides implementations for repository patterns and Unit of Work.
- **API Layer**: Defines controllers and exposes APIs.

### Patterns Used

- **CQRS with MediatR**: For segregating read and write operations.
- **Repository Pattern with Unit of Work**: Ensures encapsulation of data access logic.
- **Dependency Injection**: For managing dependencies.

## Technologies Used

- **Framework**: .NET 9.0
- **Languages**: C#
- **Real-Time Communication**: SignalR
- **Authentication**: JWT, Refresh Tokens
- **Validation**: Fluent Validation
- **Unit Testing**: xUnit
- **Logging**: Serilog
- **API Documentation**: Swagger

## Controllers and API Functionality

### AuthController

- **Endpoints**:
  - `POST /register`: Registers a new user.
  - `POST /login`: Authenticates a user and issues a JWT token.
  - `GET /refreshToken`: Generates a new JWT using a refresh token stored in cookies.

### ChatController

- **Endpoints**:
  - `POST /`: Sends a message to a chat room and broadcasts it.
  - `GET /{roomId}`: Retrieves chat history for a specific room.

### RoomController

- **Endpoints**:
  - `POST /new`: Creates a new chat room.
  - `PUT /update`: Updates an existing room's details.
  - `DELETE /{roomId}`: Deletes a room.
  - `POST /{roomId}/{userId}/add`: Adds a user to a room.
  - `POST /{roomId}/join`: Joins a user to a room.
  - `POST /{roomId}/leave`: Removes a user from a room.
  - `GET /`: Fetches all available rooms.
  - `GET /{roomId}`: Retrieves details of a specific room.

### StreamController

- **Endpoints**:
  - `POST`: Starts a new stream in a room.
  - `PUT`: Changes the stream URL.
  - Additional methods for syncing, pausing, and resuming the stream.

## Testing

- **Framework**: xUnit
- **Tests Covered**:
  - Unit tests for commands and queries.
  - SignalR connection and message broadcasting.
  - Validation rules using Fluent Validation.

## Authentication and Security

- **JWT Tokens**: Used for stateless authentication.
- **Refresh Tokens**: Implements secure token refresh mechanism using cookies.
- **Authorization**: Secures endpoints with role-based authorization.

## SignalR Streaming and Chatting

- Real-time communication using SignalR hubs.
- Broadcasting messages to groups for chat functionality.
- Managing video streaming start, pause, and resume actions.

## Setup and Deployment

1. **Clone Repository**:
   ```bash
   git clone <repository-url>
   ```
2. **Restore Dependencies**:
   ```bash
   dotnet restore
   ```
3. **Database Migration**:
   Update the connection string in `appsettings.json` and run:
   ```bash
   dotnet ef database update
   ```
4. **Run Application**:
   ```bash
   dotnet run
   ```
5. Access Swagger UI for API testing at `http://localhost:<port>/swagger`.

## Contribution Guidelines

- Fork the repository and create a branch for your feature.
- Ensure proper documentation and testing before creating a pull request.
