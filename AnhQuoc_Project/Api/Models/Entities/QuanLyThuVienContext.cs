using System;
using System.Collections.Generic;
using Api.Utilities;
using Microsoft.EntityFrameworkCore;

namespace Api.Models.Entities;

public partial class QuanLyThuVienContext : DbContext
{
    public QuanLyThuVienContext()
    {
    }

    public QuanLyThuVienContext(DbContextOptions<QuanLyThuVienContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Adult> Adults { get; set; }

    public virtual DbSet<Author> Authors { get; set; }

    public virtual DbSet<Book> Books { get; set; }

    public virtual DbSet<BookIsbn> BookIsbns { get; set; }

    public virtual DbSet<BookStatus> BookStatuses { get; set; }

    public virtual DbSet<BookTitle> BookTitles { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Child> Children { get; set; }

    public virtual DbSet<Function> Functions { get; set; }

    public virtual DbSet<LoanDetail> LoanDetails { get; set; }

    public virtual DbSet<LoanDetailHistory> LoanDetailHistories { get; set; }

    public virtual DbSet<LoanHistory> LoanHistories { get; set; }

    public virtual DbSet<LoanSlip> LoanSlips { get; set; }

    public virtual DbSet<Parameter> Parameters { get; set; }

    public virtual DbSet<PenaltyReason> PenaltyReasons { get; set; }

    public virtual DbSet<Province> Provinces { get; set; }

    public virtual DbSet<Publisher> Publishers { get; set; }

    public virtual DbSet<Reader> Readers { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<RoleFunction> RoleFunctions { get; set; }

    public virtual DbSet<Statistical> Statisticals { get; set; }

    public virtual DbSet<Translator> Translators { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserInfo> UserInfos { get; set; }

    public virtual DbSet<UserRole> UserRoles { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(Global.WebApplicationBuilder.Configuration.GetConnectionString("Default"));
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Adult>(entity =>
        {
            entity.HasKey(e => e.IdReader).HasName("PK__Adult__D616DA89B6F3392E");

            entity.ToTable("Adult");

            entity.Property(e => e.IdReader)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.Address).HasMaxLength(100);
            entity.Property(e => e.City).HasMaxLength(100);
            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            entity.Property(e => e.ExpireDate).HasColumnType("datetime");
            entity.Property(e => e.Identify)
                .HasMaxLength(12)
                .IsUnicode(false);
            entity.Property(e => e.ModifiedAt).HasColumnType("datetime");
            entity.Property(e => e.Phone)
                .HasMaxLength(12)
                .IsUnicode(false);
            entity.Property(e => e.Status).HasDefaultValue(true);

            entity.HasOne(d => d.IdReaderNavigation).WithOne(p => p.Adult)
                .HasForeignKey<Adult>(d => d.IdReader)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Adult__IdReader__3B75D760");
        });

        modelBuilder.Entity<Author>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Author__3214EC07A6055336");

            entity.ToTable("Author");

            entity.Property(e => e.Id)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.BoF)
                .HasColumnType("datetime")
                .HasColumnName("boF");
            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            entity.Property(e => e.Description).HasColumnType("ntext");
            entity.Property(e => e.ModifiedAt).HasColumnType("datetime");
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.Status).HasDefaultValue(true);
            entity.Property(e => e.Summary).HasColumnType("ntext");
        });

        modelBuilder.Entity<Book>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Book__3214EC0778057D86");

            entity.ToTable("Book");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            entity.Property(e => e.IdBookStatus)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasDefaultValue("BS1");
            entity.Property(e => e.IdPublisher)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.IdTranslator)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.Isbn)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("ISBN");
            entity.Property(e => e.Language).HasMaxLength(50);
            entity.Property(e => e.ModifiedAt).HasColumnType("datetime");
            entity.Property(e => e.Note)
                .HasDefaultValue("")
                .HasColumnType("ntext");
            entity.Property(e => e.Price).HasColumnType("decimal(12, 3)");
            entity.Property(e => e.PriceCurrent).HasColumnType("decimal(12, 3)");
            entity.Property(e => e.PublishDate).HasColumnType("datetime");
            entity.Property(e => e.Status).HasDefaultValue(true);

            entity.HasOne(d => d.IdBookStatusNavigation).WithMany(p => p.Books)
                .HasForeignKey(d => d.IdBookStatus)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Book__IdBookStat__60A75C0F");

            entity.HasOne(d => d.IdPublisherNavigation).WithMany(p => p.Books)
                .HasForeignKey(d => d.IdPublisher)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Book__IdPublishe__5EBF139D");

            entity.HasOne(d => d.IdTranslatorNavigation).WithMany(p => p.Books)
                .HasForeignKey(d => d.IdTranslator)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Book__IdTranslat__5FB337D6");

            entity.HasOne(d => d.IsbnNavigation).WithMany(p => p.Books)
                .HasForeignKey(d => d.Isbn)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Book__ISBN__5DCAEF64");
        });

        modelBuilder.Entity<BookIsbn>(entity =>
        {
            entity.HasKey(e => e.Isbn).HasName("PK__BookISBN__447D36EB5DA7A84D");

            entity.ToTable("BookISBN");

            entity.Property(e => e.Isbn)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("ISBN");
            entity.Property(e => e.Description)
                .HasDefaultValue("")
                .HasColumnType("ntext");
            entity.Property(e => e.IdAuthor)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.IdBookTitle)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.OriginLanguage).HasMaxLength(50);
            entity.Property(e => e.Status).HasDefaultValue(true);

            entity.HasOne(d => d.IdAuthorNavigation).WithMany(p => p.BookIsbns)
                .HasForeignKey(d => d.IdAuthor)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__BookISBN__IdAuth__5812160E");

            entity.HasOne(d => d.IdBookTitleNavigation).WithMany(p => p.BookIsbns)
                .HasForeignKey(d => d.IdBookTitle)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__BookISBN__IdBook__571DF1D5");
        });

        modelBuilder.Entity<BookStatus>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__BookStat__3214EC07848CB36E");

            entity.ToTable("BookStatus");

            entity.Property(e => e.Id)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            entity.Property(e => e.Description)
                .HasDefaultValue("")
                .HasColumnType("ntext");
            entity.Property(e => e.ModifiedAt).HasColumnType("datetime");
            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.Status).HasDefaultValue(true);
        });

        modelBuilder.Entity<BookTitle>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__BookTitl__3214EC07ACC24269");

            entity.ToTable("BookTitle");

            entity.Property(e => e.Id)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.IdCategory)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.Note)
                .HasDefaultValue("")
                .HasColumnType("ntext");
            entity.Property(e => e.Summary).HasColumnType("ntext");
            entity.Property(e => e.UrlImage)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasOne(d => d.IdCategoryNavigation).WithMany(p => p.BookTitles)
                .HasForeignKey(d => d.IdCategory)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__BookTitle__IdCat__52593CB8");
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Category__3214EC078D3F3FD2");

            entity.ToTable("Category");

            entity.Property(e => e.Id)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            entity.Property(e => e.ModifiedAt).HasColumnType("datetime");
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.Status).HasDefaultValue(true);
        });

        modelBuilder.Entity<Child>(entity =>
        {
            entity.HasKey(e => e.IdReader).HasName("PK__Child__D616DA89A120FD26");

            entity.ToTable("Child");

            entity.Property(e => e.IdReader)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            entity.Property(e => e.IdAdult)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.ModifiedAt).HasColumnType("datetime");
            entity.Property(e => e.Status).HasDefaultValue(true);

            entity.HasOne(d => d.IdAdultNavigation).WithMany(p => p.Children)
                .HasForeignKey(d => d.IdAdult)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Child__IdAdult__403A8C7D");

            entity.HasOne(d => d.IdReaderNavigation).WithOne(p => p.Child)
                .HasForeignKey<Child>(d => d.IdReader)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Child__IdReader__3F466844");
        });

        modelBuilder.Entity<Function>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Function__3214EC078B852A9C");

            entity.ToTable("Function");

            entity.Property(e => e.Id)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.Description).HasColumnType("ntext");
            entity.Property(e => e.IdParent)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.IsAdmin).HasDefaultValue(true);
            entity.Property(e => e.Name).HasMaxLength(60);
            entity.Property(e => e.Status).HasDefaultValue(true);
            entity.Property(e => e.UrlImage).HasColumnType("text");

            entity.HasOne(d => d.IdParentNavigation).WithMany(p => p.InverseIdParentNavigation)
                .HasForeignKey(d => d.IdParent)
                .HasConstraintName("FK__Function__IdPare__787EE5A0");
        });

        modelBuilder.Entity<LoanDetail>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__LoanDeta__3214EC07154CD72E");

            entity.ToTable("LoanDetail");

            entity.Property(e => e.Id)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.ExpDate).HasColumnType("datetime");
            entity.Property(e => e.IdLoan)
                .HasMaxLength(10)
                .IsUnicode(false);

            entity.HasOne(d => d.IdBookNavigation).WithMany(p => p.LoanDetails)
                .HasForeignKey(d => d.IdBook)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__LoanDetai__IdBoo__03F0984C");

            entity.HasOne(d => d.IdLoanNavigation).WithMany(p => p.LoanDetails)
                .HasForeignKey(d => d.IdLoan)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__LoanDetai__IdLoa__02FC7413");
        });

        modelBuilder.Entity<LoanDetailHistory>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__LoanDeta__3214EC07CAFC1F41");

            entity.ToTable("LoanDetailHistory");

            entity.Property(e => e.Id)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.ExpDate).HasColumnType("datetime");
            entity.Property(e => e.IdLoanHistory)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.Note).HasMaxLength(50);
            entity.Property(e => e.PaidMoney).HasColumnType("decimal(12, 3)");

            entity.HasOne(d => d.IdBookNavigation).WithMany(p => p.LoanDetailHistories)
                .HasForeignKey(d => d.IdBook)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__LoanDetai__IdBoo__0B91BA14");

            entity.HasOne(d => d.IdLoanHistoryNavigation).WithMany(p => p.LoanDetailHistories)
                .HasForeignKey(d => d.IdLoanHistory)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__LoanDetai__IdLoa__0A9D95DB");
        });

        modelBuilder.Entity<LoanHistory>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__LoanHist__3214EC07D85EAB9F");

            entity.ToTable("LoanHistory");

            entity.Property(e => e.Id)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.CreateAt).HasColumnType("datetime");
            entity.Property(e => e.Deposit).HasColumnType("decimal(12, 3)");
            entity.Property(e => e.ExpDate).HasColumnType("datetime");
            entity.Property(e => e.FineMoney).HasColumnType("decimal(12, 3)");
            entity.Property(e => e.IdReader)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.IdUser)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.LoanDate).HasColumnType("datetime");
            entity.Property(e => e.LoanPaid).HasColumnType("decimal(12, 3)");
            entity.Property(e => e.Note).HasMaxLength(50);
            entity.Property(e => e.Total).HasColumnType("decimal(12, 3)");

            entity.HasOne(d => d.IdReaderNavigation).WithMany(p => p.LoanHistories)
                .HasForeignKey(d => d.IdReader)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__LoanHisto__IdRea__06CD04F7");

            entity.HasOne(d => d.IdUserNavigation).WithMany(p => p.LoanHistories)
                .HasForeignKey(d => d.IdUser)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__LoanHisto__IdUse__07C12930");
        });

        modelBuilder.Entity<LoanSlip>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__LoanSlip__3214EC073BBFBB35");

            entity.ToTable("LoanSlip");

            entity.Property(e => e.Id)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.Deposit).HasColumnType("decimal(12, 3)");
            entity.Property(e => e.ExpDate).HasColumnType("datetime");
            entity.Property(e => e.IdReader)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.IdUser)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.LoanDate).HasColumnType("datetime");
            entity.Property(e => e.LoanPaid).HasColumnType("decimal(12, 3)");

            entity.HasOne(d => d.IdReaderNavigation).WithMany(p => p.LoanSlips)
                .HasForeignKey(d => d.IdReader)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__LoanSlip__IdRead__7F2BE32F");

            entity.HasOne(d => d.IdUserNavigation).WithMany(p => p.LoanSlips)
                .HasForeignKey(d => d.IdUser)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__LoanSlip__IdUser__00200768");
        });

        modelBuilder.Entity<Parameter>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Paramete__3214EC07F47B844C");

            entity.ToTable("Parameter");

            entity.Property(e => e.Id)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            entity.Property(e => e.Description)
                .HasDefaultValue("")
                .HasColumnType("ntext");
            entity.Property(e => e.ModifiedAt).HasColumnType("datetime");
            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.Status).HasDefaultValue(true);
            entity.Property(e => e.Value)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<PenaltyReason>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__PenaltyR__3214EC0719F716A1");

            entity.ToTable("PenaltyReason");

            entity.Property(e => e.Id)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            entity.Property(e => e.Description)
                .HasDefaultValue("")
                .HasColumnType("ntext");
            entity.Property(e => e.ModifiedAt).HasColumnType("datetime");
            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.Price).HasColumnType("decimal(12, 3)");
        });

        modelBuilder.Entity<Province>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Province__3214EC0775B1B67D");

            entity.ToTable("Province");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<Publisher>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Publishe__3214EC073968A677");

            entity.ToTable("Publisher");

            entity.Property(e => e.Id)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.Address).HasMaxLength(100);
            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.Phone)
                .HasMaxLength(12)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Reader>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Reader__3214EC07D8487444");

            entity.ToTable("Reader");

            entity.Property(e => e.Id)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.BoF)
                .HasColumnType("datetime")
                .HasColumnName("boF");
            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            entity.Property(e => e.Fname)
                .HasMaxLength(20)
                .HasColumnName("FName");
            entity.Property(e => e.Lname)
                .HasMaxLength(100)
                .HasColumnName("LName");
            entity.Property(e => e.ModifiedAt).HasColumnType("datetime");
            entity.Property(e => e.Status).HasDefaultValue(true);
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Role__3214EC07C848E4B1");

            entity.ToTable("Role");

            entity.Property(e => e.Id)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.Group).HasMaxLength(60);
            entity.Property(e => e.Name).HasMaxLength(60);
            entity.Property(e => e.Status).HasDefaultValue(true);
        });

        modelBuilder.Entity<RoleFunction>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__RoleFunc__3214EC077E4FBB0A");

            entity.ToTable("RoleFunction");

            entity.Property(e => e.Id)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.IdFunction)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.IdRole)
                .HasMaxLength(10)
                .IsUnicode(false);

            entity.HasOne(d => d.IdFunctionNavigation).WithMany(p => p.RoleFunctions)
                .HasForeignKey(d => d.IdFunction)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__RoleFunct__IdFun__7C4F7684");

            entity.HasOne(d => d.IdRoleNavigation).WithMany(p => p.RoleFunctions)
                .HasForeignKey(d => d.IdRole)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__RoleFunct__IdRol__7B5B524B");
        });

        modelBuilder.Entity<Statistical>(entity =>
        {
            entity.HasKey(e => e.DateTime).HasName("PK__Statisti__03BE4CA04A295DE5");

            entity.ToTable("Statistical");

            entity.Property(e => e.DateTime).HasColumnType("datetime");
            entity.Property(e => e.Data)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Description)
                .HasDefaultValue("")
                .HasColumnType("ntext");
        });

        modelBuilder.Entity<Translator>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Translat__3214EC075C76D95E");

            entity.ToTable("Translator");

            entity.Property(e => e.Id)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.BoF)
                .HasColumnType("datetime")
                .HasColumnName("boF");
            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            entity.Property(e => e.Description).HasColumnType("ntext");
            entity.Property(e => e.ModifiedAt).HasColumnType("datetime");
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.Status).HasDefaultValue(true);
            entity.Property(e => e.Summary).HasColumnType("ntext");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__User__3214EC07A5B78539");

            entity.ToTable("User");

            entity.Property(e => e.Id)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            entity.Property(e => e.ModifiedAt).HasColumnType("datetime");
            entity.Property(e => e.Note)
                .HasDefaultValue("")
                .HasColumnType("ntext");
            entity.Property(e => e.Password)
                .HasMaxLength(60)
                .IsUnicode(false);
            entity.Property(e => e.Status).HasDefaultValue(true);
            entity.Property(e => e.Username)
                .HasMaxLength(60)
                .IsUnicode(false);
        });

        modelBuilder.Entity<UserInfo>(entity =>
        {
            entity.HasKey(e => e.IdUser).HasName("PK__UserInfo__B7C9263825956759");

            entity.ToTable("UserInfo");

            entity.Property(e => e.IdUser)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.Address).HasMaxLength(100);
            entity.Property(e => e.Fname)
                .HasMaxLength(60)
                .HasColumnName("FName");
            entity.Property(e => e.Lname)
                .HasMaxLength(60)
                .HasColumnName("LName");
            entity.Property(e => e.Phone)
                .HasMaxLength(12)
                .IsUnicode(false);

            entity.HasOne(d => d.IdUserNavigation).WithOne(p => p.UserInfo)
                .HasForeignKey<UserInfo>(d => d.IdUser)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__UserInfo__IdUser__6D0D32F4");
        });

        modelBuilder.Entity<UserRole>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__UserRole__3214EC070A94942E");

            entity.ToTable("UserRole");

            entity.Property(e => e.Id)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.IdRole)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.IdUser)
                .HasMaxLength(10)
                .IsUnicode(false);

            entity.HasOne(d => d.IdRoleNavigation).WithMany(p => p.UserRoles)
                .HasForeignKey(d => d.IdRole)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__UserRole__IdRole__73BA3083");

            entity.HasOne(d => d.IdUserNavigation).WithMany(p => p.UserRoles)
                .HasForeignKey(d => d.IdUser)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__UserRole__IdUser__72C60C4A");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
