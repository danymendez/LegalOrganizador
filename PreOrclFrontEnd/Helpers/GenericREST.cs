using PreOrclFrontEnd.Utilidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace PreOrclFrontEnd.Helpers
{
    public class GenericREST
    {

      //  public const string BASEURL = "https://localhost:44341/api/";
        //public const string BASEURL = "http://192.168.0.40:5000/preorclapi/api/";
        public  string BASEURL;

        public GenericREST(UriHelpers uriHelpers) {
            BASEURL = uriHelpers.BaseUrl;
        }

        public virtual T Get<T>(string urlMethod, int? id)
        {
            T entity = default(T);
            try
            {
                HttpClient client = new HttpClient();
                string uri = BASEURL;
                //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                HttpResponseMessage response = client.GetAsync(uri + urlMethod + id).Result;

                if (response.IsSuccessStatusCode)
                    entity = response.Content.ReadAsAsync<T>().Result;

            }
            catch (Exception ex)
            {
                ExceptionUtility.LogException(ex);
            }
            return entity;
        }

        public virtual List<T> GetAll<T>(string urlMethod)
        {
            List<T> lista = new List<T>();
            try
            {
                HttpClient client = new HttpClient();
                string uri = BASEURL;
              
                HttpResponseMessage response = client.GetAsync(uri + urlMethod).Result;

                if (response.IsSuccessStatusCode)
                    lista = response.Content.ReadAsAsync<List<T>>().Result;

            }
            catch (Exception ex)
            {
                ExceptionUtility.LogException(ex);
            }
            return lista;
        }

        public virtual T Post<T>(string urlMethod, T clase)
        {
            
            try
            {
                HttpClient client = new HttpClient();
                string uri = BASEURL;
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = client.PostAsJsonAsync(uri + urlMethod, clase).Result;

                if (response.IsSuccessStatusCode)
                    clase = response.Content.ReadAsAsync<T>().Result;


            }
            catch (Exception ex)
            {
                ExceptionUtility.LogException(ex);
            }
            return clase;
        }

        public virtual T PostAuth<T>(string urlMethod, string Usuario, string Password)
        {
            T clase = default(T);

            try
            {
                HttpClient client = new HttpClient();
                string uri = BASEURL;
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = client.PostAsJsonAsync(uri + urlMethod+"?Usuario="+Usuario+"&Password="+Password,new StringContent(Usuario)).Result;

                if (response.IsSuccessStatusCode)
                    clase = response.Content.ReadAsAsync<T>().Result;


            }
            catch (Exception ex)
            {
                ExceptionUtility.LogException(ex);
           
            }
            return clase;
        }

        public virtual bool Put<T>(string Method, int? id, T clase)
        {
            bool isSuccess = false;
            try
            {
                HttpClient client = new HttpClient();
                string uri = BASEURL;
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = client.PutAsJsonAsync(uri+Method+ id, clase).Result;

                isSuccess = response.IsSuccessStatusCode;

            }
            catch (Exception ex)
            {
                ExceptionUtility.LogException(ex);
            }
            return isSuccess;
        }

        public virtual T Delete<T>(string urlMethod,int? id)
        {
            try
            {
                HttpClient client = new HttpClient();
                string uri = BASEURL;
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = client.DeleteAsync(uri + urlMethod+id).Result;

                if (response.IsSuccessStatusCode)
                    return response.Content.ReadAsAsync<T>().Result;

            }
            catch (Exception ex)
            {
                ExceptionUtility.LogException(ex);
            }
            return default(T);
        }
    }
}
