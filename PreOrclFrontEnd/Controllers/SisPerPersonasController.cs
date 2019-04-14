using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PreOrclFrontEnd.Models;
using PreOrclFrontEnd.Helpers;
using Microsoft.AspNetCore.Authorization;
using PreOrclFrontEnd.Utilidades;
using Microsoft.Extensions.Options;

namespace PreOrclFrontEnd.Controllers
{
    [Authorize(Roles ="Personas")]
    public class SisPerPersonasController : Controller
    {
 
        GenericREST generic;

        public SisPerPersonasController(IOptions<UriHelpers> configuration)
        {
        
            generic = new GenericREST(configuration.Value);

        }


        // GET: SisPerPersonas
        public async Task<IActionResult> Index()
        {
            List<SisPerPersona> listaSisPersona = await generic.GetAll<SisPerPersona>("SisPerPersonas");
        
            ViewBag.PersonasClassCssNav = "active";

            return View(listaSisPersona);
        }

        // GET: SisPerPersonas/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sisPerPersona = await generic.Get<SisPerPersona>("SisPerPersonas/", id);
            if (sisPerPersona == null)
            {
                return NotFound();
            }

            return View(sisPerPersona);
        }

        // GET: SisPerPersonas/DetailsPartial/5
        public async Task<IActionResult> DetailsPartial(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sisPerPersona = await generic.Get<SisPerPersona>("SisPerPersonas/", id);
            if (sisPerPersona == null)
            {
                return NotFound();
            }

            return PartialView("../SisPerPersonas/_DetailsPartial",sisPerPersona);
        }


        // GET: SisPerPersonas/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: SisPerPersonas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("per_IDPER,per_nombre_razon,per_apellido_comercial,per_nit,per_dui_nrc,per_direccion_departamento,per_direccion_municipio,per_direccion,per_telefono,per_movil,per_email,per_codigo,per_nacionalidad,per_tipo_contribullente,per_dir_cli,per_cobros")] SisPerPersona sisPerPersona)
        {
            if (ModelState.IsValid)
            {

                sisPerPersona = await generic.Post("SisPerPersonas", sisPerPersona);
               
                if (sisPerPersona.per_IDPER == 0) {
                    return BadRequest();
                }
                return RedirectToAction(nameof(Index));
            }
            return View(sisPerPersona);
        }

        // GET: SisPerPersonas/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sisPerPersona = await generic.Get<SisPerPersona>("SisPerPersonas/",id);
            if (sisPerPersona == null)
            {
                return NotFound();
            }
            return View(sisPerPersona);
        }

        // POST: SisPerPersonas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("per_IDPER,per_nombre_razon,per_apellido_comercial,per_nit,per_dui_nrc,per_direccion_departamento,per_direccion_municipio,per_direccion,per_telefono,per_movil,per_email,per_codigo,per_nacionalidad,per_tipo_contribullente,per_dir_cli,per_cobros")] SisPerPersona sisPerPersona)
        {
            if (id != sisPerPersona.per_IDPER)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                  bool isSaved =  await generic.Put("SisPerPersonas/",id,sisPerPersona);
                    if (!isSaved) {
                        return BadRequest();
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SisPerPersonaExists(sisPerPersona.per_IDPER))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(sisPerPersona);
        }

        private bool SisPerPersonaExists(decimal per_IDPER)
        {
            throw new NotImplementedException();
        }

        // GET: SisPerPersonas/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sisPerPersona = await generic.Get<SisPerPersona>("SisPerPersonas/", id);
            if (sisPerPersona == null)
            {
                return NotFound();
            }

            return View(sisPerPersona);
        }

        // POST: SisPerPersonas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            SisPerPersona entity = await generic.Delete<SisPerPersona>("SisPerPersonas/", id);
            
            if (entity == null)
                return BadRequest();
            return RedirectToAction(nameof(Index));
        }

       
    }
}
