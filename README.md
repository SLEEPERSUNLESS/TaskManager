# Task Management System

A simple, console-based task management application built in C# that helps you organize and track your daily tasks.

## Features

- **View All Tasks**: Get a complete list of your pending and completed tasks
- **Add New Tasks**: Create new tasks with title, description, due date, and priority
- **Mark Tasks as Complete**: Update the status of your tasks when they're done
- **Delete Tasks**: Remove tasks that are no longer needed
- **Priority Management**: Organize tasks by priority levels (Low, Medium, High, Urgent)
- **Search Functionality**: Find specific tasks using keywords
- **Persistent Storage**: Tasks are automatically saved between sessions using JSON

## Installation

### Prerequisites
- .NET Core 3.1 SDK or later
- Visual Studio 2019+ or any text editor

### Setup
1. Clone the repository or download the source code
2. Open the solution in Visual Studio or your preferred IDE
3. Build the solution to restore dependencies

```
dotnet build
```

4. Run the application

```
dotnet run
```

## Usage

Upon launching the application, you'll be presented with a menu of options:

```
=== Task Management System ===
1. View All Tasks
2. Add New Task
3. Mark Task as Complete
4. Delete Task
5. View Tasks by Priority
6. Search Tasks
7. Exit
```

### Adding a New Task

Select option 2 and follow the prompts to:
- Enter a task title
- Provide a description
- Set a due date (MM/DD/YYYY format)
- Choose a priority level

### Managing Tasks

- To view all your tasks, select option 1
- To mark a task as complete, select option 3 and enter the task ID
- To delete a task, select option 4 and enter the task ID
- To filter tasks by priority, select option 5
- To search for specific tasks, select option 6

### Task Display Format

Tasks are displayed in the following format:
```
ID. [Status] Title (Due: Due Date, Priority)
```

Where:
- Status is either [✓] for completed tasks or [ ] for pending tasks
- Due Date is shown in short date format
- Priority indicates the task's importance level

## Data Storage

All tasks are stored locally in a `tasks.json` file in the application directory. This file is automatically created the first time you add a task and is updated whenever changes are made.
