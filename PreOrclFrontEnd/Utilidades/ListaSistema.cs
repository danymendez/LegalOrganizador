using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Options;
using PreOrclFrontEnd.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PreOrclFrontEnd.Models;

namespace PreOrclFrontEnd.Utilidades
{
    public class ListaSistema
    {
        readonly GenericREST generic;
        public ListaSistema(IOptions<UriHelpers> configuration)
        {

            generic = new GenericREST(configuration.Value);

        }

        public SelectList GetSelectListClientes() {
            return new SelectList((from personas in generic.GetAll<SisPerPersona>("SisPerPersonas").Result
                                   select new SisPerPersona
                            {
                               per_IDPER= personas.per_IDPER,
                                per_nombre_razon = $"Dui: {personas.per_dui_nrc} - {personas.per_nombre_razon} {personas.per_apellido_comercial}"
                            }).ToList(), "per_IDPER", "per_nombre_razon");
        }

        public SelectList GetSelectListAbogados()
        {
          return  new SelectList((from abogados in generic.GetAll<Usuarios>("Usuarios").Result where abogados.TipoUsuario.Trim().ToUpperInvariant()=="I"
                            select new Usuarios { IdUsuario=abogados.IdUsuario, Nombre = $"{abogados.Nombre} {abogados.Apellido}" }), "IdUsuario", "Nombre");
        }

        public SelectList GetSelectListCasos()
        {
            return new SelectList((from casos in generic.GetAll<Casos>("Casos").Result
                                   select new Casos { IdCaso = casos.IdCaso,NombreCaso = $"Caso: {casos.IdCaso} Nombre:{casos.NombreCaso}" }), "IdCaso", "NombreCaso");
        }
    }
}
