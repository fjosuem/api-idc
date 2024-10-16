﻿using System.ComponentModel.DataAnnotations;

namespace APIIDC.Domain
{
    public class Griego
    {
        [Key]
        public int IdGriego { get; set; }
        public int? IdCongregacion { get; set; }
        public string? Titulo { get; set; }
        public string? Descripcion { get; set; }
        public string? UrlImagen { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public DateTime? FechaEdicion { get; set; }
        public string? UrlVideoYoutube { get; set; }
    }
}
