using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text.Json;

namespace TaskManagementSystem
{
    class Program
    {
        static void Main(string[] args)
        {
            TaskManager taskManager = new TaskManager();
            taskManager.LoadTasks();

            bool running = true;
            while (running)
            {
                Console.Clear();
                Console.WriteLine("=== Task Management System ===");
                Console.WriteLine("1. View All Tasks");
                Console.WriteLine("2. Add New Task");
                Console.WriteLine("3. Mark Task as Complete");
                Console.WriteLine("4. Delete Task");
                Console.WriteLine("5. View Tasks by Priority");
                Console.WriteLine("6. Search Tasks");
                Console.WriteLine("7. Exit");
                Console.Write("\nEnter your choice (1-7): ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        taskManager.ViewAllTasks();
                        break;
                    case "2":
                        taskManager.AddNewTask();
                        break;
                    case "3":
                        taskManager.MarkTaskAsComplete();
                        break;
                    case "4":
                        taskManager.DeleteTask();
                        break;
                    case "5":
                        taskManager.ViewTasksByPriority();
                        break;
                    case "6":
                        taskManager.SearchTasks();
                        break;
                    case "7":
                        running = false;
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }

                if (running)
                {
                    Console.WriteLine("\nPress any key to continue...");
                    Console.ReadKey();
                }
            }

            taskManager.SaveTasks();
            Console.WriteLine("Thank you for using the Task Management System!");
        }
    }

    class Task
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DueDate { get; set; }
        public Priority TaskPriority { get; set; }
        public bool IsCompleted { get; set; }
        public DateTime CreatedDate { get; set; }

        public Task()
        {
            CreatedDate = DateTime.Now;
            IsCompleted = false;
        }

        public override string ToString()
        {
            string status = IsCompleted ? "[✓]" : "[ ]";
            return $"{Id}. {status} {Title} (Due: {DueDate.ToShortDateString()}, {TaskPriority})";
        }

        public string GetDetailedInfo()
        {
            string status = IsCompleted ? "Completed" : "Pending";
            return $"ID: {Id}\n" +
                   $"Title: {Title}\n" +
                   $"Description: {Description}\n" +
                   $"Due Date: {DueDate.ToShortDateString()}\n" +
                   $"Priority: {TaskPriority}\n" +
                   $"Status: {status}\n" +
                   $"Created: {CreatedDate.ToShortDateString()}";
        }
    }

    enum Priority
    {
        Low,
        Medium,
        High,
        Urgent
    }

    class TaskManager
    {
        private List<Task> tasks;
        private int nextId;
        private const string FileName = "tasks.json";

        public TaskManager()
        {
            tasks = new List<Task>();
            nextId = 1;
        }

        public void LoadTasks()
        {
            try
            {
                if (File.Exists(FileName))
                {
                    string json = File.ReadAllText(FileName);
                    tasks = JsonSerializer.Deserialize<List<Task>>(json);

                    if (tasks.Count > 0)
                    {
                        nextId = tasks.Max(t => t.Id) + 1;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading tasks: {ex.Message}\nStarting with an empty task list.");
                tasks = new List<Task>();
            }
        }

        public void SaveTasks()
        {
            try
            {
                string json = JsonSerializer.Serialize(tasks, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(FileName, json);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving tasks: {ex.Message}");
            }
        }

        public void ViewAllTasks()
        {
            if (tasks.Count == 0)
            {
                Console.WriteLine("No tasks found.");
                return;
            }

            Console.WriteLine("\n=== All Tasks ===");
            foreach (Task task in tasks)
            {
                Console.WriteLine(task);
            }
        }

        public void AddNewTask()
        {
            Task newTask = new Task();
            newTask.Id = nextId++;

            Console.Write("\nEnter task title: ");
            newTask.Title = Console.ReadLine();

            Console.Write("Enter task description: ");
            newTask.Description = Console.ReadLine();

            Console.Write("Enter due date (MM/DD/YYYY): ");
            if (DateTime.TryParse(Console.ReadLine(), out DateTime dueDate))
            {
                newTask.DueDate = dueDate;
            }
            else
            {
                newTask.DueDate = DateTime.Now.AddDays(7);
                Console.WriteLine($"Invalid date format. Setting due date to {newTask.DueDate.ToShortDateString()}");
            }

            Console.WriteLine("Select priority:");
            Console.WriteLine("1. Low");
            Console.WriteLine("2. Medium");
            Console.WriteLine("3. High");
            Console.WriteLine("4. Urgent");
            Console.Write("Enter your choice (1-4): ");

            string priorityChoice = Console.ReadLine();
            switch (priorityChoice)
            {
                case "2":
                    newTask.TaskPriority = Priority.Medium;
                    break;
                case "3":
                    newTask.TaskPriority = Priority.High;
                    break;
                case "4":
                    newTask.TaskPriority = Priority.Urgent;
                    break;
                default:
                    newTask.TaskPriority = Priority.Low;
                    break;
            }

            tasks.Add(newTask);
            Console.WriteLine("Task added successfully!");
        }

        public void MarkTaskAsComplete()
        {
            if (tasks.Count == 0)
            {
                Console.WriteLine("No tasks found.");
                return;
            }

            ViewAllTasks();
            Console.Write("\nEnter the ID of the task to mark as complete: ");

            if (int.TryParse(Console.ReadLine(), out int taskId))
            {
                Task task = tasks.FirstOrDefault(t => t.Id == taskId);
                if (task != null)
                {
                    task.IsCompleted = true;
                    Console.WriteLine("Task marked as complete!");
                }
                else
                {
                    Console.WriteLine("Task not found.");
                }
            }
            else
            {
                Console.WriteLine("Invalid ID format.");
            }
        }

        public void DeleteTask()
        {
            if (tasks.Count == 0)
            {
                Console.WriteLine("No tasks found.");
                return;
            }

            ViewAllTasks();
            Console.Write("\nEnter the ID of the task to delete: ");

            if (int.TryParse(Console.ReadLine(), out int taskId))
            {
                Task task = tasks.FirstOrDefault(t => t.Id == taskId);
                if (task != null)
                {
                    tasks.Remove(task);
                    Console.WriteLine("Task deleted successfully!");
                }
                else
                {
                    Console.WriteLine("Task not found.");
                }
            }
            else
            {
                Console.WriteLine("Invalid ID format.");
            }
        }

        public void ViewTasksByPriority()
        {
            if (tasks.Count == 0)
            {
                Console.WriteLine("No tasks found.");
                return;
            }

            Console.WriteLine("\nSelect priority to view:");
            Console.WriteLine("1. Low");
            Console.WriteLine("2. Medium");
            Console.WriteLine("3. High");
            Console.WriteLine("4. Urgent");
            Console.WriteLine("5. All (sorted by priority)");
            Console.Write("Enter your choice (1-5): ");

            string choice = Console.ReadLine();
            Priority? selectedPriority = null;

            switch (choice)
            {
                case "1":
                    selectedPriority = Priority.Low;
                    break;
                case "2":
                    selectedPriority = Priority.Medium;
                    break;
                case "3":
                    selectedPriority = Priority.High;
                    break;
                case "4":
                    selectedPriority = Priority.Urgent;
                    break;
            }

            List<Task> filteredTasks;
            if (selectedPriority.HasValue)
            {
                filteredTasks = tasks.Where(t => t.TaskPriority == selectedPriority.Value).ToList();
                Console.WriteLine($"\n=== {selectedPriority.Value} Priority Tasks ===");
            }
            else
            {
                filteredTasks = tasks.OrderByDescending(t => t.TaskPriority).ToList();
                Console.WriteLine("\n=== All Tasks (Sorted by Priority) ===");
            }

            if (filteredTasks.Count == 0)
            {
                Console.WriteLine("No matching tasks found.");
                return;
            }

            foreach (Task task in filteredTasks)
            {
                Console.WriteLine(task);
            }
        }

        public void SearchTasks()
        {
            if (tasks.Count == 0)
            {
                Console.WriteLine("No tasks found.");
                return;
            }

            Console.Write("\nEnter search term: ");
            string searchTerm = Console.ReadLine().ToLower();

            var matchingTasks = tasks.Where(t =>
                t.Title.ToLower().Contains(searchTerm) ||
                t.Description.ToLower().Contains(searchTerm)).ToList();

            if (matchingTasks.Count == 0)
            {
                Console.WriteLine("No matching tasks found.");
                return;
            }

            Console.WriteLine($"\n=== Search Results for '{searchTerm}' ===");
            foreach (Task task in matchingTasks)
            {
                Console.WriteLine(task);
            }
        }
    }
}
