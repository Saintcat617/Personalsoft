using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;
using PolizasNET6.Models;
using System.Numerics;

namespace PolizasNET6.Repositories
{
    public class PolicyCollection : IPolicyCollection
    {
        internal MongoDBRepository _repository = new MongoDBRepository();
        private IMongoCollection<DatosPoliza> Collection;

        //Main
        public PolicyCollection()
        {
            Collection = _repository.db.GetCollection<DatosPoliza>("Policy");
        }


        public async Task DeletePolicy(string id)
        {
            var filter = Builders<DatosPoliza>.Filter.Eq(s => s.Id, new ObjectId(id));
            await Collection.DeleteOneAsync(filter);
        }

        public async Task InsertPolicy(DatosPoliza datosPoliza)
        {
            await Collection.InsertOneAsync((datosPoliza));
        }

        public async Task UpdatePolicy(DatosPoliza datosPoliza)
        {
            var filter = Builders<DatosPoliza>
                .Filter
                .Eq(s => s.Id, datosPoliza.Id);

            await Collection.ReplaceOneAsync(filter, (datosPoliza));
        }

        public async Task<List<DatosPoliza>> GetAllPolicy()
        {
            return await Collection.FindAsync(new BsonDocument()).Result.ToListAsync();
        }

        public async Task<DatosPoliza> GetPolicyById(string id)
        {
            return await Collection.FindAsync(
                new BsonDocument { { "_id", new ObjectId(id) } }).Result.FirstAsync();
        }

        public async Task<List<DatosPoliza>> GetToDoItemAsync(string placa)
        {
            return await Collection.FindAsync(p => p.PlacaAutomotor == placa).Result.ToListAsync();
        }

    }
}
