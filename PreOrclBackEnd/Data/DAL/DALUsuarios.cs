using Common.Entity.Models;
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

        public List<Usuarios> GetAllUsuarios() {

            List<Usuarios> listaUsuario = GetAll<Usuarios>();


            return listaUsuario;

        }

        public Usuarios GetUsuario(int id)
        {

            Usuarios listaUsuario = Get<Usuarios>(id);

            return listaUsuario;

        }

        public Usuarios CreateUsuario(Usuarios pUsuario) {

            Usuarios usuario = Create(pUsuario);
            return usuario;
        }
    }
}
