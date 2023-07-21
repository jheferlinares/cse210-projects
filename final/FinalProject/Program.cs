class Program
{
    static void Main(string[] args)
    {
        TaskManager taskManager = new TaskManager();

        User user1 = new User { Id = 1, Name = "John Doe", Tasks = new List<Task>() };
        User user2 = new User { Id = 2, Name = "Jane Smith", Tasks = new List<Task>() };
        taskManager.AddUser(user1);
        taskManager.AddUser(user2);

        Project project1 = new Project { Id = 1, Title = "Project A", Tasks = new List<Task>() };
        Reminder reminder1 = new Reminder { Id = 2, Title = "Reminder 1", Description = "Remember to buy groceries", DueDate = DateTime.Now.AddDays(2), Completed = false, ReminderDate = DateTime.Now.AddDays(1) };
        project1.Tasks.Add(reminder1);
        taskManager.AddProject(project1);

        user1.Tasks.Add(reminder1);

        taskManager.DisplayTasksForUser(1);
        taskManager.DisplayTasksForProject(1);

        Console.ReadLine();
    }
}


public class Task
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime DueDate { get; set; }
    public bool Completed { get; set; }

    public virtual void DisplayInfo()
    {
        Console.WriteLine($"Task ID: {Id}");
        Console.WriteLine($"Title: {Title}");
        Console.WriteLine($"Description: {Description}");
        Console.WriteLine($"Due Date: {DueDate}");
        Console.WriteLine($"Completed: {(Completed ? "Yes" : "No")}");
    }
}



public class Project : Task
{
    public List<Task> Tasks { get; set; }

    public override void DisplayInfo()
    {
        base.DisplayInfo();
        Console.WriteLine("Associated Tasks:");
        foreach (Task task in Tasks)
        {
            task.DisplayInfo();
            Console.WriteLine();
        }
    }
}



public class User
{
    public int Id { get; set; }
    public string Name { get; set; }
    public List<Task> Tasks { get; set; }

    public void DisplayTasks()
    {
        Console.WriteLine($"User: {Name}");
        Console.WriteLine("Tasks:");
        foreach (Task task in Tasks)
        {
            task.DisplayInfo();
            Console.WriteLine();
        }
    }
}



public class TaskManager
{
    public List<User> Users { get; set; }
    public List<Project> Projects { get; set; }

    public TaskManager()
    {
        Users = new List<User>();
        Projects = new List<Project>();
    }

    public void AddUser(User user)
    {
        Users.Add(user);
    }

    public void AddProject(Project project)
    {
        Projects.Add(project);
    }

    public void DisplayTasksForUser(int userId)
    {
        User user = Users.FirstOrDefault(u => u.Id == userId);
        if (user != null)
        {
            user.DisplayTasks();
        }
    }

    public void DisplayTasksForProject(int projectId)
    {
        Project project = Projects.FirstOrDefault(p => p.Id == projectId);
        if (project != null)
        {
            project.DisplayInfo();
        }
    }
}



public class SingleTask : Task
{
    public string Location { get; set; }

    public override void DisplayInfo()
    {
        base.DisplayInfo();
        Console.WriteLine($"Location: {Location}");
    }
}


public class RecurringTask : Task
{
    public int RecurrenceInterval { get; set; }

    public override void DisplayInfo()
    {
        base.DisplayInfo();
        Console.WriteLine($"Recurrence Interval: {RecurrenceInterval} days");
    }
}



public class Reminder : Task
{
    public DateTime ReminderDate { get; set; }

    public override void DisplayInfo()
    {
        base.DisplayInfo();
        Console.WriteLine($"Reminder Date: {ReminderDate}");
    }
}



public class User
{
    public int Id { get; set; }
    public string Name { get; set; }
    public List<Task> Tasks { get; set; }

    public void DisplayTasks()
    {
        Console.WriteLine($"User ID: {Id}");
        Console.WriteLine($"Name: {Name}");
        Console.WriteLine("Tasks:");
        foreach (Task task in Tasks)
        {
            task.DisplayInfo();
            Console.WriteLine();
        }
    }
}

