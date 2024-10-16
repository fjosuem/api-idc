namespace APIIDC.Application.DTOs
{
    public class AlabanzaDto
    {
        public int IdAlabanza { get; set; }
        public int? IdCongregacion { get; set; }
        public string? Titulo { get; set; }
        public string? Descripcion { get; set; }
        public string? UrlImagen { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public DateTime? FechaEdicion { get; set; }
        public string? UrlVideoYoutube { get; set; }
        public int TotalPaginas { get; set; }
        public string? UrlImagenAlabanza { get; set; }
    }
}
