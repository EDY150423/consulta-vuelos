using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

[Route("api/vuelos")]
public class VuelosController : ControllerBase
{
    [HttpGet("ciudades-origen")]
    public IActionResult CiudadesOrigen(){
        var client = new MongoClient(CadenaConexion.MONGO_DB);
        var db = client.GetDatabase("Aeropuerto");
        var collection = db.GetCollection<Vuelo>("Vuelos");

        var lista = collection.Distinct<string>("CiudadOrigen", FilterDefinition<Vuelo>.Empty).ToList();

        return Ok(lista);
    }

     [HttpGet("ciudades-destino")]
    public IActionResult CiudadesDestino(){
        var client = new MongoClient(CadenaConexion.MONGO_DB);
        var db = client.GetDatabase("Aeropuerto");
        var collection = db.GetCollection<Vuelo>("Vuelos");

        var lista = collection.Distinct<string>("CiudadDestino", FilterDefinition<Vuelo>.Empty).ToList();

        return Ok(lista);
       
    }

     [HttpGet("estatus")]
    public IActionResult ListarEstatus(){
        var client = new MongoClient(CadenaConexion.MONGO_DB);
        var db = client.GetDatabase("Aeropuerto");
        var collection = db.GetCollection<Vuelo>("Vuelos");

        var lista = collection.Distinct<string>("EstatusVuelo", FilterDefinition<Vuelo>.Empty).ToList();

        return Ok(lista);
    }

     [HttpGet("listar-vuelos")]
    public IActionResult ListarVuelos(string? estatus, string? origen, string? destino,
        string? fechaInicial, string? fechaFinal){
        var client = new MongoClient(CadenaConexion.MONGO_DB);
        var db = client.GetDatabase("Aeropuerto");
        var collection = db.GetCollection<Vuelo>("Vuelos");

        List<FilterDefinition<Vuelo>> filters = new List<FilterDefinition<Vuelo>>();

        if(!string.IsNullOrWhiteSpace(estatus)){
            var filterEstatus = Builders<Vuelo>.Filter.Eq(x => x.EstatusVuelo, estatus);
            filters.Add(filterEstatus);
        }

        if(!string.IsNullOrWhiteSpace(origen)){
            var filterOrigen = Builders<Vuelo>.Filter.Eq(x => x.CiudadOrigen, origen);
            filters.Add(filterOrigen);
        }
        
        if(!string.IsNullOrWhiteSpace(destino)){
            var filterDestino = Builders<Vuelo>.Filter.Eq(x => x.CiudadDestino, origen);
            filters.Add(filterDestino);
        }

        if(!string.IsNullOrWhiteSpace(fechaInicial)){
            if(DateTime.TryParse(fechaInicial, out DateTime fecha)){
                var filtroFechaIni = Builders<Vuelo>.Filter.Gte(x => x.FechaHoraSalida, fecha);
                filters.Add(filtroFechaIni);
            }
        }

        if(!string.IsNullOrWhiteSpace(fechaFinal)){
            if(DateTime.TryParse(fechaFinal, out DateTime fecha)){
                var filtroFechaFin = Builders<Vuelo>.Filter.Lte(x => x.FechaHoraSalida,
                new DateTime(fecha.Year, fecha.Month, fecha.Day, 23, 59, 59));
                filters.Add(filtroFechaFin);
            }
        }
        
        List<Vuelo> vuelos;
        if(filters.Count > 0) {
            var filter = Builders<Vuelo>.Filter.And(filters);
            vuelos = collection.Find(FilterDefinition<Vuelo>.Empty).ToList();
        }
        else {
            vuelos = collection.Find(FilterDefinition<Vuelo>.Empty).ToList();
        }

        return Ok(vuelos);
    }
}