using DataBaseFirst.Data;
using DataBaseFirst.Models;
using DataBaseFirst.Repository.InterfacesRepository;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace DataBaseFirst.Repository
{
    public class PrestamoRepository : IPrestamo
    {
        private readonly DBContext _dbContext;

        public PrestamoRepository(DBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Prestamo>> ListaPrestamos()
        {
            return await _dbContext.Prestamos.FromSqlRaw("EXEC PA_LISTAR_PRESTAMOS").ToListAsync();
        }
        public async Task<Prestamo> ObtenerPorID(int idPrestamo)
        {
            var id = new SqlParameter("@Id_Prestamo", idPrestamo);
            return await Task.Run(() => _dbContext.Prestamos.FromSqlRaw("EXEC PA_BUSCAR_PRESTAMO_ID @Id_Prestamo", id).AsNoTracking().AsEnumerable().FirstOrDefault());
        }

        public async Task<Prestamo> ObtenerPorUsuario(int idUsuario)
        {
            var id = new SqlParameter("@Id_Usuario", idUsuario);
            return await Task.Run(() => _dbContext.Prestamos.FromSqlRaw("EXEC PA_BUSCAR_PRESTAMO_USUARIO @Id_Usuario", id).AsNoTracking().AsEnumerable().FirstOrDefault());
        }

        public async Task<Prestamo> ObtenerPorLibro(int idLibro)
        {
            var id = new SqlParameter("@Id_Libro", idLibro);
            return await Task.Run(() => _dbContext.Prestamos.FromSqlRaw("EXEC PA_BUSCAR_PRESTAMO_LIBRO @Id_Libro", id).AsNoTracking().AsEnumerable().FirstOrDefault());
        }

        public async Task<int> RegistrarPrestamo(Prestamo prestamo)
        {
            return await _dbContext.Database.ExecuteSqlRawAsync("EXEC PA_REGISTRAR_PRESTAMO @Id_Usuario, @Id_Libro, @Fecha_Prestamo, @Fecha_Devolucion, @Estado",

                new SqlParameter("@Id_Usuario", prestamo.IdUsuario),
                new SqlParameter("@Id_Libro", prestamo.IdLibro),
                new SqlParameter("@Fecha_Prestamo", prestamo.FechaPrestamo ?? (object)DBNull.Value),
                new SqlParameter("@Fecha_Devolucion", prestamo.FechaDevolucion ?? (object)DBNull.Value),
                new SqlParameter("@Estado", prestamo.Estado ?? false)
                );
        }

    }
}
