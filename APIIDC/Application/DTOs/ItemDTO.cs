namespace APIIDC.Application.DTOs
{
    public class ItemDTO
    {
        public int Id { get; set; }
        public int IdCongregacion { get; set; }
        public string? Titulo { get; set; }
        public string? Descripcion { get; set; }
        public string? UrlImagen { get; set; }
        public int TotalPaginas { get; set; }

        public string GenerateSlug() => $"{Id}-{Titulo}".Replace(" ", "-").ToLower();
    }
}
