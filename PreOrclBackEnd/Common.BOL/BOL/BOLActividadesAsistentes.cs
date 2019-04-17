using Common.Data.DAL;
using Common.Entity.Models;
using Common.Entity.ViewModels;
using Microsoft.Graph;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Common.BOL.BOL
{
   public class BOLActividadesAsistentes
    {
        public async Task<ActividadesAsistentes> CreateActividadesAsistentes(ActividadesAsistentes pActividadesAsistentes)
        {
            ActividadesAsistentes _actividadesAsistentes = new ActividadesAsistentes();
            Task<ActividadesAsistentes> t = Task.Run(() =>
            {
                using (DALDBContext context = new DALDBContext())
                {
                    DALActividadesAsistentes dal = new DALActividadesAsistentes(context);
                    _actividadesAsistentes = dal.CreateActividadesAsistentes(pActividadesAsistentes);
                }

                return _actividadesAsistentes;
            });

            return await t;
        }

        public async Task<Tuple<Event,string>> CreateVwModelActividadesAsistentes(Actividades pActividades ,List<VwModelAsistentes> asistentes)
        {
           
        
            Task<Tuple<Event,string>> t = Task.Run(() =>
            {
                Tuple<Event, string> tupleEventoMsgError = null;
                using (DALDBContext context = new DALDBContext())
                {
                    DALActividades dalActividades = new DALActividades(context);
                    DALActividadesAsistentes dalAsistentes = new DALActividadesAsistentes(context);
                    BOLCalendar bolCalendar = new BOLCalendar();
                    
                    var actividadCreada = dalActividades.CreateActividades(pActividades);
                    foreach (var itemAsistentes in asistentes) {
                        dalAsistentes.CreateActividadesAsistentes(new ActividadesAsistentes
                        {
                            IdActividad=actividadCreada.IdActividad,
                            IdAsistente=itemAsistentes.IdAsistente
                        });
                    }
                    if (actividadCreada != null)
                 
                        tupleEventoMsgError =  bolCalendar.CreateEventByIdUsuario(pActividades, asistentes).Result;
                       
                    if (tupleEventoMsgError.Item1 == null)
                    {
                        context.sqlTran.Rollback();
                       
                    }
                    else {
                        actividadCreada.IdEvento = tupleEventoMsgError.Item1.ICalUId;
                        dalActividades.UpdateActividades(actividadCreada.IdActividad, actividadCreada);
                    }
                }

                return tupleEventoMsgError;
            });

           

            return await t;
        }

        public List<ActividadesAsistentes> GetAllActividadesAsistentes()
        {

            List<ActividadesAsistentes> listaActividadesAsistentes = null;

            using (DALDBContext context = new DALDBContext())
            {
                DALActividadesAsistentes dal = new DALActividadesAsistentes(context);
                listaActividadesAsistentes = dal.GetAllActividadesAsistentes();
            }

            return listaActividadesAsistentes;
        }

        public async Task<ActividadesAsistentes> GetActividadAsistente(decimal id)
        {
            ActividadesAsistentes actividadesAsistentes = null;
            Task<ActividadesAsistentes> t = Task.Run(() => {
                using (DALDBContext context = new DALDBContext())
                {
                    DALActividadesAsistentes dal = new DALActividadesAsistentes(context);
                    actividadesAsistentes = dal.GetActividadAsistente(id);
                }
                return actividadesAsistentes;
            });

            return await t;
        }

        public async Task<ActividadesAsistentes> UpdateActividadesAsistentes(decimal id, ActividadesAsistentes ActividadesAsistentes)
        {
            ActividadesAsistentes _actividadesAsistentes = null;
            Task<ActividadesAsistentes> t = Task.Run(() => {
                using (DALDBContext context = new DALDBContext())
                {
                    DALActividadesAsistentes dal = new DALActividadesAsistentes(context);
                    _actividadesAsistentes = dal.UpdateActividadesAsistentes(id, ActividadesAsistentes);
                }
                return _actividadesAsistentes;
            });

            return await t;
        }

        public async Task<ActividadesAsistentes> DeleteActividadesAsistentes(decimal id)
        {
            ActividadesAsistentes actividadesAsistentes = null;
            Task<ActividadesAsistentes> t = Task.Run(() => {
                using (DALDBContext context = new DALDBContext())
                {
                    DALActividadesAsistentes dal = new DALActividadesAsistentes(context);
                    actividadesAsistentes = dal.DeleteActividadesAsistentes(id);
                }
                return actividadesAsistentes;
            });


            return await t;
        }
    }
}
