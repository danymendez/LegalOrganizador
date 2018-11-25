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

        public List<Usuarios> GetAllUsuarios() {

            List<Usuarios> listaUsuario = GetAll<Usuarios>();


            return listaUsuario;

        }
    }
}
