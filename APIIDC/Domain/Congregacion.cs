using System.ComponentModel.DataAnnotations;

namespace APIIDC.Domain
{
    public class Congregacion
    {
        [Key]
        public int IdCongregacion { get; set; }
        public string? Nombre { get; set; }
        public string? UrlImagenUno { get; set; }
        public string? UrlImagenDos { get; set; }
        public string? UrlImagenTres { get; set; }
        public string? UrlImagenCuatro { get; set; }
        public string? UrlImagenOferta { get; set; }
        public string? Facebook { get; set; }
        public string? Twitter { get; set; }
        public string? UrlImagenDoctrina { get; set; }
        public string? UrlImagenQuienesSomos { get; set; }
        public string? UrlImagenCantosEspirituales { get; set; }
        public string? UrlImagenOraciones { get; set; }
        public string? UrlImagenPreguntasFrecuentes { get; set; }
        public string? UrlImagenDirectorio { get; set; }
        public string? UrlImagenEnlaces { get; set; }
        public string? UrlImagenCitasBiblicas { get; set; }
        public string? TextoAlFinal { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public DateTime? FechaEdicion { get; set; }
    }
}
