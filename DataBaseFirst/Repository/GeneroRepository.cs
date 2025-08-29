using DataBaseFirst.Data;
using DataBaseFirst.Models;
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
    public class GeneroRepository : IGenero
    {
        private readonly DBContext _dbContext;

        public GeneroRepository(DBContext dbContext)
        {

            _dbContext = dbContext;
        }

        public async Task<List<Genero>> ListaGeneros()
        {
            return await _dbContext.Generos.FromSqlRaw("EXEC PA_LISTAR_GENEROS").ToListAsync();
        }

        public async Task<Genero> ObtenerPorNombre(string nombre)
        {
            var id = new SqlParameter("@nombre", nombre);
            return await Task.Run(() => _dbContext.Generos.FromSqlRaw("EXEC PA_BUSCAR_NOMBRE_GENERO @nombre", id).AsNoTracking().AsEnumerable().FirstOrDefault());
        }

        public async Task<Genero> ObtenerPorID(int idGenero)
        {
            var id = new SqlParameter("@Id_Genero", idGenero);
            return await Task.Run(() => _dbContext.Generos.FromSqlRaw("EXEC PA_BUSCAR_GENERO_ID @Id_Genero", id).AsNoTracking().AsEnumerable().FirstOrDefault());
        }

        public async Task<int> RegistrarGenero(Genero genero)
        {
            return await _dbContext.Database.ExecuteSqlRawAsync("EXEC PA_REGISTRAR_GENERO @nombre, @estado",
                new SqlParameter("@nombre", genero.Nombre ?? (object)DBNull.Value),
                new SqlParameter("@Estado", genero.Estado ?? false)
                );
        }

        public async Task<int> EditarGenero(Genero genero)
        {
            return await _dbContext.Database.ExecuteSqlRawAsync("EXEC PA_EDITAR_GENERO @Id_Genero, @nombre, @estado",
               new SqlParameter("@Id_Genero", genero.IdGenero),
               new SqlParameter("@nombre", genero.Nombre ?? (object)DBNull.Value),
               new SqlParameter("@Estado", genero.Estado ?? false)
               );
        }

        public async Task<int> EliminarGenero(int idGenero)
        {

            return await _dbContext.Database.ExecuteSqlRawAsync("EXEC PA_ELIMINAR_GENERO @Id_Genero",
                new SqlParameter("@Id_Genero", idGenero)

                );
        }
    }
}
