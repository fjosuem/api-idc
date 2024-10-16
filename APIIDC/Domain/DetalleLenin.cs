﻿using System.ComponentModel.DataAnnotations;

namespace APIIDC.Domain
{
    public class DetalleLenin
    {
        [Key]
        public int IdDetalleLenin { get; set; }
        public int? IdLenin { get; set; }
        public string? Titulo { get; set; }
        public string? Descripcion { get; set; }
        public string? UrlImagen { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public DateTime? FechaEdicion { get; set; }
    }
}
