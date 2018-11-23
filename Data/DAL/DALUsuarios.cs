using Common.Models;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace Data.DAL
{
   public class DALUsuarios:DALBaseOrcl
    {
        private DALDBContext context;
        public DALUsuarios(DALDBContext context)
            : base(context)
        {
            this.context = context;

        }

        public Usuarios Autenticar(string usuario, string clave) {

            List<Usuarios> listaUsuario = GetAll<Usuarios>();

            var user = from usu in listaUsuario
                       where usu.Usuario.Equals(usuario.Trim()) && usu.Password.Equals(clave.Trim())
                       select usu;

            return user.FirstOrDefault();

        }
    }
}
