using Common.Entity.Models;
using Common.Utilities;
using Microsoft.Graph;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Entity.ViewModels;

namespace Common.BOL.BOL
{
    public class BOLCalendar
    {
        private BOLMSGraph graph;
        public BOLUsuarios bolUsuarios;
        public BOLCalendar() {
            graph = new BOLMSGraph();
            bolUsuarios = new BOLUsuarios();
        }
        public async Task<List<GraphCalendar>> GetCalendarByIdUsuario() {
            graph = new BOLMSGraph();
            List<GraphCalendar> lista = new List<GraphCalendar>();
            bolUsuarios = new BOLUsuarios();
            var listaUsuarios = await bolUsuarios.GetUsuarios();
            listaUsuarios = listaUsuarios.Where(c => !(c.Token is null) && !(c.TokenRefresh is null)).ToList();
            foreach (var itemUsuario in listaUsuarios) {

                GraphServiceClient authenticatedUser = null;
                List<Calendar> listaCalendario = null;
                try
                {

                    authenticatedUser = graph.GetAuthenticatedClient(Criptografia.Decrypt(itemUsuario.Token));
                    listaCalendario = await graph.GetCalendar(authenticatedUser);
                }
                catch (ServiceException ex)
                {
                    if (ex.Error.Code == "InvalidAuthenticationToken") {
                        try
                        {
                            var tokenRefreshed = graph.GetToken(Criptografia.Decrypt(itemUsuario.TokenRefresh));
                            itemUsuario.Token = Criptografia.Encrypt(tokenRefreshed);
                            await bolUsuarios.UpdateUsuarios(itemUsuario.IdUsuario, itemUsuario);
                            authenticatedUser = graph.GetAuthenticatedClient(tokenRefreshed);
                            listaCalendario = await graph.GetCalendar(authenticatedUser);
                        }
                        catch (Exception exc) {
                            ExceptionUtility.LogException(exc);
                        }
                    }
                }
                catch (Exception ex) {
                    ExceptionUtility.LogException(ex);
                }

                try
                {
                    if (!(listaCalendario is null))
                    {
                        foreach (var itemCalendario in listaCalendario)
                        {
                            lista.Add(new GraphCalendar
                            {
                                IdUsuario = itemUsuario.IdUsuario,
                                Calendar = itemCalendario,


                            });
                        }
                    }
                }
                catch (Exception ex) {
                    ExceptionUtility.LogException(ex);
                }


            }

            return lista;
        }

        public async Task<List<GraphCalendar>> GetCalendarByIdUsuario(decimal id)
        {
            graph = new BOLMSGraph();
            List<GraphCalendar> lista = new List<GraphCalendar>();
            bolUsuarios = new BOLUsuarios();
            var usuarios = await bolUsuarios.GetUsuario(id);
           
          

                GraphServiceClient authenticatedUser = null;
                List<Calendar> listaCalendario = null;
                try
                {

                    authenticatedUser = graph.GetAuthenticatedClient(Criptografia.Decrypt(usuarios.Token));
                    listaCalendario = await graph.GetCalendar(authenticatedUser);
                }
                catch (ServiceException ex)
                {
                    if (ex.Error.Code == "InvalidAuthenticationToken")
                    {
                        try
                        {
                            var tokenRefreshed = graph.GetToken(Criptografia.Decrypt(usuarios.TokenRefresh));
                            usuarios.Token = Criptografia.Encrypt(tokenRefreshed);
                            await bolUsuarios.UpdateUsuarios(usuarios.IdUsuario, usuarios);
                            authenticatedUser = graph.GetAuthenticatedClient(tokenRefreshed);
                            listaCalendario = await graph.GetCalendar(authenticatedUser);
                        }
                        catch (Exception exc)
                        {
                            ExceptionUtility.LogException(exc);
                        }
                    }
                }
                catch (Exception ex)
                {
                    ExceptionUtility.LogException(ex);
                }

                try
                {
                    if (!(listaCalendario is null))
                    {
                        foreach (var itemCalendario in listaCalendario)
                        {
                            lista.Add(new GraphCalendar
                            {
                                IdUsuario = usuarios.IdUsuario,
                                Calendar = itemCalendario,


                            });
                        }
                    }
                }
                catch (Exception ex)
                {
                    ExceptionUtility.LogException(ex);
                }



            return lista;
        }
        public async Task<List<GraphEvents>> GetEventosByIdUsuario()
        {
            graph = new BOLMSGraph();
            List<GraphEvents> lista = new List<GraphEvents>();
            bolUsuarios = new BOLUsuarios();
            var listaUsuarios = await bolUsuarios.GetUsuarios();
            listaUsuarios = listaUsuarios.Where(c => !(c.Token is null) && !(c.TokenRefresh is null)).ToList();
            foreach (var itemUsuario in listaUsuarios)
            {

                GraphServiceClient authenticatedUser = null;
                List<GraphEvents> listaEventos = null;
                try
                {

                    authenticatedUser = graph.GetAuthenticatedClient(Criptografia.Decrypt(itemUsuario.Token));
                    listaEventos = await graph.GetAllEvents(authenticatedUser);
                }
                catch (ServiceException ex)
                {
                    if (ex.Error.Code == "InvalidAuthenticationToken")
                    {
                        try
                        {
                            var tokenRefreshed = graph.GetToken(Criptografia.Decrypt(itemUsuario.TokenRefresh));
                            itemUsuario.Token = Criptografia.Encrypt(tokenRefreshed);
                            await bolUsuarios.UpdateUsuarios(itemUsuario.IdUsuario, itemUsuario);
                            authenticatedUser = graph.GetAuthenticatedClient(tokenRefreshed);
                            listaEventos = await graph.GetAllEvents(authenticatedUser);
                        }
                        catch (Exception exc)
                        {
                            ExceptionUtility.LogException(exc);
                        }
                    }
                }
                catch (Exception ex)
                {
                    ExceptionUtility.LogException(ex);
                }

                try
                {
                    if (!(listaEventos is null))
                    {
                        foreach (var itemEvento in listaEventos)
                        {
                            GraphEvents eventos = new GraphEvents {
                                IdUsuario = itemUsuario.IdUsuario,
                                Event = itemEvento.Event,
                               IdCalendar=itemEvento.IdCalendar

                            };

                            lista.Add(eventos);
                        }
                    }
                }
                catch (Exception ex)
                {
                    ExceptionUtility.LogException(ex);
                }


            }

            return lista;
        }

        public async Task<List<GraphEvents>> GetEventosByIdUsuario(decimal id)
        {
            graph = new BOLMSGraph();
            List<GraphEvents> lista = new List<GraphEvents>();
            bolUsuarios = new BOLUsuarios();
            var usuarios = await bolUsuarios.GetUsuario(id);
           

                GraphServiceClient authenticatedUser = null;
                List<GraphEvents> listaEventos = null;
                try
                {

                    authenticatedUser = graph.GetAuthenticatedClient(Criptografia.Decrypt(usuarios.Token));
                    listaEventos = await graph.GetAllEvents(authenticatedUser);
                }
                catch (ServiceException ex)
                {
                    if (ex.Error.Code == "InvalidAuthenticationToken")
                    {
                        try
                        {
                            var tokenRefreshed = graph.GetToken(Criptografia.Decrypt(usuarios.TokenRefresh));
                            usuarios.Token = Criptografia.Encrypt(tokenRefreshed);
                            await bolUsuarios.UpdateUsuarios(usuarios.IdUsuario, usuarios);
                            authenticatedUser = graph.GetAuthenticatedClient(tokenRefreshed);
                            listaEventos = await graph.GetAllEvents(authenticatedUser);
                        }
                        catch (Exception exc)
                        {
                            ExceptionUtility.LogException(exc);
                        }
                    }
                }
                catch (Exception ex)
                {
                    ExceptionUtility.LogException(ex);
                }

                try
                {
                    if (!(listaEventos is null))
                    {
                        foreach (var itemEvento in listaEventos)
                        {
                            GraphEvents eventos = new GraphEvents
                            {
                                IdUsuario = usuarios.IdUsuario,
                                Event = itemEvento.Event,
                                IdCalendar = itemEvento.IdCalendar

                            };

                            lista.Add(eventos);
                        }
                    }
                }
                catch (Exception ex)
                {
                    ExceptionUtility.LogException(ex);
                }


            return lista;
        }
        public async Task<Tuple<Event,string>> CreateEventByIdUsuario(Actividades actividades , List<VwModelAsistentes> listVwModelAsistente) {
            graph = new BOLMSGraph();
            bolUsuarios = new BOLUsuarios();
            var usuarios = await bolUsuarios.GetUsuario(actividades.IdResponsable);
            Tuple<Event,string> tupleEventMsgError = null;

                GraphServiceClient authenticatedUser = null;
              
                try
                {

                    authenticatedUser = graph.GetAuthenticatedClient(Criptografia.Decrypt(usuarios.Token));
                tupleEventMsgError = await graph.CreateEvent(authenticatedUser, actividades, listVwModelAsistente);
                }
                catch (ServiceException ex)
                {
                    if (ex.Error.Code == "InvalidAuthenticationToken")
                    {
                        try
                        {
                            var tokenRefreshed = graph.GetToken(Criptografia.Decrypt(usuarios.TokenRefresh));
                            usuarios.Token = Criptografia.Encrypt(tokenRefreshed);
                            await bolUsuarios.UpdateUsuarios(usuarios.IdUsuario, usuarios);
                            authenticatedUser = graph.GetAuthenticatedClient(tokenRefreshed);
                            tupleEventMsgError = await graph.CreateEvent(authenticatedUser, actividades, listVwModelAsistente);
                    }
                        catch (Exception exc)
                        {
                            ExceptionUtility.LogException(exc);
                        }
                    }
                }
                catch (Exception ex)
                {
                    ExceptionUtility.LogException(ex);
                }

                

            

            return tupleEventMsgError;
        }

        public async Task<List<GraphCalendarEvents>> GetCalendarEventsByUsuario() {
            List<GraphCalendarEvents> listaGraphCalendarEvents = new List<GraphCalendarEvents>();
            var listadoCalendario = await GetCalendarByIdUsuario();
            var listadoEventos = await GetEventosByIdUsuario();

            foreach(var itemListadoCalendario in listadoCalendario) {
                listaGraphCalendarEvents.Add(new GraphCalendarEvents {
                    GraphCalendar = itemListadoCalendario,
                    GraphEvents = listadoEventos.Where(c => c.IdCalendar==itemListadoCalendario.Calendar.Id).ToList()
                });

            }

            return listaGraphCalendarEvents;
          
        }
    }
}
