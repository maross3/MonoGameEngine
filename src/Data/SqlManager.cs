using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Npgsql;
namespace TestMonogame.Data;

public class SqlManager
{
    private static NpgsqlConnection _connection;

    public SqlManager() =>
        ConnectSqlAsync();

    private async void ConnectSqlAsync()
    {

        var cs = await File.ReadAllTextAsync("./database.txt");
        _connection = new NpgsqlConnection(cs);
        _connection.Open();
        await CreateTableIfNotExists();
    }

    private const string TABLE_NAME = "Inventory";

    public static async Task Add(Inventory inventory)
    {
        const string commandText = 
            $"INSERT INTO {TABLE_NAME} (ownerId, spaceAllotment, itemAndQuantity) VALUES " +
            "(@id, @space, @dict)";
        
        await using var cmd = new NpgsqlCommand(commandText, _connection);
        cmd.Parameters.AddWithValue("id", inventory.OwnerId);
        cmd.Parameters.AddWithValue("space", inventory.SpaceAllotment);
        cmd.Parameters.AddWithValue("dict", JsonConvert.SerializeObject(inventory));

        await cmd.ExecuteNonQueryAsync();
    }

    public static async Task<Inventory> Get(int ownerId)
    {
        const string commandText = $"SELECT * FROM {TABLE_NAME} WHERE ID = @id";

        await using var cmd = new NpgsqlCommand(commandText, _connection);
        cmd.Parameters.AddWithValue("id", ownerId);

        await using var reader = await cmd.ExecuteReaderAsync();
        while (await reader.ReadAsync())
        {
            var inv = ReadInventory(reader);
            return inv;
        }

        return null;
    }

    private static Inventory ReadInventory(IDataRecord reader)
    {
        var id = (int?)reader["ownerId"];
        var space = (int?)reader["spaceAlloted"];
        var itemAndQuantity = reader["itemAndQuantity"] as string;

        var inv = new Inventory
        {
            OwnerId = (int)id,
            SpaceAllotment = (int)space,
            ItemAndQuantityDictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(itemAndQuantity)
        };
        return inv;
    }

    public static async Task Update(Inventory inventory)
    {
        const string commandText = $@"UPDATE {TABLE_NAME}
                SET spaceAllotment = @space, itemAndQuantity = @iqDict
                WHERE ownerId = @id";

        await using var cmd = new NpgsqlCommand(commandText, _connection);

        cmd.Parameters.AddWithValue("id", inventory.OwnerId);
        cmd.Parameters.AddWithValue("space", inventory.SpaceAllotment);
        cmd.Parameters.AddWithValue("iqDict", JsonConvert.SerializeObject(inventory.ItemAndQuantityDictionary));

        await cmd.ExecuteNonQueryAsync();
    }

    public static async Task Delete(int ownerId)
    {
        var commandText = $"DELETE FROM {TABLE_NAME} WHERE ownerId=(@id)";
        await using var cmd = new NpgsqlCommand(commandText, _connection);

        cmd.Parameters.AddWithValue("id", ownerId);
        await cmd.ExecuteNonQueryAsync();
    }

    public async Task CreateTableIfNotExists()
    {
        const string sql = $"CREATE TABLE if not exists {TABLE_NAME}" +
                           "(" +
                           "ownerId serial PRIMARY KEY, " +
                           "spaceAllotment INTEGER NOT NULL, " +
                           "itemAndQuantity VARCHAR" +
                           ")";

        await using var cmd = new NpgsqlCommand(sql, _connection);
        await cmd.ExecuteNonQueryAsync();
    }
}