namespace APIIDC.Application.DTOs
{
    public class ImagenIdcDto
    {
        public int IdImagenIdc { get; set; }
        public int IdCategoriaImag { get; set; }
        public string? Nombre { get; set; }
        public string? Url { get; set; }
        public bool? Estado { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public DateTime? FechaEdicion { get; set; }
        public int TotalPaginas { get; set; }
        public string? UrlImagenImagenIdc { get; set; }
    }
}
