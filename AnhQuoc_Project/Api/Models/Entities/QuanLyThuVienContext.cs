using System;
using System.Collections.Generic;
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
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=localhost;Database=QuanLyThuVien;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Adult>(entity =>
        {
            entity.HasKey(e => e.IdReader).HasName("PK__Adult__D616DA89703CD086");

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

            entity.HasOne(d => d.IdReaderNavigation).WithOne(p => p.Adult)
                .HasForeignKey<Adult>(d => d.IdReader)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Adult__IdReader__7A672E12");
        });

        modelBuilder.Entity<Author>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Author__3214EC07786AD8A2");

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
            entity.HasKey(e => e.Id).HasName("PK__Book__3214EC07475061D7");

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
                .HasConstraintName("FK__Book__IdBookStat__7B5B524B");

            entity.HasOne(d => d.IdPublisherNavigation).WithMany(p => p.Books)
                .HasForeignKey(d => d.IdPublisher)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Book__IdPublishe__7C4F7684");

            entity.HasOne(d => d.IdTranslatorNavigation).WithMany(p => p.Books)
                .HasForeignKey(d => d.IdTranslator)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Book__IdTranslat__7D439ABD");

            entity.HasOne(d => d.IsbnNavigation).WithMany(p => p.Books)
                .HasForeignKey(d => d.Isbn)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Book__ISBN__7E37BEF6");
        });

        modelBuilder.Entity<BookIsbn>(entity =>
        {
            entity.HasKey(e => e.Isbn).HasName("PK__BookISBN__447D36EB3D811C84");

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
                .HasConstraintName("FK__BookISBN__IdAuth__7F2BE32F");

            entity.HasOne(d => d.IdBookTitleNavigation).WithMany(p => p.BookIsbns)
                .HasForeignKey(d => d.IdBookTitle)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__BookISBN__IdBook__00200768");
        });

        modelBuilder.Entity<BookStatus>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__BookStat__3214EC073240CA0E");

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
            entity.HasKey(e => e.Id).HasName("PK__BookTitl__3214EC0787251901");

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
                .HasConstraintName("FK__BookTitle__IdCat__01142BA1");
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Category__3214EC07A4ACFE41");

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
            entity.HasKey(e => e.IdReader).HasName("PK__Child__D616DA89460CC411");

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
                .HasConstraintName("FK__Child__IdAdult__02084FDA");

            entity.HasOne(d => d.IdReaderNavigation).WithOne(p => p.Child)
                .HasForeignKey<Child>(d => d.IdReader)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Child__IdReader__02FC7413");
        });

        modelBuilder.Entity<Function>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Function__3214EC07B6719270");

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
                .HasConstraintName("FK__Function__IdPare__03F0984C");
        });

        modelBuilder.Entity<LoanDetail>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__LoanDeta__3214EC073EB63C7C");

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
                .HasConstraintName("FK__LoanDetai__IdBoo__04E4BC85");

            entity.HasOne(d => d.IdLoanNavigation).WithMany(p => p.LoanDetails)
                .HasForeignKey(d => d.IdLoan)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__LoanDetai__IdLoa__05D8E0BE");
        });

        modelBuilder.Entity<LoanDetailHistory>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__LoanDeta__3214EC075A6E7300");

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
                .HasConstraintName("FK__LoanDetai__IdBoo__06CD04F7");

            entity.HasOne(d => d.IdLoanHistoryNavigation).WithMany(p => p.LoanDetailHistories)
                .HasForeignKey(d => d.IdLoanHistory)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__LoanDetai__IdLoa__07C12930");
        });

        modelBuilder.Entity<LoanHistory>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__LoanHist__3214EC076A6A8034");

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
                .HasConstraintName("FK__LoanHisto__IdRea__08B54D69");

            entity.HasOne(d => d.IdUserNavigation).WithMany(p => p.LoanHistories)
                .HasForeignKey(d => d.IdUser)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__LoanHisto__IdUse__09A971A2");
        });

        modelBuilder.Entity<LoanSlip>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__LoanSlip__3214EC07C44328C5");

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
                .HasConstraintName("FK__LoanSlip__IdRead__0A9D95DB");

            entity.HasOne(d => d.IdUserNavigation).WithMany(p => p.LoanSlips)
                .HasForeignKey(d => d.IdUser)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__LoanSlip__IdUser__0B91BA14");
        });

        modelBuilder.Entity<Parameter>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Paramete__3214EC07FE4EEEEC");

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
            entity.HasKey(e => e.Id).HasName("PK__PenaltyR__3214EC074F3D9028");

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
            entity.HasKey(e => e.Id).HasName("PK__Province__3214EC07A5EC1109");

            entity.ToTable("Province");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<Publisher>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Publishe__3214EC078A821771");

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
            entity.HasKey(e => e.Id).HasName("PK__Reader__3214EC07449A0168");

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
            entity.HasKey(e => e.Id).HasName("PK__Role__3214EC0708059F08");

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
            entity.HasKey(e => e.Id).HasName("PK__RoleFunc__3214EC077AC21165");

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
                .HasConstraintName("FK__RoleFunct__IdFun__0C85DE4D");

            entity.HasOne(d => d.IdRoleNavigation).WithMany(p => p.RoleFunctions)
                .HasForeignKey(d => d.IdRole)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__RoleFunct__IdRol__0D7A0286");
        });

        modelBuilder.Entity<Statistical>(entity =>
        {
            entity.HasKey(e => e.DateTime).HasName("PK__Statisti__03BE4CA0BDABAB20");

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
            entity.HasKey(e => e.Id).HasName("PK__Translat__3214EC079167A01F");

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
            entity.HasKey(e => e.Id).HasName("PK__User__3214EC07E49AF2A4");

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
            entity.HasKey(e => e.IdUser).HasName("PK__UserInfo__B7C926380DB0BC6A");

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
                .HasConstraintName("FK__UserInfo__IdUser__0E6E26BF");
        });

        modelBuilder.Entity<UserRole>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__UserRole__3214EC07D3BECB01");

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
                .HasConstraintName("FK__UserRole__IdRole__0F624AF8");

            entity.HasOne(d => d.IdUserNavigation).WithMany(p => p.UserRoles)
                .HasForeignKey(d => d.IdUser)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__UserRole__IdUser__10566F31");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
