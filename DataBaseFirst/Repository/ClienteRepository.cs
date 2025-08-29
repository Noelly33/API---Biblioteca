using DataBaseFirst.Data;
using DataBaseFirst.Models;
using DataBaseFirst.Models.Dto;
using DataBaseFirst.Repository.InterfacesRepository;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBaseFirst.Repository
{
    public class ClienteRepository : ICliente
    {

        private readonly DBContext _dbContext;

        public ClienteRepository(DBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Cliente>> ListaClientes()
        {
            return await _dbContext.Clientes.FromSqlRaw("EXEC PA_LISTAR_CLIENTES").ToListAsync();
        }

        public async Task<Cliente> ObtenerPorNombre(string nombres)
        {
            var nombre = new SqlParameter("@Nombres", nombres);
            return await Task.Run(() => _dbContext.Clientes.FromSqlRaw("EXEC PA_BUSCAR_NOMBRE_CLIENTE @Nombres", nombre).AsNoTracking().AsEnumerable().FirstOrDefault());
        }

        public async Task<Cliente> ObtenerPorID(int idCliente)
        {
            var id = new SqlParameter("@Id_Cliente", idCliente);
            return await Task.Run(() => _dbContext.Clientes.FromSqlRaw("EXEC PA_BUSCAR_CLIENTE_ID @Id_Cliente", id).AsNoTracking().AsEnumerable().FirstOrDefault());
        }

        public async Task<ClienteLibroPrestamoDto> ObtenerClienteLibroPrestamo(string nombres)
        {
            var ClienteLibroPrestamo = new SqlParameter("@Nombres", nombres);
            return await Task.Run(() => _dbContext.ClienteLibroPrestamo.FromSqlRaw("EXEC PA_BUSCAR_CLIENTE_LIBRO_PRESTAMO @nombres", ClienteLibroPrestamo).AsNoTracking().AsEnumerable().FirstOrDefault());
        }


        public async Task<int> RegistrarCliente(Cliente cliente)
        {
            return await _dbContext.Database.ExecuteSqlRawAsync("EXEC PA_REGISTRAR_CLIENTE @nombres, @apellidos, @cedula, @estado",
                new SqlParameter("@nombres", cliente.Nombres ?? (object)DBNull.Value),
                new SqlParameter("@Apellidos", cliente.Apellidos ?? (object)DBNull.Value),
                new SqlParameter("@cedula", cliente.Cedula ?? (object)DBNull.Value),
                new SqlParameter("@nombre", cliente.Cedula ?? (object)DBNull.Value),
                new SqlParameter("@estado", cliente.Estado ?? false)
                );
        }

        public async Task<int> EditarCliente(Cliente cliente)
        {
            return await _dbContext.Database.ExecuteSqlRawAsync("EXEC PA_EDITAR_CLIENTE @id_cliente, @nombres, @apellidos, @cedula, @estado",
                new SqlParameter("@id_cliente", cliente.IdCliente),
                new SqlParameter("@nombres", cliente.Nombres ?? (object)DBNull.Value),
                new SqlParameter("@Apellidos", cliente.Apellidos ?? (object)DBNull.Value),
                new SqlParameter("@cedula", cliente.Cedula ?? (object)DBNull.Value),
                new SqlParameter("@nombre", cliente.Cedula ?? (object)DBNull.Value),
                new SqlParameter("@estado", cliente.Estado ?? false)
                );
        }

        public async Task<int> EliminarCliente(int id)
        {
            return await _dbContext.Database.ExecuteSqlRawAsync("EXEC PA_ELIMINAR_Cliente @id_cliente",
                new SqlParameter("@id_cliente", id)
            );
        }

    }
}
