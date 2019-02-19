using System;
using System.Linq;
using System.Collections.Generic;
using ASPNETCore.Models;
using Microsoft.AspNetCore.Mvc;

namespace ASPNETCore.Controllers
{
    public class CursoController : Controller
    { 
        [Route("Curso/EditarCurso")]
        [Route("Curso/EditarCurso/{idCurso}")]
        public IActionResult EditarCurso(string idCurso)
        {
            var curso = from cur in _context.Cursos
                            where cur.Id == idCurso
                            select cur;
            if (string.IsNullOrWhiteSpace(idCurso)) return MultiCurso();
                return View(curso.SingleOrDefault());
        }

        [Route("Curso/EditarCurso/{idCurso}")]
        [HttpPost]
        public IActionResult EditarDato(string idCurso, Curso cursoForm)
        {
            var curso = _context.Cursos.Find(idCurso);

            if (!ModelState.IsValid) return View("EditarCurso", curso);

            var modelCurso = _context.Cursos.SingleOrDefault(c => c.Id == idCurso);

            if(modelCurso == null) return MultiCurso();

            modelCurso.Nombre = cursoForm.Nombre;
            modelCurso.Dirección = cursoForm.Dirección;
            modelCurso.Jornada = cursoForm.Jornada;

            _context.SaveChanges();

            ViewBag.MensajeExtra = "Curso Actualizado";

            return View("Index", curso);
        }

        [Route("Curso/BorrarCurso")]
        [Route("Curso/BorrarCurso/{idCurso}")]
        public IActionResult BorrarCurso(string idCurso)
        {
            var curso = from cur in _context.Cursos
                            where cur.Id == idCurso
                            select cur;
            if (!string.IsNullOrWhiteSpace(idCurso)) 
            {
                _context.Cursos.RemoveRange(_context.Cursos.Where(p => p.Id == idCurso));
                _context.SaveChanges();
                ViewBag.MensajeExtra = "Se elimino el dato exitosamente";
                return MultiCurso();
            }
            else
            {
                return View(curso.SingleOrDefault());
            }
        }
        public IActionResult Index(string id)
        {
            if (!string.IsNullOrWhiteSpace(id))
            {
                var curso = from cur in _context.Cursos
                            where cur.Id == id
                            select cur;
                return View(curso.SingleOrDefault());
            }
            else
            {
                return View("MultiCurso", _context.Cursos);
            }
            
        }
        public IActionResult MultiCurso()
        {
            ViewBag.CosaDinamica = "La Monja";
            ViewBag.Fecha = DateTime.Now;
            return View("MultiCurso", _context.Cursos);
        }
        public IActionResult Create()
        {
            ViewBag.Fecha = DateTime.Now;
            return View();
        }

        [HttpPost]
        public IActionResult Create(Curso curso)
        {
            ViewBag.Fecha = DateTime.Now;
            if (ModelState.IsValid)
            {
                var escuela = _context.Escuelas.FirstOrDefault();

                curso.EscuelaId = escuela.Id;
                _context.Cursos.Add(curso);
                _context.SaveChanges();
                ViewBag.MensajeExtra = "Curso Creado";
                return View("Index", curso);
            }
            else
            {
                return View(curso);
            }
        }
        
        private EscuelaContext _context;
        public CursoController(EscuelaContext context)
        {
            _context = context;
        }
    }
}