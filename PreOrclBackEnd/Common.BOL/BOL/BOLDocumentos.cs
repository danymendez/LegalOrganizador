using Common.Entity.Models;
using Common.Data.DAL;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Common.BOL.BOL
{
   public class BOLDocumentos
    {
        public async Task<Documentos> CreateDocumentos(Documentos _Documentos)
        {
            Documentos Documentos = new Documentos();
            Task<Documentos> t = Task.Run(() =>
            {
                using (DALDBContext context = new DALDBContext())
                {
                    DALDocumentos dal = new DALDocumentos(context);
                    Documentos = dal.CreateDocumentos(_Documentos);
                }

                return Documentos;
            });

            return await t;
        }

        public List<Documentos> GetAllDocumentos()
        {

            List<Documentos> listaDocumentos = null;

            using (DALDBContext context = new DALDBContext())
            {
                DALDocumentos dal = new DALDocumentos(context);
                listaDocumentos = dal.GetAllDocumentos();
            }

            return listaDocumentos;
        }

        public async Task<Documentos> GetDocumento(decimal id)
        {
            Documentos _documento = null;
            Task<Documentos> t = Task.Run(() => {
                using (DALDBContext context = new DALDBContext())
                {
                    DALDocumentos dal = new DALDocumentos(context);
                    _documento = dal.GetDocumento(id);
                }
                return _documento;
            });

            return await t;
        }

        public async Task<Documentos> UpdateDocumentos(decimal id, Documentos _Documentos)
        {
            Documentos Documentos = null;
            Task<Documentos> t = Task.Run(() => {
                using (DALDBContext context = new DALDBContext())
                {
                    DALDocumentos dal = new DALDocumentos(context);
                    Documentos = dal.UpdateDocumentos(id, _Documentos);
                }
                return Documentos;
            });

            return await t;
        }

        public async Task<Documentos> DeleteDocumentos(decimal id)
        {
            Documentos _Documentos = null;
            Task<Documentos> t = Task.Run(() => {
                using (DALDBContext context = new DALDBContext())
                {
                    DALDocumentos dal = new DALDocumentos(context);
                    _Documentos = dal.DeleteDocumento(id);
                }
                return _Documentos;
            });

            return await t;
        }
    }
}
