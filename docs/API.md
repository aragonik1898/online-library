# API Документация - Электронная библиотека

## Обзор

API предоставляет RESTful интерфейс для управления библиотекой, включая книги, авторов, жанры и пользовательские закладки.

## Базовый URL

- Локальная разработка: `http://localhost:5000`
- Docker: `http://localhost:8080`

## Аутентификация

API использует ASP.NET Core Identity для аутентификации. Большинство endpoints требуют авторизации.

## Endpoints

### Книги

#### GET /Books
Получить список всех книг с пагинацией.

**Параметры запроса:**
- `page` (int, optional) - номер страницы (по умолчанию: 1)
- `pageSize` (int, optional) - размер страницы (по умолчанию: 8)

**Ответ:**
```json
{
  "items": [
    {
      "id": 1,
      "title": "Название книги",
      "description": "Описание",
      "publicationYear": 2023,
      "author": {
        "id": 1,
        "firstName": "Имя",
        "lastName": "Фамилия"
      },
      "genre": {
        "id": 1,
        "name": "Жанр"
      }
    }
  ],
  "totalCount": 25,
  "pageNumber": 1,
  "pageSize": 8,
  "totalPages": 4
}
```

#### GET /Books/Details/{id}
Получить детальную информацию о книге.

**Параметры пути:**
- `id` (int) - ID книги

**Ответ:**
```json
{
  "id": 1,
  "title": "Название книги",
  "description": "Описание книги",
  "publicationYear": 2023,
  "author": {
    "id": 1,
    "firstName": "Имя",
    "lastName": "Фамилия",
    "biography": "Биография автора"
  },
  "genre": {
    "id": 1,
    "name": "Жанр",
    "description": "Описание жанра"
  },
  "filePath": "/uploads/books/book.pdf",
  "fileName": "book.pdf"
}
```

#### POST /Books/Create
Создать новую книгу.

**Тело запроса:**
```json
{
  "title": "Название книги",
  "description": "Описание",
  "publicationYear": 2023,
  "authorId": 1,
  "genreId": 1,
  "file": "файл_книги.pdf"
}
```

#### POST /Books/Edit/{id}
Обновить информацию о книге.

#### GET /Books/Delete/{id}
Удалить книгу.

### Авторы

#### GET /Authors
Получить список всех авторов с пагинацией.

**Параметры запроса:**
- `page` (int, optional) - номер страницы
- `pageSize` (int, optional) - размер страницы

#### GET /Authors/Details/{id}
Получить детальную информацию об авторе.

### Жанры

Жанры отображаются на главной странице и в деталях книг.

### Закладки (Personal)

#### GET /Personal/Bookmarks
Получить закладки текущего пользователя.

**Требует аутентификации**

**Параметры запроса:**
- `page` (int, optional) - номер страницы
- `pageSize` (int, optional) - размер страницы

**Ответ:**
```json
{
  "items": [
    {
      "id": 1,
      "title": "Название закладки",
      "description": "Описание закладки",
      "pageNumber": 15,
      "quote": "Цитата из книги",
      "book": {
        "id": 1,
        "title": "Название книги",
        "author": {
          "firstName": "Имя",
          "lastName": "Фамилия"
        }
      },
      "createdDate": "2023-01-01T00:00:00Z"
    }
  ],
  "totalCount": 10,
  "pageNumber": 1,
  "pageSize": 6,
  "totalPages": 2
}
```

#### POST /Personal/AddBookmark
Добавить новую закладку.

**Требует аутентификации**

**Тело запроса:**
```json
{
  "bookId": 1,
  "title": "Название закладки",
  "description": "Описание",
  "pageNumber": 15,
  "quote": "Цитата из книги"
}
```

#### POST /Personal/EditBookmark/{id}
Редактировать закладку.

**Требует аутентификации**

#### POST /Personal/DeleteBookmark/{id}
Удалить закладку.

**Требует аутентификации**

### Чтение книг

#### GET /Reading/Read/{id}
Открыть книгу для чтения.

**Параметры пути:**
- `id` (int) - ID книги

**Ответ:** HTML страница с PDF viewer

#### GET /Reading/Upload
Форма загрузки файла книги.

### Аутентификация

#### GET /Account/Login
Страница входа.

#### POST /Account/Login
Вход в систему.

**Тело запроса:**
```json
{
  "email": "user@example.com",
  "password": "password"
}
```

#### GET /Account/Register
Страница регистрации.

#### POST /Account/Register
Регистрация нового пользователя.

**Тело запроса:**
```json
{
  "email": "user@example.com",
  "password": "password",
  "confirmPassword": "password",
  "firstName": "Имя",
  "lastName": "Фамилия"
}
```

#### POST /Account/Logout
Выход из системы.

#### GET /Account/Profile
Профиль пользователя.

**Требует аутентификации**

## Коды ответов

- `200 OK` - Успешный запрос
- `302 Found` - Перенаправление
- `400 Bad Request` - Неверный запрос
- `401 Unauthorized` - Требуется аутентификация
- `403 Forbidden` - Доступ запрещен
- `404 Not Found` - Ресурс не найден
- `500 Internal Server Error` - Внутренняя ошибка сервера

## Пагинация

Многие endpoints поддерживают пагинацию:

**Параметры:**
- `page` - номер страницы (начиная с 1)
- `pageSize` - количество элементов на странице

**Ответ включает:**
- `items` - массив элементов
- `totalCount` - общее количество элементов
- `pageNumber` - текущая страница
- `pageSize` - размер страницы
- `totalPages` - общее количество страниц
- `hasPreviousPage` - есть ли предыдущая страница
- `hasNextPage` - есть ли следующая страница

## Обработка ошибок

API возвращает HTML страницы с информацией об ошибках:

- **404** - "Страница не найдена"
- **500** - "Внутренняя ошибка сервера"
- **Валидация** - сообщения об ошибках валидации

## Примеры использования

### Получение списка книг
```bash
curl -X GET "http://localhost:8080/Books?page=1&pageSize=10"
```

### Добавление закладки
```bash
curl -X POST "http://localhost:8080/Personal/AddBookmark" \
  -H "Content-Type: application/x-www-form-urlencoded" \
  -d "bookId=1&title=Моя закладка&description=Важная страница&pageNumber=25"
```

### Вход в систему
```bash
curl -X POST "http://localhost:8080/Account/Login" \
  -H "Content-Type: application/x-www-form-urlencoded" \
  -d "email=user@example.com&password=password"
```

