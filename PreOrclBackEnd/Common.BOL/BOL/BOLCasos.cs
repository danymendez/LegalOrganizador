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
                    listaCasos = dal.GetAllCasos();
                    List<VwModelCasos> listaVwModelCasos = new List<VwModelCasos>();
                    DALActividadesAsistentes dalActividadesAsistentes = new DALActividadesAsistentes(context);
                    DALActividades dalActividades = new DALActividades(context);
                    DALSisPerPersona dalSisPerPersonas = new DALSisPerPersona(context);
                    DALUsuarios dalUsuarios = new DALUsuarios(context);
                    foreach (var itemCasos in listaCasos) {

                    List<VwModelActividadesAsistentes> listaVwModelActividadesAsistentes = new List<VwModelActividadesAsistentes>();
                    var listaActividades = dalActividades.GetAllActividades().Where(c=>c.IdCaso==itemCasos.IdCaso).ToList();
                    var listaActividadesAsistentes = dalActividadesAsistentes.GetAllActividadesAsistentes();
                    var listaUsuarios = dalUsuarios.GetAllUsuarios();
                    var cliente = dalSisPerPersonas.GetPersona(itemCasos.IdCliente);

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
                        Abogado = listaUsuarios.Where(c=>c.IdUsuario==itemCasos.IdAbogado).FirstOrDefault(),
                        ListVwModelActividadesAsistentes = listaVwModelActividadesAsistentes
                    });
                        
                }
                    return listaVwModelCasos;
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
