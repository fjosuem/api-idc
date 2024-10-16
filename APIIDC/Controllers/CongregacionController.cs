using APIIDC.Application.DTOs;
using APIIDC.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APIIDC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CongregacionController : ControllerBase
    {
        private readonly ILogger<CongregacionController> _logger;
        private readonly ApplicationDbContext _context;

        public CongregacionController(ILogger<CongregacionController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet(Name = "Congregacion")]
        public async Task<IEnumerable<Application.DTOs.CongregacionDto>> Get()
        {
            return await _context.Congregacion
                    .AsNoTracking()
                    .Where(x => x.IdCongregacion == 1)
                    .Select(x => new CongregacionDto
                    {
                        IdCongregacion = x.IdCongregacion,
                        Nombre = x.Nombre,
                        UrlImagenUno = x.UrlImagenUno,
                        UrlImagenDos = x.UrlImagenDos,
                        UrlImagenTres = x.UrlImagenTres,
                        UrlImagenCuatro = x.UrlImagenCuatro,
                        UrlImagenOferta = x.UrlImagenOferta,
                        Facebook = x.Facebook,
                        Twitter = x.Twitter,
                        UrlImagenDoctrina = x.UrlImagenDoctrina,
                        UrlImagenQuienesSomos = x.UrlImagenQuienesSomos,
                        UrlImagenCantosEspirituales = x.UrlImagenCantosEspirituales,
                        UrlImagenOraciones = x.UrlImagenOraciones,
                        UrlImagenPreguntasFrecuentes = x.UrlImagenPreguntasFrecuentes,
                        UrlImagenDirectorio = x.UrlImagenDirectorio,
                        UrlImagenEnlaces = x.UrlImagenEnlaces,
                        UrlImagenCitasBiblicas = x.UrlImagenCitasBiblicas,
                        TextoAlFinal = x.TextoAlFinal,
                        FechaCreacion = x.FechaCreacion,
                        FechaEdicion = x.FechaEdicion
                    })
                    .ToListAsync();

        }
    }
}
