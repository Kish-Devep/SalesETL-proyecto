using SalesETL.Models;


namespace SalesETL.Services
{
    public class DataTransformer
    {
        public void TransformData(
            List<Customer> customers,
            List<Product> products,
            List<Order> orders,
            List<OrderDetail> orderDetails)
        {
            // Eliminar duplicados de customers por CustomerID
            var uniqueCustomers = customers.GroupBy(c => c.CustomerID)
                                           .Select(g => g.First())
                                           .ToList();
            customers.Clear();
            customers.AddRange(uniqueCustomers);

            // Eliminar duplicados de products por ProductID
            var uniqueProducts = products.GroupBy(p => p.ProductID)
                                         .Select(g => g.First())
                                         .ToList();
            products.Clear();
            products.AddRange(uniqueProducts);

            // Eliminar duplicados de orders por OrderID
            var uniqueOrders = orders.GroupBy(o => o.OrderID)
                                     .Select(g => g.First())
                                     .ToList();
            orders.Clear();
            orders.AddRange(uniqueOrders);

            // Eliminar duplicados de orderDetails por OrderID y ProductID
            var uniqueOrderDetails = orderDetails.GroupBy(od => new { od.OrderID, od.ProductID })
                                                 .Select(g => g.First())
                                                 .ToList();
            orderDetails.Clear();
            orderDetails.AddRange(uniqueOrderDetails);

            // Validar referencias: orderDetails debe tener OrderID y ProductID existentes
            orderDetails.RemoveAll(od =>
                !orders.Any(o => o.OrderID == od.OrderID) ||
                !products.Any(p => p.ProductID == od.ProductID));

            // Validar orders: deben tener CustomerID existente
            orders.RemoveAll(o => !customers.Any(c => c.CustomerID == o.CustomerID));
        }
    }
}
