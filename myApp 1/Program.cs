using Azure;
using Microsoft.Data.SqlClient;
using myApp_1;
using System.Data;
using System.Diagnostics;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;


DataBase db = new DataBase();


//myApp1
/*using (db.GetConnection())
{
    await db.GetConnection().OpenAsync();

    SqlCommand command = new SqlCommand();
    command.CommandText = "CREATE TABLE Users (Id INT PRIMARY KEY IDENTITY, Surname VARCHAR(50) NOT NULL, Name VARCHAR(50) NOT NULL," +
        "Patronymic VARCHAR(50), DateOfBirthday DATE NOT NULL, Gender TINYINT NOT NULL)";
    command.Connection = db.GetConnection();
    await command.ExecuteNonQueryAsync();

    Console.WriteLine("Таблица Users создана");
}
Console.Read();
*/

//myApp2 Первый вариант
/*string sqlExpression = "INSERT INTO Users (Surname, Name, Patronymic, DateOfBirthday, Gender) VALUES ('Goncharov', 'Vitaly', ' ', '08.11.2001', '1')";

using (db.GetConnection())
{
    await db.GetConnection().OpenAsync();

    SqlCommand command = new SqlCommand(sqlExpression, db.GetConnection());
    int number = await command.ExecuteNonQueryAsync();
    Console.WriteLine($"Добавлено пользователей: {number}");
}
Console.Read();
*/

//myApp2 Второй вариант
/*
Console.Write("Введите фамилию: ");
string surname = Console.ReadLine();
Console.Write("Введите имя: ");
string name = Console.ReadLine();
Console.Write("Введите отчество (не обязательно): ");
string patronymic = Console.ReadLine();
Console.Write("Введите дату рождения: ");
DateTime date = DateTime.Parse(Console.ReadLine());
Console.Write("Введите пол 1 (муж), 2 (жен): ");
int gender = Int16.Parse(Console.ReadLine());

string sqlExpression = $"INSERT INTO Users (Surname, Name, Patronymic, DateOfBirthday, Gender) VALUES ('{surname}', '{name}', '{patronymic}', {date}, {gender})";

using (db.GetConnection())
{
    await db.GetConnection().OpenAsync();

    SqlCommand command = new SqlCommand(sqlExpression, db.GetConnection());
    int number = await command.ExecuteNonQueryAsync();
    Console.WriteLine($"Добавлено пользователей: {number}");
}
Console.Read();
*/

//myApp3
/*string sqlExpression = "SELECT * FROM Users";

using (db.GetConnection())
{
    await db.GetConnection().OpenAsync();

    SqlCommand command = new SqlCommand(sqlExpression, db.GetConnection());
    SqlDataReader reader = await command.ExecuteReaderAsync();

    if (reader.HasRows) // если есть данные
    {
        // выводим названия столбцов
        string columnName1 = reader.GetName(0);
        string columnName2 = reader.GetName(1);
        string columnName3 = reader.GetName(2);
        string columnName4 = reader.GetName(3);
        string columnName5 = reader.GetName(4);
        string columnName6 = reader.GetName(5);

        Console.WriteLine($"{columnName1,-5} {columnName2,-15} {columnName3,-15} {columnName4,-20} {columnName5,-12} {columnName6,-10}Age");

        while (await reader.ReadAsync()) // построчно считываем данные
        {
            object id = reader.GetValue(0);
            object surname = reader.GetValue(1);
            object name = reader.GetValue(2);
            object patronymic = reader.GetValue(3);
            object date = reader.GetValue(4);
            object gender = reader.GetValue(5);

            DateTime birthdate = Convert.ToDateTime(date); // Преобразуем дату рождения в DateTime
            DateTime now = DateTime.Now; // Текущая дата

            int age = (int)((now.Subtract(birthdate)).TotalDays / 365.25); // Вычисляем возраст
            string genderStr = (gender.ToString() == "1") ? "  Man" : "  Girl"; // Определяем пол

            Console.WriteLine($"{id,-5} {surname,-15} {name,-15} {patronymic,-20} {birthdate.ToShortDateString(),-12} {genderStr,-10} {age}");
        }
    }

    await reader.CloseAsync();
}

Console.Read();
*/

//myApp4 
/*
string[] maleNames = { "John", "Robert", "Michael", "Fabian", "Fabio", "Fadil" }; // массив имен мужчин
string[] femaleNames = { "Mary", "Patricia", "Jennifer"}; // массив имен женщин
string[] lastNames = { "Smith", "Johnson", "Williams", "Faber", "Fairlie", "Fairman" }; // массив фамилий
string[] patronymics = { "James", "David", "Joseph", "Fabian" }; // массив отчеств

Random random = new Random();


string sqlExpression = "INSERT INTO Users (Surname, Name, Patronymic, DateOfBirthday, Gender) " +
                       "VALUES (@Surname, @Name, @Patronymic, @DateOfBirthday, @Gender)";

using (SqlConnection connection = db.GetConnection())
{
    await connection.OpenAsync();

    SqlCommand command = new SqlCommand(sqlExpression, connection);

    for (int i = 0; i < 1000000; i++)
    {
        string firstName;
        string gender;

        if (random.Next(2) == 0) // случайное число от 0 до 1
        {
            gender = "1";
            firstName = maleNames[random.Next(maleNames.Length)]; // случайное имя из массива мужчин
        }
        else
        {
            gender = "0";
            firstName = femaleNames[random.Next(femaleNames.Length)]; // случайное имя из массива женщин
        }

        string lastName = lastNames[random.Next(lastNames.Length)]; // случайная фамилия
        string patronymic = patronymics[random.Next(patronymics.Length)]; // случайное отчество

        DateTime birthDate = new DateTime(random.Next(1960, 2005), random.Next(1, 13), random.Next(1, 29)); // случайная дата рождения

        // Добавляем параметры команды
        command.Parameters.AddWithValue("@Surname", lastName);
        command.Parameters.AddWithValue("@Name", firstName);
        command.Parameters.AddWithValue("@Patronymic", patronymic);
        command.Parameters.AddWithValue("@DateOfBirthday", birthDate);
        command.Parameters.AddWithValue("@Gender", gender);

        // Выполняем команду
        command.ExecuteNonQuery();

        // Очищаем параметры команды перед добавлением следующей записи
        command.Parameters.Clear();
    }

    for (int i = 0; i < 100; i++)
    {
        string surname = "F" + Guid.NewGuid().ToString().Substring(0, 7);
        string sqlExpressionF = "INSERT INTO Users (Surname, Name, Patronymic, DateOfBirthday, Gender) " +
                                "VALUES (@Surname, 'Smith', 'Patron', '1990-01-01', '1')";

        // Добавляем параметр команды
        command.Parameters.AddWithValue("@Surname", surname);

        // Выполняем команду
        command.ExecuteNonQuery();

        // Очищаем параметр команды перед добавлением следующей записи
        command.Parameters.Clear();
    }
}
*/
//myApp5
/*
Stopwatch stopwatch = new Stopwatch();

using (db.GetConnection())
{
    db.openConnection();

    // Замер времени выполнения
    stopwatch.Start();

    // Выборка данных из таблицы Users
    string sqlExpression = "SELECT * FROM Users WHERE Gender = 1 AND Surname LIKE 'F%'";
    SqlCommand command = new SqlCommand(sqlExpression, db.GetConnection());
    SqlDataReader reader = command.ExecuteReader();

    // Вывод результатов на экран
    Console.WriteLine("Results:");
    Console.WriteLine("----------------------------------------");
    while (reader.Read())
    {
        string genderStr = reader["Gender"].ToString() == "1" ? "Male" : "Female";
        Console.WriteLine($"{reader["Surname"]} {reader["Name"]} {reader["Patronymic"]}, {((DateTime)reader["DateOfBirthday"]).ToString("yyyy-MM-dd")}, {genderStr}");
    }
    Console.WriteLine("----------------------------------------");

    // Остановка таймера и вывод времени выполнения
    stopwatch.Stop();
    Console.WriteLine($"Времени прошло: {stopwatch.ElapsedMilliseconds} ms");

    reader.Close();
    Console.Read();
}
*/

//myApp6 (Поменял запрос немного)
/*
Stopwatch stopwatch = new Stopwatch();

using (db.GetConnection())
{
    db.openConnection();

    // Замер времени выполнения
    stopwatch.Start();

    // Выборка данных из таблицы Users
    string sqlExpression = "SELECT * FROM Users WHERE Gender = @Gender AND Surname LIKE @Surname";
    SqlCommand command = new SqlCommand(sqlExpression, db.GetConnection());
    command.Parameters.AddWithValue("@Gender", 1);
    command.Parameters.AddWithValue("@Surname", "F%");

    SqlDataReader reader = command.ExecuteReader();

    // Вывод результатов на экран
    Console.WriteLine("Results:");
    Console.WriteLine("----------------------------------------");
    while (reader.Read())
    {
        string genderStr = reader["Gender"].ToString() == "1" ? "Male" : "Female";
        Console.WriteLine($"{reader["Surname"]} {reader["Name"]} {reader["Patronymic"]}, {((DateTime)reader["DateOfBirthday"]).ToString("yyyy-MM-dd")}, {genderStr}");
    }
    Console.WriteLine("----------------------------------------");

    // Остановка таймера и вывод времени выполнения
    stopwatch.Stop();
    Console.WriteLine($"Времени прошло: {stopwatch.ElapsedMilliseconds} ms");

    reader.Close();
    Console.Read();
}
*/