using APIIDC.Application.DTOs;
using APIIDC.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APIIDC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctrinaController : ControllerBase
    {
        private readonly ILogger<DoctrinaController> _logger;
        private readonly ApplicationDbContext _context;

        public DoctrinaController(ILogger<DoctrinaController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet(Name = "Doctrina")]
        public async Task<IEnumerable<Domain.Doctrina>> Get()
        {
            var asd = _logger;
            return await _context.Doctrina.Where(x => x.IdCongregacion == 1).ToListAsync();
        }

        [HttpGet("pag/{pageIndex}/{pageSize}", Name = "DoctrinaPagination")]
        public async Task<IEnumerable<Application.DTOs.DoctrinaDto>> Get(int pageIndex = 1, int pageSize = 12)
        {
            // Ejecutar una sola consulta para obtener totalItems y urlImagenDoctrina
            var congregacionInfo = await _context.Congregacion
                .Where(x => x.IdCongregacion == 1)
                .Select(x => new
                {
                    TotalItems = _context.Doctrina.Count(d => d.IdCongregacion == 1),
                    UrlImagenDoctrina = x.UrlImagenDoctrina
                })
                .FirstOrDefaultAsync();

            if (congregacionInfo == null)
            {
                // Manejo en caso de que no existan datos para la congregación especificada
                return new List<DoctrinaDto>();
            }

            // Usar los datos obtenidos para poblar los DTOs
            var doctrinas = await _context.Doctrina
                .Where(x => x.IdCongregacion == 1)
                .OrderByDescending(c => c.IdDoctrina)
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .Select(x => new DoctrinaDto
                {
                    IdDoctrina = x.IdDoctrina,
                    IdCongregacion = x.IdCongregacion,
                    Titulo = x.Titulo,
                    Descripcion = x.Descripcion,
                    UrlImagen = x.UrlImagen,
                    FechaCreacion = x.FechaCreacion,
                    FechaEdicion = x.FechaEdicion,
                    UrlVideoYoutube = x.UrlVideoYoutube,
                    TotalPaginas = congregacionInfo.TotalItems / pageSize,
                    UrlImagenDoctrina = congregacionInfo.UrlImagenDoctrina
                })
                .ToListAsync();

            return doctrinas;
        }

        [HttpGet("{id}", Name = "DetalleDoctrina")]
        public async Task<IEnumerable<Application.DTOs.DetalleDoctrinaDto>> Get(int id)
        {
            return await (
                        from detalle in _context.DetalleDoctrina
                        join doctrina in _context.Doctrina on detalle.IdDoctrina equals doctrina.IdDoctrina
                        where detalle.IdDoctrina == id
                        select new DetalleDoctrinaDto
                        {
                            IdDetalleDoctrina = detalle.IdDetalleDoctrina,
                            IdDoctrina = detalle.IdDoctrina,
                            Titulo = doctrina.Titulo,
                            Descripcion = detalle.Descripcion,
                            UrlImagen = detalle.UrlImagen,
                            UrlVideoYoutube = doctrina.UrlVideoYoutube ?? string.Empty
                        }
                    ).ToListAsync();
        }
    }
}
