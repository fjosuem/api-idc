using System.ComponentModel.DataAnnotations;

namespace APIIDC.Domain
{
    public class DetalleQuienesSomos
    {
        [Key]
        public int IdDetalleQuienesSomos { get; set; }
        public int IdQuienesSomos { get; set; }
        public string? Titulo { get; set; }
        public string? Descripcion { get; set; }
        public string? UrlImagen { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public DateTime? FechaEdicion { get; set; }

        public virtual QuienesSomos? QuienesSomos { get; set; }
    }
}
