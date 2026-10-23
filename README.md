# Task Manager in C#

A fully functional console-based Task Manager built in C# with SQLite. This application allows users to create, view, filter, update and delete tasks, with support for priorities and due dates.

## Screenshots

![Main Menu](screenshots/Main%20menu%20screenshot.png)
*The main navigation menu.*

![Adding a task](screenshots/Add%20task%20flow.png)
*Creating a task with priority and due date validation.*

![Viewing Tasks](screenshots/Filtered%20viewing%20of%20tasks.png)
*Viewing tasks with status, priority, and formatted dates.*

## ✨ Features

- **CRUD Operations:** Add, View, Update Status, and Delete tasks.
- **Filtering:** Filter tasks by Status (Pending, In Progress, Done).
- **Priority Levels:** Assign Low, Medium, or High priority to tasks.
- **Date Tracking:** Automatically stamps the creation date, allows setting a strict due date (`YYYY-MM-DD`), and stamps the completion date when a task is marked as Done.
- **Bulk Actions:** Clear all completed tasks at once.
- **Data Validation:** Robust input validation prevents crashes and handles invalid dates, numbers, and empty inputs.
- **Database Migrations:** Upgraded the SQLite schema safely to include Priority and Date columns without losing data.

## 🛠️ Tech Stack

- **Language:** C# (.NET 8)
- **Database:** SQLite (via `Microsoft.Data.Sqlite`)
- **Tools:** VS Code, DB Browser for SQLite, Git

## 🚀 How to Run

### Prerequisites
- [.NET 8 SDK](https://dotnet.microsoft.com/download) or later
- Git

### Steps
1. Clone the repository:
   ```bash
   git clone https://github.com/Meruki-ui/TaskManager.git
   cd TaskManager

2. Run the application:
    ```bash
    dotnet run --project src/TaskManager.App


## What I Learned
Building this project was a massive step up from a basic calculator. I learned how to:

Architect a C# application using Separation of Concerns (Models, Data/Repository, and UI layers).

Perform full CRUD operations using raw SQLite commands via ADO.NET.

Use SQL parameters to prevent SQL injection.

Handle schema migrations using ALTER TABLE to add new features (Priority and Dates) to an existing database.

Validate user input safely, including strict DateTime.TryParseExact formatting and null-handling for database reads (IsDBNull).

Refactor redundant code (combining MarkAsDone and MarkAsInProgress into a single UpdateTaskStatus method).

## Author
Melquisedeque Fialho

- Linkedin: [www.linkedin.com/in/melquisedeque-oliveira-253ba42b8]
- GitHub: @Meruki-ui

---