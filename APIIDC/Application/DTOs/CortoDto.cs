namespace APIIDC.Application.DTOs
{
    public class CortoDto
    {
        public int IdCorto { get; set; }
        public int IdCongregacion { get; set; }
        public string? Titulo { get; set; }
        public string? Descripcion { get; set; }
        public string? UrlImagen { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public DateTime? FechaEdicion { get; set; }
        public string? UrlVideoYoutube { get; set; }
        public int TotalPaginas { get; set; }
        public string? UrlImagenCorto { get; set; }
    }
}
