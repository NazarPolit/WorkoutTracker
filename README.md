# Workout Tracker API 🏋️‍♂️

## Description

This repository contains the backend API for a workout tracker application. It allows users to manage workout templates, log their workouts (including exercises and sets), and track their progress. Built with .NET 8, ASP.NET Core Web API, Entity Framework Core, and MS SQL Server.

---

## Features ✨

* **User Authentication & Authorization:** Secure registration and login using JWT tokens. Users can only access their own data.
* **Workout Templates:** Predefined workout plans (e.g., "Full Body", "Push", "Pull", "Legs") with associated exercises.
* **Workout Logging:** Users can create workout logs, add exercises from the directory or templates, and record sets (reps & weight).
* **Exercise Directory:** A comprehensive list of exercises categorized by muscle group.
* **Pagination & Filtering:** Efficiently retrieve lists of workouts, templates, and exercises with pagination and filtering options.
* **Role-Based Access (Admin):** Functionality to manage roles and potentially protect administrative actions (like editing the Exercise Directory).

---

## Technologies Used 🛠️

* **Backend:** ASP.NET Core Web API (.NET 8)
* **Database:** MS SQL Server
* **ORM:** Entity Framework Core 8
* **Authentication:** ASP.NET Core Identity, JWT Bearer Tokens
* **Mapping:** AutoMapper
* **Architecture:** 3-Tier (API Layer, Business Logic Layer, Data Access Layer) with Repository and Unit of Work patterns.

---

## Getting Started 🚀

### Prerequisites

* .NET 8 SDK
* MS SQL Server (e.g., SQL Server Express)
* An IDE like Visual Studio or VS Code

### Setup Instructions

1.  **Clone the repository:**
    ```bash
    git clone [https://github.com/NazarPolit/WorkoutTracker.git](https://github.com/NazarPolit/WorkoutTracker.git)
    cd WorkoutTracker/backend
    ```
2.  **Configure User Secrets:**
    * Right-click on the `Back_WorkoutTracket` project in Visual Studio and select "Manage User Secrets".
    * Add the following configuration, replacing placeholders with your actual values:
        ```json
        {
          "ConnectionStrings": {
            "DefaultConnection": "Server=YOUR_SQL_SERVER_INSTANCE;Database=workouttracker_dev;Trusted_Connection=True;TrustServerCertificate=True;"
          },
          "Jwt": {
            "Key": "YOUR_SUPER_SECRET_JWT_KEY_MINIMUM_32_CHARS",
            "Issuer": "https://localhost:XXXX", // Check your launchSettings.json
            "Audience": "https://localhost:XXXX" // Check your launchSettings.json
          }
        }
        ```
3.  **Apply Database Migrations:**
    Open a terminal in the `backend` folder and run:
    ```bash
    dotnet ef database update --project DataAccess -s Back_WorkoutTracket
    ```
4.  **(Optional) Seed Initial Data:**
    If you want to populate the database with exercise types and workout templates:
    ```bash
    dotnet run --project Back_WorkoutTracket seeddata
    ```
5.  **Run the Application:**
    ```bash
    dotnet run --project Back_WorkoutTracket
    ```
    The API will be available at the address specified in `Properties/launchSettings.json` (e.g., `https://localhost:7157`).

---

## API Endpoints 🗺️

* **`Auth`**: `/api/auth/register`, `/api/auth/login`
* **`ExerciseTypes`**: CRUD operations for the exercise directory (Admin restricted for CUD).
* **`WorkoutTemplates`**: Read operations for workout templates.
* **`Workouts`**: CRUD operations for user workout logs.
* **`WorkoutExercises`**: Operations to add/remove exercises within a workout log.
* **`Sets`**: CRUD operations for sets within a workout exercise.
* **`Admin`**: Endpoints for managing user roles.
* **`Stats`**: (Optional) Endpoints for retrieving workout statistics.
