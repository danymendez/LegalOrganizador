using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Common.Entity.Models;
using PreOrclFrontEnd.Models;
using PreOrclFrontEnd.Helpers;

namespace PreOrclFrontEnd.Controllers
{
    public class SisPerPersonasController : Controller
    {
        private readonly PreOrclFrontEndContext _context;
        GenericREST generic;

        public SisPerPersonasController(PreOrclFrontEndContext context)
        {
            _context = context;
            generic = new GenericREST();
        }

        // GET: SisPerPersonas
        public IActionResult Index()
        {
          
            List<SisPerPersona> listaSisPersona = generic.GetAll<SisPerPersona>("SisPerPersonas");
            return View(listaSisPersona);
        }

        // GET: SisPerPersonas/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sisPerPersona = generic.Get<SisPerPersona>("SisPerPersonas/", id);
            if (sisPerPersona == null)
            {
                return NotFound();
            }

            return View(sisPerPersona);
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
        public  IActionResult Create([Bind("per_IDPER,per_nombre_razon,per_apellido_comercial,per_nit,per_dui_nrc,per_direccion_departamento,per_direccion_municipio,per_direccion,per_telefono,per_movil,per_email,per_codigo,per_nacionalidad,per_tipo_contribullente,per_dir_cli,per_cobros")] SisPerPersona sisPerPersona)
        {
            if (ModelState.IsValid)
            {
                sisPerPersona = generic.Post("SisPerPersonas",sisPerPersona);
                return RedirectToAction(nameof(Index));
            }
            return View(sisPerPersona);
        }

        // GET: SisPerPersonas/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sisPerPersona = generic.Get<SisPerPersona>("SisPerPersonas/",id);
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
        public IActionResult Edit(int id, [Bind("per_IDPER,per_nombre_razon,per_apellido_comercial,per_nit,per_dui_nrc,per_direccion_departamento,per_direccion_municipio,per_direccion,per_telefono,per_movil,per_email,per_codigo,per_nacionalidad,per_tipo_contribullente,per_dir_cli,per_cobros")] SisPerPersona sisPerPersona)
        {
            if (id != sisPerPersona.per_IDPER)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                  bool a =  generic.Put<SisPerPersona>("SisPerPersonas/",id,sisPerPersona);
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

        // GET: SisPerPersonas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sisPerPersona = await _context.SisPerPersona
                .FirstOrDefaultAsync(m => m.per_IDPER == id);
            if (sisPerPersona == null)
            {
                return NotFound();
            }

            return View(sisPerPersona);
        }

        // POST: SisPerPersonas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var sisPerPersona = await _context.SisPerPersona.FindAsync(id);
            _context.SisPerPersona.Remove(sisPerPersona);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SisPerPersonaExists(int id)
        {
            return _context.SisPerPersona.Any(e => e.per_IDPER == id);
        }
    }
}
