using DataBaseFirst.Data;
using DataBaseFirst.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBaseFirst.Service
{
    public class MenuService
    {
        private readonly DBContext _context;
        public MenuService(DBContext context)
        {
            _context = context;
        }

        public async Task<List<Menu>> ObtenerMenu(int menu)
        {
            var menus = new SqlParameter("@id_usuario", menu);
            return await Task.Run(() => _context.Menus.FromSqlRaw("EXEC PA_OBTENER_MENU @id_usuario", menus).AsNoTracking().ToList());
        }
    }
}
