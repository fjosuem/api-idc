using System.ComponentModel.DataAnnotations;

namespace APIIDC.Domain
{
    public class DetalleCorto
    {
        [Key]
        public int IdDetalleCorto { get; set; }
        public int IdCorto { get; set; }
        public string? Titulo { get; set; }
        public string? Descripcion { get; set; }
        public string? UrlImagen { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public DateTime? FechaEdicion { get; set; }
    }
}
