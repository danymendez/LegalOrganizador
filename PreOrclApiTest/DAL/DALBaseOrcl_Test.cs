using Data.DAL;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PreOrclApi.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PreOrclApiTest.DAL
{

    [TestClass]
    class DALBaseOrcl_Test
    {

        [TestMethod]
        public void Insertar()
        {
            using (DALBaseOrcl baseorc = new DALBaseOrcl())
            {



                baseorc.Create(new SisPerPersona
                {
                    per_IDPER = 11,
                    per_nombre_razon = "Daniel",
                    per_apellido_comercial = "Méndez",
                    per_nit = "a",
                    per_dui_nrc = "a",
                    per_direccion_departamento = "a",
                    per_direccion_municipio = "a",
                    per_direccion = "a",
                    per_telefono = "a",
                    per_movil = "a",
                    per_email = "",
                    per_codigo = "",
                    per_nacionalidad = "",
                    per_tipo_contribullente = "",
                    per_dir_cli = "",
                    per_cobros = ""

                });
            }
        }
    }
}
