using Common.Entity.Models;
using Common.Data.DAL;
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
                    
                    return user;
                }
            });

            return t;
        }

        public Task<List<Usuarios>> GetUsuarios() {
            Task<List<Usuarios>> t = Task.Run(() =>
            {
                using (DALDBContext context = new DALDBContext())
                {
                    DALUsuarios dal = new DALUsuarios(context);
                    var listuser = dal.GetAllUsuarios();
                                   
                    
                    return listuser;
                }
            });

            return t;
        }

        public Task<Usuarios> GetUsuario(decimal id) {
            Task<Usuarios> t = Task.Run(() =>
            {
                using (DALDBContext context = new DALDBContext())
                {
                    DALUsuarios dal = new DALUsuarios(context);
                    var usuario = dal.GetUsuario(id);


                    return usuario;
                }
            });

            return t;
        }

        public Task<Usuarios> CreateUsuario(Usuarios pUsuario)
        {
            Task<Usuarios> t = Task.Run(() =>
            {
                using (DALDBContext context = new DALDBContext())
                {
                    DALUsuarios dal = new DALUsuarios(context);
                    var usuario = dal.CreateUsuario(pUsuario);


                    return usuario;
                }
            });

            return t;
        }

    }
}
