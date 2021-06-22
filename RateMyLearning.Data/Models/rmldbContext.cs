using System;
using System.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace RateMyLearning.Data.Models {
    public partial class rmldbContext : DbContext {
        public rmldbContext(DbContextOptions<rmldbContext> options)
            : base(options) { }

        public virtual DbSet<Address> Address { get; set; }
        public virtual DbSet<Campus> Campus { get; set; }
        public virtual DbSet<City> City { get; set; }
        public virtual DbSet<Course> Course { get; set; }
        public virtual DbSet<Interest> Interest { get; set; }
        public virtual DbSet<Program> Program { get; set; }
        public virtual DbSet<Province> Province { get; set; }
        public virtual DbSet<Review> Review { get; set; }
        public virtual DbSet<School> School { get; set; }
        public virtual DbSet<Users> Users { get; set; }
        public virtual DbSet<UsersType> UsersType { get; set; }

        // map out the database
        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            modelBuilder.Entity<Address>(entity => {
                entity.ToTable("address");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Address1)
                    .IsRequired()
                    .HasColumnName("address")
                    .HasMaxLength(255);

                entity.Property(e => e.Address2)
                    .HasColumnName("address2")
                    .HasMaxLength(255);

                entity.Property(e => e.CityId).HasColumnName("city_id");

                entity.Property(e => e.Phone)
                    .HasColumnName("phone")
                    .HasMaxLength(255);

                entity.Property(e => e.PostalCode)
                    .IsRequired()
                    .HasColumnName("postal_code")
                    .HasMaxLength(255);

                entity.HasOne(d => d.City)
                    .WithMany(p => p.Address)
                    .HasForeignKey(d => d.CityId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("address_city_id_fkey");
            });

            modelBuilder.Entity<Campus>(entity => {
                entity.ToTable("campus");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.AddressId).HasColumnName("address_id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(255);

                entity.Property(e => e.SchoolId).HasColumnName("school_id");

                entity.HasOne(d => d.Address)
                    .WithMany(p => p.Campus)
                    .HasForeignKey(d => d.AddressId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("campus_address_id_fkey");

                entity.HasOne(d => d.School)
                    .WithMany(p => p.Campus)
                    .HasForeignKey(d => d.SchoolId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("campus_school_id_fkey");
            });

            modelBuilder.Entity<City>(entity => {
                entity.ToTable("city");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasDefaultValueSql("nextval('city_city_id_seq'::regclass)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(255);

                entity.Property(e => e.ProvinceId).HasColumnName("province_id");

                entity.HasOne(d => d.Province)
                    .WithMany(p => p.City)
                    .HasForeignKey(d => d.ProvinceId)
                    .HasConstraintName("city_province_id_fkey");
            });

            modelBuilder.Entity<Course>(entity => {
                entity.ToTable("course");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasColumnName("description")
                    .HasMaxLength(255);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(255);

                entity.Property(e => e.ProgramId).HasColumnName("program_id");

                entity.Property(e => e.CourseCode)
                    .IsRequired()
                    .HasColumnName("course_code")
                    .HasMaxLength(255);

                entity.HasOne(d => d.Program)
                    .WithMany(p => p.Course)
                    .HasForeignKey(d => d.ProgramId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("course_program_id_fkey");

                entity.Property(e => e.IsElective).HasColumnName("is_elective");

                entity.Property(e => e.ContinuingEducation).HasColumnName("continuing_education");
            });

            modelBuilder.Entity<Interest>(entity => {
                entity.ToTable("interest");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(255);

                entity.Property(e => e.SchoolId)
                    .HasColumnName("school_id")
                    .ValueGeneratedOnAdd();

                entity.HasOne(d => d.School)
                    .WithMany(p => p.Interest)
                    .HasForeignKey(d => d.SchoolId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("interest_school_id_fkey");
            });

            modelBuilder.Entity<Program>(entity => {
                entity.ToTable("program");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasColumnName("description")
                    .HasMaxLength(255);

                entity.Property(e => e.InterestId)
                    .HasColumnName("interest_id")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.Keywords)
                    .IsRequired()
                    .HasColumnName("keywords")
                    .HasMaxLength(255);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(255);

                entity.HasOne(d => d.Interest)
                    .WithMany(p => p.Program)
                    .HasForeignKey(d => d.InterestId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("program_interest_id_fkey");
            });

            modelBuilder.Entity<Province>(entity => {
                entity.ToTable("province");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasDefaultValueSql("nextval('province_province_id_seq'::regclass)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(255);
            });

            modelBuilder.Entity<Review>(entity => {
                entity.ToTable("review");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CampusId).HasColumnName("campus_id");

                entity.Property(e => e.CourseId).HasColumnName("course_id");

                entity.Property(e => e.CreatedOn)
                    .HasColumnName("created_on")
                    .HasDefaultValueSql("now()");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasColumnName("description")
                    .HasMaxLength(255);

                entity.Property(e => e.InterestId).HasColumnName("interest_id");

                entity.Property(e => e.ProgramId).HasColumnName("program_id");

                entity.Property(e => e.Rating).HasColumnName("rating");

                entity.Property(e => e.SchoolId).HasColumnName("school_id");

                entity.Property(e => e.UsersId).HasColumnName("users_id");

                entity.HasOne(d => d.Campus)
                    .WithMany(p => p.Review)
                    .HasForeignKey(d => d.CampusId)
                    .HasConstraintName("review_campus_id_fkey");

                entity.HasOne(d => d.Course)
                    .WithMany(p => p.Review)
                    .HasForeignKey(d => d.CourseId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("review_course_id_fkey");

                entity.HasOne(d => d.Interest)
                    .WithMany(p => p.Review)
                    .HasForeignKey(d => d.InterestId)
                    .HasConstraintName("review_interest_id_fkey");

                entity.HasOne(d => d.Program)
                    .WithMany(p => p.Review)
                    .HasForeignKey(d => d.ProgramId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("review_program_id_fkey");

                entity.HasOne(d => d.School)
                    .WithMany(p => p.Review)
                    .HasForeignKey(d => d.SchoolId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("review_school_id_fkey");

                entity.HasOne(d => d.Users)
                    .WithMany(p => p.Review)
                    .HasForeignKey(d => d.UsersId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("review_users_id_fkey");
            });

            modelBuilder.Entity<School>(entity => {
                entity.ToTable("school");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(255);
            });

            modelBuilder.Entity<Users>(entity => {
                entity.ToTable("users");

                entity.HasIndex(e => e.Email)
                    .HasName("users_email_key")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasDefaultValueSql("nextval('users_user_id_seq'::regclass)");

                entity.Property(e => e.CreatedOn)
                    .HasColumnName("created_on")
                    .HasDefaultValueSql("now()");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasColumnName("email")
                    .HasMaxLength(255);

                entity.Property(e => e.EmployeeId).HasColumnName("employee_id");

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasColumnName("first_name")
                    .HasMaxLength(255);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasColumnName("last_name")
                    .HasMaxLength(255);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasColumnName("password")
                    .HasMaxLength(50);

                entity.Property(e => e.StudentId).HasColumnName("student_id");

                entity.Property(e => e.TypeId).HasColumnName("type_id");

                entity.HasOne(d => d.Type)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.TypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("users_type_id_fkey");
            });

            modelBuilder.Entity<UsersType>(entity => {
                entity.HasKey(e => e.TypeId)
                    .HasName("users_type_pkey");

                entity.ToTable("users_type");

                entity.Property(e => e.TypeId).HasColumnName("type_id");

                entity.Property(e => e.IsFaculty).HasColumnName("is_faculty");

                entity.Property(e => e.IsNeither).HasColumnName("is_neither");

                entity.Property(e => e.IsStudent).HasColumnName("is_student");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
