using DataBaseFirst.Data;
using DataBaseFirst.Models;
using DataBaseFirst.Models.Dto;
using DataBaseFirst.Repository.InterfacesRepository;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace DataBaseFirst.Repository
{
    public class UsuarioRepository : IUsuario
    {
        private readonly DBContext _dbContext; 

        public UsuarioRepository(DBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Usuario>> ListaUsuarios()
        {
            return await _dbContext.Usuarios.FromSqlRaw("EXEC PA_LISTAR_USUARIOS").ToListAsync();
        }

        public async Task<Usuario> ObtenerUsuarioNombre(string nombres)
        {
            var id = new SqlParameter("@Nombres", nombres);
            return await Task.Run(() => _dbContext.Usuarios.FromSqlRaw("EXEC PA_BUSCAR_NOMBRE_USUARIO @nombres", id).AsNoTracking().AsEnumerable().FirstOrDefault());
        }

        public async Task<Usuario> ObtenerUsuarioID(int idUsuario)
        {
            var id = new SqlParameter("@Id_Usuario", idUsuario);
            return await Task.Run(() => _dbContext.Usuarios.FromSqlRaw("EXEC PA_BUSCAR_ID_USUARIO @Id_Usuario", id).AsNoTracking().AsEnumerable().FirstOrDefault());
        }

        public async Task<UsuarioLibroPrestamoDto> ObtenerUsuarioLibroPrestamo(string nombres)
        {
            var usuarioLibroPrestamo = new SqlParameter("@Nombres", nombres);
            return await Task.Run(() => _dbContext.UsuarioLibroPrestamo.FromSqlRaw("EXEC PA_BUSCAR_USUARIO_LIBRO_PRESTAMO @Nombres", usuarioLibroPrestamo).AsNoTracking().AsEnumerable().FirstOrDefault());
        }

        public async Task<int> RegistrarUsuario(Usuario usuario)
        {
            return await _dbContext.Database.ExecuteSqlRawAsync("EXEC PA_REGISTRAR_USUARIO @Nombres, @Apellidos, @Correo, @Estado",

                new SqlParameter("@Nombres", usuario.Nombres ?? (object)DBNull.Value),
                new SqlParameter("@Apellidos", usuario.Apellidos ?? (object)DBNull.Value),
                new SqlParameter("@Correo", usuario.Correo ?? (object)DBNull.Value),
                new SqlParameter("@Estado", usuario.Estado ?? false)
                );
        }

        public async Task<int> EditarUsuario(Usuario usuario)
        {
            return await _dbContext.Database.ExecuteSqlRawAsync("EXEC PA_EDITAR_USUARIO @Id_Usuario, @Nombres, @Apellidos, @Correo, @Estado",

                new SqlParameter("@Id_Usuario", usuario.IdUsuario),
                new SqlParameter("@Nombres", usuario.Nombres ?? (object)DBNull.Value),
                new SqlParameter("@Apellidos", usuario.Apellidos ?? (object)DBNull.Value),
                new SqlParameter("@Correo", usuario.Correo ?? (object)DBNull.Value),
                new SqlParameter("@Estado", usuario.Estado ?? false)
                );
        }

        public async Task<int> EliminarUsuario(int id)
        {
            return await _dbContext.Database.ExecuteSqlRawAsync("EXEC PA_ELIMINAR_USUARIO @Id_Usuario",
                new SqlParameter("@Id_Usuario", id)
                );
        }

    }
}
