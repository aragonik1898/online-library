using Microsoft.AspNetCore.Identity;
using Library.Models;

namespace Library.Data
{
    public static class DbInitializer
    {
        public static async Task Initialize(ApplicationDbContext context, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            // Создаем роли
            if (!await roleManager.RoleExistsAsync("Admin"))
            {
                await roleManager.CreateAsync(new IdentityRole("Admin"));
            }

            if (!await roleManager.RoleExistsAsync("User"))
            {
                await roleManager.CreateAsync(new IdentityRole("User"));
            }

            // Создаем администратора
            if (await userManager.FindByEmailAsync("admin@library.com") == null)
            {
                var admin = new ApplicationUser
                {
                    UserName = "admin@library.com",
                    Email = "admin@library.com",
                    FirstName = "Администратор",
                    LastName = "Системы",
                    EmailConfirmed = true
                };

                await userManager.CreateAsync(admin, "Admin123!");
                await userManager.AddToRoleAsync(admin, "Admin");
            }

            // Добавляем тестовые данные, если их нет
            if (!context.Genres.Any())
            {
                var genres = new[]
                {
                    new Genre { Name = "Художественная литература", Description = "Романы, повести, рассказы" },
                    new Genre { Name = "Научная литература", Description = "Научные труды и исследования" },
                    new Genre { Name = "Техническая литература", Description = "Книги по программированию и IT" },
                    new Genre { Name = "История", Description = "Исторические произведения" },
                    new Genre { Name = "Философия", Description = "Философские труды" },
                    new Genre { Name = "Фантастика", Description = "Научная фантастика и фэнтези" },
                    new Genre { Name = "Детектив", Description = "Детективные романы и триллеры" },
                    new Genre { Name = "Романтика", Description = "Любовные романы" },
                    new Genre { Name = "Биография", Description = "Биографии и мемуары" },
                    new Genre { Name = "Психология", Description = "Книги по психологии и саморазвитию" },
                    new Genre { Name = "Экономика", Description = "Экономические труды и бизнес-литература" },
                    new Genre { Name = "Поэзия", Description = "Стихотворения и поэмы" }
                };

                context.Genres.AddRange(genres);
                await context.SaveChangesAsync();
            }

            if (!context.Authors.Any())
            {
                var authors = new[]
                {
                    // Русские классики
                    new Author 
                    { 
                        FirstName = "Александр", 
                        LastName = "Пушкин", 
                        BirthDate = new DateTime(1799, 6, 6),
                        DeathDate = new DateTime(1837, 2, 10),
                        Country = "Россия",
                        Biography = "Великий русский поэт, драматург и прозаик"
                    },
                    new Author 
                    { 
                        FirstName = "Лев", 
                        LastName = "Толстой", 
                        BirthDate = new DateTime(1828, 9, 9),
                        DeathDate = new DateTime(1910, 11, 20),
                        Country = "Россия",
                        Biography = "Русский писатель и мыслитель"
                    },
                    new Author 
                    { 
                        FirstName = "Фёдор", 
                        LastName = "Достоевский", 
                        BirthDate = new DateTime(1821, 11, 11),
                        DeathDate = new DateTime(1881, 2, 9),
                        Country = "Россия",
                        Biography = "Русский писатель, мыслитель, философ и публицист"
                    },
                    new Author 
                    { 
                        FirstName = "Антон", 
                        LastName = "Чехов", 
                        BirthDate = new DateTime(1860, 1, 29),
                        DeathDate = new DateTime(1904, 7, 15),
                        Country = "Россия",
                        Biography = "Русский писатель, прозаик, драматург"
                    },
                    new Author 
                    { 
                        FirstName = "Николай", 
                        LastName = "Гоголь", 
                        BirthDate = new DateTime(1809, 4, 1),
                        DeathDate = new DateTime(1852, 3, 4),
                        Country = "Россия",
                        Biography = "Русский прозаик, драматург, поэт, критик, публицист"
                    },
                    new Author 
                    { 
                        FirstName = "Иван", 
                        LastName = "Тургенев", 
                        BirthDate = new DateTime(1818, 11, 9),
                        DeathDate = new DateTime(1883, 9, 3),
                        Country = "Россия",
                        Biography = "Русский писатель-реалист, поэт, публицист, драматург, переводчик"
                    },
                    
                    // Зарубежные классики
                    new Author 
                    { 
                        FirstName = "Уильям", 
                        LastName = "Шекспир", 
                        BirthDate = new DateTime(1564, 4, 26),
                        DeathDate = new DateTime(1616, 4, 23),
                        Country = "Англия",
                        Biography = "Английский поэт и драматург, величайший писатель англоязычного мира"
                    },
                    new Author 
                    { 
                        FirstName = "Чарльз", 
                        LastName = "Диккенс", 
                        BirthDate = new DateTime(1812, 2, 7),
                        DeathDate = new DateTime(1870, 6, 9),
                        Country = "Англия",
                        Biography = "Английский писатель, романист и очеркист"
                    },
                    new Author 
                    { 
                        FirstName = "Виктор", 
                        LastName = "Гюго", 
                        BirthDate = new DateTime(1802, 2, 26),
                        DeathDate = new DateTime(1885, 5, 22),
                        Country = "Франция",
                        Biography = "Французский писатель, поэт, драматург"
                    },
                    new Author 
                    { 
                        FirstName = "Марк", 
                        LastName = "Твен", 
                        BirthDate = new DateTime(1835, 11, 30),
                        DeathDate = new DateTime(1910, 4, 21),
                        Country = "США",
                        Biography = "Американский писатель, журналист и общественный деятель"
                    },
                    
                    // Современные авторы
                    new Author 
                    { 
                        FirstName = "Борис", 
                        LastName = "Акунин", 
                        BirthDate = new DateTime(1956, 5, 20),
                        Country = "Россия",
                        Biography = "Русский писатель, литературовед, переводчик, общественный деятель"
                    },
                    new Author 
                    { 
                        FirstName = "Людмила", 
                        LastName = "Улицкая", 
                        BirthDate = new DateTime(1943, 2, 21),
                        Country = "Россия",
                        Biography = "Русская писательница, сценарист"
                    },
                    new Author 
                    { 
                        FirstName = "Джордж", 
                        LastName = "Оруэлл", 
                        BirthDate = new DateTime(1903, 6, 25),
                        DeathDate = new DateTime(1950, 1, 21),
                        Country = "Англия",
                        Biography = "Британский писатель и публицист"
                    },
                    new Author 
                    { 
                        FirstName = "Рэй", 
                        LastName = "Брэдбери", 
                        BirthDate = new DateTime(1920, 8, 22),
                        DeathDate = new DateTime(2012, 6, 5),
                        Country = "США",
                        Biography = "Американский писатель, известный прежде всего фантастическими произведениями"
                    },
                    new Author 
                    { 
                        FirstName = "Агата", 
                        LastName = "Кристи", 
                        BirthDate = new DateTime(1890, 9, 15),
                        DeathDate = new DateTime(1976, 1, 12),
                        Country = "Англия",
                        Biography = "Английская писательница, автор детективных романов"
                    },
                    
                    // IT и техническая литература
                    new Author 
                    { 
                        FirstName = "Роберт", 
                        LastName = "Мартин", 
                        BirthDate = new DateTime(1952, 12, 5),
                        Country = "США",
                        Biography = "Американский инженер-программист, автор книг по разработке программного обеспечения"
                    },
                    new Author 
                    { 
                        FirstName = "Мартин", 
                        LastName = "Фаулер", 
                        BirthDate = new DateTime(1963, 12, 18),
                        Country = "Англия",
                        Biography = "Британский инженер-программист, автор книг по разработке программного обеспечения"
                    },
                    new Author 
                    { 
                        FirstName = "Эрик", 
                        LastName = "Фримен", 
                        BirthDate = new DateTime(1965, 1, 1),
                        Country = "США",
                        Biography = "Американский программист и автор технических книг"
                    }
                };

                context.Authors.AddRange(authors);
                await context.SaveChangesAsync();
            }

            if (!context.Books.Any())
            {
                var books = new[]
                {
                    // Русская классика
                    new Book
                    {
                        Title = "Евгений Онегин",
                        Description = "Роман в стихах Александра Пушкина, одно из самых значительных произведений русской словесности",
                        ISBN = "978-5-17-123456-7",
                        PublicationYear = 1833,
                        Publisher = "АСТ",
                        PageCount = 320,
                        AuthorId = 1,
                        GenreId = 1,
                        IsAvailable = true
                    },
                    new Book
                    {
                        Title = "Война и мир",
                        Description = "Роман-эпопея Льва Толстого, описывающий русское общество в эпоху войн против Наполеона",
                        ISBN = "978-5-17-123457-8",
                        PublicationYear = 1869,
                        Publisher = "АСТ",
                        PageCount = 1274,
                        AuthorId = 2,
                        GenreId = 1,
                        IsAvailable = true
                    },
                    new Book
                    {
                        Title = "Преступление и наказание",
                        Description = "Роман Фёдора Достоевского о нравственных проблемах общества",
                        ISBN = "978-5-17-123458-9",
                        PublicationYear = 1866,
                        Publisher = "АСТ",
                        PageCount = 671,
                        AuthorId = 3,
                        GenreId = 1,
                        IsAvailable = true
                    },
                    new Book
                    {
                        Title = "Вишнёвый сад",
                        Description = "Пьеса Антона Чехова о судьбе русской усадьбы",
                        ISBN = "978-5-17-123459-0",
                        PublicationYear = 1904,
                        Publisher = "АСТ",
                        PageCount = 96,
                        AuthorId = 4,
                        GenreId = 1,
                        IsAvailable = true
                    },
                    new Book
                    {
                        Title = "Мёртвые души",
                        Description = "Поэма Николая Гоголя о русской действительности",
                        ISBN = "978-5-17-123460-1",
                        PublicationYear = 1842,
                        Publisher = "АСТ",
                        PageCount = 352,
                        AuthorId = 5,
                        GenreId = 1,
                        IsAvailable = true
                    },
                    new Book
                    {
                        Title = "Отцы и дети",
                        Description = "Роман Ивана Тургенева о конфликте поколений",
                        ISBN = "978-5-17-123461-2",
                        PublicationYear = 1862,
                        Publisher = "АСТ",
                        PageCount = 288,
                        AuthorId = 6,
                        GenreId = 1,
                        IsAvailable = true
                    },
                    
                    // Зарубежная классика
                    new Book
                    {
                        Title = "Гамлет",
                        Description = "Трагедия Уильяма Шекспира о датском принце",
                        ISBN = "978-5-17-123462-3",
                        PublicationYear = 1603,
                        Publisher = "АСТ",
                        PageCount = 256,
                        AuthorId = 7,
                        GenreId = 1,
                        IsAvailable = true
                    },
                    new Book
                    {
                        Title = "Большие надежды",
                        Description = "Роман Чарльза Диккенса о жизни сироты Пипа",
                        ISBN = "978-5-17-123463-4",
                        PublicationYear = 1861,
                        Publisher = "АСТ",
                        PageCount = 544,
                        AuthorId = 8,
                        GenreId = 1,
                        IsAvailable = true
                    },
                    new Book
                    {
                        Title = "Собор Парижской Богоматери",
                        Description = "Роман Виктора Гюго о любви и страданиях",
                        ISBN = "978-5-17-123464-5",
                        PublicationYear = 1831,
                        Publisher = "АСТ",
                        PageCount = 624,
                        AuthorId = 9,
                        GenreId = 1,
                        IsAvailable = true
                    },
                    new Book
                    {
                        Title = "Приключения Тома Сойера",
                        Description = "Роман Марка Твена о приключениях мальчика",
                        ISBN = "978-5-17-123465-6",
                        PublicationYear = 1876,
                        Publisher = "АСТ",
                        PageCount = 320,
                        AuthorId = 10,
                        GenreId = 1,
                        IsAvailable = true
                    },
                    
                    // Современная литература
                    new Book
                    {
                        Title = "Азазель",
                        Description = "Детективный роман Бориса Акунина о сыщике Эрасте Фандорине",
                        ISBN = "978-5-17-123466-7",
                        PublicationYear = 1998,
                        Publisher = "АСТ",
                        PageCount = 320,
                        AuthorId = 11,
                        GenreId = 7,
                        IsAvailable = true
                    },
                    new Book
                    {
                        Title = "Зелёный шатёр",
                        Description = "Роман Людмилы Улицкой о советской интеллигенции",
                        ISBN = "978-5-17-123467-8",
                        PublicationYear = 2011,
                        Publisher = "АСТ",
                        PageCount = 448,
                        AuthorId = 12,
                        GenreId = 1,
                        IsAvailable = true
                    },
                    new Book
                    {
                        Title = "1984",
                        Description = "Антиутопический роман Джорджа Оруэлла о тоталитарном обществе",
                        ISBN = "978-5-17-123468-9",
                        PublicationYear = 1949,
                        Publisher = "АСТ",
                        PageCount = 320,
                        AuthorId = 13,
                        GenreId = 6,
                        IsAvailable = true
                    },
                    new Book
                    {
                        Title = "451 градус по Фаренгейту",
                        Description = "Антиутопический роман Рэя Брэдбери о будущем без книг",
                        ISBN = "978-5-17-123469-0",
                        PublicationYear = 1953,
                        Publisher = "АСТ",
                        PageCount = 256,
                        AuthorId = 14,
                        GenreId = 6,
                        IsAvailable = true
                    },
                    new Book
                    {
                        Title = "Убийство в Восточном экспрессе",
                        Description = "Детективный роман Агаты Кристи с Эркюлем Пуаро",
                        ISBN = "978-5-17-123470-1",
                        PublicationYear = 1934,
                        Publisher = "АСТ",
                        PageCount = 288,
                        AuthorId = 15,
                        GenreId = 7,
                        IsAvailable = true
                    },
                    
                    // Техническая литература
                    new Book
                    {
                        Title = "Чистый код",
                        Description = "Руководство по написанию чистого и читаемого кода",
                        ISBN = "978-5-17-123471-2",
                        PublicationYear = 2008,
                        Publisher = "Питер",
                        PageCount = 464,
                        AuthorId = 16,
                        GenreId = 3,
                        IsAvailable = true
                    },
                    new Book
                    {
                        Title = "Рефакторинг",
                        Description = "Улучшение дизайна существующего кода",
                        ISBN = "978-5-17-123472-3",
                        PublicationYear = 1999,
                        Publisher = "Питер",
                        PageCount = 432,
                        AuthorId = 17,
                        GenreId = 3,
                        IsAvailable = true
                    },
                    new Book
                    {
                        Title = "Паттерны проектирования",
                        Description = "Элементы переиспользуемого объектно-ориентированного ПО",
                        ISBN = "978-5-17-123473-4",
                        PublicationYear = 1994,
                        Publisher = "Питер",
                        PageCount = 512,
                        AuthorId = 18,
                        GenreId = 3,
                        IsAvailable = true
                    },
                    
                    // Дополнительные книги для разнообразия
                    new Book
                    {
                        Title = "Анна Каренина",
                        Description = "Роман Льва Толстого о любви и семейных отношениях",
                        ISBN = "978-5-17-123474-5",
                        PublicationYear = 1877,
                        Publisher = "АСТ",
                        PageCount = 864,
                        AuthorId = 2,
                        GenreId = 1,
                        IsAvailable = true
                    },
                    new Book
                    {
                        Title = "Братья Карамазовы",
                        Description = "Последний роман Фёдора Достоевского",
                        ISBN = "978-5-17-123475-6",
                        PublicationYear = 1880,
                        Publisher = "АСТ",
                        PageCount = 832,
                        AuthorId = 3,
                        GenreId = 1,
                        IsAvailable = true
                    },
                    new Book
                    {
                        Title = "Три сестры",
                        Description = "Пьеса Антона Чехова о жизни провинциальной интеллигенции",
                        ISBN = "978-5-17-123476-7",
                        PublicationYear = 1901,
                        Publisher = "АСТ",
                        PageCount = 128,
                        AuthorId = 4,
                        GenreId = 1,
                        IsAvailable = true
                    },
                    new Book
                    {
                        Title = "Ромео и Джульетта",
                        Description = "Трагедия Уильяма Шекспира о любви",
                        ISBN = "978-5-17-123477-8",
                        PublicationYear = 1597,
                        Publisher = "АСТ",
                        PageCount = 192,
                        AuthorId = 7,
                        GenreId = 1,
                        IsAvailable = true
                    },
                    new Book
                    {
                        Title = "Оливер Твист",
                        Description = "Роман Чарльза Диккенса о жизни сироты",
                        ISBN = "978-5-17-123478-9",
                        PublicationYear = 1838,
                        Publisher = "АСТ",
                        PageCount = 480,
                        AuthorId = 8,
                        GenreId = 1,
                        IsAvailable = true
                    },
                    new Book
                    {
                        Title = "Отверженные",
                        Description = "Роман Виктора Гюго о социальной несправедливости",
                        ISBN = "978-5-17-123479-0",
                        PublicationYear = 1862,
                        Publisher = "АСТ",
                        PageCount = 1408,
                        AuthorId = 9,
                        GenreId = 1,
                        IsAvailable = true
                    },
                    new Book
                    {
                        Title = "Приключения Гекльберри Финна",
                        Description = "Роман Марка Твена о приключениях мальчика",
                        ISBN = "978-5-17-123480-1",
                        PublicationYear = 1884,
                        Publisher = "АСТ",
                        PageCount = 384,
                        AuthorId = 10,
                        GenreId = 1,
                        IsAvailable = true
                    }
                };

                context.Books.AddRange(books);
                await context.SaveChangesAsync();
            }
        }
    }
}


