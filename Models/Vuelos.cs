using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

public class Vuelo
{
    [BsonId, BsonRepresentation(BsonType.ObjectId)]

    public string? Id { get; set; }

    [BsonElement("pais_origen")]
    public string PaisOrigen { get; set; } = string.Empty;

    [BsonElement("ciudad_origen")]
    public string CiudadOrigen { get; set; } = string.Empty;

    [BsonElement("aeropuerto_origen")]
    public string AeropuertoOrigen { get; set; } = string.Empty;

    [BsonElement("pais_destino")]
    public string PaisDestino { get; set; } = string.Empty;

    [BsonElement("ciudad_destino")]
    public string CiudadDestino { get; set; } = string.Empty;

    [BsonElement("aeropuerto_destino")]
    public string AeropuertoDestino { get; set; } = string.Empty;

    [BsonElement("tipo_avion")]
    public string TipoAvion { get; set; } = string.Empty;

    [BsonElement("cupo_avion")]
    public int CupoAvion { get; set; }

    [BsonElement("pasajeros_actuales")]
    public int PasajerosActuales { get; set; }

    [BsonElement("nombre_piloto")]
    public string NombrePiloto { get; set; } = string.Empty;

    [BsonElement("estatus_vuelo")]
    public string EstatusVuelo { get; set; } = string.Empty;

    [BsonElement("fecha_hora_salida")]
    public DateTime FechaHoraSalida { get; set; }

    [BsonElement("fecha_hora_llegada_aproximada")]
    public DateTime FechaHoraLlegadaAproximada { get; set; }
}