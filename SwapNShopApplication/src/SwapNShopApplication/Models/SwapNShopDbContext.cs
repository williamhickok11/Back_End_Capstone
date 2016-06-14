using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SwapNShopApplication.Models
{
    public class SwapNShopDbContext : DbContext
    {
        public SwapNShopDbContext(DbContextOptions<SwapNShopDbContext> options)
            : base(options)
        { }

        public DbSet<Category> Category { get; set; }
        public DbSet<Comment> Comment { get; set; }
        public DbSet<CommentList> CommentList { get; set; }
        public DbSet<Equipment> Equipment { get; set; }
        public DbSet<Musician> Musician { get; set; }
        public DbSet<Picture> Picture { get; set; }
        public DbSet<PictureList> PictureList { get; set; }
        public DbSet<RentalDates> RentalDates { get; set; }
    }
}
