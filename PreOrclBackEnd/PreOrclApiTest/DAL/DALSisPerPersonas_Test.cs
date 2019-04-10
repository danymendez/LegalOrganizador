using Common.Data.DAL;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Common.Entity.Models;
using System.Collections.Generic;
using System.Linq;
using Common.BOL.BOL;
using Common.Data.SQLBuilders;
using System.Text;

namespace PreOrclApiTest.DAL
{
    [TestClass]
    public class DALSisPerPersonas_Test
    {
        [TestMethod]
        public void CRUD_Test()

        {
            SisPerPersona sisPerPersonaCreate = null;
            List<SisPerPersona> listaPersona = new List<SisPerPersona>();
            SisPerPersona sisPerPersonaOne = null;
            SisPerPersona sisPerPersonaDelete = null;
            using (DALDBContext context = new DALDBContext())
            {
                DALSisPerPersona dal = new DALSisPerPersona(context);
                listaPersona = dal.GetAllSisPerPersona();
                int idMax = listaPersona.Select(c => c.per_IDPER).Max();
                sisPerPersonaOne = dal.GetPersona(idMax);
                sisPerPersonaCreate = dal.CreateSisPerPersona(new SisPerPersona
              {
                  per_IDPER = 0,
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

                sisPerPersonaDelete = dal.DeletePersona(sisPerPersonaCreate.per_IDPER);
            }

            Assert.AreNotEqual(0, sisPerPersonaCreate.per_IDPER);
            Assert.AreNotEqual(0, listaPersona.Count());
            Assert.IsNotNull(sisPerPersonaDelete);
            Assert.IsNotNull(sisPerPersonaOne);
        }

        [TestMethod]
        public async System.Threading.Tasks.Task Autenticar_TestAsync() {
            BOLUsuarios bol = new BOLUsuarios();
            Usuarios a = await bol.Autenticar("danielmendez", "1234");
            Assert.IsNotNull(a);
        }


        [TestMethod]
        public void Update_Test() {
            BOLSisPerPersonas bol = new BOLSisPerPersonas();

           var sisPerPersonaCreate =  bol.UpdateSisPerPersona(21,new SisPerPersona
            {
                per_IDPER = 21,
                per_nombre_razon = "Daniel",
                per_apellido_comercial = "Méndez",
                per_nit = "a",
                per_dui_nrc = "a",
                per_direccion_departamento = "a",
                per_direccion_municipio = "a",
                per_direccion = "El salvador",
                per_telefono = "a",
                per_movil = "a",
                per_email = "",
                per_codigo = "",
                per_nacionalidad = "",
                per_tipo_contribullente = "",
                per_dir_cli = "",
                per_cobros = ""

            }).Result;

            Assert.AreNotEqual(0,sisPerPersonaCreate.per_IDPER);
        }
        //[TestMethod]
        //public void SelectAll() {

        //    List<SisPerPersona> lista = null;

        //    using (DALDBContext context = new DALDBContext())
        //    {
        //        DALSisPerPersona dal = new DALSisPerPersona(context);

        //        lista = dal.GetAllSisPerPersona();
        //    }

        //    Assert.IsNotNull(lista);
        //}

        //[TestMethod]
        //public void DeletePersona_Test() {

        //    SisPerPersona sis = null;

        //    using (DALDBContext context = new DALDBContext())
        //    {
        //        DALSisPerPersona dal = new DALSisPerPersona(context);
        //        var a = dal.DeletePersona(15);
        //    }

        //    Assert.IsNotNull(sis);
        //}

        //[TestMethod]
        //public void GetPersona_Test()
        //{
        //    SisPerPersona sis = null;
        //    using (DALDBContext context = new DALDBContext())
        //    {
        //        DALSisPerPersona dal = new DALSisPerPersona(context);
        //        sis = dal.GetPersona(17);
        //    }

        //    Assert.IsNotNull(sis);
        //}

        [TestMethod]
        public void SqlQuery_Test() {

            //Usuarios us = new Usuarios { IdUsuario=0,Usuario="da",Nombre="Daniel", Apellido="Méndez",Password="123"};

            //using (DALDBContext db = new DALDBContext()) {



            //}
            SqlQueryBuilder sqlQuery = new SqlQueryBuilder();
            sqlQuery.TableAttributeBindName<Usuarios>("\"", ":");
            var valor = sqlQuery.InsertQuery<Usuarios>();
        }

        [TestMethod]
        public void Encripter_Test() {
            StringBuilder st = new StringBuilder();

            for (int i = 0; i < 1000; i++) {
                st.Append(i + " " + "Encriptado: " + PreOrclApi.Utilidades.Criptografia.Encrypt(i.ToString())+" /n");
            }

            string a = st.ToString();
        }


    }
}
