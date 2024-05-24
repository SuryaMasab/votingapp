using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VoterApp.Domain.Models;
using VotersApi.Controllers;
using VotersApi.Repository.Interfaces;

namespace VotersApi.TestProj;

[TestClass]
public class CandidateControllerTests
{
    [TestMethod]
    public async Task GetCandidates_ReturnsOkResult()
    {
        // Arrange
        var mockRepo = new Mock<ICandidateRepository>();
        var logger = new Mock<ILogger<CandidateController>>();
        mockRepo.Setup(repo => repo.GetCandidates())
            .ReturnsAsync(GetTestCandidates());
        var controller = new CandidateController(mockRepo.Object, logger.Object);

        //setup the mock repository
        mockRepo.Setup(repo => repo.GetCandidates())
            .ReturnsAsync(GetTestCandidates());

        // Act
        var result = await controller.GetCandidates();

        // Assert 
        Assert.IsNotNull(result);
        Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult));       
        
        Assert.IsNotInstanceOfType(result.Result,typeof(NotFoundResult));

        if(result.Result is OkObjectResult okObjectResult && result.Result!= null )
        {
            object value = ((OkObjectResult)result.Result).Value!;
            Assert.IsInstanceOfType(okObjectResult.Value, typeof(IEnumerable<Candidate>));
            Assert.AreEqual(3, ((IEnumerable<Candidate>)value).Count());
        }
      
    }


    [TestMethod]
    public async Task GetCandidate_ReturnsOkResult()
    {
        // Arrange
        var mockRepo = new Mock<ICandidateRepository>();
        var logger = new Mock<ILogger<CandidateController>>();
     
        mockRepo.Setup(repo => repo.GetCandidate(1))
            .ReturnsAsync(GetTestCandidates().FirstOrDefault(p => p.Id == 1));

        var controller = new CandidateController(mockRepo.Object, logger.Object);

        // Act
        var result = await controller.GetCandidate(1);

        // Assert
        Assert.IsNotNull(result);
        Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult));
        Assert.IsNotInstanceOfType(result.Result, typeof(NotFoundResult));

        if (result.Result is OkObjectResult okObjectResult && result.Result != null)
        {
            object value = ((OkObjectResult)result.Result).Value!;
            Assert.IsInstanceOfType(okObjectResult.Value, typeof(Candidate));
            Assert.AreEqual(1, ((Candidate)value).Id);
        }
    }

    [TestMethod]
    public async Task AddCandidate_ReturnsOkResult()
    {
        // Arrange
        var mockRepo = new Mock<ICandidateRepository>();

        var logger = new Mock<ILogger<CandidateController>>();
        var newCandidate = new Candidate { Id = 4, Name = "Test Candidate 4", VotesCount = 0 };

        mockRepo.Setup(repo => repo.AddCandidate(newCandidate))
            .ReturnsAsync(newCandidate);
        var controller = new CandidateController(mockRepo.Object, logger.Object);

        // Act
        var result = await controller.AddCandidate(newCandidate);

        // Assert
        Assert.IsNotNull(result);
        Assert.IsInstanceOfType(result.Result, typeof(CreatedAtActionResult));
        Assert.IsNotInstanceOfType(result.Result, typeof(BadRequestResult));

        if (result.Result is CreatedAtActionResult createdResult && result.Result != null)
        {
            object value = createdResult.Value!;
            Assert.IsInstanceOfType(createdResult.Value, typeof(Candidate));
            Assert.AreEqual(4, ((Candidate)value).Id);
        }
    }


    #region [Dummy Data]

    private static IEnumerable<Candidate> GetTestCandidates()
    {
        // create some dummy data
        var candidates = new List<Candidate>
        {
            new() { Id = 1, Name = "Test Candidate 1", VotesCount=0 },
            new() { Id = 2, Name = "Test Candidate 2", VotesCount=1 },
            new() { Id = 3, Name = "Test Candidate 3", VotesCount=1 }
        };

        return candidates;
    }

    #endregion
}
