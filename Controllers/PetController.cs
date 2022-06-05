using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel;
using System.Threading.Tasks;
using SoteriaPet.Models;
using SoteriaPet.Services;

namespace SoteriaPet.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class PetController : ControllerBase
    {
        private readonly ICosmosDbService _cosmosService;

        public PetController(ICosmosDbService cosmosService)
        {
            _cosmosService = cosmosService;
        }

        //[HttpPost]
        public async Task Create(Pet pet)
        {
            CheckEnums(pet);
            await _cosmosService.CreatePetAsync(pet);
        }

        [HttpGet]
        public Pet Retrieve(string id)
        {
            return _cosmosService.ReadPetAsync(id).Result;
        }

        [HttpPut]
        public void Update(string id, Pet pet)
        {
            CheckEnums(pet);
            _cosmosService.UpdatePetAsync(id, pet);
        }

        [HttpDelete]
        public void Delete(Pet pet)
        {
            _cosmosService.DeletePetAsync(pet.id);
        }

        private void CheckEnums(Pet pet)
        {
            if (!Enum.IsDefined(typeof(Gender), pet.Sex))
            {
                throw new InvalidEnumArgumentException("Gender is not valid");
            }
            if (!Enum.IsDefined(typeof(Species), pet.Species))
            {
                throw new InvalidEnumArgumentException("Species is not valid");
            }
        }
    }
}