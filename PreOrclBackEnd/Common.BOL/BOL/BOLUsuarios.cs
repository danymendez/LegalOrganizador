using Common.Entity.Models;
using Common.Models;
using Data.DAL;
using System.Linq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Common.BOL.BOL
{
   public class BOLUsuarios
    {
        public Task<Usuarios> Autenticar(string usuario, string clave)
        {

            Usuarios user = null;
            Task<Usuarios> t = Task.Run(() =>
            {
                using (DALDBContext context = new DALDBContext())
                {
                    DALUsuarios dal = new DALUsuarios(context);
                    var listuser = dal.GetAllUsuarios();
                    var usu = from us in listuser where us.Usuario.Equals(usuario.Trim()) && us.Password.Equals(clave.Trim()) select us;
                    user = usu.FirstOrDefault();
                    user.Password = "";
                    return user;
                }
            });

            return t;
        }
    }
}
