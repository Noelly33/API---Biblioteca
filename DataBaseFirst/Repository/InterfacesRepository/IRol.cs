using DataBaseFirst.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBaseFirst.Repository.InterfacesRepository
{
    public interface IRol
    {
        Task<List<Rol>> ListaRoles();

    }
}
