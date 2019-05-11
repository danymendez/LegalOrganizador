using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using PreOrclFrontEnd.Helpers;
using PreOrclFrontEnd.Models;
using PreOrclFrontEnd.Utilidades;
using PreOrclFrontEnd.ViewModels;

namespace PreOrclFrontEnd.Controllers
{
    [Authorize(Roles = "Casos")]

    public class CasosController : Controller
    {

        GenericREST generic;
        ListaSistema listaSistema;
        private decimal idUsuario = 0;
        private readonly CacheItems cacheItems;
        public CasosController(IOptions<UriHelpers> configuration, IMemoryCache memoryCache)
        {

            generic = new GenericREST(configuration.Value);
            listaSistema = new ListaSistema(configuration);
            cacheItems = new CacheItems(memoryCache);
        }
        public IActionResult Index()
        {
            bool isAuthenticatedAdmin = false;
          
            if (User != null)
            {
                if (User.Identity.IsAuthenticated)
                    idUsuario = Convert.ToDecimal(User.Identities
                                    .Where(c => c.IsAuthenticated).FirstOrDefault()
                                    .Claims.Where(c => c.Type == ClaimTypes.NameIdentifier).FirstOrDefault().Value);


                isAuthenticatedAdmin = User.Identities
                                    .Where(c => c.IsAuthenticated).FirstOrDefault()
                                    .Claims.Where(c => c.Value == "Administrador").FirstOrDefault() != null;

          


            }


            var listViewModelCasos = generic.GetAll<VwModelCasos>("Casos/GetAllVwModelCasos").Result;
            if (!isAuthenticatedAdmin) {
               
                    listViewModelCasos = listViewModelCasos.Where(c => ((c.Casos.IdAbogado == idUsuario && c.Casos.Tipo == "P")||c.Casos.Tipo=="U") || c.ListVwModelActividadesAsistentes.Where(d=>d.IdAsistentes.Contains(idUsuario) || d.Actividades.IdResponsable==idUsuario).FirstOrDefault()!=null).ToList();
              
             
            }
            ViewBag.listaEstadoCasos = ListaGenericaCollection.GetSelectListItemEstadoCaso();
            return View(listViewModelCasos);
        }

     
        public IActionResult Create()
        {

            ViewBag.listaPersonas = listaSistema.GetSelectListClientes();
            ViewBag.listaImputados = listaSistema.GetSelectListClientes();
            ViewBag.listaCategorias = ListaGenericaCollection.GetSelectListItemCategorias();
            ViewBag.listaTipos = ListaGenericaCollection.GetSelectListItemTipo();
            ViewBag.listaAbogados = listaSistema.GetSelectListAbogados();
            ViewBag.listaEstadoCasos = ListaGenericaCollection.GetSelectListItemEstadoCaso();

            return View();
        }

        public SelectList GetListaPersonas() {
            var lista = new SelectList(generic.GetAll<SisPerPersona>("SisPerPersonas").Result, "per_IDPER", "per_nombre_razon");
            return lista;
        }

        public async Task<IActionResult> Edit(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ViewBag.listaPersonas = listaSistema.GetSelectListClientes();
            ViewBag.listaImputados = listaSistema.GetSelectListClientes();
            ViewBag.listaCategorias = ListaGenericaCollection.GetSelectListItemCategorias();
            ViewBag.listaTipos = ListaGenericaCollection.GetSelectListItemTipo();
            ViewBag.listaAbogados = listaSistema.GetSelectListAbogados();
            ViewBag.listaEstadoCasos = ListaGenericaCollection.GetSelectListItemEstadoCaso();
            var casos = await generic.Get<VwModelCasos>("Casos/GetVwModelCasos/", id);
            if (casos == null)
            {
                return NotFound();
            }
            return View(casos);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, VwModelCasos vwModelCasos)
        {
            if (User != null)
            {
                if (User.Identity.IsAuthenticated)
                    idUsuario = Convert.ToDecimal(User.Identities
                                    .Where(c => c.IsAuthenticated).FirstOrDefault()
                                    .Claims.Where(c => c.Type == ClaimTypes.NameIdentifier).FirstOrDefault().Value);

                  

            }

            if (id != vwModelCasos.Casos.IdCaso)
            {
                return NotFound();
            }

            if (vwModelCasos.ListadoDocumentos != null || vwModelCasos.Documentos != null) {
                ModelState["Documentos"].ValidationState = Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Valid;
            }

            Regex regex = new Regex(@"^[0-9]{1,3}(,[0-9]{3}){0,2}(\.[0-9]{2})$");
            if (regex.IsMatch(ModelState["Casos.PrecioPactado"].AttemptedValue))
                ModelState["Casos.PrecioPactado"].ValidationState = Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Valid;
            if (ModelState.IsValid)
            {
                try
                {
                    vwModelCasos.Casos.UpdatedAt = DateTime.Now;
                    if (vwModelCasos.Casos.EstadoCaso == "C") {
                        vwModelCasos.Casos.IdUsuarioCierre = idUsuario;
                    }
                    vwModelCasos.ListadoDocumentos = vwModelCasos.ListadoDocumentos ?? new List<Documentos>();
                    if (vwModelCasos.Documentos != null)
                    {
                        foreach (var itemDocumento in vwModelCasos.Documentos)
                        {
                            vwModelCasos.ListadoDocumentos.Add(new Documentos { Nombre = itemDocumento.FileName, CreatedAt = DateTime.Now, Url = itemDocumento.FileName, Archivo = ConvertFileToByte(itemDocumento) });
                        }
                    }
                    bool isSaved = await generic.Put("Casos/PutVwModelCasos/", id, vwModelCasos);
                    if (!isSaved)
                    {
                        return BadRequest();
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    return BadRequest();
                }
                return RedirectToAction(nameof(Index));
            }
            return View(vwModelCasos.Casos);
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(VwModelCasos vwModelCasos)
        {
            if (User != null)
            {
                if (User.Identity.IsAuthenticated)
                    idUsuario = Convert.ToDecimal(User.Identities
                                    .Where(c => c.IsAuthenticated).FirstOrDefault()
                                    .Claims.Where(c => c.Type == ClaimTypes.NameIdentifier).FirstOrDefault().Value);
                                         

            }

            Regex regex = new Regex(@"^[0-9]{1,3}(,[0-9]{3}){0,2}(\.[0-9]{2})$");
            if (regex.IsMatch(ModelState["Casos.PrecioPactado"].AttemptedValue))
                ModelState["Casos.PrecioPactado"].ValidationState = Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Valid;
            if (ModelState.IsValid)
            {
                vwModelCasos.Casos.CreatedAt = DateTime.Now;
                vwModelCasos.Casos.Cancelado = 0;
                vwModelCasos.Casos.Inactivo = 0;
                vwModelCasos.Casos.IdCreador = idUsuario;
                
                vwModelCasos.ListadoDocumentos = new List<Documentos>();
                foreach (var itemDocumento in vwModelCasos.Documentos) {
                    vwModelCasos.ListadoDocumentos.Add(new Documentos {Nombre=itemDocumento.FileName,CreatedAt=DateTime.Now,Url=itemDocumento.FileName, Archivo= ConvertFileToByte(itemDocumento) });
                }
                vwModelCasos = await generic.Post("Casos/PostVwModelCasos", vwModelCasos);

                if (vwModelCasos.Casos.IdCaso == 0)
                {
                    return BadRequest();
                }
                return RedirectToAction(nameof(Index));
            }
            return View(vwModelCasos);
        }

        public byte[] ConvertFileToByte(IFormFile file) {

            byte[] bytes = null;
            if (file.Length > 0)
            {

                using (Stream fs = file.OpenReadStream())
                {
                    using (BinaryReader br = new BinaryReader(fs))
                    {
                      bytes = br.ReadBytes((Int32)fs.Length);
                    }
                }

                        //var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName;
                        //using (var reader = new StreamReader(file.OpenReadStream()))
                        //{
                        //    string contentAsString = reader.ReadToEnd();
                        //     bytes = new byte[contentAsString.Length * sizeof(char)];
                        //    System.Buffer.BlockCopy(contentAsString.ToCharArray(), 0, bytes, 0, bytes.Length);
                        //}
                    }

            return bytes;
        }

        public FileContentResult Download(decimal id)
        {
            var documento = generic.Get<Documentos>("Documentos/", id).Result;
            byte[] fileBytes = documento.Archivo;
            return File(fileBytes, "application/x-msdownload", documento.Nombre);
        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            base.OnActionExecuted(context);
            ViewData["img"] = cacheItems.GetImageBase64FromCache(User);
        }
    }
}