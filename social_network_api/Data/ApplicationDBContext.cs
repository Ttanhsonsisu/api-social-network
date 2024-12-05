using Microsoft.EntityFrameworkCore;
using social_network_api.Models.Authen;
using social_network_api.Models.Command;
using social_network_api.Models.Common;
using social_network_api.Models.Info;
using social_network_api.Models.MasterData;
using social_network_api.Models.NetWork;
using social_network_api.Models.Post;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace social_network_api.Data
{
    public class ApplicationDBContext : DbContext
    {

        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
        {
        }

        public DbSet<Action1> Actions { get; set; }
        public DbSet<Function> Functions { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserGroup> UserGroups { get; set; }
        public DbSet<UserGroupPermission> UserGroupPermissions { get; set; }
        public DbSet<UserPermission> UserPermissions { get; set; }
        public DbSet<Comment> Commands { get; set; }
        public DbSet<CommentLike> CommandLikes { get; set; }
        public DbSet<Logging> Loggings { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<Education> Educations { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<Job> Jobs { get; set; }
        public DbSet<Profile> Profiles { get; set; }
        public DbSet<UserEducation> UserEducations { get; set; }
        public DbSet<Media> Medias { get; set; }
        public DbSet<FriendShip> FriendShips { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<PostLike> PostLikes { get; set; }
        public DbSet<PostShare> PostShares { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Action1>().ToTable("Action").HasKey(v => v.Id);
            modelBuilder.Entity<Function>().ToTable("Function").HasKey(v => v.Id);
            modelBuilder.Entity<User>().ToTable("User").HasKey(v => v.Id);
            modelBuilder.Entity<UserGroup>().ToTable("UserGroup").HasKey(v => v.Id);
            modelBuilder.Entity<UserGroupPermission>().ToTable("UserGroupPermission").HasKey(v => v.Id);
            modelBuilder.Entity<UserPermission>().ToTable("UserPermission").HasKey(v => v.Id);
            modelBuilder.Entity<Comment>().ToTable("Command").HasKey(v => v.Id);
            modelBuilder.Entity<CommentLike>().ToTable("CommandLike").HasKey(v => v.Id);
            modelBuilder.Entity<Logging>().ToTable("Logging").HasKey(v => v.Id);
            modelBuilder.Entity<Notification>().ToTable("Notification").HasKey(v => v.Id);
            modelBuilder.Entity<Education>().ToTable("Education").HasKey(v => v.Id);
            modelBuilder.Entity<Image>().ToTable("Image").HasKey(v => v.Id);
            modelBuilder.Entity<Job>().ToTable("Job").HasKey(v => v.Id);
            modelBuilder.Entity<Profile>().ToTable("Profile").HasKey(v => v.Id);
            modelBuilder.Entity<UserEducation>().ToTable("UserEducation").HasKey(v => v.Id);
            modelBuilder.Entity<Media>().ToTable("Media").HasKey(v => v.Id);
            modelBuilder.Entity<FriendShip>().ToTable("FriendShip").HasKey(v => v.Id);
            modelBuilder.Entity<Post>().ToTable("Post").HasKey(v => v.Id);
            modelBuilder.Entity<PostLike>().ToTable("PostLike").HasKey(v => v.Id);
            modelBuilder.Entity<PostShare>().ToTable("PostShare").HasKey(v => v.Id);
            
        }
    }
}
