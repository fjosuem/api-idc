using APIIDC.Application.DTOs;
using APIIDC.Infrastructure.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APIIDC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DirectorioController : ControllerBase
    {
        private readonly ILogger<DirectorioController> _logger;
        private readonly ApplicationDbContext _context;

        public DirectorioController(ILogger<DirectorioController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet(Name = "Directorio")]
        public async Task<IEnumerable<Domain.Directorio>> Get()
        {
            var asd = _logger;
            return await _context.Directorio.Where(x => x.IdCongregacion == 1).ToListAsync();
        }

        [HttpGet("pag/{pageIndex}/{pageSize}", Name = "DirectorioPagination")]
        public async Task<IEnumerable<Application.DTOs.DirectorioDto>> Get(int pageIndex = 1, int pageSize = 12)
        {
            // Ejecutar una sola consulta para obtener totalItems y urlImagenDoctrina
            var congregacionInfo = await _context.Congregacion
                .Where(x => x.IdCongregacion == 1)
                .Select(x => new
                {
                    TotalItems = _context.Directorio.Count(d => d.IdCongregacion == 1),
                    UrlImagenDirectorio = x.UrlImagenDirectorio
                })
                .FirstOrDefaultAsync();

            if (congregacionInfo == null)
            {
                // Manejo en caso de que no existan datos para la congregación especificada
                return new List<DirectorioDto>();
            }

            // Usar los datos obtenidos para poblar los DTOs
            var directorioItems = await _context.Directorio
                .Where(x => x.IdCongregacion == 1)
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .Select(x => new DirectorioDto
                {
                    IdDirectorio = x.IdDirectorio,
                    IdCongregacion = x.IdCongregacion,
                    Titulo = x.Titulo,
                    Descripcion = x.Descripcion,
                    UrlImagen = x.UrlImagen,
                    FechaCreacion = x.FechaCreacion,
                    FechaEdicion = x.FechaEdicion,
                    TotalPaginas = congregacionInfo.TotalItems / pageSize,
                    UrlImagenDirectorio = congregacionInfo.UrlImagenDirectorio
                })
                .ToListAsync();

            return directorioItems;

        }

        [HttpGet("{id}", Name = "DetalleDirectorio")]
        public async Task<IEnumerable<Application.DTOs.DetalleDirectorioDto>> Get(int id)
        {
            var asd = _logger;
            return await (
                        from detalle in _context.DetalleDirectorio
                        join directorio in _context.Directorio on detalle.IdDirectorio equals directorio.IdDirectorio
                        where detalle.IdDirectorio == id
                        select new DetalleDirectorioDto
                        {
                            IdDetalleDirectorio = detalle.IdDetalleDirectorio,
                            IdDirectorio = detalle.IdDirectorio,
                            Titulo = directorio.Titulo,
                            Descripcion = detalle.Descripcion,
                            UrlImagen = detalle.UrlImagen,
                            Nombre = detalle.Titulo
                        }
                    ).ToListAsync();
        }
    }
}
