using System.ComponentModel.DataAnnotations;

namespace APIIDC.Domain
{
    public class DetalleGriego
    {
        [Key]
        public int IdDetalleGriego { get; set; }
        public int? IdGriego { get; set; }
        public string? Titulo { get; set; }
        public string? Descripcion { get; set; }
        public string? UrlImagen { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public DateTime? FechaEdicion { get; set; }
    }
}
