using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Common.Entity.Models;
using Common.Entity.ViewModels;
using Common.Utilities;
using Microsoft.Graph;
using Newtonsoft.Json;



namespace Common.BOL.BOL
{
    public class BOLMSGraph
    {

        public GraphServiceClient GetAuthenticatedClient(string token)
        {

            GraphServiceClient _graphClient = new GraphServiceClient(new DelegateAuthenticationProvider(
                async requestMessage =>
                {
                    // Passing tenant ID to the sample auth provider to use as a cache key
                    var accessToken = token;

                    // Append the access token to the request
                    requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

                    // This header identifies the sample in the Microsoft Graph service. If extracting this code for your project please remove.
                    //  requestMessage.Headers.Add("SampleID", "aspnetcore-connect-sample");
                }));
            // c = _authProvider.GetUserAccessTokenAsync(userId).Result;
            return _graphClient;
        }

        private async Task<Microsoft.Graph.User> GetUser(GraphServiceClient graphClient)
        {
            return await graphClient.Me.Request().GetAsync();
        }

        private async Task<Stream> GetPictureStream(GraphServiceClient graphClient)
        {


            Stream pictureStream = null;

            try
            {
                try
                {
                    // Load user's profile picture.
                    pictureStream = await graphClient.Me.Photo.Content.Request().GetAsync();
                }
                catch (ServiceException e)
                {
                    if (e.Error.Code == "GetUserPhoto") // User is using MSA, we need to use beta endpoint
                    {
                        // Set Microsoft Graph endpoint to beta, to be able to get profile picture for MSAs 
                        graphClient.BaseUrl = "https://graph.microsoft.com/beta";

                        // Get profile picture from Microsoft Graph
                        pictureStream = await graphClient.Me.Photo.Content.Request().GetAsync();

                        // Reset Microsoft Graph endpoint to v1.0
                        graphClient.BaseUrl = "https://graph.microsoft.com/v1.0";
                    }
                }
            }
            catch (ServiceException e)
            {
                switch (e.Error.Code)
                {
                    case "Request_ResourceNotFound":
                    case "ResourceNotFound":
                    case "ErrorItemNotFound":
                    case "itemNotFound":
                    case "ErrorInvalidUser":
                        // If picture not found, return the default image.
                        throw new Exception("ResourceNotFound");
                    case "TokenNotFound":
                        //   await httpContext.ChallengeAsync();
                        return null;
                    default:
                        return null;
                }
            }

            return pictureStream;
        }

        private async Task<string> GetPictureBase64(GraphServiceClient graphClient)
        {
            try
            {
                // Load user's profile picture.
                var pictureStream = await GetPictureStream(graphClient);

                // Copy stream to MemoryStream object so that it can be converted to byte array.
                var pictureMemoryStream = new MemoryStream();
                await pictureStream.CopyToAsync(pictureMemoryStream);

                // Convert stream to byte array.
                var pictureByteArray = pictureMemoryStream.ToArray();

                // Convert byte array to base64 string.
                var pictureBase64 = Convert.ToBase64String(pictureByteArray);

                return "data:image/jpeg;base64," + pictureBase64;
            }
            catch (Exception e)
            {
                switch (e.Message)
                {
                    case "ResourceNotFound":
                        // If picture not found, return the default image.
                        return "data:image/svg+xml;base64,PD94bWwgdmVyc2lvbj0iMS4wIiBlbmNvZGluZz0iVVRGLTgiPz4NCjwhRE9DVFlQRSBzdmcgIFBVQkxJQyAnLS8vVzNDLy9EVEQgU1ZHIDEuMS8vRU4nICAnaHR0cDovL3d3dy53My5vcmcvR3JhcGhpY3MvU1ZHLzEuMS9EVEQvc3ZnMTEuZHRkJz4NCjxzdmcgd2lkdGg9IjQwMXB4IiBoZWlnaHQ9IjQwMXB4IiBlbmFibGUtYmFja2dyb3VuZD0ibmV3IDMxMi44MDkgMCA0MDEgNDAxIiB2ZXJzaW9uPSIxLjEiIHZpZXdCb3g9IjMxMi44MDkgMCA0MDEgNDAxIiB4bWw6c3BhY2U9InByZXNlcnZlIiB4bWxucz0iaHR0cDovL3d3dy53My5vcmcvMjAwMC9zdmciPg0KPGcgdHJhbnNmb3JtPSJtYXRyaXgoMS4yMjMgMCAwIDEuMjIzIC00NjcuNSAtODQzLjQ0KSI+DQoJPHJlY3QgeD0iNjAxLjQ1IiB5PSI2NTMuMDciIHdpZHRoPSI0MDEiIGhlaWdodD0iNDAxIiBmaWxsPSIjRTRFNkU3Ii8+DQoJPHBhdGggZD0ibTgwMi4zOCA5MDguMDhjLTg0LjUxNSAwLTE1My41MiA0OC4xODUtMTU3LjM4IDEwOC42MmgzMTQuNzljLTMuODctNjAuNDQtNzIuOS0xMDguNjItMTU3LjQxLTEwOC42MnoiIGZpbGw9IiNBRUI0QjciLz4NCgk8cGF0aCBkPSJtODgxLjM3IDgxOC44NmMwIDQ2Ljc0Ni0zNS4xMDYgODQuNjQxLTc4LjQxIDg0LjY0MXMtNzguNDEtMzcuODk1LTc4LjQxLTg0LjY0MSAzNS4xMDYtODQuNjQxIDc4LjQxLTg0LjY0MWM0My4zMSAwIDc4LjQxIDM3LjkgNzguNDEgODQuNjR6IiBmaWxsPSIjQUVCNEI3Ii8+DQo8L2c+DQo8L3N2Zz4NCg==";
                    case "EmailIsNull":
                        return JsonConvert.SerializeObject(new { Message = "Email address cannot be null." }, Newtonsoft.Json.Formatting.Indented);
                    default:
                        return null;
                }
            }
        }

        public async Task<List<Calendar>> GetCalendar(GraphServiceClient graphClient) {
            var calendars = await graphClient.Me.Calendars.Request().GetAsync();
            return calendars.ToList();
        }

        public async Task<Tuple<Event,string>> CreateEvent(GraphServiceClient graphClient, Actividades actividades,List<VwModelAsistentes> vwModelAsistentes)
        {
            string messageError = "";

            Event createdEvent= null;
            try
            {

                List<Option> requestOptions = new List<Option>()
            {
                new HeaderOption("Prefer", "outlook.timezone=\"" + TimeZoneInfo.FindSystemTimeZoneById("Central America Standard Time") + "\"")
            };
            string guid = Guid.NewGuid().ToString();

            List<Attendee> _attendees = new List<Attendee>();

                foreach (var itemAsistente in vwModelAsistentes) {
                    _attendees.Add(new Attendee
                    {
                        EmailAddress = new EmailAddress
                        {
                            Address = itemAsistente.Correo
                        },
                        Type = AttendeeType.Required
                    });
                }


                

                // Event body
                ItemBody body = new ItemBody
            {
                Content = $"{actividades.NombreActividad} - Caso: {actividades.IdCaso}",
                ContentType = BodyType.Text
            };

            // Event start and end time
            // Another example date format: `new DateTime(2017, 12, 1, 9, 30, 0).ToString("o")`
            DateTimeTimeZone startTime = new DateTimeTimeZone
            {
                DateTime = actividades.StartTime.ToString(),
                TimeZone = actividades.TimeZone
            };
            DateTimeTimeZone endTime = new DateTimeTimeZone
            {
                DateTime = actividades.EndTime.ToString(),
                TimeZone = actividades.TimeZone
            };

            // Event location
            Location location = new Location
            {
                DisplayName ="",
            };

           
                 createdEvent = await graphClient.Me.Calendars[actividades.IdCalendario].Events.Request(requestOptions)

                   .AddAsync(new Event
                   {
                       Subject =actividades.NombreActividad,
                       Location = location,
                       Attendees = _attendees,
                       Body = body,
                       Start = startTime,
                       End = endTime
                   });

                actividades.IdEvento = createdEvent.Id;

            }
            catch (ServiceException ex) {
                messageError = ex.Error.Code;
                ExceptionUtility.LogException(ex);
            }
            catch (Exception ex)
            {
                ExceptionUtility.LogException(ex);
            }


            return new Tuple<Event,string>(createdEvent,messageError);
        }

        public async Task<Tuple<Event, string>> UpdateEvent(GraphServiceClient graphClient, Actividades actividades, List<VwModelAsistentes> vwModelAsistentes)
        {
            string messageError = "";

            Event createdEvent = null;
            try
            {

                List<Option> requestOptions = new List<Option>()
            {
                new HeaderOption("Prefer", "outlook.timezone=\"" + TimeZoneInfo.FindSystemTimeZoneById("Central America Standard Time") + "\"")
            };
                string guid = Guid.NewGuid().ToString();

                List<Attendee> _attendees = new List<Attendee>();

                foreach (var itemAsistente in vwModelAsistentes)
                {
                    _attendees.Add(new Attendee
                    {
                        EmailAddress = new EmailAddress
                        {
                            Address = itemAsistente.Correo
                        },
                        Type = AttendeeType.Required
                    });
                }




                // Event body
                ItemBody body = new ItemBody
                {
                    Content = $"{actividades.NombreActividad} - Caso: {actividades.IdCaso}",
                    ContentType = BodyType.Text
                };

                // Event start and end time
                // Another example date format: `new DateTime(2017, 12, 1, 9, 30, 0).ToString("o")`
                DateTimeTimeZone startTime = new DateTimeTimeZone
                {
                    DateTime = actividades.StartTime.ToString(),
                    TimeZone = actividades.TimeZone
                };
                DateTimeTimeZone endTime = new DateTimeTimeZone
                {
                    DateTime = actividades.EndTime.ToString(),
                    TimeZone = actividades.TimeZone
                };

                // Event location
                Location location = new Location
                {
                    DisplayName = "",
                };



                createdEvent = await graphClient.Me.Calendars[actividades.IdCalendario].Events[actividades.IdEvento].Request(requestOptions).UpdateAsync(new Event
                {
                    Subject = actividades.NombreActividad,
                    Location = location,
                    Attendees = _attendees,
                    Body = body,
                    Start = startTime,
                    End = endTime
                });

                actividades.IdEvento = createdEvent.Id;

            }
            catch (ServiceException ex)
            {
                messageError = ex.Error.Code;
                ExceptionUtility.LogException(ex);
            }
            catch (Exception ex)
            {
                ExceptionUtility.LogException(ex);
            }


            return new Tuple<Event, string>(createdEvent, messageError);
        }

        public async Task<Tuple<bool, string>> CancelEvent(GraphServiceClient graphClient, Actividades actividades)
        {
            string messageError = "";

            bool isDeleted = false;
            try
            {

                List<Option> requestOptions = new List<Option>()
            {
                new HeaderOption("Prefer", "outlook.timezone=\"" + TimeZoneInfo.FindSystemTimeZoneById("Central America Standard Time") + "\"")
            };
       
    

               await graphClient.Me.Calendars[actividades.IdCalendario].Events[actividades.IdEvento].Request(requestOptions).DeleteAsync();

                isDeleted = true;

            }
            catch (ServiceException ex)
            {
                messageError = ex.Error.Code;
                ExceptionUtility.LogException(ex);
            }
            catch (Exception ex)
            {
                ExceptionUtility.LogException(ex);
            }


            return new Tuple<bool, string>(isDeleted, messageError);
        }


        public async Task<List<GraphEvents>> GetAllEvents(GraphServiceClient graphClient)
        {
            var calendarios = await graphClient.Me.Calendars.Request().GetAsync();
            List<GraphEvents> eventos = new List<GraphEvents>();
            foreach (var itemCalendario in calendarios) {

                foreach (var itemEvento in await graphClient.Me.Calendars[itemCalendario.Id.ToString()].Events.Request().GetAsync()) {
                    eventos.Add(new GraphEvents {
                        Event = itemEvento,
                        IdCalendar = itemCalendario.Id
                    });
                }
                
            }
            return eventos;
        }

        public async Task<List<Event>> GetEventsByIdCalendar(GraphServiceClient graphClient,string IdCalendar)
        {
            return (await graphClient.Me.Calendars[IdCalendar].Events.Request().GetAsync()).ToList();
        }
        public string GetToken(string refreshToken)
        {

            TokenRefreshed t = new TokenRefreshed();
            Peticion p =
                new Peticion
                {
                    client_id = "212a93ce-c93e-43b8-adaf-cc32d3606e75",
                    client_secret = "ysCRFE4_}fenfmVKW2574${",
                    scope = "calendars.read user.read user.readbasic.all mail.read",
                    //scope = "https://graph.microsoft.com/.default",
                    grant_type = "refresh_token",
                    refresh_token = refreshToken,
                    redirect_uri = "http://localhost:50222/Auth/LoginMicrosoft/"
                };
            try
            {
                HttpClient client = new HttpClient();

                var parames = new Dictionary<string, string>();
                string uri = "https://login.microsoftonline.com/common/oauth2/v2.0/token";

                parames.Add("client_id", p.client_id);
                parames.Add("client_secret", p.client_secret);
                parames.Add("scope", p.scope);
                parames.Add("grant_type", p.grant_type);
                parames.Add("refresh_token", p.refresh_token);
                parames.Add("redirect_uri", p.redirect_uri);

                // client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "EwCIA8l6BAAURSN/FHlDW5xN74t6GzbtsBBeBUYAAc6BnbUNV9Ul2QBvqM4NqjxDYRNLTlQOierQyQE7q1MXDn141wYU2h5o8e8EYVLSAcWk3BtTPt3mAqloE7sH8q2DLC0X00NVnQOCf6j89n1QMHBnBY/eZGE4CkzEKRAqPyDeHkdDAmxa4g53nS8wuaExrLPEttS4hFAxq4Su6a2KTFSWFphPqB+d0m5bOvRuS6XVBOYD9U657wwDcSH5T4vc6ZZUxUcXTQ53xPYYnLWsk8BYWzUVYClcCPZylvIudL6wVClf8s8TAx5qo+8nWWZvLh7l54Qwj/P0b/EhLPwuWsrJlem0mNpN5IJYO8noZn/hDiInpL5SLE0fOVgPuksDZgAACGcxIOebsKW0WAKT6HlLsNJwSze9N9LM3MqViDqcopnz4Ek2TrHeBCSAubCOb9xi1nNB+XqexhHvpNXYGcqox/sRS6a7DIKfEcBC8WNcRz5p3WbETefYmHTTY433HGHDz1VDOU8Bx2csbfvCD+gRM8Jtg5+CLZqFahWgW8hW3TaJ3kCXQ2KX4o2dtiuGZe18skvUe/aq92D3h3W2WU8tsIoFod/p12lDQGAGGHLm6CSZmmKYpuSJPQ6sMaB3hMkERu0jtwT65JhshxrE1li3ypiI0/qWZeLXMVt/7uf0Wep990Qgp7zBmqbHSxx8ncvBIYG6LRQ1kEMjC2LYLLXLro8zc0a6C3Ns+3VwkC3pY53atPo+0V7bRTD46363gJ/DG6HFpU8wvFoMOgaHKe3VryAj9bYABLjKS+DUdhaquz1IDv8RNUrF5VKkyhvwzcGlQXxj2bnxGKu0pB/L5PJjmdd0Yv0V6AJfykWiFI+yMZX9eQ6lXN4b59ezcuyfAb5xPb14NNEPA0SAe0usbk6cf0KgLLQ19X/Tg6M+UlvXFotDR1TiwjO3Mg+CphaAv0UkPN0jT/5RMHkVDW39PyIesH134q3QFnU0w2KL0ZChy/njvFSN7gd+q0Z767fSsSIhwy7fOU8LAdPfd472rUNPPoySwlUJBDr4IpdY2v3eJbGKR9y452ItNZomKQ/Hk4RLZwCbqoN+MnYisJTsNwQCEKyXgzRduLdV/AjZH4E4cHtgiOpA2uiHAe39Zz2UOx7djHPWkWdVlSaMgSdOR0LTXA3LybpHU+7uEhMcIiImButU4HChAg==");
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = client.PostAsync(uri, new FormUrlEncodedContent(parames)).Result;

                if (response.IsSuccessStatusCode)
                    t = response.Content.ReadAsAsync<TokenRefreshed>().Result;

            }
            catch (Exception ex)
            {
                ExceptionUtility.LogException(ex);
            }

            return t.access_token;
        }

        private class Peticion
        {
            public string client_id { get; set; }
            public string client_secret { get; set; }
            public string scope { get; set; }
            public string grant_type { get; set; }
            public string refresh_token { get; set; }
            public string redirect_uri { get; set; }

        }

        private class TokenRefreshed
        {
            public string token_type { get; set; }
            public string scope { get; set; }
            public string expires_in { get; set; }
            public string ext_expires_in { get; set; }
            public string access_token { get; set; }
            public string refresh_token { get; set; }
        }
    }
}
