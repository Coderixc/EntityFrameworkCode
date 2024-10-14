using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace EntityFrameworkCode.T2
{
    public class Sector
    {
        public int SectorId { get; set; }
        public string SectorName { get; set; }

        // Navigation property to representRELATION -->  the one-to-many relationship
        public ICollection<Stock> Stocks { get; set; }
    }
    public class Stock
    {
        public int StockId { get; set; }
        public string StockName { get; set; }

        // Foreign key for the Sector
        public int SectorId { get; set; }
        public Sector Sector { get; set; }
    }

    public  class RelationOneToManyContext:DbContext
    {
        public DbSet<Stock> Stock { get;set; }

        public DbSet<Sector> Sector { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string _ip = "127.0.0.1";
            string _name = "Stocksandsectors";
            string _username = "root";
            string _passwrod = "123456";
            //string _database = "Stocksandsectors";
            var optionbuilder = optionsBuilder.UseMySql($"Server={_ip};Database={_name};" +
                $"User={_username};Password={_passwrod};",
                                 new MySqlServerVersion(new Version(6, 0, 2)));

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Define the one-to-many relationship between Sector and Stock
            modelBuilder.Entity<Sector>()
                .HasMany(s => s.Stocks)        // A sector has many stocks
                .WithOne(st => st.Sector)      // Each stock belongs to one sector
                .HasForeignKey(st => st.SectorId); // Foreign key in Stock entity

            base.OnModelCreating(modelBuilder);
        }

    }
    public static class DatabaseInitializer
    {
        public static void Seed(RelationOneToManyContext context)
        {
            //Seed" is commonly used in programming to mean "populate initial data." 
            if (!context.Sector.Any())
            {
                var technologySector = new Sector
                {
                    SectorName = "Technology",
                    Stocks = new List<Stock>
                {
                    new Stock { StockName = "TCS" },
                    new Stock { StockName = "Infosys" }
                }
                };

                var financeSector = new Sector
                {
                    SectorName = "Finance",
                    Stocks = new List<Stock>
                {
                    new Stock { StockName = "HDFC Bank" },
                    new Stock { StockName = "ICICI Bank" }
                }
                };

                context.Sector.AddRange(technologySector, financeSector);
                context.SaveChanges();
            }
        }
    }

    class EntityFramwworkOnetoMany
    {
        public static void Start()
        {
            using var context = new RelationOneToManyContext();

            // Seed database with initial data
            DatabaseInitializer.Seed(context);

            // Query and display sectors and their stocks
            var sectors = context.Sector.Include(s => s.Stocks).ToList();
            foreach (var sector in sectors)
            {
                Console.WriteLine($"Sector: {sector.SectorName}");
                foreach (var stock in sector.Stocks)
                {
                    Console.WriteLine($"  - Stock: {stock.StockName}");
                }
            }



            // Display all sectors and their stocks
            DisplaySectorsWithStocks(context);

            // Example CRUD operations
            CreateSector(context);
            UpdateStock(context);
            DeleteStock(context);

        }


        // Display sectors with their stocks
        private static void DisplaySectorsWithStocks(RelationOneToManyContext context)
        {
            var sectors = context.Sector.Include(s => s.Stocks).ToList();
            foreach (var sector in sectors)
            {
                Console.WriteLine($"Sector: {sector.SectorName}");
                foreach (var stock in sector.Stocks)
                {
                    Console.WriteLine($"  - Stock: {stock.StockName}");
                }
            }
        }

        // Create a new sector
        private static void CreateSector(RelationOneToManyContext context)
        {
            var newSector = new Sector
            {
                SectorName = "Healthcare",
                Stocks = new List<Stock>
                {
                    new Stock { StockName = "Apollo Hospitals" },
                    new Stock { StockName = "Fortis Healthcare" }
                }
            };

            context.Sector.Add(newSector);
            context.SaveChanges();

            Console.WriteLine("New sector created: " + newSector.SectorName);
        }

        // Update an existing stock
        private static void UpdateStock(RelationOneToManyContext context)
        {
            var stockToUpdate = context.Stock.FirstOrDefault(s => s.StockName == "TCS");
            if (stockToUpdate != null)
            {
                stockToUpdate.StockName = "Tata Consultancy Services"; // Renaming TCS
                context.SaveChanges();
                Console.WriteLine("Updated stock name to: " + stockToUpdate.StockName);
            }
        }

        // Delete a stock
        private static void DeleteStock(RelationOneToManyContext context)
        {
            var stockToDelete = context.Stock.FirstOrDefault(s => s.StockName == "ICICI Bank");
            if (stockToDelete != null)
            {
                context.Stock.Remove(stockToDelete);
                context.SaveChanges();
                Console.WriteLine("Deleted stock: " + stockToDelete.StockName);
            }
        }
    }

}
