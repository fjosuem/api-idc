using APIIDC.Application.DTOs;
using APIIDC.Infrastructure.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APIIDC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagenIdcController : ControllerBase
    {
        private readonly ILogger<ImagenIdcController> _logger;
        private readonly ApplicationDbContext _context;

        public ImagenIdcController(ILogger<ImagenIdcController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet(Name = "ImagenIdc")]
        public async Task<IEnumerable<Domain.ImagenIdc>> Get()
        {
            var asd = _logger;
            return await _context.ImagenIdc.ToListAsync();
        }

        [HttpGet("pag/{pageIndex}/{pageSize}", Name = "ImagenIdcPagination")]
        public async Task<IEnumerable<Application.DTOs.ImagenIdcDto>> Get(int pageIndex = 1, int pageSize = 12)
        {
            var query = _context.ImagenIdc
                        .Where(c => c.Estado == true)
                        .OrderByDescending(c => c.IdImagenIdc);

            // Obtenemos el total de registros (puede ser necesario ejecutar una consulta aquí dependiendo de la ORM)
            var totalItems = await query.CountAsync();

            // Obtenemos los items de la página actual
            var imagenesPaginadas = await query
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .Select(x => new ImagenIdcDto
                {
                    IdImagenIdc = x.IdImagenIdc,
                    IdCategoriaImag = x.IdCategoriaImag,
                    Nombre = x.Nombre,
                    Url = x.Url,
                    Estado = x.Estado,
                    FechaCreacion = x.FechaCreacion,
                    FechaEdicion = x.FechaEdicion,
                    UrlImagenImagenIdc = string.Empty,
                    TotalPaginas = 0 // Temporalmente seteado a 0, se ajustará después
                })
                .ToListAsync();

            // Ajustar el TotalPaginas para todos los items en el resultado
            imagenesPaginadas.ForEach(i => i.TotalPaginas = totalItems / pageSize);

            return imagenesPaginadas;
        }
    }
}
