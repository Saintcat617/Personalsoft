using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace PolizasNET6.Models
{
    public class DatosPoliza
    {
        [BsonId]
        public ObjectId Id { get; set; }
        public string NombreCliente { get; set; }
        public string IdentificacionCliente { get; set; }
        public string? FechaNacimientoCliente { get; set; }
        public string? CiudadResidenciaCliente { get; set; }
        public string? DireccionResidencia { get; set; }
        public string NomplePlanPoliza { get; set; }
        public DateTime FechaInicioPoliza { get; set; }
        public int añosDeCoberturaPagados { get; set; }
        public DateTime FechaVigenciaPoliza { get; set; }
        public string CoberturasPoliza { get; set; }
        public int ValorMaximoCubiertoPoliza { get; set; }
        public string PlacaAutomotor { get; set; }
        public string ModeloAutomotor { get; set; }
        public string TieneInspeccion { get; set; }
    }
}
