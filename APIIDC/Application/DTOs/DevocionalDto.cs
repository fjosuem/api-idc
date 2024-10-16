namespace APIIDC.Application.DTOs
{
    public class DevocionalDto
    {
        public int IdDevocional { get; set; }
        public int? IdCongregacion { get; set; }
        public string? Titulo { get; set; }
        public string? Descripcion { get; set; }
        public string? UrlImagen { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public DateTime? FechaEdicion { get; set; }
        public string? UrlVideoYoutube { get; set; }
        public int TotalPaginas { get; set; }
        public string? UrlImagenDevocional { get; set; }
    }
}
