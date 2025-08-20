using DataBaseFirst.Data;
using DataBaseFirst.Models;
using DataBaseFirst.Models.Dto;
using DataBaseFirst.Repository.InterfacesRepository;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace DataBaseFirst.Repository
{
    public class AutorRepository : IAutor
    {
        private readonly DBContext _dbContext;

        public AutorRepository(DBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Autor>> ListaAutores()
        {
            return await _dbContext.Autors.FromSqlRaw("EXEC PA_LISTAR_AUTORES").ToListAsync();
        }

        public async Task<Autor> ObtenerAutorNombre(string nombre)
        {
            var id = new SqlParameter("@Nombre", nombre);
            return await Task.Run(() => _dbContext.Autors.FromSqlRaw("EXEC PA_BUSCAR_NOMBRE @Nombre", id).AsNoTracking().AsEnumerable().FirstOrDefault());
        }

        public async Task<Autor> ObtenerAutorID(int idAutor)
        {
            var id = new SqlParameter("@Id_Autor", idAutor);
            return await Task.Run(() => _dbContext.Autors.FromSqlRaw("EXEC PA_BUSCAR_AUTOR_ID @Id_Autor", id).AsNoTracking().AsEnumerable().FirstOrDefault());
        }

        public async Task<AutorLibroDto> ObtenerAutorLibro(string nombre)
        {
            var autorLibro = new SqlParameter("@Nombre", nombre );
            return await Task.Run(() => _dbContext.AutorLibro.FromSqlRaw("EXEC PA_BUSCAR_AUTOR_LIBRO @Nombre", autorLibro).AsNoTracking().AsEnumerable().FirstOrDefault());
        }


        public async Task<int> RegistrarAutor(Autor autor)
        {
            return await _dbContext.Database.ExecuteSqlRawAsync("EXEC PA_REGISTRAR_AUTOR @nombre, @nacionalidad, @estado",
                new SqlParameter("@nombre", autor.Nombre ??(object)DBNull.Value),
                new SqlParameter("@nacionalidad", autor.Nacionalidad ?? (object)DBNull.Value),
                new SqlParameter("@estado", autor.Estado ?? false)
                );
        }

        public async Task<int> EditarAutor(Autor autor)
        {
            return await _dbContext.Database.ExecuteSqlRawAsync("EXEC PA_EDITAR_AUTOR @Id_Autor, @nombre, @nacionalidad, @estado",
                new SqlParameter("@Id_Autor", autor.IdAutor),
                new SqlParameter("@nombre", autor.Nombre ?? (object)DBNull.Value),
                new SqlParameter("@nacionalidad", autor.Nacionalidad ?? (object)DBNull.Value),
                new SqlParameter("@estado", autor.Estado ?? false)
                );
        }

        public async Task<int> EliminarAutor(int id)
        {
            return await _dbContext.Database.ExecuteSqlRawAsync("EXEC PA_ELIMINAR_AUTOR @Id_Autor",
                new SqlParameter("@Id_Autor", id)
            );
        }

    }
}
