using Common.Data.DAL;
using Common.Entity.Models;
using Common.Entity.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.BOL.BOL
{
   public class BOLCasos
    {
        public async Task<Casos> CreateCasos(Casos pCasos)
        {
            Casos _casos = new Casos();
            Task<Casos> t = Task.Run(() =>
            {
                using (DALDBContext context = new DALDBContext())
                {
                    DALCasos dal = new DALCasos(context);
                    _casos = dal.CreateCasos(pCasos);
                }

                return _casos;
            });

            return await t;
        }

        public async Task<Casos> CreateVwModelCasos(VwModelCasos vwModelCasos)
        {
            
            Casos _casos = new Casos();
            Task<Casos> t = Task.Run(() =>
            {
                using (DALDBContext context = new DALDBContext())
                {
                    DALCasos dal = new DALCasos(context);
                    DALCasosClientes dalCasosClientes = new DALCasosClientes(context);
                    DALDocumentos dalDocumentos = new DALDocumentos(context);
                    _casos = dal.CreateCasos(vwModelCasos.Casos);
                    foreach (var itemsImputados in vwModelCasos.IdImputados) {
                        var casosclientes = dalCasosClientes.CreateCasosClientes(new CasosClientes { IdCaso = _casos.IdCaso, IdCliente = itemsImputados, CreatedAt = DateTime.Now });
                    }

                    if (!(vwModelCasos.ListadoDocumentos is null))
                    {
                        foreach (var itemsDocumentos in vwModelCasos.ListadoDocumentos)
                        {
                            var documentos = dalDocumentos.CreateDocumentos(itemsDocumentos);
                        }
                    }
                }

                return _casos;
            });

            return await t;
        }

        public List<Casos> GetAllCasos()
        {

            List<Casos> listaCasos = null;

            using (DALDBContext context = new DALDBContext())
            {
                DALCasos dal = new DALCasos(context);
                listaCasos = dal.GetAllCasos();
            }

            return listaCasos;
        }

        public async Task<List<VwModelCasos>> GetAllVwModelCasos()
        {

            List<Casos> listaCasos = null;
            Task<List<VwModelCasos>> t = Task.Run(() => {
                using (DALDBContext context = new DALDBContext())
            {
                    DALCasos dal = new DALCasos(context);
                    DALCasosClientes dalCasoClientes = new DALCasosClientes(context);
                    listaCasos = dal.GetAllCasos();
                    var listadoCasosClientes = dalCasoClientes.GetAllCasosClientes();
                    List<VwModelCasos> listaVwModelCasos = new List<VwModelCasos>();
                    DALActividadesAsistentes dalActividadesAsistentes = new DALActividadesAsistentes(context);
                    DALActividades dalActividades = new DALActividades(context);
                    DALDocumentos dalDocumentos = new DALDocumentos(context);
                    var listadoDocumentos = dalDocumentos.GetAllDocumentos();
                    DALSisPerPersona dalSisPerPersonas = new DALSisPerPersona(context);
                    DALUsuarios dalUsuarios = new DALUsuarios(context);
                    foreach (var itemCasos in listaCasos) {

                    List<VwModelActividadesAsistentes> listaVwModelActividadesAsistentes = new List<VwModelActividadesAsistentes>();
                    var listaActividades = dalActividades.GetAllActividades().Where(c=>c.IdCaso==itemCasos.IdCaso).ToList();
                    var listaActividadesAsistentes = dalActividadesAsistentes.GetAllActividadesAsistentes();
                    var listaUsuarios = dalUsuarios.GetAllUsuarios();
                    var cliente = dalSisPerPersonas.GetPersona(itemCasos.IdCliente);
                    var imputados = (from imputado in dalSisPerPersonas.GetAllSisPerPersona() join casosClientes in listadoCasosClientes on imputado.per_IDPER equals casosClientes.IdCliente
                                        where casosClientes.IdCaso == itemCasos.IdCaso select imputado).ToList();
                    foreach (var itemActividades in listaActividades)
                    {
                        
                        listaVwModelActividadesAsistentes.Add(new VwModelActividadesAsistentes
                        {
                            Actividades = itemActividades,
                            Responsable = listaUsuarios.Where(c => c.IdUsuario == itemActividades.IdResponsable).FirstOrDefault(),
                            ListVwModelAsistentes = (from actividadesAsistentes in listaActividadesAsistentes
                                                     where actividadesAsistentes.IdActividad == itemActividades.IdActividad
                                                     select new VwModelAsistentes
                                                     {
                                                         IdActividadesAsistentes = actividadesAsistentes.IdActividadAsistentes,
                                                         IdAsistente = actividadesAsistentes.IdAsistente,
                                                         Asistente = listaUsuarios.Where(c => c.IdUsuario == actividadesAsistentes.IdAsistente).FirstOrDefault(),
                                                         Correo = listaUsuarios
                                                                        .Where(c => c.IdUsuario == actividadesAsistentes.IdAsistente)
                                                                        .Select(c => c.Usuario).FirstOrDefault() ?? "",
                                                         CreatedAt = actividadesAsistentes.CreatedAt

                                                     }).ToList() ?? new List<VwModelAsistentes>(),
                            IdAsistentes = listaActividadesAsistentes.Where(c => c.IdActividad == itemActividades.IdActividad).Select(c => c.IdAsistente).ToArray()
                            
                        });
                    }

                        listaVwModelCasos.Add(new VwModelCasos {
                            Casos = itemCasos,
                            Cliente = cliente,
                            Abogado = listaUsuarios.Where(c => c.IdUsuario == itemCasos.IdAbogado).FirstOrDefault(),
                            ListVwModelActividadesAsistentes = listaVwModelActividadesAsistentes,
                            ListadoImputados = imputados,
                            ListadoDocumentos = listadoDocumentos.Where(c => c.IdCaso == itemCasos.IdCaso).ToList(),
                            IdDocumentos = listadoDocumentos.Where(c=>c.IdCaso==itemCasos.IdCaso).Select(c=>c.IdDocumento).ToArray(),
                            IdImputados = imputados.Select(c=>c.per_IDPER).ToArray()
                    });
                        
                }
                    return listaVwModelCasos;
                }

                 });

            return await t;

        }

        public async Task<VwModelCasos> GetVwModelCasos(decimal id)
        {

            Casos caso = null;
            Task<VwModelCasos> t = Task.Run(() => {
                using (DALDBContext context = new DALDBContext())
                {
                    DALCasos dal = new DALCasos(context);
                    DALCasosClientes dalCasoClientes = new DALCasosClientes(context);
                    caso = dal.GetCaso(id);
                    var listadoCasosClientes = dalCasoClientes.GetAllCasosClientes();
                    VwModelCasos vwModelCasos = new VwModelCasos();
                    DALActividadesAsistentes dalActividadesAsistentes = new DALActividadesAsistentes(context);
                    DALActividades dalActividades = new DALActividades(context);
                    DALDocumentos dalDocumentos = new DALDocumentos(context);
                    var listadoDocumentos = dalDocumentos.GetAllDocumentos();
                    DALSisPerPersona dalSisPerPersonas = new DALSisPerPersona(context);
                    DALUsuarios dalUsuarios = new DALUsuarios(context);
                   

                        List<VwModelActividadesAsistentes> listaVwModelActividadesAsistentes = new List<VwModelActividadesAsistentes>();
                        var listaActividades = dalActividades.GetAllActividades().Where(c => c.IdCaso == caso.IdCaso).ToList();
                        var listaActividadesAsistentes = dalActividadesAsistentes.GetAllActividadesAsistentes();
                        var listaUsuarios = dalUsuarios.GetAllUsuarios();
                        var cliente = dalSisPerPersonas.GetPersona(caso.IdCliente);
                        var imputados = (from imputado in dalSisPerPersonas.GetAllSisPerPersona()
                                         join casosClientes in listadoCasosClientes on imputado.per_IDPER equals casosClientes.IdCliente
                                         where casosClientes.IdCaso == caso.IdCaso
                                         select imputado).ToList();
                        foreach (var itemActividades in listaActividades)
                        {

                            listaVwModelActividadesAsistentes.Add(new VwModelActividadesAsistentes
                            {
                                Actividades = itemActividades,
                                Responsable = listaUsuarios.Where(c => c.IdUsuario == itemActividades.IdResponsable).FirstOrDefault(),
                                ListVwModelAsistentes = (from actividadesAsistentes in listaActividadesAsistentes
                                                         where actividadesAsistentes.IdActividad == itemActividades.IdActividad
                                                         select new VwModelAsistentes
                                                         {
                                                             IdActividadesAsistentes = actividadesAsistentes.IdActividadAsistentes,
                                                             IdAsistente = actividadesAsistentes.IdAsistente,
                                                             Asistente = listaUsuarios.Where(c => c.IdUsuario == actividadesAsistentes.IdAsistente).FirstOrDefault(),
                                                             Correo = listaUsuarios
                                                                            .Where(c => c.IdUsuario == actividadesAsistentes.IdAsistente)
                                                                            .Select(c => c.Usuario).FirstOrDefault() ?? "",
                                                             CreatedAt = actividadesAsistentes.CreatedAt

                                                         }).ToList() ?? new List<VwModelAsistentes>(),
                                IdAsistentes = listaActividadesAsistentes.Where(c => c.IdActividad == itemActividades.IdActividad).Select(c => c.IdAsistente).ToArray()

                            });
                        }

                        vwModelCasos = new VwModelCasos
                        {
                            Casos = caso,
                            Cliente = cliente,
                            Abogado = listaUsuarios.Where(c => c.IdUsuario == caso.IdAbogado).FirstOrDefault(),
                            ListVwModelActividadesAsistentes = listaVwModelActividadesAsistentes,
                            ListadoImputados = imputados,
                            ListadoDocumentos = listadoDocumentos.Where(c => c.IdCaso == caso.IdCaso).ToList(),
                            IdDocumentos = listadoDocumentos.Where(c => c.IdCaso == caso.IdCaso).Select(c => c.IdDocumento).ToArray(),
                            IdImputados = imputados.Select(c => c.per_IDPER).ToArray()
                        };

      
                    return vwModelCasos;
                }

            });

            return await t;

        }

        public async Task<Casos> GetCaso(decimal id)
        {
            Casos casos = null;
            Task<Casos> t = Task.Run(() => {
                using (DALDBContext context = new DALDBContext())
                {
                    DALCasos dal = new DALCasos(context);
                    casos = dal.GetCaso(id);
                }
                return casos;
            });

            return await t;
        }

        public async Task<Casos> UpdateCasos(decimal id, Casos pCasos)
        {
            Casos _casos = null;
            Task<Casos> t = Task.Run(() => {
                using (DALDBContext context = new DALDBContext())
                {
                    DALCasos dal = new DALCasos(context);
                    _casos = dal.UpdateCasos(id, pCasos);
                }
                return _casos;
            });

            return await t;
        }

        public async Task<Casos> DeleteCasos(decimal id)
        {
            Casos Casos = null;
            Task<Casos> t = Task.Run(() => {
                using (DALDBContext context = new DALDBContext())
                {
                    DALCasos dal = new DALCasos(context);
                    Casos = dal.DeleteCasos(id);
                }
                return Casos;
            });


            return await t;
        }
    }
}
