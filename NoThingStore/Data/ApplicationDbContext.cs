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
        public DbSet<Product> Products { get; set; }
        public DbSet<ActivationKey> ActivationKeys { get; set; }
        public DbSet<EBook> EBooks { get; set; }
        public DbSet<Software> Softwares { get; set; }
        public DbSet<VideoCourse> VideoCourses { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }

        int _imageCounter = 0;
        readonly Random _random = new();

        private ProductImage GenerateRandomImage(Random random, int productId)
        {
            int width = random.Next(3000, 4000);
            int height = random.Next(3000, 4000);
            string backgroundColor = string.Format("{0:X6}", random.Next(0x1000000));
            string textColor = string.Format("{0:X6}", random.Next(0x1000000));

            ProductImage productImage = new()
            {
                Id = ++_imageCounter,
                ProductId = productId,
                ImageUrl = $"https://dummyimage.com/{width}x{height}/{backgroundColor}/{textColor}.jpg"
            };
            return productImage;
        }

        private List<ProductImage> GenerateRandomListOfImages(Random random, int productId)
        {
            List<ProductImage> productImages = new();
            int numberOfImages = random.Next(3, 7);
            for (int i = 0; i < numberOfImages; i++)
            {
                productImages.Add(GenerateRandomImage(random, productId));
            }
            return productImages;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // connect images to products

            modelBuilder.Entity<Product>()
                .HasMany(p => p.ProductImages)
                .WithOne(pi => pi.Product)
                .HasForeignKey(pi => pi.ProductId);

            modelBuilder.Entity<Order>()
                .HasMany(o => o.OrderItems)
                .WithOne(oi => oi.Order)
                .HasForeignKey(oi => oi.OrderId);


            // Use TPC inheritance strategy

            modelBuilder.Entity<Product>().UseTptMappingStrategy();

            // Seed the database with random data

            modelBuilder.Entity<ActivationKey>().HasData(new List<ActivationKey>
            {
                new ActivationKey()
                {
                    Id = 1,
                    Name = "Microsoft Office Professional Plus 2019",
                    ShortDescription = "The latest version of Microsoft Office for professionals",
                    LongDescription = "Microsoft Office Professional Plus 2019 is the latest version of Microsoft Office for professionals. It includes Word, Excel, PowerPoint, Outlook, OneNote, Publisher, Access, Skype for Business, and OneDrive for Business. It is a one-time purchase that does not receive feature updates.",
                    Price = 399.99m,
                    IsAvailable = true,
                    Key = "ABCD-1234-EFGH-5678",
                    TargetProgramName = "Microsoft Office",
                    ProgramVersion = "2019",
                    ExpirationDate = new DateTime(2024, 12, 31),
                    HowToActivate = "Enter the key during the installation process",
                },
                new ActivationKey()
                {
                    Id = 2,
                    Name = "Adobe Creative Cloud All Apps",
                    ShortDescription = "Access to all Adobe Creative Cloud apps",
                    LongDescription = "Adobe Creative Cloud All Apps is a subscription plan that gives you access to all Adobe Creative Cloud apps, including Photoshop, Illustrator, InDesign, Premiere Pro, Acrobat Pro DC, and more. It is a one-year subscription that does not receive feature updates.",
                    Price = 599.99m,
                    IsAvailable = false,
                    Key = "WXYZ-5678-IJKL-9012",
                    TargetProgramName = "Adobe Creative Cloud",
                    ProgramVersion = "2022",
                    ExpirationDate = new DateTime(2023, 6, 30),
                    HowToActivate = "Sign in with your Adobe ID and enter the key in the 'Redeem a product code' section",
                },
                new ActivationKey()
                {
                    Id = 3,
                    Name = "Windows 10 Pro",
                    ShortDescription = "The latest version of Windows for professionals",
                    LongDescription = "Windows 10 Pro is the latest version of Windows for professionals. It includes all the features of Windows 10 Home, plus important business functionality for encryption, remote log-in, creating virtual machines, and more.",
                    Price = 199.99m,
                    IsAvailable = true,
                    Key = "MNOP-3456-QRST-7890",
                    TargetProgramName = "Windows 10",
                    ProgramVersion = "Pro",
                    ExpirationDate = new DateTime(2030, 12, 31),
                    HowToActivate = "Enter the key during the installation process",
                },
                new ActivationKey()
                {
                    Id = 4,
                    Name = "Windows 10 Home",
                    ShortDescription = "The latest version of Windows for home users",
                    LongDescription = "Windows 10 Home is the latest version of Windows for home users. It includes all the features of Windows 10 Home, plus important business functionality for encryption, remote log-in, creating virtual machines, and more.",
                    Price = 99.99m,
                    IsAvailable = false,
                    Key = "EFGH-5678-IJKL-9012",
                    TargetProgramName = "Windows 10",
                    ProgramVersion = "Home",
                    ExpirationDate = new DateTime(2030, 12, 31),
                    HowToActivate = "Enter the key during the installation process",
                },
                new ActivationKey()
                {
                    Id = 5,
                    Name = "Windows 11 Pro",
                    ShortDescription = "The latest version of Windows for professionals",
                    LongDescription = "Windows 11 Pro is the latest version of Windows for professionals. It includes all the features of Windows 11 Home, plus important business functionality for encryption, remote log-in, creating virtual machines, and more.",
                    Price = 299.99m,
                    IsAvailable = true,
                    Key = "QRST-7890-UVWX-1234",
                    TargetProgramName = "Windows 11",
                    ProgramVersion = "Pro",
                    ExpirationDate = new DateTime(2030, 12, 31),
                    HowToActivate = "Enter the key during the installation process",
                },
                new ActivationKey()
                {
                    Id = 6,
                    Name = "Windows 11 Home",
                    ShortDescription = "The latest version of Windows for home users",
                    LongDescription = "Windows 11 Home is the latest version of Windows for home users. It includes all the features of Windows 11 Home, plus important business functionality for encryption, remote log-in, creating virtual machines, and more.",
                    Price = 199.99m,
                    IsAvailable = false,
                    Key = "IJKL-9012-MNOP-3456",
                    TargetProgramName = "Windows 11",
                    ProgramVersion = "Home",
                    ExpirationDate = new DateTime(2030, 12, 31),
                    HowToActivate = "Enter the key during the installation process",
                },
                new ActivationKey()
                {
                    Id = 7,
                    Name = "Microsoft Office Home and Student 2021",
                    ShortDescription = "The latest version of Microsoft Office for home users",
                    LongDescription = "Microsoft Office Home and Student 2021 is the latest version of Microsoft Office for home users. It includes Word, Excel, PowerPoint, and OneNote. It is a one-time purchase that does not receive feature updates.",
                    Price = 149.99m,
                    IsAvailable = true,
                    Key = "UVWX-1234-YZAB-5678",
                    TargetProgramName = "Microsoft Office",
                    ProgramVersion = "Home and Student 2021",
                    ExpirationDate = new DateTime(2026, 12, 31),
                    HowToActivate = "Enter the key during the installation process",
                },
                new ActivationKey()
                {
                    Id = 8,
                    Name = "Microsoft Office Home and Business 2021",
                    ShortDescription = "The latest version of Microsoft Office for home users and small businesses",
                    LongDescription = "Microsoft Office Home and Business 2021 is the latest version of Microsoft Office for home users and small businesses. It includes Word, Excel, PowerPoint, Outlook, and OneNote. It is a one-time purchase that does not receive feature updates.",
                    Price = 249.99m,
                    IsAvailable = false,
                    Key = "YZAB-5678-CDGH-9012",
                    TargetProgramName = "Microsoft Office",
                    ProgramVersion = "Home and Business 2021",
                    ExpirationDate = new DateTime(2026, 12, 31),
                    HowToActivate = "Enter the key during the installation process",
                },
            });

            modelBuilder.Entity<EBook>().HasData(new List<EBook>
            {
                new EBook()
                {
                    Id = 9,
                    Name = "The Lean Startup",
                    ShortDescription = "A guide to building a successful startup",
                    LongDescription = "The Lean Startup is a book by Eric Ries describing the lean startup approach, which aims to shorten product development cycles and rapidly discover if a proposed business model is viable.",
                    Price = 9.99m,
                    IsAvailable = true,
                    DownloadUrl = "https://example.com/the-lean-startup.pdf",
                    MegabyteSize = 2,
                    Authorship = "Eric Ries",
                    Format = "PDF",
                    CountOfPages = 320
                },
                new EBook()
                {
                    Id = 10,
                    Name = "The Da Vinci Code",
                    ShortDescription = "A thriller novel by Dan Brown",
                    LongDescription = "The Da Vinci Code is a 2003 mystery thriller novel by Dan Brown. It is Brown's second novel to include the character Robert Langdon: the first was his 2000 novel Angels & Demons.",
                    Price = 7.99m,
                    IsAvailable = true,
                    DownloadUrl = "https://example.com/the-da-vinci-code.epub",
                    MegabyteSize = 1,
                    Authorship = "Dan Brown",
                    Format = "EPUB",
                    CountOfPages = 689
                },
                new EBook()
                {
                    Id = 11,
                    Name = "The Design of Everyday Things",
                    ShortDescription = "A guide to design principles",
                    LongDescription = "The Design of Everyday Things is a best-selling book by cognitive scientist and usability engineer Donald Norman about how design serves as the communication between object and user, and how to optimize that conduit of communication in order to make the experience of using the object pleasurable.",
                    Price = 12.99m,
                    IsAvailable = true,
                    DownloadUrl = "https://example.com/the-design-of-everyday-things.mobi",
                    MegabyteSize = 3,
                    Authorship = "Don Norman",
                    Format = "MOBI",
                    CountOfPages = 368
                },
            });

            modelBuilder.Entity<Software>().HasData(new List<Software>
            {
                new Software()
                {
                    Id = 12,
                    Name = "Antivirus PRO",
                    ShortDescription = "The ultimate antivirus software for your computer.",
                    LongDescription = "Antivirus PRO is the ultimate antivirus software for your computer. It protects you from all types of malware and viruses.",
                    Price = 19.99m,
                    IsAvailable = true,
                    DownloadUrl = "https://example.com/antivirus-pro-download",
                    MegabyteSize = 120,
                    HowToInstall = "Download the software from the link, run the setup file and follow the instructions.",
                    SystemRequirement = "1GB RAM, 1.5GHz processor, 500MB free disk space",
                    Authorship = "Example Inc."
                },
                new Software()
                {
                    Id = 13,
                    Name = "Video Editing Software PRO",
                    ShortDescription = "Professional video editing software with advanced features.",
                    LongDescription = "Video Editing Software PRO is a professional video editing software with advanced features. It allows you to edit videos in 4K resolution and export them to any format.",
                    Price = 49.99m,
                    IsAvailable = true,
                    DownloadUrl = "https://example.com/video-editing-software-pro-download",
                    MegabyteSize = 500,
                    HowToInstall = "Download the software from the link, run the setup file and follow the instructions.",
                    SystemRequirement = "4GB RAM, 2.5GHz processor, 1GB free disk space",
                    Authorship = "Example Inc."
                },
                new Software()
                {
                    Id = 14,
                    Name = "Backup Software PRO",
                    ShortDescription = "The best backup software for your computer.",
                    LongDescription = "Backup Software PRO is the best backup software for your computer. It allows you to backup all your files and folders automatically.",
                    Price = 9.99m,
                    IsAvailable = true,
                    DownloadUrl = "https://example.com/backup-software-pro-download",
                    MegabyteSize = 50,
                    HowToInstall = "Download the software from the link, run the setup file and follow the instructions.",
                    SystemRequirement = "512MB RAM, 1GHz processor, 100MB free disk space",
                    Authorship = "Example Inc."
                }
            });

            modelBuilder.Entity<VideoCourse>().HasData(new List<VideoCourse>
            {
                new VideoCourse()
                {
                    Id = 15,
                    Name = "Learn C#",
                    ShortDescription = "Learn C# from scratch",
                    LongDescription = "Learn C# from scratch. This course will teach you the basics of C# programming language.",
                    Price = 49.99m,
                    IsAvailable = true,
                    DownloadUrl = "https://example.com/learn-csharp",
                    CountOfVideos = 100,
                    Format = "MP4",
                    Duration = 100,
                    Language = "English"
                },
                new VideoCourse()
                {
                    Id = 16,
                    Name = "Learn JavaScript",
                    ShortDescription = "Learn JavaScript from scratch",
                    LongDescription = "Learn JavaScript from scratch. This course will teach you the basics of JavaScript programming language.",
                    Price = 49.99m,
                    IsAvailable = true,
                    DownloadUrl = "https://example.com/learn-javascript",
                    CountOfVideos = 45,
                    Format = "MP4",
                    Duration = 90,
                    Language = "English"
                },
                new VideoCourse()
                {
                    Id = 17,
                    Name = "Learn Python",
                    ShortDescription = "Learn Python from scratch",
                    LongDescription = "Learn Python from scratch. This course will teach you the basics of Python programming language.",
                    Price = 49.99m,
                    IsAvailable = true,
                    DownloadUrl = "https://example.com/learn-python",
                    CountOfVideos = 80,
                    Format = "MP4",
                    Duration = 120,
                    Language = "English"
                }
            });

            List<ProductImage> productImages = new();

            for (int i = 3; i <= 17; i++)
            {
                productImages.AddRange(GenerateRandomListOfImages(_random, i));
            }

            modelBuilder.Entity<ProductImage>().HasData(productImages);

        }
    }
}