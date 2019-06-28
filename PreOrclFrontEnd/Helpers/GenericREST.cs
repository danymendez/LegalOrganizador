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

        public  string BASEURL;

        public GenericREST(UriHelpers uriHelpers) {
            BASEURL = uriHelpers.BaseUrl;
        }

        public virtual async Task<T> Get<T>(string urlMethod,decimal? id)
        {
            T entity = default(T);
            try
            {
                HttpClient client = new HttpClient();
                string uri = BASEURL;
                //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                HttpResponseMessage response =await client.GetAsync(uri + urlMethod + id);

                if (response.IsSuccessStatusCode)
                    entity = await response.Content.ReadAsAsync<T>();

            }
            catch (Exception ex)
            {
                ExceptionUtility.LogException(ex);
            }
            return entity;
        }

        public virtual async Task<List<T>> GetAll<T>(string urlMethod)
        {
            List<T> lista = new List<T>();
            try
            {
                HttpClient client = new HttpClient();
                string uri = BASEURL;
              
                HttpResponseMessage response = await client.GetAsync(uri + urlMethod);

                if (response.IsSuccessStatusCode)
                    lista = await response.Content.ReadAsAsync<List<T>>();

            }
            catch (Exception ex)
            {
                ExceptionUtility.LogException(ex);
            }
            return lista;
        }

        public virtual async Task<T> Post<T>(string urlMethod, T clase)
        {
            
            try
            {
                HttpClient client = new HttpClient();
                string uri = BASEURL;
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response =await client.PostAsJsonAsync(uri + urlMethod, clase);

                if (response.IsSuccessStatusCode)
                    clase = await response.Content.ReadAsAsync<T>();


            }
            catch (Exception ex)
            {
                ExceptionUtility.LogException(ex);
            }
            return clase;
        }

        public virtual async Task<bool> PostIsSaved<T>(string urlMethod, T clase)
        {
            bool isSaved = false;
            try
            {
                HttpClient client = new HttpClient();
                string uri = BASEURL;
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = await client.PostAsJsonAsync(uri + urlMethod, clase);

                if (response.IsSuccessStatusCode)
                    isSaved = await response.Content.ReadAsAsync<bool>();


            }
            catch (Exception ex)
            {
                ExceptionUtility.LogException(ex);
            }
            return isSaved;
        }

        public virtual async Task<bool> PutIsSaved<T>(string urlMethod, T clase)
        {
            bool isSaved = false;
            try
            {
                HttpClient client = new HttpClient();
                string uri = BASEURL;
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = await client.PutAsJsonAsync(uri + urlMethod, clase);

                if (response.IsSuccessStatusCode)
                    isSaved = await response.Content.ReadAsAsync<bool>();


            }
            catch (Exception ex)
            {
                ExceptionUtility.LogException(ex);
            }
            return isSaved;
        }
        public virtual async Task<T> PostAuth<T>(string urlMethod, string Usuario, string Password)
        {
            T clase = default(T);

            try
            {
                HttpClient client = new HttpClient();
                string uri = BASEURL;
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = await client.PostAsJsonAsync(uri + urlMethod+"?Usuario="+Usuario+"&Password="+Password,new StringContent(Usuario));

                if (response.IsSuccessStatusCode)
                    clase = await response.Content.ReadAsAsync<T>();


            }
            catch (Exception ex)
            {
                ExceptionUtility.LogException(ex);
           
            }
            return clase;
        }

        public virtual async Task<bool> Put<T>(string Method, decimal? id, T clase)
        {
            bool isSuccess = false;
            try
            {
                HttpClient client = new HttpClient();
                string uri = BASEURL;
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = await client.PutAsJsonAsync(uri+Method+ id, clase);

                isSuccess = response.IsSuccessStatusCode;

            }
            catch (Exception ex)
            {
                ExceptionUtility.LogException(ex);
            }
            return isSuccess;
        }

        public virtual async Task<T> Delete<T>(string urlMethod,decimal? id)
        {
            try
            {
                HttpClient client = new HttpClient();
                string uri = BASEURL;
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = await client.DeleteAsync(uri + urlMethod+id);

                if (response.IsSuccessStatusCode)
                    return await response.Content.ReadAsAsync<T>();

            }
            catch (Exception ex)
            {
                ExceptionUtility.LogException(ex);
            }
            return default(T);
        }
    }
}
