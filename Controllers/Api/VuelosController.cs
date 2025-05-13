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
    public IActionResult ListarVuelos(string? estatus){
        var client = new MongoClient(CadenaConexion.MONGO_DB);
        var db = client.GetDatabase("Aeropuerto");
        var collection = db.GetCollection<Vuelo>("Vuelos");

        List<FilterDefinition<Vuelo>> filters = new List<FilterDefinition<Vuelo>>();

        if(!string.IsNullOrWhiteSpace(estatus)){
            var filterEstatus = Builders<Vuelo>.Filter.Eq(x => x.EstatusVuelo, estatus);
            filters.Add(filterEstatus);
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