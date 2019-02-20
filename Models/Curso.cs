using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ASPNETCore.Models
{
    public class Curso : ObjetoEscuelaBase
    {
        [Required(ErrorMessage ="El nombre del curso es requerido")]
        [StringLength(5)]
        public override string Nombre { get; set; }
        public TiposJornada Jornada { get; set; }
        public List<Asignatura> Asignaturas { get; set; }
        public List<Alumno> Alumnos { get; set; }

        [Display(Prompt = "Dirección correspondencia", Name = "Address")]
        [Required(ErrorMessage = "El campo dirección no puede estar vacio")]
        [MinLength(10, ErrorMessage = "La longitud minima de la dirección es de 10 caracteres")]
        public string Dirección { get; set; }
        public string EscuelaId { get; set; }
        public Escuela Escuela { get; set; }
  }
}