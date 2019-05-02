using Common.Entity.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Data.DAL
{
   public class DALCasosClientes : DALBaseOrcl
    {
        private DALDBContext context;
        public DALCasosClientes(DALDBContext context)
            : base(context)
        {
            this.context = context;

        }


        public CasosClientes CreateCasosClientes(CasosClientes casosClientes)
        {

            return Create(casosClientes);

        }

        public List<CasosClientes> GetAllCasosClientes()
        {
            return GetAll<CasosClientes>();
        }

        public CasosClientes DeleteDocumento(decimal id)
        {
            return Delete<CasosClientes>(id);
        }

        public CasosClientes UpdateCasosClientes(decimal id, CasosClientes casosClientes)
        {

            return Update(id, casosClientes);
        }

        public CasosClientes GetCasoCliente(decimal id)
        {
            return Get<CasosClientes>(id);
        }
    }
}
