# Электронная библиотека

## Основные возможности

- **Каталог книг** - просмотр и поиск книг
- **Управление авторами** - информация об авторах
- **Жанры** - категоризация книг по жанрам
- **Закладки** - сохранение закладок из прочитанных книг
- **Чтение книг** - просмотр PDF файлов онлайн
- **Профиль пользователя** - управление личными данными

## Навигация по документации

### Архитектура и дизайн
- [**Архитектурная диаграмма**](docs/diagrams/architecture.puml) - Общая архитектура системы
- [**Диаграмма классов**](docs/diagrams/class-diagram.puml) - Структура классов и моделей
- [**ER диаграмма**](docs/diagrams/database.puml) - Схема базы данных
- [**Описание модулей**](docs/Модули.md) - Детальное описание рабочих модулей

### Процессы и сценарии
- [**Use Case диаграмма**](docs/diagrams/use-case.puml) - Функциональные требования
- [**Диаграмма последовательности**](docs/diagrams/sequence.puml) - Взаимодействие компонентов
- [**Пользовательские сценарии**](docs/diagrams/user-flow.puml) - Потоки пользователей

### Техническая документация
- [**API документация**](docs/API.md) - Описание REST API
- [**Инструкции по диаграммам**](docs/diagrams/README.md) - Как работать с PlantUML

### Быстрый доступ к диаграммам
| Диаграмма | Описание | Исходный код | Изображение |
|-----------|----------|-------------|-------------|
| Архитектура | Слои и компоненты системы | [architecture.puml](docs/diagrams/architecture.puml) | [Просмотр](docs/diagrams/img/architecture.png) |
| База данных | ER схема и связи | [database.puml](docs/diagrams/database.puml) | [Просмотр](docs/diagrams/img/database.png) |
| Классы | Модели и контроллеры | [class-diagram.puml](docs/diagrams/class-diagram.puml) | [Просмотр](docs/diagrams/img/class-diagram.png) |
| Use Case | Роли и функции | [use-case.puml](docs/diagrams/use-case.puml) | [Просмотр](docs/diagrams/img/use-case.png) |
| Последовательность | Временные диаграммы | [sequence.puml](docs/diagrams/sequence.puml) | [Просмотр](docs/diagrams/img/sequence.png) |
| Пользователи | Потоки пользователей | [user-flow.puml](docs/diagrams/user-flow.puml) | [Просмотр](docs/diagrams/img/user-flow.png) |

## Технологии

- **Backend**: ASP.NET Core 8.0 MVC
- **Database**: SQLite
- **ORM**: Entity Framework Core
- **Frontend**: Bootstrap 5, HTML5, CSS3, JavaScript
- **Icons**: Font Awesome
- **Containerization**: Docker

## Структура проекта

```
Library/
├── Controllers/          # Контроллеры MVC
├── Models/              # Модели данных
├── Views/               # Представления
├── Data/                # Контекст базы данных
├── Services/            # Бизнес-логика
├── wwwroot/            # Статические файлы
├── Migrations/         # Миграции БД
└── docs/              # Документация
```

## Установка и запуск

### Локальная разработка

1. Установите .NET 8.0 SDK
2. Клонируйте репозиторий
3. Восстановите зависимости:
   ```bash
   dotnet restore
   ```
4. Примените миграции:
   ```bash
   dotnet ef database update
   ```
5. Запустите приложение:
   ```bash
   dotnet run
   ```

### Учетные записи по умолчанию

После первого запуска приложения создаются тестовые учетные записи:

#### Администратор
- **Email**: admin@library.com
- **Пароль**: Admin123!
- **Роль**: Администратор
- **Возможности**: Управление всеми функциями системы

#### Обычный пользователь
- **Email**: user@library.com
- **Пароль**: User123!
- **Роль**: Пользователь
- **Возможности**: Чтение книг, управление закладками

### Docker

1. Убедитесь, что Docker установлен
2. Соберите и запустите контейнер:
   ```bash
   docker-compose up --build
   ```
3. Приложение будет доступно на http://localhost:8080

## API Endpoints

### Книги
- `GET /Books` - список всех книг
- `GET /Books/Details/{id}` - детали книги
- `GET /Books/Create` - форма создания книги
- `POST /Books/Create` - создание книги
- `GET /Books/Edit/{id}` - форма редактирования
- `POST /Books/Edit/{id}` - обновление книги
- `GET /Books/Delete/{id}` - удаление книги

### Авторы
- `GET /Authors` - список авторов
- `GET /Authors/Details/{id}` - детали автора

### Личное
- `GET /Personal/Bookmarks` - закладки пользователя
- `POST /Personal/AddBookmark` - добавление закладки
- `POST /Personal/EditBookmark/{id}` - редактирование закладки
- `POST /Personal/DeleteBookmark/{id}` - удаление закладки

### Чтение
- `GET /Reading/Read/{id}` - чтение книги
- `GET /Reading/Upload` - загрузка файла книги

## Модели данных

### Book (Книга)
- Id, Title, Description, PublicationYear
- AuthorId, GenreId (связи)
- FilePath, FileName (файлы)
- CreatedDate, UpdatedDate

### Author (Автор)
- Id, FirstName, LastName, Biography
- Books (коллекция книг)

### Genre (Жанр)
- Id, Name, Description
- Books (коллекция книг)

### Bookmark (Закладка)
- Id, Title, Description, PageNumber, Quote
- UserId, BookId (связи)
- CreatedDate

## База данных

Приложение использует SQLite для хранения данных. Основные таблицы:
- Books, Authors, Genres
- Bookmarks, BookLoans
- AspNetUsers (Identity)

## Безопасность

- Аутентификация через ASP.NET Core Identity
- Авторизация для личных функций
- Валидация входных данных
- Защита от CSRF атак

### Роли и права доступа

#### Администратор
- Управление книгами (создание, редактирование, удаление)
- Управление авторами и жанрами
- Просмотр всех пользователей
- Доступ ко всем функциям системы

#### Пользователь
- Просмотр каталога книг
- Чтение книг онлайн
- Управление личными закладками
- Редактирование профиля

#### Гость
- Просмотр каталога книг
- Регистрация в системе
- Вход в систему

## Развертывание

### Docker Compose
```yaml
services:
  library-app:
    build: .
    ports:
      - "8080:8080"
    volumes:
      - library-data:/app/data
      - library-uploads:/app/wwwroot/uploads
```

### Переменные окружения
- `ASPNETCORE_ENVIRONMENT` - среда выполнения
- `ConnectionStrings__DefaultConnection` - строка подключения к БД

## Мониторинг и логирование

- Логирование через ASP.NET Core Logging
- Обработка ошибок с пользовательскими страницами
- Валидация моделей

## Разработка

### Добавление новых функций
1. Создайте модель в `Models/`
2. Добавьте миграцию: `dotnet ef migrations add FeatureName`
3. Обновите БД: `dotnet ef database update`
4. Создайте контроллер и представления

### Стилизация
- Основные стили в `wwwroot/css/site.css`
- Bootstrap 5 для компонентов
- Font Awesome для иконок
