
using System;


class Book
{
    public string Title { get; set; }
    public string Author { get; set; }

    public Book(string title, string author)
    {
        Title = title;
        Author = author;
    }
}

class Library
{
    private Book[] books;

    public Library(Book[] initialBooks)
    {
        books = initialBooks;
    }

    public Book this[int index]
    {
        get
        {
            if (index >= 0 && index < books.Length)
                return books[index];

            throw new ArgumentOutOfRangeException();
        }
        set
        {
            if (index >= 0 && index < books.Length)
                books[index] = value;
            else
                throw new ArgumentOutOfRangeException();
        }
    }
}

class Person
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string Role { get; set; }

    public Person(string name, string email, string role)
    {
        Name = name;
        Email = email;
        Role = role;
    }
}

class CompanyCatalog
{
    private Person[] staff;

    public CompanyCatalog(Person[] people)
    {
        staff = people;
    }


    public Person this[int index]
    {
        get => staff[index];
        set => staff[index] = value;
    }


    public Person this[string name]
    {
        get
        {
            foreach (var p in staff)
            {
                if (p.Name == name)
                    return p;
            }

            throw new KeyNotFoundException("Сотрудник не найден");
        }
    }
}

class UserContainer
{
    private Person user;

    public UserContainer(Person person)
    {
        user = person;
    }

    public string this[string field]
    {
        get
        {
            switch (field.ToLower())
            {
                case "name": return user.Name;
                case "email": return user.Email;
                case "role": return user.Role;
                default:
                    throw new ArgumentException("Неизвестное поле");
            }
        }
    }
}


class GameGrid
{
    private int[,] matrix;

    public GameGrid(int r, int c)
    {
        matrix = new int[r, c];
    }

    public int this[int row, int col]
    {
        get => matrix[row, col];
        private set => matrix[row, col] = value;
    }

    public void SetCell(int r, int c, int val)
    {
        this[r, c] = val;
    }
}

class Program
{
    static void Main()
    {

        Book[] books =
        {
            new Book("Война и мир", "Толстой"),
            new Book("Преступление и наказание", "Достоевский"),
            new Book("Мастер и Маргарита", "Булгаков"),
            new Book("Евгений Онегин", "Пушкин")
        };

        Library library = new Library(books);

        Console.WriteLine("Книга [1]: " + library[1].Title);

        library[0] = new Book("Отцы и дети", "Тургенев");

        Console.WriteLine("Новая книга [0]: " + library[0].Title);

        try
        {
            Console.WriteLine(library[99].Title);
        }
        catch (ArgumentOutOfRangeException)
        {
            Console.WriteLine("Ошибка: индекс вне диапазона!");
        }


        Person[] people =
        {
            new Person("Alice", "alice@gmail.com", "Manager"),
            new Person("Bob", "bob@gmail.com", "Developer"),
            new Person("Charlie", "charlie@gmail.com", "Designer")
        };

        CompanyCatalog catalog = new CompanyCatalog(people);

        Console.WriteLine("По индексу [0]: " + catalog[0].Name);

        Console.WriteLine("По имени Alice: " + catalog["Alice"].Email);

        UserContainer user = new UserContainer(people[0]);

        Console.WriteLine("Имя: " + user["name"]);
        Console.WriteLine("Email: " + user["email"]);
        Console.WriteLine("Роль: " + user["role"]);

        try
        {
            Console.WriteLine(catalog["David"].Name);
        }
        catch (KeyNotFoundException ex)
        {
            Console.WriteLine("Ошибка: " + ex.Message);
        }


        GameGrid grid = new GameGrid(3, 3);

        grid.SetCell(0, 0, 1);
        grid.SetCell(0, 1, 2);
        grid.SetCell(0, 2, 3);

        grid.SetCell(1, 0, 4);
        grid.SetCell(1, 1, 5);
        grid.SetCell(1, 2, 6);

        grid.SetCell(2, 0, 7);
        grid.SetCell(2, 1, 8);
        grid.SetCell(2, 2, 9);

        Console.WriteLine("Игровое поле:");

        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                Console.Write(grid[i, j] + "\t");
            }

            Console.WriteLine();
        }



        Console.WriteLine("\nИзменение через метод SetCell:");
        grid.SetCell(0, 0, 5);
        Console.WriteLine("grid[0,0] = " + grid[0, 0]);
    }
}
