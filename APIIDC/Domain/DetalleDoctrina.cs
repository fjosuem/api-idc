using System.ComponentModel.DataAnnotations;

namespace APIIDC.Domain
{
    public class DetalleDoctrina
    {
        [Key]
        public int IdDetalleDoctrina { get; set; }
        public int IdDoctrina { get; set; }
        public string? Titulo { get; set; }
        public string? Descripcion { get; set; }
        public string? UrlImagen { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public DateTime? FechaEdicion { get; set; }
    }
}
