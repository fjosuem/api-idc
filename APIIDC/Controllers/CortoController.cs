using APIIDC.Application.DTOs;
using APIIDC.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APIIDC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CortoController : ControllerBase
    {
        private readonly ILogger<CortoController> _logger;
        private readonly ApplicationDbContext _context;

        public CortoController(ILogger<CortoController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet(Name = "Corto")]
        public async Task<IEnumerable<Domain.Corto>> Get()
        {
            var asd = _logger;
            return await _context.Corto.Where(x => x.IdCongregacion == 1).ToListAsync();
        }

        [HttpGet("pag/{pageIndex}/{pageSize}", Name = "CortoPagination")]
        public async Task<IEnumerable<Application.DTOs.CortoDto>> Get(int pageIndex = 1, int pageSize = 12)
        {
            // Realizar una sola llamada para obtener el conteo y los datos en una operación estructurada
            var cortosQuery = _context.Corto
                .Where(x => x.IdCongregacion == 1)
                .OrderByDescending(c => c.IdCorto);

            // Obtener el total de ítems
            int totalItems = await cortosQuery.CountAsync();

            // Continuar con la obtención de datos paginados
            var cortos = await cortosQuery
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .Select(x => new CortoDto
                {
                    IdCorto = x.IdCorto,
                    IdCongregacion = x.IdCongregacion,
                    Titulo = x.Titulo,
                    Descripcion = x.Descripcion,
                    UrlImagen = x.UrlImagen,
                    FechaCreacion = x.FechaCreacion,
                    FechaEdicion = x.FechaEdicion,
                    UrlVideoYoutube = x.UrlVideoYoutube,
                    TotalPaginas = totalItems / pageSize, // Asegura el cálculo correcto del redondeo hacia arriba
                    UrlImagenCorto = string.Empty
                })
                .ToListAsync();

            return cortos;

        }

        [HttpGet("{id}", Name = "DetalleCorto")]
        public async Task<IEnumerable<Application.DTOs.DetalleCortoDto>> Get(int id)
        {
            var asd = _logger;
            return await (
                        from detalle in _context.DetalleCorto
                        join griego in _context.Corto on detalle.IdCorto equals griego.IdCorto
                        where detalle.IdCorto == id
                        select new DetalleCortoDto
                        {
                            IdDetalleCorto = detalle.IdDetalleCorto,
                            IdCorto = detalle.IdCorto,
                            Titulo = griego.Titulo,
                            Descripcion = detalle.Descripcion,
                            UrlImagen = detalle.UrlImagen,
                            UrlVideoYoutube = griego.UrlVideoYoutube ?? string.Empty
                        }
                    ).ToListAsync();
        }
    }
}
