using System;
using System.Data;
using System.Data.SQLite;
using System.IO;

namespace SuperShopManagement
{
    public static class DatabaseHelper
    {
        private static string dbFile = "supershop.db";
        private static string connectionString = "Data Source=supershop.db;Version=3;";

        public static void InitializeDatabase()
        {
            if (!File.Exists(dbFile))
                SQLiteConnection.CreateFile(dbFile);

            using (SQLiteConnection con = new SQLiteConnection(connectionString))
            {
                con.Open();

                Run(con, @"CREATE TABLE IF NOT EXISTS Users (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    Username TEXT NOT NULL,
                    Password TEXT NOT NULL,
                    Role TEXT NOT NULL
                )");

                Run(con, @"CREATE TABLE IF NOT EXISTS Products (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    ProductName TEXT NOT NULL UNIQUE,
                    Price REAL NOT NULL
                )");

                Run(con, @"CREATE TABLE IF NOT EXISTS Sales (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    ProductName TEXT NOT NULL,
                    Price REAL NOT NULL,
                    Quantity REAL NOT NULL,
                    Total REAL NOT NULL,
                    SaleDate TEXT NOT NULL
                )");

                SQLiteCommand userCmd = new SQLiteCommand("SELECT COUNT(*) FROM Users", con);
                if ((long)userCmd.ExecuteScalar() == 0)
                {
                    Run(con, "INSERT INTO Users (Username, Password, Role) VALUES ('admin','1234','Admin')");
                    Run(con, "INSERT INTO Users (Username, Password, Role) VALUES ('employee','1234','Employee')");
                }

                SQLiteCommand productCmd = new SQLiteCommand("SELECT COUNT(*) FROM Products", con);
                if ((long)productCmd.ExecuteScalar() == 0)
                {
                    AddProduct("Rice", 80);
                    AddProduct("Oil", 180);
                    AddProduct("Sugar", 120);
                    AddProduct("Egg", 15);
                    AddProduct("Milk", 90);
                    AddProduct("Soap", 45);
                    AddProduct("Tea", 150);
                    AddProduct("Biscuit", 20);
                }
            }
        }

        private static void Run(SQLiteConnection con, string query)
        {
            using (SQLiteCommand cmd = new SQLiteCommand(query, con))
                cmd.ExecuteNonQuery();
        }

        public static string Login(string username, string password)
        {
            using (SQLiteConnection con = new SQLiteConnection(connectionString))
            {
                con.Open();

                string query = "SELECT Role FROM Users WHERE Username=@u AND Password=@p";

                using (SQLiteCommand cmd = new SQLiteCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@u", username);
                    cmd.Parameters.AddWithValue("@p", password);

                    object result = cmd.ExecuteScalar();

                    if (result != null)
                        return result.ToString();

                    return "";
                }
            }
        }

        public static DataTable GetProducts(string search = "")
        {
            using (SQLiteConnection con = new SQLiteConnection(connectionString))
            {
                con.Open();

                string query = "SELECT ProductName, Price FROM Products WHERE ProductName LIKE @s ORDER BY ProductName";

                using (SQLiteCommand cmd = new SQLiteCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@s", "%" + search + "%");

                    SQLiteDataAdapter adapter = new SQLiteDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    return dt;
                }
            }
        }

        public static bool AddProduct(string name, double price)
        {
            try
            {
                using (SQLiteConnection con = new SQLiteConnection(connectionString))
                {
                    con.Open();

                    string query = "INSERT INTO Products (ProductName, Price) VALUES (@n,@p)";

                    using (SQLiteCommand cmd = new SQLiteCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@n", name);
                        cmd.Parameters.AddWithValue("@p", price);
                        cmd.ExecuteNonQuery();
                        return true;
                    }
                }
            }
            catch
            {
                return false;
            }
        }

        public static bool UpdateProductPrice(string name, double price)
        {
            using (SQLiteConnection con = new SQLiteConnection(connectionString))
            {
                con.Open();

                string query = "UPDATE Products SET Price=@p WHERE ProductName=@n";

                using (SQLiteCommand cmd = new SQLiteCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@p", price);
                    cmd.Parameters.AddWithValue("@n", name);

                    int rows = cmd.ExecuteNonQuery();
                    return rows > 0;
                }
            }
        }

        public static long AddSale(string product, double price, double qty, double total)
        {
            using (SQLiteConnection con = new SQLiteConnection(connectionString))
            {
                con.Open();

                string query = @"
                INSERT INTO Sales (ProductName, Price, Quantity, Total, SaleDate)
                VALUES (@product,@price,@qty,@total,@date);
                SELECT last_insert_rowid();";

                using (SQLiteCommand cmd = new SQLiteCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@product", product);
                    cmd.Parameters.AddWithValue("@price", price);
                    cmd.Parameters.AddWithValue("@qty", qty);
                    cmd.Parameters.AddWithValue("@total", total);
                    cmd.Parameters.AddWithValue("@date", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

                    return (long)cmd.ExecuteScalar();
                }
            }
        }

        public static void UpdateSale(long id, double qty, double total)
        {
            using (SQLiteConnection con = new SQLiteConnection(connectionString))
            {
                con.Open();

                string query = "UPDATE Sales SET Quantity=@qty, Total=@total WHERE Id=@id";

                using (SQLiteCommand cmd = new SQLiteCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@qty", qty);
                    cmd.Parameters.AddWithValue("@total", total);
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public static void DeleteSale(long id)
        {
            using (SQLiteConnection con = new SQLiteConnection(connectionString))
            {
                con.Open();

                string query = "DELETE FROM Sales WHERE Id=@id";

                using (SQLiteCommand cmd = new SQLiteCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public static DataTable GetSalesByDate(DateTime date)
        {
            using (SQLiteConnection con = new SQLiteConnection(connectionString))
            {
                con.Open();

                string query = @"
                SELECT ProductName, Price, Quantity, Total, SaleDate
                FROM Sales
                WHERE date(SaleDate)=date(@date)
                ORDER BY SaleDate DESC";

                using (SQLiteCommand cmd = new SQLiteCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@date", date.ToString("yyyy-MM-dd"));

                    SQLiteDataAdapter adapter = new SQLiteDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    return dt;
                }
            }
        }
    }
}