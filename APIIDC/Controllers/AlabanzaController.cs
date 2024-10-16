using APIIDC.Application.DTOs;
using APIIDC.Infrastructure.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APIIDC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlabanzaController : ControllerBase
    {
        private readonly ILogger<AlabanzaController> _logger;
        private readonly ApplicationDbContext _context;

        public AlabanzaController(ILogger<AlabanzaController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet(Name = "Alabanza")]
        public async Task<IEnumerable<Domain.Alabanza>> Get()
        {
            var asd = _logger;
            return await _context.Alabanza.Where(x => x.IdCongregacion == 1).ToListAsync();
        }

        [HttpGet("pag/{pageIndex}/{pageSize}", Name = "AlabanzaPagination")]
        public async Task<IEnumerable<Application.DTOs.AlabanzaDto>> Get(int pageIndex = 1, int pageSize = 12)
        {
            var totalItems = await _context.Alabanza
                               .Where(x => x.IdCongregacion == 1)
                               .CountAsync();

            return await _context.Alabanza
                        .Where(x => x.IdCongregacion == 1)
                        .Select(x => new AlabanzaDto
                        {
                            IdAlabanza = x.IdAlabanza,
                            IdCongregacion = x.IdCongregacion,
                            Titulo = x.Titulo,
                            Descripcion = x.Descripcion,
                            UrlImagen = x.UrlImagen,
                            FechaCreacion = x.FechaCreacion,
                            FechaEdicion = x.FechaEdicion,
                            UrlVideoYoutube = x.UrlVideoYoutube,
                            TotalPaginas = totalItems / pageSize,
                            UrlImagenAlabanza = string.Empty
                        })
                        .OrderByDescending(c => c.IdAlabanza)
                        .Skip((pageIndex - 1) * pageSize)
                        .Take(pageSize)
                        .ToListAsync();
        }

        [HttpGet("{id}", Name = "DetalleAlabanza")]
        public async Task<IEnumerable<Application.DTOs.DetalleAlabanzaDto>> Get(int id)
        {
            var asd = _logger;
            return await (
                        from detalle in _context.DetalleAlabanza
                        join griego in _context.Alabanza on detalle.IdAlabanza equals griego.IdAlabanza
                        where detalle.IdAlabanza == id
                        select new DetalleAlabanzaDto
                        {
                            IdDetalleAlabanza = detalle.IdDetalleAlabanza,
                            IdAlabanza = detalle.IdAlabanza,
                            Titulo = griego.Titulo,
                            Descripcion = detalle.Descripcion,
                            UrlImagen = detalle.UrlImagen,
                            UrlVideoYoutube = griego.UrlVideoYoutube ?? string.Empty
                        }
                    ).ToListAsync();
        }
    }
}
