using APIIDC.Application.DTOs;
using APIIDC.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APIIDC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GriegoController : ControllerBase
    {
        private readonly ILogger<GriegoController> _logger;
        private readonly ApplicationDbContext _context;

        public GriegoController(ILogger<GriegoController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet(Name = "Griego")]
        public async Task<IEnumerable<Domain.Griego>> Get()
        {
            var asd = _logger;
            return await _context.Griego.Where(x => x.IdCongregacion == 1).ToListAsync();
        }

        [HttpGet("pag/{pageIndex}/{pageSize}", Name = "GriegoPagination")]
        public async Task<IEnumerable<Application.DTOs.GriegoDto>> Get(int pageIndex = 1, int pageSize = 12)
        {
            // Prepare the query that will be used for both counting and fetching data
            var griegoQuery = _context.Griego
                .Where(x => x.IdCongregacion == 1)
                .OrderBy(c => c.IdGriego);  // Note: Ensure the sorting is correct as per your requirements

            // First, get the total count of items that match the criteria
            int totalItems = await griegoQuery.CountAsync();

            // Then, execute the paginated query to fetch the actual data
            var griegos = await griegoQuery
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .Select(x => new GriegoDto
                {
                    IdGriego = x.IdGriego,
                    IdCongregacion = x.IdCongregacion,
                    Titulo = x.Titulo,
                    Descripcion = x.Descripcion,
                    UrlImagen = x.UrlImagen,
                    FechaCreacion = x.FechaCreacion,
                    FechaEdicion = x.FechaEdicion,
                    UrlVideoYoutube = x.UrlVideoYoutube,
                    TotalPaginas = totalItems / pageSize,  // Calculate total pages dynamically
                    UrlImagenGriego = string.Empty  // This is set to empty, adjust if needed
                })
                .ToListAsync();

            return griegos;

        }

        [HttpGet("{id}", Name = "DetalleGriego")]
        public async Task<IEnumerable<Application.DTOs.DetalleGriegoDto>> Get(int id)
        {
            var asd = _logger;
            return await (
                        from detalle in _context.DetalleGriego
                        join griego in _context.Griego on detalle.IdGriego equals griego.IdGriego
                        where detalle.IdGriego == id
                        select new DetalleGriegoDto
                        {
                            IdDetalleGriego = detalle.IdDetalleGriego,
                            IdGriego = detalle.IdGriego,
                            Titulo = griego.Titulo,
                            Descripcion = detalle.Descripcion,
                            UrlImagen = detalle.UrlImagen,
                            UrlVideoYoutube = griego.UrlVideoYoutube ?? string.Empty
                        }
                    ).ToListAsync();
        }
    }
}
