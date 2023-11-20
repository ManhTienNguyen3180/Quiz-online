using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace project.Models
{
    public partial class ProjectPrnContext : DbContext
    {
        public ProjectPrnContext()
        {
        }

        public ProjectPrnContext(DbContextOptions<ProjectPrnContext> options)
            : base(options)
        {
        }

        public virtual DbSet<CategoryQuiz> CategoryQuizzes { get; set; } = null!;
        public virtual DbSet<Quiz> Quizzes { get; set; } = null!;
        public virtual DbSet<QuizAnswer> QuizAnswers { get; set; } = null!;
        public virtual DbSet<QuizQuestion> QuizQuestions { get; set; } = null!;
        public virtual DbSet<Take> Takes { get; set; } = null!;
        public virtual DbSet<TakeAnswer> TakeAnswers { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("server =DESKTOP-HQFRO2L; database = ProjectPrn;uid=sa;pwd=123;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CategoryQuiz>(entity =>
            {
                entity.ToTable("Category_quiz");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CategoryDescription)
                    .HasColumnType("text")
                    .HasColumnName("category_description");

                entity.Property(e => e.CategoryName)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("category_name");
            });

            modelBuilder.Entity<Quiz>(entity =>
            {
                entity.ToTable("Quiz");

                entity.Property(e => e.QuizId).HasColumnName("quiz_id");

                entity.Property(e => e.CategoryId).HasColumnName("category_id");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("date")
                    .HasColumnName("created_at");

                entity.Property(e => e.Description)
                    .HasColumnType("text")
                    .HasColumnName("description");

                entity.Property(e => e.EndAt)
                    .HasColumnType("datetime")
                    .HasColumnName("end_at");

                entity.Property(e => e.Score).HasColumnName("score");

                entity.Property(e => e.StartAt)
                    .HasColumnType("datetime")
                    .HasColumnName("start_at");

                entity.Property(e => e.Summary)
                    .HasColumnType("text")
                    .HasColumnName("summary");

                entity.Property(e => e.Title)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("title");

                entity.Property(e => e.Type).HasColumnName("type");

                entity.Property(e => e.UpdateAt)
                    .HasColumnType("date")
                    .HasColumnName("update_at");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Quizzes)
                    .HasForeignKey(d => d.CategoryId)
                    .HasConstraintName("FK_Quiz_Category_quiz");
            });

            modelBuilder.Entity<QuizAnswer>(entity =>
            {
                entity.ToTable("Quiz_answer");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Correct).HasColumnName("correct");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("date")
                    .HasColumnName("created_at");

                entity.Property(e => e.Description)
                    .HasColumnType("text")
                    .HasColumnName("description");

                entity.Property(e => e.QuestionId).HasColumnName("question_id");

                entity.Property(e => e.QuizId).HasColumnName("quiz_id");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("date")
                    .HasColumnName("updated_at");

                entity.HasOne(d => d.Question)
                    .WithMany(p => p.QuizAnswers)
                    .HasForeignKey(d => d.QuestionId)
                    .HasConstraintName("FK_Quiz_answer_Quiz_question");
            });

            modelBuilder.Entity<QuizQuestion>(entity =>
            {
                entity.ToTable("Quiz_question");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Active).HasColumnName("active");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("date")
                    .HasColumnName("created_at");

                entity.Property(e => e.Description)
                    .HasColumnType("text")
                    .HasColumnName("description");

                entity.Property(e => e.QuizId).HasColumnName("quiz_id");

                entity.Property(e => e.Score).HasColumnName("score");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("date")
                    .HasColumnName("updated_at");

                entity.HasOne(d => d.Quiz)
                    .WithMany(p => p.QuizQuestions)
                    .HasForeignKey(d => d.QuizId)
                    .HasConstraintName("FK_Quiz_question_Quiz");
            });

            modelBuilder.Entity<Take>(entity =>
            {
                entity.ToTable("Take");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Description)
                    .HasColumnType("text")
                    .HasColumnName("description");

                entity.Property(e => e.EndAt)
                    .HasColumnType("datetime")
                    .HasColumnName("end_at");

                entity.Property(e => e.QuizId).HasColumnName("quiz_id");

                entity.Property(e => e.Score).HasColumnName("score");

                entity.Property(e => e.StartAt)
                    .HasColumnType("datetime")
                    .HasColumnName("start_at");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.Quiz)
                    .WithMany(p => p.Takes)
                    .HasForeignKey(d => d.QuizId)
                    .HasConstraintName("FK_take_Quiz");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Takes)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_take_User");
            });

            modelBuilder.Entity<TakeAnswer>(entity =>
            {
                entity.ToTable("Take_answer");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Active).HasColumnName("active");

                entity.Property(e => e.AnswerId).HasColumnName("answer_id");

                entity.Property(e => e.QuestionId).HasColumnName("question_id");

                entity.Property(e => e.TakeId).HasColumnName("take_id");

                entity.HasOne(d => d.Answer)
                    .WithMany(p => p.TakeAnswers)
                    .HasForeignKey(d => d.AnswerId)
                    .HasConstraintName("FK_Take_answer_Quiz_answer");

                entity.HasOne(d => d.Question)
                    .WithMany(p => p.TakeAnswers)
                    .HasForeignKey(d => d.QuestionId)
                    .HasConstraintName("FK_Take_answer_Quiz_question");

                entity.HasOne(d => d.Take)
                    .WithMany(p => p.TakeAnswers)
                    .HasForeignKey(d => d.TakeId)
                    .HasConstraintName("FK_Take_answer_take");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.Property(e => e.Role).HasColumnName("role");

                entity.Property(e => e.Username)
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasColumnName("username");

                entity.Property(e => e.Userpassword)
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasColumnName("userpassword");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
