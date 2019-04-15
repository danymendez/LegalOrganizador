using Common.Entity.Models;
using Common.Utilities;
using Microsoft.Graph;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.BOL.BOL
{
    public class BOLCalendar
    {
        public async Task<List<GraphCalendar>> GetCalendarByUsuario() {
            BOLMSGraph graph = new BOLMSGraph();
            List<GraphCalendar> lista = new List<GraphCalendar>();
            BOLUsuarios bolUsuarios = new BOLUsuarios();
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
                                Id = itemCalendario.Id,
                                CanShare = itemCalendario.CanShare,
                                Name = itemCalendario.Name,
                                Color = itemCalendario.Color.ToString(),
                                ChangeKey = itemCalendario.ChangeKey,
                                CanViewPrivateItems = itemCalendario.CanViewPrivateItems,
                                CanEdit = itemCalendario.CanEdit,
                                Owner = new GraphOwner { Address = itemCalendario.Owner.Address, Name = itemCalendario.Owner.Name }
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
    }
}
