using DataBaseFirst.Data;
using DataBaseFirst.Models;
using DataBaseFirst.Repository.InterfacesRepository;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace DataBaseFirst.Repository
{
    public class LibroRepository : ILibro
    {
        private readonly DBContext _dbContext;

        public LibroRepository(DBContext dbContext) {

            _dbContext = dbContext;
        }

        public async Task<List<Libro>> ListaLibros()
        {
            return await _dbContext.Libros.FromSqlRaw("EXEC PA_LISTAR_LIBROS").ToListAsync();
        }

        public async Task<Libro> BuscarPorTitulo(string titulo)
        {
            var id = new SqlParameter("@Titulo", titulo);
            return await Task.Run(() => _dbContext.Libros.FromSqlRaw("EXEC PA_BUSCAR_TITULO @Titulo", id).AsNoTracking().AsEnumerable().FirstOrDefault());
        }

        public async Task<Libro> BuscarPorId(int idLibro)
        {
            var id = new SqlParameter("@Id_Libro", idLibro);
            return await Task.Run(() => _dbContext.Libros.FromSqlRaw("EXEC PA_BUSCAR_LIBRO_ID @Id_Libro", id).AsNoTracking().AsEnumerable().FirstOrDefault());
        }

        public async Task<int> RegistrarLibro(Libro libro)
        {
            return await _dbContext.Database.ExecuteSqlRawAsync("EXEC PA_REGISTRAR_LIBRO @Titulo, @Genero, @Id_Autor, @Numero_Paginas, @Fecha_Publicacion, @Estado",
                new SqlParameter("@Titulo", libro.Titulo ??(object)DBNull.Value),
                new SqlParameter("@Genero", libro.Genero ?? (object)DBNull.Value),
                new SqlParameter("@Id_Autor", libro.IdAutor),
                new SqlParameter("@Numero_Paginas", libro.NumeroPaginas ?? 0),
                new SqlParameter("@Fecha_Publicacion", libro.FechaPublicacion ??(object)DBNull.Value),
                new SqlParameter("@Estado",libro.Estado ?? false)

                );
        }

        public async Task<int> EditarLibro(Libro libro)
        {
            return await _dbContext.Database.ExecuteSqlRawAsync("EXEC PA_EDITAR_LIBRO @Id_Libro, @Titulo, @Genero, @Id_Autor, @Numero_Paginas, @Fecha_Publicacion, @Estado",
                new SqlParameter("@Id_Libro", libro.IdLibro),
                new SqlParameter("@Titulo", libro.Titulo ?? (object)DBNull.Value),
                new SqlParameter("@Genero", libro.Genero ?? (object)DBNull.Value),
                new SqlParameter("@Id_Autor", libro.IdAutor),
                new SqlParameter("@Numero_Paginas", libro.NumeroPaginas ?? 0),
                new SqlParameter("@Fecha_Publicacion", libro.FechaPublicacion ?? (object)DBNull.Value),
                new SqlParameter("@Estado", libro.Estado ?? false)
                );
        }

        public async Task<int> EliminarLibro(int idLibro)
        {

            return await _dbContext.Database.ExecuteSqlRawAsync("EXEC PA_ELIMINAR_LIBRO @Id_Libro",
                new SqlParameter("@Id_Libro", idLibro)

                );
        }
    }
}
