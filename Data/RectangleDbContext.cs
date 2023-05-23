using Microsoft.EntityFrameworkCore;
using RectangleCoordinatesApi.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Data
{
    public class RectangleDbContext : DbContext
    {
        public RectangleDbContext()
        {

        }
        public RectangleDbContext(DbContextOptions<RectangleDbContext> options) : base(options)
        {

        }
        public void SeedData()
        {
            List<Rectangle> Rectangless = new List<Rectangle>();
            // Add 200 rectangle data entries to the Rectangles list
            for (int i = 0; i < 200; i++)
            {
                // Generate random coordinates and dimensions for each rectangle
                Random random = new Random();
                int x = random.Next(0, 100);
                int y = random.Next(0, 100);
                int width = random.Next(1, 10);
                int height = random.Next(1, 10);

                Rectangless.Add(new Rectangle { X = x, Y = y, Width = width, Height = height });
            }
            Rectangles.AddRange(Rectangless);
            SaveChanges();
        }
        public virtual DbSet<Rectangle> Rectangles { get; set; }
    }
}
