﻿using System.ComponentModel.DataAnnotations;

namespace APIIDC.Domain
{
    public class DetalleDirectorio
    {
        [Key]
        public int IdDetalleDirectorio { get; set; }
        public int IdDirectorio { get; set; }
        public string? Titulo { get; set; }
        public string? Descripcion { get; set; }
        public string? UrlImagen { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public DateTime? FechaEdicion { get; set; }
    }
}