using api;
using System.Data.SqlClient;

public class DatabaseManager
{
    private readonly string _connectionString;

    public DatabaseManager(string connectionString)
    {
        _connectionString = connectionString;
    }

    public async Task<List<Payment>> GetProductsAsync()
    {
        List<Payment> products = new List<Payment>();
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            string query = "SELECT Id, Name, Price FROM Products";
            SqlCommand command = new SqlCommand(query, connection);
            await connection.OpenAsync();
            using (SqlDataReader reader = await command.ExecuteReaderAsync())
            {
                while (await reader.ReadAsync())
                {
                    products.Add(new Payment
                    {
                        Id = (int)reader["Id"],
                        CustomerName = (string)reader["Name"],
                        Amount = (decimal)reader["Price"]
                    });
                }
            }
        }
        return products;
    }
}

