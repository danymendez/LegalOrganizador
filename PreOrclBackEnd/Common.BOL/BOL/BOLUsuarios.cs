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
        public async Task<Usuarios> Autenticar(string usuario, string clave)
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

            return await t;
        }

        public async Task<List<Usuarios>> GetUsuarios() {
            Task<List<Usuarios>> t = Task.Run(() =>
            {
                using (DALDBContext context = new DALDBContext())
                {
                    DALUsuarios dal = new DALUsuarios(context);
                    var listuser = dal.GetAllUsuarios();
                                   
                    
                    return listuser;
                }
            });

            return await t;
        }

        public async Task<Usuarios> GetUsuario(decimal id) {
            Task<Usuarios> t = Task.Run(() =>
            {
                using (DALDBContext context = new DALDBContext())
                {
                    DALUsuarios dal = new DALUsuarios(context);
                    var usuario = dal.GetUsuario(id);


                    return usuario;
                }
            });

            return await t;
        }

        public async Task<Usuarios> CreateUsuario(Usuarios pUsuario)
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

            return await t;
        }



        public async Task<Usuarios> UpdateUsuarios(decimal id, Usuarios pUsuario) {
            var entity = await GetUsuario(pUsuario.IdUsuario);

            Task<Usuarios> t = Task.Run(() =>
            {
                using (DALDBContext context = new DALDBContext())
                {
                    pUsuario.Password = entity.Password;
                    DALUsuarios dal = new DALUsuarios(context);
                    var usuario = dal.UpdateUsuarios(id,pUsuario);


                    return usuario;
                }
            });

            return await t;
        }
    }
}
