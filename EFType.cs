using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using Microsoft.Extensions.Logging;

using System.ComponentModel.DataAnnotations.Schema;

namespace EntityFrameworkCode.Relation
{
    public class Student
    {
        [Key]
        public int StudentId { get; set; }
        public string Name { get; set; }
        public DateTime EnrollmentDate { get; set; }

        // Navigation property for the one-to-one relationship
        public StudentAddress StudentAddress { get; set; }   //One student has many courses
    }


    public class StudentAddress
    {
        [Key, ForeignKey("Student")] // Primary key and foreign key reference
        public int StudentId { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }

        // Navigation property to Student
        public Student Student { get; set; }
    }


    internal class RelationContext : DbContext
    {

        public DbSet<Student> students { get; set; }
        public DbSet<StudentAddress> addresses { get; set; }

        public bool Isconnected = false;


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            string _ip = "127.0.0.1";
            string _name = "Zroot";
            string _username = "root";
            string _passwrod = "123456";
            string _database = "schooldb";
            var optionbuilder = optionsBuilder.UseMySql($"Server={_ip};Database={_name};" +
                $"User={_username};Password={_passwrod};",
                                 new MySqlServerVersion(new Version(6, 0, 2)));






        }

        public async Task<bool> IsDatabaseConnectedAsync()
        {
            try
            {
                // Attempt to open the database connection
                await this.Database.OpenConnectionAsync();
                Console.WriteLine($"Connection Susesfully");
                Isconnected = true;
                return true; // If successful, the connection is established
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Connection failed: {ex.Message}");
                return false; // If there's an exception, the connection is not established
            }
            finally
            {
                // Always ensure to close the connection
                //await this.Database.CloseConnectionAsync();
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)  //model relation
        {
            // Configuring one-to-one relationship
            modelBuilder.Entity<Student>()
                .HasOne(s => s.StudentAddress)
                .WithOne(sa => sa.Student)
                .HasForeignKey<StudentAddress>(sa => sa.StudentId);
        }



    }

    public class RunModel : IDisposable
    {
        private bool disposedValue;

        public void Run()
        {

            using (var context = new RelationContext())
            {


               var isconnected =  context.IsDatabaseConnectedAsync();

                // Ensure database is created
                bool isDatabasecreated = context.Database.EnsureCreated();
                if (isDatabasecreated == true)
                {
                    Console.WriteLine($"Data Base is Not present, Created New Database ..");
                }



                // Create a student with an address
                //data : one hstuident cna enroll in  multiple courdes
                var student = new Student
                {
                    Name = "Kamal Kumar Chanchal",
                    EnrollmentDate = DateTime.Now,
                    StudentAddress = new StudentAddress
                    {
                        Address = "122 Main St",
                        City = "Metropolis",
                        State = "CA"
                    }
                };

                context.students.Add(student);
                try
                {
                    int result = context.SaveChanges();
                    Console.WriteLine(result > 0 ? "Data inserted successfully" : "No data inserted");

        
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message); // Print error message
                }

            }




        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects)
                }

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                disposedValue = true;
            }
        }

        // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        // ~RunModel()
        // {
        //     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        //     Dispose(disposing: false);
        // }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}