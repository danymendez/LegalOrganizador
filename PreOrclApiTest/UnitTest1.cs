using Data.DAL;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PreOrclApi.Models;

namespace PreOrclApiTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()

        {
            SisPerPersona sis = new SisPerPersona();
            using (DALBaseOrcl baseorc = new DALBaseOrcl())
            {



              sis =  baseorc.Create(new SisPerPersona
                {
                    per_IDPER = 14,
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

            Assert.AreNotEqual(0,sis.per_IDPER);
        }
    }
}
