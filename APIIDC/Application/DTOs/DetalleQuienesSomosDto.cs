using APIIDC.Domain;

namespace APIIDC.Application.DTOs
{
    public class DetalleQuienesSomosDto
    {
        public int IdDetalleQuienesSomos { get; set; }
        public int IdQuienesSomos { get; set; }
        public string? Titulo { get; set; }
        public string? Descripcion { get; set; }
        public string? UrlImagen { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public DateTime? FechaEdicion { get; set; }
        public string? UrlVideoYoutube { get; set; }
    }
}
