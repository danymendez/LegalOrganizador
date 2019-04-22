using Common.Data.DAL;
using Common.Entity.Models;
using Common.Entity.ViewModels;
using Microsoft.Graph;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public async Task<Tuple<Event, string>> EditVwModelActividadesAsistentes(Actividades pActividades, List<VwModelAsistentes> asistentes)
        {


            Task<Tuple<Event, string>> t = Task.Run(() =>
            {
                Tuple<Event, string> tupleEventoMsgError = null;
                using (DALDBContext context = new DALDBContext())
                {
                    DALActividades dalActividades = new DALActividades(context);
                    DALActividadesAsistentes dalAsistentes = new DALActividadesAsistentes(context);
                    BOLCalendar bolCalendar = new BOLCalendar();

                    var actividadEditada = dalActividades.UpdateActividades(pActividades.IdActividad,pActividades);
                    foreach (var itemAsistentes in asistentes)
                    {
                        decimal idActividadesAsistentes = GetAllActividadesAsistentes()
                                                            .Where(c => c.IdActividad == actividadEditada.IdActividad && c.IdAsistente == itemAsistentes.IdAsistente)
                                                            .FirstOrDefault().IdActividadAsistentes;
                        dalAsistentes.UpdateActividadesAsistentes(idActividadesAsistentes,new ActividadesAsistentes
                        { IdActividadAsistentes = idActividadesAsistentes,
                            IdActividad = actividadEditada.IdActividad,
                            IdAsistente = itemAsistentes.IdAsistente
                        });
                    }
                    if (actividadEditada != null)

                        tupleEventoMsgError = bolCalendar.CreateEventByIdUsuario(pActividades, asistentes).Result;

                    if (tupleEventoMsgError.Item1 == null)
                    {
                        context.sqlTran.Rollback();

                    }
                    else
                    {
                        actividadEditada.IdEvento = tupleEventoMsgError.Item1.ICalUId;
                        dalActividades.UpdateActividades(actividadEditada.IdActividad, actividadEditada);
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

        public async Task<List<VwModelActividadesAsistentes>> GetAllVwModelActividadesAsistentes()
        {

            Task<List<VwModelActividadesAsistentes>> t = Task.Run(() =>
            {
                List<VwModelActividadesAsistentes> listaVwModelActividadesAsistentes = new List<VwModelActividadesAsistentes>(); ;

                using (DALDBContext context = new DALDBContext())
                {
                    DALActividadesAsistentes dalActividadesAsistentes = new DALActividadesAsistentes(context);
                    DALActividades dalActividades = new DALActividades(context);
                    DALUsuarios dalUsuarios = new DALUsuarios(context);
                    var listaActividades = dalActividades.GetAllActividades();
                    var listaActividadesAsistentes = dalActividadesAsistentes.GetAllActividadesAsistentes();
                    var listaUsuarios = dalUsuarios.GetAllUsuarios();

                    foreach (var itemActividades in listaActividades)
                    {
                                                
                        listaVwModelActividadesAsistentes.Add(new VwModelActividadesAsistentes
                        {
                            Actividades = itemActividades,
                            ListVwModelAsistentes = (from actividadesAsistentes in listaActividadesAsistentes
                                                     where actividadesAsistentes.IdActividad == itemActividades.IdActividad
                                                     select new VwModelAsistentes
                                                     {
                                                         IdActividadesAsistentes = actividadesAsistentes.IdActividadAsistentes,
                                                         IdAsistente = actividadesAsistentes.IdAsistente,
                                                         Correo = listaUsuarios
                                                                        .Where(c => c.IdUsuario == actividadesAsistentes.IdAsistente)
                                                                        .Select(c => c.Usuario).FirstOrDefault() ?? "",
                                                         CreatedAt = actividadesAsistentes.CreatedAt

                                                     }).ToList() ?? new List<VwModelAsistentes>(),
                            IdAsistentes = listaActividadesAsistentes.Where(c=>c.IdActividad==itemActividades.IdActividad).Select(c =>c.IdAsistente).ToArray() 

                        });
                    }
                }

                return listaVwModelActividadesAsistentes;
            });

            return await t;
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
