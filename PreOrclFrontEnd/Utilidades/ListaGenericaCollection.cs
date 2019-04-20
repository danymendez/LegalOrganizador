using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PreOrclFrontEnd.Utilidades
{
    public static class ListaGenericaCollection
    {
        public static SelectList GetSelectListCategorias() {
            return new SelectList( new List<SelectListItem>()
            {
                new SelectListItem{ Text = "Categoria 1", Value="A" },
                new SelectListItem{ Text = "Categoria 2", Value="B" },

            });
        }

        public static SelectList GetSelectListTipo()
        {
            return new SelectList( new List<SelectListItem>()
            {
                new SelectListItem{ Text = "Privado", Value="P" },
                new SelectListItem{ Text = "Público", Value="U" },

            });
        }


        public static SelectList GetSelectListEstadoCaso()
        {
            return new SelectList(new List<SelectListItem>()
            {
                new SelectListItem{ Text = "Abierto", Value="A" },
                new SelectListItem{ Text = "Cerrado", Value="C" },

            });
        }

        public static List<SelectListItem> GetSelectListItemCategorias()
        {
           return new List<SelectListItem>()
            {
                new SelectListItem{ Text = "Categoria 1", Value="A" },
                new SelectListItem{ Text = "Categoria 2", Value="B" },

            };
        }

        public static List<SelectListItem> GetSelectListItemTipo()
        {
            return new List<SelectListItem>()
            {
                new SelectListItem{ Text = "Privado", Value="P" },
                new SelectListItem{ Text = "Público", Value="U" },

            };
        }


        public static List<SelectListItem> GetSelectListItemEstadoCaso()
        {
            return new List<SelectListItem>()
            {
                new SelectListItem{ Text = "Abierto", Value="A" },
                new SelectListItem{ Text = "Cerrado", Value="C" },

            };
        }


    }
}

    