using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace EntityFrameworkCode.T1
{
    public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public StudentAddress Address { get; set; }
    }

    public class StudentAddress
    {
        public int StudentAddressId { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }

        public int AddressOfStudentId { get; set; }
        public Student Student { get; set; }
    }
    public class SchoolContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string _ip = "127.0.0.1";
            string _name = "db2";
            string _username = "root";
            string _passwrod = "123456";
            string _database = "db2";
            var optionbuilder = optionsBuilder.UseMySql($"Server={_ip};Database={_name};" +
                $"User={_username};Password={_passwrod};",
                                 new MySqlServerVersion(new Version(6, 0, 2)));


        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Student>()
                .HasOne<StudentAddress>(s => s.Address)
                .WithOne(ad => ad.Student)
                .HasForeignKey<StudentAddress>(ad => ad.AddressOfStudentId);
        }

        public DbSet<Student> Students { get; set; }
        public DbSet<StudentAddress> StudentAddresses { get; set; }
    }
    public class RelationOneToOne
    {
        public void InsertStudent()
        {
            using (var context = new SchoolContext())
            {
                var student = new Student
                {
                    Name = "Kamal Kumar",
                    Address = new StudentAddress
                    {
                        Address = "123 Main St",
                        City = "Metropolis",
                        State = "CA",
                        Country = "USA"
                    }
                };

                context.Students.Add(student);
                context.SaveChanges();
                Console.WriteLine("Student inserted successfully.");
            }
        }

        public void GetStudents()
        {
            using (var context = new SchoolContext())
            {
                var students = context.Students.Include(s => s.Address).ToList();
                foreach (var student in students)
                {
                    Console.WriteLine($"Id: {student.Id}, Name: {student.Name}, Address: {student.Address.Address}, City: {student.Address.City}");
                }
            }
        }

        public void UpdateStudent(int studentId, string newName)
        {
            using (var context = new SchoolContext())
            {
                var student = context.Students.Find(studentId);
                if (student != null)
                {
                    student.Name = newName;
                    context.SaveChanges();
                    Console.WriteLine("Student updated successfully.");
                }
                else
                {
                    Console.WriteLine("Student not found.");
                }
            }
        }

        public void DeleteStudent(int studentId)
        {
            using (var context = new SchoolContext())
            {
                var student = context.Students.Include(s => s.Address).FirstOrDefault(s => s.Id == studentId);
                if (student != null)
                {
                    context.Students.Remove(student);
                    context.SaveChanges();
                    Console.WriteLine("Student deleted successfully.");
                }
                else
                {
                    Console.WriteLine("Student not found.");
                }
            }


        }
        public  void ControlCRUD( )
        {
            //var relation = new RelationOneToOne();
            this.InsertStudent(); // Insert a new student
            this.GetStudents(); // Get all students
            this.UpdateStudent(1, "Updated Name"); // Update the student with Id 1
            this.DeleteStudent(1); // Delete the student with Id 1
        }

    }


    public class RelationForeignKey
    {
        public void InsertStudent()
        {
            using (var context = new SchoolContext())
            {
                var student = new Student
                {
                    Name = " Kumar",
                    Address = new StudentAddress
                    {
                        Address = "123 Main St",
                        City = "Metropolis",
                        State = "CA",
                        Country = "USA"
                    }
                };

                context.Students.Add(student);
                context.SaveChanges();
                Console.WriteLine("Student inserted successfully.");
            }
        }

        public void GetStudents()
        {
            using (var context = new SchoolContext())
            {
                var students = context.Students.Include(s => s.Address).ToList();
                foreach (var student in students)
                {
                    Console.WriteLine($"Id: {student.Id}, Name: {student.Name}, Address: {student.Address.Address}, City: {student.Address.City}");
                }
            }
        }

        public void UpdateStudent(int studentId, string newName)
        {
            using (var context = new SchoolContext())
            {
                var student = context.Students.Find(studentId);
                if (student != null)
                {
                    student.Name = newName;
                    context.SaveChanges();
                    Console.WriteLine("Student updated successfully.");
                }
                else
                {
                    Console.WriteLine("Student not found.");
                }
            }
        }

        public void DeleteStudent(int studentId)
        {
            using (var context = new SchoolContext())
            {
                var student = context.Students.Include(s => s.Address).FirstOrDefault(s => s.Id == studentId);
                if (student != null)
                {
                    context.Students.Remove(student);
                    context.SaveChanges();
                    Console.WriteLine("Student deleted successfully.");
                }
                else
                {
                    Console.WriteLine("Student not found.");
                }
            }
        }

        public void CRUDOnReferenceIntegrity()
        {
            this.InsertStudent(); // Insert a new student
            this.GetStudents(); // Get all students
            this.UpdateStudent(3, "Updated Name"); // Update the student with Id 1
            this.DeleteStudent(3); // Delete the student with Id 1
        }

    }

}
