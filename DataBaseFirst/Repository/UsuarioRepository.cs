using DataBaseFirst.Data;
using DataBaseFirst.Models;
using DataBaseFirst.Models.Dto;
using DataBaseFirst.Repository.InterfacesRepository;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Utilities.Security;

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

        /*  public async Task<Usuario1> ObtenerUsuarioNombre(string nombres)
          {
              var id = new SqlParameter("@Nombres", nombres);
              return await Task.Run(() => _dbContext.Usuarios.FromSqlRaw("EXEC PA_BUSCAR_NOMBRE_USUARIO @nombres", id).AsNoTracking().AsEnumerable().FirstOrDefault());
          }*/

        public async Task<Usuario> ObtenerUsuarioID(int idUsuario)
        {
            var id = new SqlParameter("@id_usuario", idUsuario);
            return await Task.Run(() => _dbContext.Usuarios.FromSqlRaw("EXEC PA_BUSCAR_USUARIO_ID @id_usuario", id).AsNoTracking().AsEnumerable().FirstOrDefault());
        }
        public async Task<UsuarioRolDto?> IniciarSesion(LoginDto login)
        {
            string claveEncriptada = Encriptacion.EncriptarClave(login.clave ?? "");

            var usuario = new SqlParameter("@usuario", login.usuario ?? (object)DBNull.Value);
            var clave = new SqlParameter("@clave", claveEncriptada);
            return await Task.Run(() => _dbContext.UsuarioRol.FromSqlRaw("EXEC PA_INICIAR_SESION @usuario, @clave", usuario, clave).AsNoTracking().AsEnumerable().FirstOrDefault());
        }

        public async Task<int> RegistrarUsuario(Usuario usuario)
        {
            string claveEncriptada = Encriptacion.EncriptarClave(usuario.Clave ?? "");

            return await _dbContext.Database.ExecuteSqlRawAsync("EXEC PA_REGISTRAR_USUARIO @usuario, @correo, @clave, @id_rol, @Estado",

                new SqlParameter("@usuario", usuario.Usuario1 ?? (object)DBNull.Value),
                new SqlParameter("@correo", usuario.Correo ?? (object)DBNull.Value),
                new SqlParameter("@clave", claveEncriptada),
                new SqlParameter("@id_rol", usuario.IdRol),
                new SqlParameter("@Estado", usuario.Estado ?? false)
                );
        }

        public async Task<int> EditarUsuario(Usuario usuario)
        {
            string claveEncriptada = Encriptacion.EncriptarClave(usuario.Clave ?? "");

            return await _dbContext.Database.ExecuteSqlRawAsync("EXEC PA_EDITAR_USUARIO @id_usuario, @usuario, @correo, @clave, @id_rol, @Estado",

                new SqlParameter("@usuario", usuario.Usuario1 ?? (object)DBNull.Value),
                new SqlParameter("@correo", usuario.Correo ?? (object)DBNull.Value),
                new SqlParameter("@clave", claveEncriptada),
                new SqlParameter("@id_rol", usuario.IdRol),
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
