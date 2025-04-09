using Microsoft.EntityFrameworkCore;

namespace Orkaris_Back.Models.EntityFramework
{
    public class WorkoutDBContext : DbContext
    {

        public WorkoutDBContext()
        {
        }
        public WorkoutDBContext(DbContextOptions<WorkoutDBContext> options) : base(options)
        {

        }


        public DbSet<Sport> Sports { get; set; }
        public DbSet<Workout> Workouts { get; set; }
        public DbSet<ExerciseGoal> ExerciseGoals { get; set; }
        public DbSet<Exercise> Exercises { get; set; }
        public DbSet<Category> Categorys { get; set; }
        public DbSet<Session> Sessions { get; set; }
        public DbSet<ExerciseCategory> ExerciseCategorys { get; set; }
        public DbSet<SessionPerformance> SessionPerformances { get; set; }
        public DbSet<ExerciseGoalPerformance> ExerciseGoalPerformances { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<SessionExercise> SessionExercises { get; set; }
        public DbSet<EmailConfirmationToken> EmailConfirmationTokens { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseNpgsql("Host=postgres;Port=5432;Username=postgres;Password=postgres;Database=orkaris");
            }
        }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("t_e_user_usr");

                entity.HasKey(e => e.Id).HasName("PK_User");
                entity.Property(e => e.Id).HasColumnName("usr_id").ValueGeneratedOnAdd();
                entity.Property(e => e.Name).IsRequired().HasMaxLength(100).HasColumnName("usr_name");
                entity.Property(e => e.Email).IsRequired().HasMaxLength(255).HasColumnName("usr_email");
                entity.Property(e => e.Password).IsRequired().HasColumnName("usr_password");
                entity.Property(e => e.Gender).IsRequired().HasMaxLength(50).HasColumnName("usr_gender");
                entity.Property(e => e.Height).IsRequired().HasColumnName("usr_height");
                entity.Property(e => e.Weight).IsRequired().HasColumnName("usr_weight");
                entity.Property(e => e.BirthDate).IsRequired().HasColumnName("usr_birth_date");
                entity.Property(e => e.ProfileType).IsRequired().HasColumnName("usr_profile_type");
                entity.Property(e => e.IsVerified).IsRequired().HasColumnName("usr_is_verified");
                entity.Property(e => e.CreatedAt).HasColumnName("usr_created_at");

                entity.HasIndex(e => e.Name).IsUnique();
                entity.HasIndex(e => e.Email).IsUnique();
                // Navigation properties
                entity.HasMany(e => e.WorkoutUser).WithOne(p => p.UserWorkout).HasForeignKey(p => p.UserId).OnDelete(DeleteBehavior.Cascade);
                entity.HasMany(e => e.SessionUser).WithOne(s => s.UserSession).HasForeignKey(s => s.UserId).OnDelete(DeleteBehavior.Cascade);
                entity.HasMany(e => e.EmailUser).WithOne(e => e.UserEmail).HasForeignKey(e => e.UserId).OnDelete(DeleteBehavior.Cascade);
            });
            modelBuilder.Entity<Session>(entity =>
            {
                entity.ToTable("t_e_session_ses");
                entity.HasKey(e => e.Id).HasName("PK_Session");
                entity.Property(e => e.Id).HasColumnName("ses_id");
                entity.Property(e => e.Name).IsRequired().HasMaxLength(100).HasColumnName("ses_name");
                entity.Property(e => e.UserId).IsRequired().HasColumnName("usr_id");
                entity.Property(e => e.CreatedAt).IsRequired().HasColumnName("ses_created_at");

                // Navigation properties
                entity.HasOne(e => e.UserSession).WithMany(u => u.SessionUser).HasForeignKey(e => e.UserId).OnDelete(DeleteBehavior.Cascade);
                entity.HasOne(e => e.WorkoutSession).WithMany(u => u.SessionWorkout).HasForeignKey(e => e.UserId).OnDelete(DeleteBehavior.Cascade);
                entity.HasMany(e => e.SessionExerciseSession).WithOne(se => se.SessionSessionExercise).HasForeignKey(se => se.SessionId).OnDelete(DeleteBehavior.Cascade);
                entity.HasMany(e => e.SessionPerformanceSession).WithOne(sp => sp.SessionSessionPerformance).HasForeignKey(sp => sp.SessionId).OnDelete(DeleteBehavior.Cascade);
            });
            modelBuilder.Entity<SessionExercise>(entity =>
            {
                entity.ToTable("t_j_session_exercise_goal_seg");
                entity.HasKey(e => new { e.SessionId, e.ExerciseId }).HasName("PK_SessionExercise");
                entity.Property(e => e.SessionId).IsRequired().HasColumnName("ses_id");
                entity.Property(e => e.ExerciseId).IsRequired().HasColumnName("exg_id");

                // Navigation properties
                entity.HasOne(e => e.SessionSessionExercise).WithMany(s => s.SessionExerciseSession).HasForeignKey(e => e.SessionId).OnDelete(DeleteBehavior.Cascade);
                entity.HasOne(e => e.ExerciseGoalSessionExercise).WithMany(eg => eg.SessionExerciseExerciseGoal).HasForeignKey(e => e.ExerciseId).OnDelete(DeleteBehavior.Cascade);
            });
            modelBuilder.Entity<Sport>(entity =>
            {
                entity.ToTable("t_e_sport_spo");
                entity.HasKey(e => e.Id).HasName("PK_Sport");
                entity.Property(e => e.Id).HasColumnName("spo_id").ValueGeneratedOnAdd();
                entity.Property(e => e.Name).IsRequired().HasMaxLength(50).HasColumnName("spo_name");


                entity.HasIndex(e => e.Name).IsUnique();
                // Navigation properties
                entity.HasMany(e => e.CategorySport).WithOne(t => t.SportCategory).HasForeignKey(t => t.SportId).OnDelete(DeleteBehavior.Cascade);
            });
            modelBuilder.Entity<Category>(entity =>
            {
                entity.ToTable("t_e_category_cat");
                entity.HasKey(e => e.Id).HasName("PK_Category");
                entity.Property(e => e.Id).HasColumnName("cat_id").ValueGeneratedOnAdd();
                entity.Property(e => e.Name).IsRequired().HasMaxLength(100).HasColumnName("cat_name");
                entity.Property(e => e.SportId).IsRequired().HasColumnName("spo_id");
                entity.Property(e => e.CreatedAt).HasColumnName("cat_created_at");

                entity.HasIndex(e => e.Name).IsUnique();
                // Navigation properties
                entity.HasOne(e => e.SportCategory).WithMany(s => s.CategorySport).HasForeignKey(e => e.SportId).OnDelete(DeleteBehavior.Cascade);
                entity.HasMany(e => e.ExerciseCategoryCategory).WithOne(et => et.CategoryExerciseCategory).HasForeignKey(et => et.CategoryId).OnDelete(DeleteBehavior.Cascade);
            });
            modelBuilder.Entity<SessionPerformance>(entity =>
            {
                entity.ToTable("t_e_session_performance_spe");
                entity.HasKey(e => e.Id).HasName("PK_SessionPerformance");
                entity.Property(e => e.Id).HasColumnName("spe_id").ValueGeneratedOnAdd();
                entity.Property(e => e.SessionId).IsRequired().HasColumnName("ses_id");
                entity.Property(e => e.feeling).HasColumnName("spe_feeling");
                entity.Property(e => e.Date).IsRequired().HasColumnName("spe_date");

                // Navigation properties
                entity.HasOne(e => e.SessionSessionPerformance).WithMany(s => s.SessionPerformanceSession).HasForeignKey(e => e.SessionId).OnDelete(DeleteBehavior.Cascade);
            });
            modelBuilder.Entity<Workout>(entity =>
            {
                entity.ToTable("t_e_Workout_pgr");
                entity.HasKey(e => e.Id).HasName("PK_Workout");
                entity.Property(e => e.Id).HasColumnName("pfr_id").ValueGeneratedOnAdd();
                entity.Property(e => e.Name).IsRequired().HasMaxLength(100).HasColumnName("pfr_name");
                entity.Property(e => e.UserId).IsRequired().HasColumnName("usr_id");
                entity.Property(e => e.CreatedAt).HasColumnName("pfr_created_at");

                // Navigation properties
                entity.HasOne(e => e.UserWorkout).WithMany(u => u.WorkoutUser).HasForeignKey(e => e.UserId).OnDelete(DeleteBehavior.Cascade);
                entity.HasMany(e => e.SessionWorkout).WithOne(s => s.WorkoutSession).HasForeignKey(s => s.WorkoutId).OnDelete(DeleteBehavior.Cascade);
            });
            modelBuilder.Entity<ExerciseCategory>(entity =>
            {
                entity.ToTable("t_j_exercise_category_ext");
                entity.HasKey(e => new { e.ExerciseId, e.CategoryId }).HasName("PK_ExerciseCategory");
                entity.Property(e => e.ExerciseId).IsRequired().HasColumnName("exe_id");
                entity.Property(e => e.CategoryId).IsRequired().HasColumnName("cat_id");

                // Navigation properties
                entity.HasOne(e => e.CategoryExerciseCategory).WithMany(ex => ex.ExerciseCategoryCategory).HasForeignKey(e => e.ExerciseId).OnDelete(DeleteBehavior.Cascade);
                entity.HasOne(e => e.ExerciseExerciseCategory).WithMany(t => t.ExerciseCategoryExercise).HasForeignKey(e => e.CategoryId).OnDelete(DeleteBehavior.Cascade);
            });
            modelBuilder.Entity<ExerciseGoalPerformance>(entity =>
            {
                entity.ToTable("t_e_exercise_goal_performance_egp");
                entity.HasKey(e => e.Id).HasName("PK_ExerciseGoalPerformance");
                entity.Property(e => e.Id).HasColumnName("egp_id").ValueGeneratedOnAdd();
                entity.Property(e => e.Reps).HasColumnName("egp_reps");
                entity.Property(e => e.Sets).HasColumnName("egp_sets");
                entity.Property(e => e.CreatedAt).HasColumnName("egp_created_at");
                entity.Property(e => e.ExerciseGoalId).IsRequired().HasColumnName("exg_id");

                // Navigation properties
                entity.HasOne(e => e.ExerciseGoalExerciseGoalPerformance).WithMany(eg => eg.ExerciseGoalPerformanceExerciseGoal).HasForeignKey(e => e.ExerciseGoalId).OnDelete(DeleteBehavior.Cascade);
            });
            modelBuilder.Entity<Exercise>(entity =>
            {
                entity.ToTable("t_e_Exercise_exr");
                entity.HasKey(e => e.Id).HasName("PK_Exercise");
                entity.Property(e => e.Id).HasColumnName("exr_id").ValueGeneratedOnAdd();
                entity.Property(e => e.Name).IsRequired().HasMaxLength(100).HasColumnName("exr_name");
                entity.Property(e => e.Description).HasMaxLength(400).HasColumnName("exr_description");
                entity.Property(e => e.CreatedAt).HasColumnName("exr_created_at");

                entity.HasIndex(e => e.Name).IsUnique();
                // Navigation properties
                entity.HasMany(e => e.ExerciseGoalExercice).WithOne(eg => eg.ExerciseExerciseGoal).HasForeignKey(eg => eg.ExerciseId).OnDelete(DeleteBehavior.Cascade);
                entity.HasMany(e => e.ExerciseCategoryExercise).WithOne(et => et.ExerciseExerciseCategory).HasForeignKey(et => et.ExerciseId).OnDelete(DeleteBehavior.Cascade);
            });
            modelBuilder.Entity<ExerciseGoal>(entity =>
            {
                entity.ToTable("t_e_exercise_goal_exg");
                entity.HasKey(e => e.Id).HasName("PK_ExerciseGoal");
                entity.Property(e => e.Id).HasColumnName("exg_id").ValueGeneratedOnAdd();
                entity.Property(e => e.Reps).HasColumnName("exg_reps");
                entity.Property(e => e.Sets).HasColumnName("exg_sets");
                entity.Property(e => e.CreatedAt).HasColumnName("exr_created_at");
                entity.Property(e => e.ExerciseId).IsRequired().HasColumnName("exr_id");

                // Navigation properties
                entity.HasOne(e => e.ExerciseExerciseGoal).WithMany(ex => ex.ExerciseGoalExercice).HasForeignKey(e => e.ExerciseId).OnDelete(DeleteBehavior.Cascade);
                entity.HasMany(e => e.SessionExerciseExerciseGoal).WithOne(se => se.ExerciseGoalSessionExercise).HasForeignKey(se => se.ExerciseId).OnDelete(DeleteBehavior.Cascade);
                entity.HasMany(e => e.ExerciseGoalPerformanceExerciseGoal).WithOne(egp => egp.ExerciseGoalExerciseGoalPerformance).HasForeignKey(egp => egp.ExerciseGoalId).OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<EmailConfirmationToken>(entity =>
            {
                entity.ToTable("t_email_confirmation_tokens_ect");
                entity.HasKey(e => e.Id).HasName("PK_EmailConfirmationToken");
                entity.Property(e => e.Id).HasColumnName("ect_id").ValueGeneratedOnAdd();
                entity.Property(e => e.Token).IsRequired().HasMaxLength(100).HasColumnName("ect_token");
                entity.Property(e => e.UserId).IsRequired().HasColumnName("usr_id");
                entity.Property(e => e.ExpirationDate).IsRequired().HasColumnName("ect_expiration_date");
                entity.Property(e => e.IsUsed).IsRequired().HasColumnName("ect_is_used");

                // Navigation properties
                entity.HasOne(e => e.UserEmail).WithMany(u => u.EmailUser).HasForeignKey(e => e.UserId).OnDelete(DeleteBehavior.Cascade);
            });
        }
    }
}