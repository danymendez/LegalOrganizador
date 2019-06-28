using Common.Entity.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Data.DAL
{
   public class DALDocumentos : DALBaseOrcl
    {
        private DALDBContext context;
        public DALDocumentos(DALDBContext context)
            : base(context)
        {
            this.context = context;

        }


        public Documentos CreateDocumentos(Documentos documentos)
        {

            return Create(documentos);

        }

        public List<Documentos> GetAllDocumentos()
        {
            return GetAll<Documentos>();
        }

        public Documentos DeleteDocumento(decimal id)
        {
            return Delete<Documentos>(id);
        }

        public Documentos UpdateDocumentos(decimal id, Documentos documentos)
        {

            return Update(id, documentos);
        }

        public Documentos GetDocumento(decimal id)
        {
            return Get<Documentos>(id);
        }
    }
}
