using Oracle.ManagedDataAccess.Client;
using Common.Entity.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Common.Data.DAL
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

        public SisPerPersona DeletePersona(decimal id) {


            return Delete<SisPerPersona>(id);
        }

        public SisPerPersona UpdateSisPerPersona(decimal id, SisPerPersona sisPerPersona) {

            return Update(id, sisPerPersona);
        }

        public SisPerPersona GetPersona(decimal id) {
            return Get<SisPerPersona>(id);
        }

    }
}