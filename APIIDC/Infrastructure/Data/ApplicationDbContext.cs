using APIIDC.Domain;
using Microsoft.EntityFrameworkCore;

namespace APIIDC.Infrastructure.Data
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options) { }

        public DbSet<Doctrina> Doctrina {  get; set; }
        public DbSet<DetalleDoctrina> DetalleDoctrina {  get; set; }
        public DbSet<QuienesSomos> QuienesSomos {  get; set; }
        public DbSet<DetalleQuienesSomos> DetalleQuienesSomos {  get; set; }
        public DbSet<Congregacion> Congregacion {  get; set; }
        public DbSet<Lenin> Lenin {  get; set; }
        public DbSet<DetalleLenin> DetalleLenin {  get; set; }
        public DbSet<Griego> Griego {  get; set; }
        public DbSet<DetalleGriego> DetalleGriego {  get; set; }
        public DbSet<Devocional> Devocional {  get; set; }
        public DbSet<DetalleDevocional> DetalleDevocional {  get; set; }
        public DbSet<Corto> Corto {  get; set; }
        public DbSet<DetalleCorto> DetalleCorto {  get; set; }
        public DbSet<DetalleAlabanza> DetalleAlabanza {  get; set; }
        public DbSet<Alabanza> Alabanza {  get; set; }
        public DbSet<ImagenIdc> ImagenIdc {  get; set; }
        public DbSet<Directorio> Directorio {  get; set; }
        public DbSet<DetalleDirectorio> DetalleDirectorio {  get; set; }
    }
}
