using System;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Collections.Generic;
using EntityFrameworkCode.Relation;
using EntityFrameworkCode.T1;
using EntityFrameworkCode.T2;
class OptionLogin
{
    public string Ip { get; set; }
    public string Name { get; set; }
    public string Username { get; set; }
    public string Passwrod { get; set; }
    public string Database { get; set; }

}
class Program
{

    static void Main(string[] args)
    {
        Console.WriteLine("******** Entity Framework ***************");

        Console.WriteLine("Please choose Mechanism");

        Console.WriteLine("Press  1 : One to One Relation");
        Console.WriteLine("Press  2 : One to Many Relation");

        var input = int.Parse(Console.ReadLine());

        switch (input)
        {
            case 1:
                {
                    var rel = new RelationOneToOne();

                    rel.ControlCRUD();

                    var rel2 = new RelationForeignKey();

                    rel2.CRUDOnReferenceIntegrity();
                }
                break;
            case 2:
                {
                    var contectmanytoone = new EntityFramwworkOnetoMany();
                    EntityFramwworkOnetoMany.Start();


                }
                break;
            default:
                Console.WriteLine("Invalid selection. Please choose either 1 or 2.");
                break;






        }



        //using (var model = new RunModel())
        //{
        //    model.Run();    


        //}






    }


    //static void Main(string[] args)
    //{
    //    Console.WriteLine("******** Entity Framework ***************");

    //    OptionLogin o = new OptionLogin();
    //    o.Ip = "127.0.0.1";
    //    o.Name = "Zroot";
    //    o.Passwrod = "123456";
    //    o.Username = "root";
    //    o.Database = "SchoolDB";

    //    EntityFrameworkTemplate context = new EntityFrameworkTemplate(o);

    //    // Create a new student
    //    var student = new Student
    //    {
    //        FirstName = "John",
    //        LastName = "Doe",
    //        EnrollmentDate = DateTime.Now
    //    };

    //    // Add student to DbSet
    //    context.Students.Add(student);

    //    // Save changes to the database
    //    context.SaveChanges();

    //    //read rows from table
    //    var students =context.Students.ToList();
    //    foreach (var s in students)
    //    {
    //        Console.WriteLine($"ID: {s.StudentId}, Name: {s.FirstName} {s.LastName}");
    //    }


    //    // Find a student to update
    //    var s2 = context.Students.FirstOrDefault(s => s.FirstName == "John");

    //    if (s2 != null)
    //    {
    //        s2.LastName = "Smith";

    //        // Save changes to the database

    //        context.Remove(s2);

    //        context.SaveChanges();
    //    }

    //}


}

public class Student
{
    // Primary key
    //automatically treat properties ending with Id as the primary key.
    public int StudentId { get; set; }

    // Columns in the table
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime EnrollmentDate { get; set; }
}




class EntityFrameworkTemplate :DbContext
{
    /*
     DbContext - The DbContext class is the primary class in EF Core. It is responsible for interacting with the database
     
     */
    private string _ip = string.Empty;
    private string _name = string.Empty;
    private string _username = string.Empty;
    private string _passwrod = string.Empty;
    private string _database = string.Empty;

    public DbSet<Student> Students { get; set; }
    public EntityFrameworkTemplate(OptionLogin optionlogin)
    {
        Console.WriteLine("This class EntityFramework will Handle All Work");
        _ip = optionlogin.Ip;   
        _name = optionlogin.Name;   
        _username = optionlogin.Username;
        _passwrod = optionlogin.Passwrod;   
        _database = optionlogin.Database;

    }


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        //OnConfiguring- This method configures the connection to MySQL database

            //string _ip = "127.0.0.1";
            //string _name = "Zroot";
            //string _username = "root";
            //string _passwrod = "123456";
            //string _database = "SchoolDB";
            optionsBuilder.UseMySql($"Server={_ip};Database={_name};User={_username};Password={_passwrod};",
                                new MySqlServerVersion(new Version(6, 0, 2)));  //6.0.2

    }


}


class EFGeneric : DbContext
{
    /*
     DbContext - The DbContext class is the primary class in EF Core. It is responsible for interacting with the database
     
     */
    private string _ip = string.Empty;
    private string _name = string.Empty;
    private string _username = string.Empty;
    private string _passwrod = string.Empty;
    private string _database = string.Empty;

    public void AddEntity<T>(T entity) where T : class
    {
        Set<T>().Add(entity);
        SaveChanges();
    }

    public IEnumerable<T> GetAllRecord<T>() where T : class
    {
        return Set<T>().ToList();
    }
    // Generic method to find an entity by ID
    public T FindEntityById<T>(object id) where T : class
    {
        return Set<T>().Find(id);
    }

    //public DbSet<Student> Students { get; set; }
    public EFGeneric(OptionLogin optionlogin)
    {
        Console.WriteLine("This class EntityFramework will Handle All Work");
        _ip = optionlogin.Ip;
        _name = optionlogin.Name;
        _username = optionlogin.Username;
        _passwrod = optionlogin.Passwrod;
        _database = optionlogin.Database;

    }


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        //OnConfiguring- This method configures the connection to MySQL database

        //string _ip = "127.0.0.1";
        //string _name = "Zroot";
        //string _username = "root";
        //string _passwrod = "123456";
        //string _database = "SchoolDB";
        optionsBuilder.UseMySql($"Server={_ip};Database={_name};User={_username};Password={_passwrod};",
                            new MySqlServerVersion(new Version(6, 0, 2)));  //6.0.2

    }


}