using APIIDC.Application.DTOs;
using APIIDC.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APIIDC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DevocionalController : ControllerBase
    {
        private readonly ILogger<DevocionalController> _logger;
        private readonly ApplicationDbContext _context;

        public DevocionalController(ILogger<DevocionalController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet(Name = "Devocional")]
        public async Task<IEnumerable<Domain.Devocional>> Get()
        {
            var asd = _logger;
            return await _context.Devocional.Where(x => x.IdCongregacion == 1).ToListAsync();
        }

        [HttpGet("pag/{pageIndex}/{pageSize}", Name = "DevocionalPagination")]
        public async Task<IEnumerable<Application.DTOs.DevocionalDto>> Get(int pageIndex = 1, int pageSize = 12)
        {
            // Preparar la consulta principal
            var devocionalesQuery = _context.Devocional
                .Where(x => x.IdCongregacion == 1)
                .OrderByDescending(c => c.IdDevocional);

            // Realizar una sola llamada para obtener el total de items
            int totalItems = await devocionalesQuery.CountAsync();

            // Ejecutar la consulta paginada y seleccionar los datos necesarios
            var devocionales = await devocionalesQuery
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .Select(x => new DevocionalDto
                {
                    IdDevocional = x.IdDevocional,
                    IdCongregacion = x.IdCongregacion,
                    Titulo = x.Titulo,
                    Descripcion = x.Descripcion,
                    UrlImagen = x.UrlImagen,
                    FechaCreacion = x.FechaCreacion,
                    FechaEdicion = x.FechaEdicion,
                    UrlVideoYoutube = x.UrlVideoYoutube,
                    TotalPaginas = totalItems / pageSize, // Redondeo hacia arriba para el total de páginas
                    UrlImagenDevocional = string.Empty
                })
                .ToListAsync();

            return devocionales;

        }

        [HttpGet("{id}", Name = "DetalleDevocional")]
        public async Task<IEnumerable<Application.DTOs.DetalleDevocionalDto>> Get(int id)
        {
            var asd = _logger;
            return await (
                        from detalle in _context.DetalleDevocional
                        join griego in _context.Devocional on detalle.IdDevocional equals griego.IdDevocional
                        where detalle.IdDevocional == id
                        select new DetalleDevocionalDto
                        {
                            IdDetalleDevocional = detalle.IdDetalleDevocional,
                            IdDevocional = detalle.IdDevocional,
                            Titulo = griego.Titulo,
                            Descripcion = detalle.Descripcion,
                            UrlImagen = detalle.UrlImagen,
                            UrlVideoYoutube = griego.UrlVideoYoutube ?? string.Empty
                        }
                    ).ToListAsync();
        }
    }
}
