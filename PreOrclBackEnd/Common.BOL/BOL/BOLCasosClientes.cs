using Common.Entity.Models;
using Common.Data.DAL;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Common.BOL.BOL
{
   public class BOLCasosClientes
    {
        public async Task<CasosClientes> CreateCasosClientes(CasosClientes _casosClientes)
        {
            CasosClientes casosClientes = new CasosClientes();
            Task<CasosClientes> t = Task.Run(() =>
            {
                using (DALDBContext context = new DALDBContext())
                {
                    DALCasosClientes dal = new DALCasosClientes(context);
                    casosClientes = dal.CreateCasosClientes(_casosClientes);
                }

                return casosClientes;
            });

            return await t;
        }

        public List<CasosClientes> GetAllCasosClientes()
        {

            List<CasosClientes> listaCasosClientes = null;

            using (DALDBContext context = new DALDBContext())
            {
                DALCasosClientes dal = new DALCasosClientes(context);
                listaCasosClientes = dal.GetAllCasosClientes();
            }

            return listaCasosClientes;
        }

        public async Task<CasosClientes> GetCasoCliente(decimal id)
        {
            CasosClientes _casoCliente = null;
            Task<CasosClientes> t = Task.Run(() => {
                using (DALDBContext context = new DALDBContext())
                {
                    DALCasosClientes dal = new DALCasosClientes(context);
                    _casoCliente = dal.GetCasoCliente(id);
                }
                return _casoCliente;
            });

            return await t;
        }

        public async Task<CasosClientes> UpdateCasosClientes(decimal id, CasosClientes _casosClientes)
        {
            CasosClientes casosClientes = null;
            Task<CasosClientes> t = Task.Run(() => {
                using (DALDBContext context = new DALDBContext())
                {
                    DALCasosClientes dal = new DALCasosClientes(context);
                    casosClientes = dal.UpdateCasosClientes(id, _casosClientes);
                }
                return casosClientes;
            });

            return await t;
        }

        public async Task<CasosClientes> DeleteCasosClientes(decimal id)
        {
            CasosClientes _casosClientes = null;
            Task<CasosClientes> t = Task.Run(() => {
                using (DALDBContext context = new DALDBContext())
                {
                    DALCasosClientes dal = new DALCasosClientes(context);
                    _casosClientes = dal.DeleteCasoCliente(id);
                }
                return _casosClientes;
            });

            return await t;
        }
    }
}
