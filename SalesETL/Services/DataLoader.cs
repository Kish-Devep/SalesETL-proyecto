using Microsoft.Data.SqlClient;
using SalesETL.Models;


namespace SalesETL.Services
{
    public class DataLoader
    {
        private string _connectionString;

        public DataLoader(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void LoadCustomers(List<Customer> customers)
        {
            using var connection = new SqlConnection(_connectionString);
            connection.Open();

            foreach (var customer in customers)
            {
                var command = new SqlCommand(
                    "INSERT INTO Customers (CustomerID, FirstName, LastName, Email, Phone, City, Country) " +
                    "VALUES (@id, @first, @last, @email, @phone, @city, @country)",
                    connection);

                command.Parameters.AddWithValue("@id", customer.CustomerID);
                command.Parameters.AddWithValue("@first", customer.FirstName ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@last", customer.LastName ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@email", customer.Email ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@phone", customer.Phone ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@city", customer.City ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@country", customer.Country ?? (object)DBNull.Value);

                command.ExecuteNonQuery();
            }
        }

        public void LoadProducts(List<Product> products)
        {
            using var connection = new SqlConnection(_connectionString);
            connection.Open();

            foreach (var product in products)
            {
                var command = new SqlCommand(
                    "INSERT INTO Products (ProductID, ProductName, Category, Price, Stock) " +
                    "VALUES (@id, @name, @category, @price, @stock)",
                    connection);

                command.Parameters.AddWithValue("@id", product.ProductID);
                command.Parameters.AddWithValue("@name", product.ProductName ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@category", product.Category ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@price", product.Price);
                command.Parameters.AddWithValue("@stock", product.Stock);

                command.ExecuteNonQuery();
            }
        }

        public void LoadOrders(List<Order> orders)
        {
            using var connection = new SqlConnection(_connectionString);
            connection.Open();

            foreach (var order in orders)
            {
                var command = new SqlCommand(
                    "INSERT INTO Orders (OrderID, CustomerID, OrderDate, Status) " +
                    "VALUES (@id, @customerId, @date, @status)",
                    connection);

                command.Parameters.AddWithValue("@id", order.OrderID);
                command.Parameters.AddWithValue("@customerId", order.CustomerID);
                command.Parameters.AddWithValue("@date", order.OrderDate);
                command.Parameters.AddWithValue("@status", order.Status ?? (object)DBNull.Value);

                command.ExecuteNonQuery();
            }
        }

        public void LoadOrderDetails(List<OrderDetail> orderDetails)
        {
            using var connection = new SqlConnection(_connectionString);
            connection.Open();

            foreach (var detail in orderDetails)
            {
                var command = new SqlCommand(
                    "INSERT INTO OrderDetails (OrderID, ProductID, Quantity, TotalPrice) " +
                    "VALUES (@orderId, @productId, @quantity, @totalPrice)",
                    connection);

                command.Parameters.AddWithValue("@orderId", detail.OrderID);
                command.Parameters.AddWithValue("@productId", detail.ProductID);
                command.Parameters.AddWithValue("@quantity", detail.Quantity);
                command.Parameters.AddWithValue("@totalPrice", detail.TotalPrice);

                command.ExecuteNonQuery();
            }
        }
    }
}
