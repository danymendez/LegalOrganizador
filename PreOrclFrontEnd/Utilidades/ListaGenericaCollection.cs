using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PreOrclFrontEnd.Utilidades
{
    public static class ListaGenericaCollection
    {
        public static List<ListItem> GetListCategorias() {
            return new List<ListItem>()
            {
                new ListItem{ Text = "Categoria 1", Value="A" },
                new ListItem{ Text = "Categoria 2", Value="B" },

            };
        }

        public static List<ListItem> GetListTipo()
        {
            return new List<ListItem>()
            {
                new ListItem{ Text = "Privado", Value="P" },
                new ListItem{ Text = "Público", Value="U" },

            };
        }


        public static List<ListItem> GetListEstadoCaso()
        {
            return new List<ListItem>()
            {
                new ListItem{ Text = "Abierto", Value="A" },
                new ListItem{ Text = "Cerrado", Value="C" },

            };
        }
    }

        public class ListItem
        {
            public string Text { get; set; }
            public string Value { get; set; }
        }
}

    