using Oracle.ManagedDataAccess.Client;
using Common.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Data.DAL
{
   public sealed class DALSisPerPersona : DALBaseOrcl
    {
        private DALDBContext context;
        public DALSisPerPersona(DALDBContext context)
            :base(context)
        {
            this.context = context;

        }


        public SisPerPersona CreateSisPerPersona(SisPerPersona sisPerPersona) {

            return Create(sisPerPersona);

        }

        public List<SisPerPersona> GetAllSisPerPersona() {
            return GetAll<SisPerPersona>();
        }

    }
}