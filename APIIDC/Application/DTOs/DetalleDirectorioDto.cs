﻿namespace APIIDC.Application.DTOs
{
    public class DetalleDirectorioDto
    {
        public int IdDetalleDirectorio { get; set; }
        public int IdDirectorio { get; set; }
        public string? Titulo { get; set; }
        public string? Descripcion { get; set; }
        public string? Nombre { get; set; }
        public string? UrlImagen { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public DateTime? FechaEdicion { get; set; }
    }
}