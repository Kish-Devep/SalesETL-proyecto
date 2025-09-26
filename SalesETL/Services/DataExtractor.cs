using SalesETL.Models;
using System.Globalization;

namespace SalesETL.Services
{
    public class DataExtractor
    {
        public List<Customer> ExtractCustomers(string filePath)
        {
            var customers = new List<Customer>();


            if (!File.Exists(filePath))
            {
                Console.WriteLine($" Archivo no encontrado: {filePath}");
                return customers;
            }

            using var reader = new StreamReader(filePath);
            var headerLine = reader.ReadLine(); // Leer encabezado

            if (headerLine == null) return customers;

            // Detectar automáticamente si usa tabulaciones o comas
            char delimiter = headerLine.Contains('\t') ? '\t' : ',';
            Console.WriteLine($"- Detectado delimitador: {(delimiter == '\t' ? "TAB" : "COMA")}");

            int lineNumber = 1;
            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();
                lineNumber++;

                if (string.IsNullOrWhiteSpace(line)) continue;

                try
                {
                    var values = line.Split(delimiter);

                    // Validar que tenga suficientes columnas
                    if (values.Length >= 7)
                    {
                        customers.Add(new Customer
                        {
                            CustomerID = int.Parse(values[0].Trim()),
                            FirstName = values[1].Trim(),
                            LastName = values[2].Trim(),
                            Email = values[3].Trim(),
                            Phone = values[4].Trim(),
                            City = values[5].Trim(),
                            Country = values[6].Trim()
                        });
                    }
                    else
                    {
                        Console.WriteLine($"- Línea {lineNumber} ignorada (columnas insuficientes): {line}");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"- Error en línea {lineNumber}: {ex.Message}");
                }
            }

            Console.WriteLine($"- Clientes extraídos: {customers.Count}");
            return customers;
        }

        public List<Product> ExtractProducts(string filePath)
        {
            var products = new List<Product>();

            if (!File.Exists(filePath))
            {
                Console.WriteLine($"❌ Archivo no encontrado: {filePath}");
                return products;
            }

            using var reader = new StreamReader(filePath);
            var headerLine = reader.ReadLine();

            if (headerLine == null) return products;

            char delimiter = headerLine.Contains('\t') ? '\t' : ',';
            Console.WriteLine($"- Detectado delimitador: {(delimiter == '\t' ? "TAB" : "COMA")}");

            int lineNumber = 1;
            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();
                lineNumber++;

                if (string.IsNullOrWhiteSpace(line)) continue;

                try
                {
                    var values = line.Split(delimiter);

                    if (values.Length >= 5)
                    {
                        products.Add(new Product
                        {
                            ProductID = int.Parse(values[0].Trim()),
                            ProductName = values[1].Trim(),
                            Category = values[2].Trim(),
                            Price = decimal.Parse(values[3].Trim()),
                            Stock = int.Parse(values[4].Trim())
                        });
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"- Error en línea {lineNumber}: {ex.Message}");
                }
            }

            Console.WriteLine($"- Productos extraídos: {products.Count}");
            return products;
        }

        public List<Order> ExtractOrders(string filePath)
        {
            var orders = new List<Order>();

            if (!File.Exists(filePath))
            {
                Console.WriteLine($"- Archivo no encontrado: {filePath}");
                return orders;
            }

            using var reader = new StreamReader(filePath);
            var headerLine = reader.ReadLine();

            if (headerLine == null) return orders;

            char delimiter = headerLine.Contains('\t') ? '\t' : ',';
            Console.WriteLine($"- Detectado delimitador: {(delimiter == '\t' ? "TAB" : "COMA")}");

            int lineNumber = 1;
            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();
                lineNumber++;

                if (string.IsNullOrWhiteSpace(line)) continue;

                try
                {
                    var values = line.Split(delimiter);

                    if (values.Length >= 4)
                    {
                        orders.Add(new Order
                        {
                            OrderID = int.Parse(values[0].Trim()),
                            CustomerID = int.Parse(values[1].Trim()),
                            OrderDate = DateTime.Parse(values[2].Trim()),
                            Status = values[3].Trim()
                        });
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"- Error en línea {lineNumber}: {ex.Message}");
                }
            }

            Console.WriteLine($"- Pedidos extraídos: {orders.Count}");
            return orders;
        }

        public List<OrderDetail> ExtractOrderDetails(string filePath)
        {
            var orderDetails = new List<OrderDetail>();

            if (!File.Exists(filePath))
            {
                Console.WriteLine($"- Archivo no encontrado: {filePath}");
                return orderDetails;
            }

            using var reader = new StreamReader(filePath);
            var headerLine = reader.ReadLine();

            if (headerLine == null) return orderDetails;

            char delimiter = headerLine.Contains('\t') ? '\t' : ',';
            Console.WriteLine($"- Detectado delimitador: {(delimiter == '\t' ? "TAB" : "COMA")}");

            int lineNumber = 1;
            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();
                lineNumber++;

                if (string.IsNullOrWhiteSpace(line)) continue;

                try
                {
                    var values = line.Split(delimiter);

                    if (values.Length >= 4)
                    {
                        orderDetails.Add(new OrderDetail
                        {
                            OrderID = int.Parse(values[0].Trim()),
                            ProductID = int.Parse(values[1].Trim()),
                            Quantity = int.Parse(values[2].Trim()),
                            TotalPrice = decimal.Parse(values[3].Trim())
                        });
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"- Error en línea {lineNumber}: {ex.Message}");
                }
            }

            Console.WriteLine($"- Detalles extraídos: {orderDetails.Count}");
            return orderDetails;
        }
    }
}
 

