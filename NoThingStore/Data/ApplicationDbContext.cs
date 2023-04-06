using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NoThingStore.Models;

namespace NoThingStore.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
        public DbSet<ActivationKey> ActivationKeys { get; set; }
        public DbSet<EBook> EBooks { get; set; }
        public DbSet<Software> Softwares { get; set; }
        public DbSet<VideoCourse> VideoCourses { get; set; }
        public DbSet<Order> Orders { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ActivationKey>().HasData(new List<ActivationKey>
            {
                new ActivationKey()
                {
                    Id = 1,
                    Name = "Microsoft Office Professional Plus 2019",
                    Description = "The latest version of Microsoft Office for professionals",
                    Price = 399.99m,
                    IsAvailable = true,
                    Key = "ABCD-1234-EFGH-5678",
                    TargetProgramName = "Microsoft Office",
                    ProgramVersion = "2019",
                    ExpirationDate = new DateTime(2024, 12, 31),
                    HowToActivate = "Enter the key during the installation process",
                }.GenerateSlug(),
                new ActivationKey()
                {
                    Id = 2,
                    Name = "Adobe Creative Cloud All Apps",
                    Description = "Access to all Adobe Creative Cloud apps",
                    Price = 599.99m,
                    IsAvailable = true,
                    Key = "WXYZ-5678-IJKL-9012",
                    TargetProgramName = "Adobe Creative Cloud",
                    ProgramVersion = "2022",
                    ExpirationDate = new DateTime(2023, 6, 30),
                    HowToActivate = "Sign in with your Adobe ID and enter the key in the 'Redeem a product code' section",
                }.GenerateSlug(),
                new ActivationKey()
                {
                    Id = 3,
                    Name = "Windows 10 Pro",
                    Description = "The latest version of Windows for professionals",
                    Price = 199.99m,
                    IsAvailable = true,
                    Key = "MNOP-3456-QRST-7890",
                    TargetProgramName = "Windows 10",
                    ProgramVersion = "Pro",
                    ExpirationDate = new DateTime(2030, 12, 31),
                    HowToActivate = "Enter the key during the installation process",
                }.GenerateSlug()
            });

            modelBuilder.Entity<EBook>().HasData(new List<EBook>
            {
                new EBook()
                {
                    Id = 4,
                    Name = "The Lean Startup",
                    Description = "A guide to building a successful startup",
                    Price = 9.99m,
                    IsAvailable = true,
                    DownloadUrl = "https://example.com/the-lean-startup.pdf",
                    MegabyteSize = 2,
                    Authorship = "Eric Ries",
                    Format = "PDF",
                    CountOfPages = 320
                }.GenerateSlug(),
                new EBook()
                {
                    Id = 5,
                    Name = "The Da Vinci Code",
                    Description = "A thriller novel by Dan Brown",
                    Price = 7.99m,
                    IsAvailable = true,
                    DownloadUrl = "https://example.com/the-da-vinci-code.epub",
                    MegabyteSize = 1,
                    Authorship = "Dan Brown",
                    Format = "EPUB",
                    CountOfPages = 689
                }.GenerateSlug(),
                new EBook()
                {
                    Id = 6,
                    Name = "The Design of Everyday Things",
                    Description = "A guide to design principles",
                    Price = 12.99m,
                    IsAvailable = true,
                    DownloadUrl = "https://example.com/the-design-of-everyday-things.mobi",
                    MegabyteSize = 3,
                    Authorship = "Don Norman",
                    Format = "MOBI",
                    CountOfPages = 368
                }.GenerateSlug(),
            });

            modelBuilder.Entity<Software>().HasData(new List<Software>
            {
                new Software()
                {
                    Id = 7,
                    Name = "Antivirus PRO",
                    Description = "The ultimate antivirus software for your computer.",
                    Price = 19.99m,
                    IsAvailable = true,
                    DownloadUrl = "https://example.com/antivirus-pro-download",
                    MegabyteSize = 120,
                    HowToInstall = "Download the software from the link, run the setup file and follow the instructions.",
                    SystemRequirement = "1GB RAM, 1.5GHz processor, 500MB free disk space",
                    Authorship = "Example Inc."
                }.GenerateSlug(),
                new Software()
                {
                    Id = 8,
                    Name = "Video Editing Software PRO",
                    Description = "Professional video editing software with advanced features.",
                    Price = 49.99m,
                    IsAvailable = true,
                    DownloadUrl = "https://example.com/video-editing-software-pro-download",
                    MegabyteSize = 500,
                    HowToInstall = "Download the software from the link, run the setup file and follow the instructions.",
                    SystemRequirement = "4GB RAM, 2.5GHz processor, 1GB free disk space",
                    Authorship = "Example Inc."
                }.GenerateSlug(),
                new Software()
                {
                    Id = 9,
                    Name = "Backup Software PRO",
                    Description = "The best backup software for your computer.",
                    Price = 9.99m,
                    IsAvailable = true,
                    DownloadUrl = "https://example.com/backup-software-pro-download",
                    MegabyteSize = 50,
                    HowToInstall = "Download the software from the link, run the setup file and follow the instructions.",
                    SystemRequirement = "512MB RAM, 1GHz processor, 100MB free disk space",
                    Authorship = "Example Inc."
                }.GenerateSlug()
            });

            modelBuilder.Entity<VideoCourse>().HasData(new List<VideoCourse>
            {
                new VideoCourse()
                {
                    Id = 10,
                    Name = "Learn C#",
                    Description = "Learn C# from scratch",
                    Price = 49.99m,
                    IsAvailable = true,
                    DownloadUrl = "https://example.com/learn-csharp",
                    CountOfVideos = 100,
                    Format = "MP4",
                    Duration = 100,
                    Language = "English"
                }.GenerateSlug(),
                new VideoCourse()
                {
                    Id = 11,
                    Name = "Learn JavaScript",
                    Description = "Learn JavaScript from scratch",
                    Price = 49.99m,
                    IsAvailable = true,
                    DownloadUrl = "https://example.com/learn-javascript",
                    CountOfVideos = 45,
                    Format = "MP4",
                    Duration = 90,
                    Language = "English"
                }.GenerateSlug(),
                new VideoCourse()
                {
                    Id = 12,
                    Name = "Learn Python",
                    Description = "Learn Python from scratch",
                    Price = 49.99m,
                    IsAvailable = true,
                    DownloadUrl = "https://example.com/learn-python",
                    CountOfVideos = 80,
                    Format = "MP4",
                    Duration = 120,
                    Language = "English"
                }.GenerateSlug()
            });
        }
    }
}