using ShoppingCart.API.Models;
using System.Data;
using Dapper;
using ShoppingCart.API.Models.DTO;

namespace ShoppingCart.API.Repositories
{
    public class SqlClientClass : IRepository
    {
        private readonly IDbConnection _connection;

        public SqlClientClass(IDbConnection dbConnection)
        {
            this._connection = dbConnection;
        }

        public async Task<Customer> AddCustomer(CustomerDTO customer)
        {
            var parameters = new DynamicParameters();
            parameters.Add("Username", customer.Username);
            parameters.Add("Password", customer.Password);
            parameters.Add("Gender", customer.Gender);
            parameters.Add("PhoneNumber", customer.PhoneNumber);
            parameters.Add("State", customer.State);
            parameters.Add("NewCustomerID", dbType: DbType.Int32, direction: ParameterDirection.Output);
            await _connection.ExecuteAsync("CreateCustomer", parameters, commandType: CommandType.StoredProcedure);
            int InsertedCustomerID = parameters.Get<int>("@NewCustomerID");
            var NewCustomer = await GetCustomerByIdAsync(InsertedCustomerID);
            return NewCustomer;
        }

        public async Task<Customer> DeleteCustomerAsync(int id)
        {
            var customer = await GetCustomerByIdAsync(id);
            if (customer == null)
                return null;
            var param = new DynamicParameters();
            param.Add("@CustomerID", id, DbType.Int32);
            await _connection.ExecuteAsync("DeleteCustomer", param, commandType: CommandType.StoredProcedure);
            return customer;
        }

        public async Task<Customer> GetCustomerByIdAsync(int id)
        {
            var param = new DynamicParameters();
            param.Add("@CustomerID", id, DbType.Int32);
            var customer = await _connection.QueryFirstOrDefaultAsync<Customer>
                              ("GetCustomerById", param, commandType: CommandType.StoredProcedure);
            return customer;
        }

        public async Task<List<Customer>> GetCustomersAsync()
        {
            var customers = await _connection.QueryAsync<Customer>
                              ("GetAllCustomers", null, commandType: CommandType.StoredProcedure);
            return customers.ToList();
        }


        public async Task<Customer> UpdateCustomerAsync(int id, CustomerDTO customer)
        {
            var Cust = await GetCustomerByIdAsync(id);
            if (Cust == null)
                return null;
            var parameters = new DynamicParameters();
            parameters.Add("@CustomerID", id, DbType.Int32);
            parameters.Add("Username", customer.Username);
            parameters.Add("Password", customer.Password);
            parameters.Add("Gender", customer.Gender);
            parameters.Add("PhoneNumber", customer.PhoneNumber);
            parameters.Add("State", customer.State);

            await _connection.ExecuteAsync("UpdateCustomer", parameters, commandType: CommandType.StoredProcedure);
            Cust = await GetCustomerByIdAsync(id);
            return Cust;
        }


        // Product Method Implementations
        public async Task<Product> GetProductByIdAsync(int id)
        {
            var param = new DynamicParameters();
            param.Add("@ProductID", id, DbType.Int32);
            var product = await _connection.QueryFirstOrDefaultAsync<Product>
                              ("GetProductById", param, commandType: CommandType.StoredProcedure);
            return product;

        }

        public async Task<List<Product>> GetProductsAsync()
        {
            var Products = await _connection.QueryAsync<Product>
                              ("GetAllProducts", null, commandType: CommandType.StoredProcedure);
            return Products.ToList();
        }

        public async Task<Product> AddProduct(ProductDTO product)
        {
            var parameters = new DynamicParameters();
            parameters.Add("Name", product.Name, DbType.String);
            parameters.Add("Description", product.Description, DbType.String);
            parameters.Add("Price", product.Price, DbType.Decimal);
            parameters.Add("Stock", product.Stock, DbType.Int32);
            parameters.Add("ImageURL", product.ImageURL, DbType.String);
            parameters.Add("CategoryID", product.CategoryID, DbType.Int32);
            parameters.Add("NewProductID", dbType: DbType.Int32, direction: ParameterDirection.Output);
            await _connection.ExecuteAsync("CreateProduct", parameters, commandType: CommandType.StoredProcedure);
            int InsertedProductID = parameters.Get<int>("@NewProductID");
            var NewProduct = await GetProductByIdAsync(InsertedProductID);
            return NewProduct;
        }

        public async Task<Product> DeleteProductAsync(int id)
        {
            var Product = await GetProductByIdAsync(id);
            if (Product == null)
                return null;
            var param = new DynamicParameters();
            param.Add("@ProductID", id, DbType.Int32);
            await _connection.ExecuteAsync("DeleteProduct", param, commandType: CommandType.StoredProcedure);
            return Product;
        }

        public async Task<Product> UpdateProductAsync(int id, ProductDTO product)
        {
            var Product = await GetProductByIdAsync(id);
            if (Product == null)
                return null;
            var parameters = new DynamicParameters();
            parameters.Add("@ProductID", id, DbType.Int32);
            parameters.Add("Name", product.Name, DbType.String);
            parameters.Add("Description", product.Description, DbType.String);
            parameters.Add("Price", product.Price, DbType.Decimal);
            parameters.Add("Stock", product.Stock, DbType.Int32);
            parameters.Add("ImageURL", product.ImageURL, DbType.String);
            parameters.Add("CategoryID", product.CategoryID, DbType.Int32);

            await _connection.ExecuteAsync("UpdateProduct", parameters, commandType: CommandType.StoredProcedure);
            Product = await GetProductByIdAsync(id);
            return Product;
        }

        // Methods for Category

        public async Task<Category> AddCategory(CategoryDTO category)
        {
            var parameters = new DynamicParameters();
            parameters.Add("Name", category.Name, DbType.String);
            parameters.Add("NewCategoryID", dbType: DbType.Int32, direction: ParameterDirection.Output);
            await _connection.ExecuteAsync("CreateCategory", parameters, commandType: CommandType.StoredProcedure);
            int insertedCategoryId = parameters.Get<int>("@NewCategoryID");
            var newCategory = await GetCategoryByIdAsync(insertedCategoryId);
            return newCategory;
        }

        public async Task<Category> GetCategoryByIdAsync(int id)
        {
            var param = new DynamicParameters();
            param.Add("@CategoryID", id, DbType.Int32);
            var category = await _connection.QueryFirstOrDefaultAsync<Category>(
                "GetCategoryByID",
                param,
                commandType: CommandType.StoredProcedure
            );
            return category;
        }

        public async Task<List<Category>> GetCategoriesAsync()
        {
            var categories = await _connection.QueryAsync<Category>(
                "GetCategories",
                null,
                commandType: CommandType.StoredProcedure
            );
            return categories.ToList();
        }

        public async Task<Category> UpdateCategoryAsync(int id, CategoryDTO category)
        {
            var existingCategory = await GetCategoryByIdAsync(id);
            if (existingCategory == null)
                return null;

            var parameters = new DynamicParameters();
            parameters.Add("@CategoryID", id, DbType.Int32);
            parameters.Add("Name", category.Name, DbType.String);

            await _connection.ExecuteAsync("UpdateCategory", parameters, commandType: CommandType.StoredProcedure);
            existingCategory = await GetCategoryByIdAsync(id);
            return existingCategory;
        }

        public async Task<Category> DeleteCategoryAsync(int id)
        {
            var category = await GetCategoryByIdAsync(id);
            if (category == null)
                return null;

            var param = new DynamicParameters();
            param.Add("@CategoryID", id, DbType.Int32);
            await _connection.ExecuteAsync("DeleteCategory", param, commandType: CommandType.StoredProcedure);
            return category;
        }

        // Cart methods implementation
        public async Task<List<Cart>> GetCartsAsync()
        {
            var carts = await _connection.QueryAsync<Cart>("GetAllCartItems", null, commandType: CommandType.StoredProcedure);
            return carts.ToList();
        }

        public async Task<Cart> GetCartByUserIdAsync(int id)
        {
            var param = new DynamicParameters();
            param.Add("@UserID", id, DbType.Int32);
            var cart = await _connection.QueryFirstOrDefaultAsync<Cart>(
                "GetCartItemsByUserId",
                param,
                commandType: CommandType.StoredProcedure
            );
            return cart;
        }

        public async Task<Cart> GetCartByIdAsync(int id)
        {
            var param = new DynamicParameters();
            param.Add("@CartID", id, DbType.Int32);
            var cart = await _connection.QueryFirstOrDefaultAsync<Cart>(
                "GetCartItemById",
                param,
                commandType: CommandType.StoredProcedure
            );
            return cart;
        }

        public async Task<Cart> AddCart(CartDTO cart)
        {
            var parameters = new DynamicParameters();
            parameters.Add("UserID", cart.UserID, DbType.Int32);
            parameters.Add("ProductID", cart.ProductID, DbType.Int32);
            parameters.Add("Quantity", cart.Quantity, DbType.Int32);
            parameters.Add("NewCartID", dbType: DbType.Int32, direction: ParameterDirection.Output);

            await _connection.ExecuteAsync("CreateCartItem", parameters, commandType: CommandType.StoredProcedure);

            int newCartID = parameters.Get<int>("NewCartID");
            var newCart = await GetCartByIdAsync(newCartID);
            return newCart;
        }

        public async Task<Cart> UpdateCartItemQuantityByIdAsync(int id, int Quantity)
        {
            var existingCart = await GetCartByIdAsync(id);
            if (existingCart == null)
                return null;

            var parameters = new DynamicParameters();
            parameters.Add("@CartID", id, DbType.Int32);
            parameters.Add("Quantity", Quantity, DbType.Int32);

            await _connection.ExecuteAsync("UpdateCartItemQuantity", parameters, commandType: CommandType.StoredProcedure);

            var updatedCart = await GetCartByIdAsync(id);
            return updatedCart;
        }

        public async Task<Cart> DeleteCartByIdAsync(int id)
        {
            var cart = await GetCartByIdAsync(id);
            if (cart == null)
                return null;

            var param = new DynamicParameters();
            param.Add("@CartID", id, DbType.Int32);
            await _connection.ExecuteAsync("DeleteCartItem", param, commandType: CommandType.StoredProcedure);
            return cart;
        }


        // Orders Implementation
        public async Task<List<Order>> GetAllOrdersAsync()
        {
            var orders = await _connection.QueryAsync<Order>(
                "GetAllOrders",
                null,
                commandType: CommandType.StoredProcedure
            );
            return orders.ToList();
        }

        public async Task<Order> GetOrderByIdAsync(int id)
        {
            var param = new DynamicParameters();
            param.Add("@OrderID", id, DbType.Int32);
            var order = await _connection.QueryFirstOrDefaultAsync<Order>(
                "GetOrdersById",
                param,
                commandType: CommandType.StoredProcedure
            );
            return order;
        }

        public async Task<Order> CreateOrderAsync(OrderDTO order)
        {
            var parameters = new DynamicParameters();
            parameters.Add("UserID", order.UserID, DbType.Int32);
            parameters.Add("OrderDate", order.OrderDate, DbType.DateTime);
            parameters.Add("Status", order.Status, DbType.String);
            parameters.Add("NewOrderID", dbType: DbType.Int32, direction: ParameterDirection.Output);

            await _connection.ExecuteAsync("CreateOrder", parameters, commandType: CommandType.StoredProcedure);

            int newOrderID = parameters.Get<int>("NewOrderID");
            var newOrder = await GetOrderByIdAsync(newOrderID);
            return newOrder;
        }

        public async Task<List<Order>> GetOrdersByUserIdAsync(int userId)
        {
            var param = new DynamicParameters();
            param.Add("@UserID", userId, DbType.Int32);
            var orders = await _connection.QueryAsync<Order>(
                "GetOrdersByUserId",
                param,
                commandType: CommandType.StoredProcedure
            );
            return orders.ToList();
        }

        public async Task<Order> UpdateOrderStatusAsync(int id, string status)
        {
            var existingOrder = await GetOrderByIdAsync(id);
            if (existingOrder == null)
                return null;

            var parameters = new DynamicParameters();
            parameters.Add("@OrderID", id, DbType.Int32);
            parameters.Add("Status", status, DbType.String);

            await _connection.ExecuteAsync("UpdateOrderStatus", parameters, commandType: CommandType.StoredProcedure);

            var updatedOrder = await GetOrderByIdAsync(id);
            return updatedOrder;
        }

        public async Task<Order> DeleteOrderAsync(int id)
        {
            var order = await GetOrderByIdAsync(id);
            if (order == null)
                return null;

            var param = new DynamicParameters();
            param.Add("@OrderID", id, DbType.Int32);
            await _connection.ExecuteAsync("DeleteOrder", param, commandType: CommandType.StoredProcedure);
            return order;
        }
    }
}
