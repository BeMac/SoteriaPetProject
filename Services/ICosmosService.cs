namespace SoteriaPet.Services
{
    using System.Threading.Tasks;
    using Models;

    public interface ICosmosDbService
    {
        Task CreatePetAsync(Pet pet);
        Task<Pet> ReadPetAsync(string id);
        Task UpdatePetAsync(string id, Pet pet);
        Task DeletePetAsync(string id);
    }
}