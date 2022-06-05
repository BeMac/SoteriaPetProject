namespace SoteriaPet.Services
{
    using System.Threading.Tasks;
    using Models;
    using Microsoft.Azure.Cosmos;

    public class CosmosService : ICosmosDbService
    {
        private Container _cosmosContainer;

        public CosmosService(CosmosClient dbClient, string databaseName, string containerName)
        {
            _cosmosContainer = dbClient.GetContainer(databaseName, containerName);
        }

        public async Task CreatePetAsync(Pet pet)
        {
            await this._cosmosContainer.UpsertItemAsync<Pet>(pet, new PartitionKey(pet.id));
        }

        public async Task<Pet> ReadPetAsync(string id)
        {
            try
            {
                ItemResponse<Pet> response = await this._cosmosContainer.ReadItemAsync<Pet>(id, new PartitionKey(id));
                return response.Resource;
            }
            catch (CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return null;
            }
        }

        public async Task UpdatePetAsync(string id, Pet pet)
        {
            await this._cosmosContainer.UpsertItemAsync<Pet>(pet, new PartitionKey(pet.id));
        }

        public async Task DeletePetAsync(string id)
        {
            await this._cosmosContainer.DeleteItemAsync<Pet>(id, new PartitionKey(id));
        }
    }
}