using APIIDC.Application.DTOs;
using APIIDC.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APIIDC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuienesSomosController : ControllerBase
    {
        private readonly ILogger<QuienesSomosController> _logger;
        private readonly ApplicationDbContext _context;

        public QuienesSomosController(ILogger<QuienesSomosController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet(Name = "QuienesSomos")]
        public async Task<IEnumerable<Domain.QuienesSomos>> Get()
        {
            var asd = _logger;
            return await _context.QuienesSomos.Where(x => x.IdCongregacion == 1).ToListAsync();
        }

        [HttpGet("pag/{pageIndex}/{pageSize}", Name = "QuienesSomosPagination")]
        public async Task<IEnumerable<Application.DTOs.QuienesSomosDto>> Get(int pageIndex = 1, int pageSize = 12)
        {
            // Ejecutar una sola consulta para obtener totalItems y urlImagenQuienesSomos
            var congregacionInfo = await _context.Congregacion
                .Where(x => x.IdCongregacion == 1)
                .Select(x => new
                {
                    TotalItems = _context.QuienesSomos.Count(q => q.IdCongregacion == 1),
                    UrlImagenQuienesSomos = x.UrlImagenQuienesSomos
                })
                .FirstOrDefaultAsync();

            if (congregacionInfo == null)
            {
                // Manejo en caso de que no existan datos para la congregación especificada
                return new List<QuienesSomosDto>();
            }

            // Usar los datos obtenidos para poblar los DTOs
            var quienesSomosPaginados = await _context.QuienesSomos
                .Where(x => x.IdCongregacion == 1)
                .OrderBy(c => c.IdQuienesSomos) // Asegúrate de que el orden es el deseado (ascendente en este caso)
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .Select(x => new QuienesSomosDto
                {
                    IdQuienesSomos = x.IdQuienesSomos,
                    IdCongregacion = x.IdCongregacion,
                    Titulo = x.Titulo,
                    Descripcion = x.Descripcion,
                    UrlImagen = x.UrlImagen,
                    FechaCreacion = x.FechaCreacion,
                    FechaEdicion = x.FechaEdicion,
                    UrlVideoYoutube = x.UrlVideoYoutube,
                    TotalPaginas = congregacionInfo.TotalItems / pageSize, // Calcula el total de páginas
                    UrlImagenQuienesSomos = congregacionInfo.UrlImagenQuienesSomos
                })
                .ToListAsync();

            return quienesSomosPaginados;
        }

        [HttpGet("{id}", Name = "DetalleQuienesSomos")]
        public async Task<IEnumerable<Application.DTOs.DetalleQuienesSomosDto>> Get(int id)
        {
            var asd = _logger;
            return await (
                        from detalle in _context.DetalleQuienesSomos
                        join quienesSomos in _context.QuienesSomos on detalle.IdQuienesSomos equals quienesSomos.IdQuienesSomos
                        where detalle.IdQuienesSomos == id
                        select new DetalleQuienesSomosDto
                        {
                            IdDetalleQuienesSomos = detalle.IdDetalleQuienesSomos,
                            IdQuienesSomos = detalle.IdQuienesSomos,
                            Titulo = quienesSomos.Titulo,
                            Descripcion = detalle.Descripcion,
                            UrlImagen = detalle.UrlImagen,
                            UrlVideoYoutube = quienesSomos.UrlVideoYoutube ?? string.Empty
                        }
                    ).ToListAsync();
        }
    }
}
