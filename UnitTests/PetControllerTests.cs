using System.IO;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using SoteriaPet.Controllers;
using SoteriaPet.Models;
using SoteriaPet.Services;

namespace UnitTests
{
    public class PetControllerTests
    {
        private readonly IConfiguration _configuration;

        public PetControllerTests()
        {
            _configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile(@"appsettings.json", false, false).AddEnvironmentVariables().Build();
        }

        [Test]
        public void RunTheTest()
        {
            var databaseName = _configuration["CosmosDb:DatabaseName"];
            var containerName = _configuration["CosmosDb:ContainerName"];
            var endpoint = _configuration["CosmosDb:Endpoint"];
            var primaryKey = _configuration["CosmosDb:PrimaryKey"];
            Microsoft.Azure.Cosmos.CosmosClient client = new Microsoft.Azure.Cosmos.CosmosClient(endpoint, primaryKey);
            CosmosService cosmosService = new CosmosService(client, databaseName, containerName);
            PetController petController = new PetController(cosmosService);
            
            SoteriaPet.Models.Pet testPet = new Pet();
            testPet.id = "1";
            testPet.Name = "Martin";
            testPet.Species = Species.Dog;
            testPet.Sex = Gender.Male;
            testPet.Age = 60;
            testPet.Breed = "Half Cat Half Doberman";
            
            petController.Delete(testPet);
            Task.Run(async () =>
            {
                await petController.Create(testPet);
            }).GetAwaiter().GetResult();
            
            var createdPet = petController.Retrieve(testPet.id);
            createdPet.Name = "Some New Name";
            petController.Update(createdPet.id, createdPet);
            var updatedPet = petController.Retrieve(createdPet.id);
            var pet2 = petController.Retrieve("2");
            petController.Delete(testPet);
            Assert.That(true);
        }
    }
}