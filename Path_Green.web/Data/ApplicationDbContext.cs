using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Path_Green.web.Models;

namespace Path_Green.web.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<School> Schools { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Inventory> Inventories { get; set; }
        public DbSet<OrderStatus> OrderStatuses { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Donation> Donations { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Survey> Surveys { get; set; }
        public DbSet<SurveyQuestion> SurveyQuestions { get; set; }
        public DbSet<SurveyResponse> SurveyResponses { get; set; }
        public DbSet<SurveyAnswer> SurveyAnswers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            modelBuilder.Entity<Inventory>()
                .HasOne(i => i.Product)
                .WithOne(p => p.Inventory)
                .HasForeignKey<Inventory>(i => i.ProductID);

            modelBuilder.Entity<Order>()
                .HasOne(o => o.User)
                .WithMany()
                .HasForeignKey(o => o.UserID);

            modelBuilder.Entity<Order>()
                .HasOne(o => o.OrderStatus)
                .WithMany(os => os.Orders)
                .HasForeignKey(o => o.OrderStatusID);

            modelBuilder.Entity<OrderItem>()
                .HasOne(oi => oi.Order)
                .WithMany(o => o.OrderItems)
                .HasForeignKey(oi => oi.OrderID);

            modelBuilder.Entity<OrderItem>()
                .HasOne(oi => oi.Product)
                .WithMany(p => p.OrderItems)
                .HasForeignKey(oi => oi.ProductID);

            modelBuilder.Entity<Donation>()
                .HasOne(d => d.User)
                .WithMany()
                .HasForeignKey(d => d.UserID);

            modelBuilder.Entity<Message>()
                .HasOne(m => m.Sender)
                .WithMany()
                .HasForeignKey(m => m.PostedByUserID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<SurveyQuestion>()
                .HasOne(sq => sq.Survey)
                .WithMany(s => s.Questions)
                .HasForeignKey(sq => sq.SurveyID);

            modelBuilder.Entity<SurveyResponse>()
                .HasOne(sr => sr.Survey)
                .WithMany()
                .HasForeignKey(sr => sr.SurveyID);

            modelBuilder.Entity<SurveyResponse>()
                .HasOne(sr => sr.User)
                .WithMany()
                .HasForeignKey(sr => sr.UserID);

            modelBuilder.Entity<SurveyResponse>()
                .HasOne(sr => sr.Order)
                .WithMany(o => o.SurveyResponses)
                .HasForeignKey(sr => sr.OrderID);

            modelBuilder.Entity<SurveyAnswer>()
                .HasOne(sa => sa.SurveyResponse)
                .WithMany(sr => sr.Answers)
                .HasForeignKey(sa => sa.SurveyResponseID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<SurveyAnswer>()
                .HasOne(sa => sa.SurveyQuestion)
                .WithMany(sq => sq.Answers)
                .HasForeignKey(sa => sa.QuestionID)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
