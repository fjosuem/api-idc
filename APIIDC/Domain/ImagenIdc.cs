using System.ComponentModel.DataAnnotations;

namespace APIIDC.Domain
{
    public class ImagenIdc
    {
        [Key]
        public int IdImagenIdc { get; set; }
        public int IdCategoriaImag { get; set; }
        public string? Nombre { get; set; }
        public string? Url { get; set; }
        public bool? Estado { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public DateTime? FechaEdicion { get; set; }
    }
}
