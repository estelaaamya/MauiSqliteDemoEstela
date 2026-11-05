
using SQLite;

namespace MauiSqliteDemoEstela;

public class LocalDbService
{
    private readonly SQLiteAsyncConnection _database;

    public LocalDbService()
    {
        string dbPath = Path.Combine(
            FileSystem.AppDataDirectory,
            "Clientes.db3"
        );

        _database = new SQLiteAsyncConnection(dbPath);

        _database.CreateTableAsync<Cliente>().Wait();
    }

    // Consultar todos los clientes
    public Task<List<Cliente>> GetClientes()
    {
        return _database.Table<Cliente>().ToListAsync();
    }

    // Consultar un cliente por su ID
    public Task<Cliente?> GetCliente(int id)
    {
        return _database.Table<Cliente>()
            .Where(x => x.Id == id)
            .FirstOrDefaultAsync();
    }

    // Crear un registro
    public Task<int> Create(Cliente cliente)
    {
        return _database.InsertAsync(cliente);
    }

    // Actualizar un registro
    public Task<int> Update(Cliente cliente)
    {
        return _database.UpdateAsync(cliente);
    }

    // Eliminar un registro
    public Task<int> Delete(Cliente cliente)
    {
        return _database.DeleteAsync(cliente);
    }
}
