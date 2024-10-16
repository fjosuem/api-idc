using APIIDC.Application.DTOs;
using APIIDC.Infrastructure.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APIIDC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LeninController : ControllerBase
    {
        private readonly ILogger<LeninController> _logger;
        private readonly ApplicationDbContext _context;

        public LeninController(ILogger<LeninController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet(Name = "Lenin")]
        public async Task<IEnumerable<Domain.Lenin>> Get()
        {
            var asd = _logger;
            return await _context.Lenin.Where(x => x.IdCongregacion == 1).ToListAsync();
        }

        [HttpGet("pag/{pageIndex}/{pageSize}", Name = "LeninPagination")]
        public async Task<IEnumerable<Application.DTOs.LeninDto>> Get(int pageIndex = 1, int pageSize = 12)
        {
            // Prepare the base query for Lenin data that matches the specified conditions
            var leninQuery = _context.Lenin
                .Where(x => x.IdCongregacion == 1)
                .OrderByDescending(c => c.IdLenin);

            // Execute the count query to find the total number of items
            int totalItems = await leninQuery.CountAsync();

            // Now, perform the paginated data fetching using the same base query
            var leninItems = await leninQuery
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .Select(x => new LeninDto
                {
                    IdLenin = x.IdLenin,
                    IdCongregacion = x.IdCongregacion,
                    Titulo = x.Titulo,
                    Descripcion = x.Descripcion,
                    UrlImagen = x.UrlImagen,
                    FechaCreacion = x.FechaCreacion,
                    FechaEdicion = x.FechaEdicion,
                    UrlVideoYoutube = x.UrlVideoYoutube,
                    TotalPaginas = totalItems / pageSize, // Correctly compute total pages
                    UrlImagenLenin = string.Empty  // Assuming URL is not available and marked as empty
                })
                .ToListAsync();

            return leninItems;

        }

        [HttpGet("{id}", Name = "DetalleLenin")]
        public async Task<IEnumerable<Application.DTOs.DetalleLeninDto>> Get(int id)
        {
            var asd = _logger;
            return await (
                        from detalle in _context.DetalleLenin
                        join lenin in _context.Lenin on detalle.IdLenin equals lenin.IdLenin
                        where detalle.IdLenin == id
                        select new DetalleLeninDto
                        {
                            IdDetalleLenin = detalle.IdDetalleLenin,
                            IdLenin = detalle.IdLenin,
                            Titulo = lenin.Titulo,
                            Descripcion = detalle.Descripcion,
                            UrlImagen = detalle.UrlImagen,
                            UrlVideoYoutube = lenin.UrlVideoYoutube ?? string.Empty
                        }
                    ).ToListAsync();
        }
    }
}
