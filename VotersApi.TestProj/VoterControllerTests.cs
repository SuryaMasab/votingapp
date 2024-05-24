using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using VoterApp.Domain.Models;
using VotersApi.Controllers;
using VotersApi.Repository.Interfaces;

namespace VotersApi.TestProj;

[TestClass]
public class VoterControllerTests
{
    [TestMethod]
    public async Task GetVoters_ReturnsOkResult()
    {
        // Arrange
        var mockRepo = new Mock<IVoterRepository>();
        var logger = new Mock<ILogger<VoterController>>();
        mockRepo.Setup(repo => repo.GetVoters())
            .ReturnsAsync(GetTestVoters());

        var controller = new VoterController(mockRepo.Object, logger.Object);

        //setup the mock repository
        mockRepo.Setup(repo => repo.GetVoters())
            .ReturnsAsync(GetTestVoters());

        // Act
        var result = await controller.GetVoters();

        // Assert 
        Assert.IsNotNull(result);
        Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult));       
        
        Assert.IsNotInstanceOfType(result.Result,typeof(NotFoundResult));

        if(result.Result is OkObjectResult okObjectResult && result.Result!= null )
        {
            object value = ((OkObjectResult)result.Result).Value!;
            Assert.IsInstanceOfType(okObjectResult.Value, typeof(IEnumerable<Voter>));
            Assert.AreEqual(3, ((IEnumerable<Voter>)value).Count());
        }
      
    }

    [TestMethod]
    public async Task GetVoter_ReturnsOkResult()
    {
        // Arrange
        var mockRepo = new Mock<IVoterRepository>();
        var logger = new Mock<ILogger<VoterController>>();

        mockRepo.Setup(repo => repo.GetVoter(1))
            .ReturnsAsync(GetTestVoters().FirstOrDefault(p => p.Id == 1));

        var controller = new VoterController(mockRepo.Object, logger.Object);

        // Act
        var result = await controller.GetVoter(1);

        // Assert
        Assert.IsNotNull(result);
        Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult));
        Assert.IsNotInstanceOfType(result.Result, typeof(NotFoundResult));
        Assert.AreEqual(1, ((Voter)((OkObjectResult)result.Result).Value!).Id);
    }

    [TestMethod]
    public async Task AddVoter_ReturnsOkResult()
    {
        // Arrange
        var mockRepo = new Mock<IVoterRepository>();
        var logger = new Mock<ILogger<VoterController>>();
        var newVoter = new Voter { Id = 4, Name = "Voter4", HasVoted = false };

        mockRepo.Setup(repo => repo.AddVoter(newVoter))
            .ReturnsAsync(newVoter);

        var controller = new VoterController(mockRepo.Object, logger.Object);

        // Act
        var result = await controller.AddVoter(newVoter);

        // Assert
        Assert.IsNotNull(result);
        Assert.IsInstanceOfType(result.Result, typeof(CreatedAtActionResult));
        Assert.AreEqual(4, ((Voter)((CreatedAtActionResult)result.Result).Value!).Id);
    }


    private IEnumerable<Voter> GetTestVoters()
    {
        //Create a list of voters
        var voters = new List<Voter>
        {
            new() { Id = 1, Name = "Voter1", HasVoted = false },
            new() { Id = 2, Name = "Voter2", HasVoted = true },
            new() { Id = 3, Name = "Voter3", HasVoted = false }
        };

        return voters; 
    }
}
