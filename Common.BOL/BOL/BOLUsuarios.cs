using Common.Entity.Models;
using Common.Models;
using Data.DAL;
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
                    user = dal.Autenticar(usuario, clave);
                    return user;
                }
            });

            return t;
        }
    }
}
