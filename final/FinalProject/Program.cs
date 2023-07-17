class MainProgram
{
    static void Main(string[] args)
    {
        TaskManager taskManager = new TaskManager();


        taskManager.DisplayTasksForUser(1);

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

    }
}


public class Project : Task
{
    public List<Task> Tasks { get; set; }
    public DateTime ReminderDate { get; set; }

    public override void DisplayInfo()
    {
     
    }
}


public class User
{
    public int Id { get; set; }
    public string Name { get; set; }
    public List<Task> Tasks { get; set; }

    public void DisplayTasks()
    {

    }
}


public class TaskManager
{
    public List<User> Users { get; set; }

    public TaskManager()
    {
        Users = new List<User>();
    }

    public void AddUser(User user)
    {
        Users.Add(user);
    }

    public void DisplayTasksForUser(int userId)
    {
       
    }
}


public class SingleTask : Task
{
    public string Location { get; set; }

    public override void DisplayInfo()
    {
      
    }
}


public class RecurringTask : Task
{
    public int RecurrenceInterval { get; set; }

    public override void DisplayInfo()
    {
    }
}


public class Reminder : Task
{
    public DateTime ReminderDate { get; set; }

    public override void DisplayInfo()
    {

    }
}


public class Users
{
    public int Id { get; set; }
    public string Name { get; set; }
    public List<Task> Tasks { get; set; }

}
