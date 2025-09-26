using SalesETL.Models;
using SalesETL.Services;

class Program
{
    static void Main()
    {
        Console.WriteLine("- Iniciando proceso ETL...");

        try
        {
            var extractor = new DataExtractor();
            var transformer = new DataTransformer();
            var loader = new DataLoader("Server=localhost;Database=SalesAnalysisDB;Trusted_Connection=True;TrustServerCertificate=true;");

            string[] files = { "customers.csv", "products.csv", "orders.csv", "order_details.csv" };
            foreach (var file in files)
            {
                if (File.Exists(file))
                    Console.WriteLine($"- Archivo encontrado: {file}");
                else
                    Console.WriteLine($"- Archivo NO encontrado: {file}");
            }

            Console.WriteLine("\n- Extrayendo datos de archivos CSV...");
            var customers = extractor.ExtractCustomers("customers.csv");
            var products = extractor.ExtractProducts("products.csv");
            var orders = extractor.ExtractOrders("orders.csv");
            var orderDetails = extractor.ExtractOrderDetails("order_details.csv");

            Console.WriteLine($"\n- Resumen extracción: {customers.Count} clientes, {products.Count} productos, {orders.Count} pedidos, {orderDetails.Count} detalles");

            if (customers.Count == 0 && products.Count == 0 && orders.Count == 0 && orderDetails.Count == 0)
            {
                Console.WriteLine("- No se extrajeron datos. Verifica los archivos CSV.");
                Console.ReadKey();
                return;
            }

            Console.WriteLine("\n- Transformando datos...");
            transformer.TransformData(customers, products, orders, orderDetails);
            Console.WriteLine($"- Resumen transformación: {customers.Count} clientes, {products.Count} productos, {orders.Count} pedidos, {orderDetails.Count} detalles");

            Console.WriteLine("\n- Cargando datos a la base de datos...");
            loader.LoadCustomers(customers);
            loader.LoadProducts(products);
            loader.LoadOrders(orders);
            loader.LoadOrderDetails(orderDetails);

            Console.WriteLine("\n- ETL completado exitosamente!");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"\n- Error durante el proceso ETL: {ex.Message}");
            Console.WriteLine($"Detalles: {ex.InnerException?.Message}");
        }

        Console.WriteLine("\nPresiona cualquier tecla para salir...");
        Console.ReadKey();
    }
}