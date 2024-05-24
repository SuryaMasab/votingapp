using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using VoterApp.Domain.Models;
using VotersApi.Controllers;
using VotersApi.Repository.Interfaces;

namespace VotersApi.TestProj;

[TestClass]
public class VoteControllerTests
{
    [TestMethod]
    public async Task CastVote_ReturnsOkResult()
    {
        // Arrange
        var mockVoterRepo = new Mock<IVoterRepository>();
        var mockCandidateRepo = new Mock<ICandidateRepository>();
        var mockCastVoteRepo = new Mock<ICastVoteRepository>();
        var logger = new Mock<ILogger<VoteController>>();
        var controller = new VoteController(mockVoterRepo.Object, mockCandidateRepo.Object, mockCastVoteRepo.Object, logger.Object);
        var newVote = new Vote { VoterId = 1, CandidateId = 1 };

        mockVoterRepo.Setup(repo => repo.GetVoter(1))
            .ReturnsAsync(new Voter { Id = 1, HasVoted = false });

        _ = mockCandidateRepo.Setup(repo => repo.GetCandidate(1))
            .ReturnsAsync(new Candidate { Id = 1, Name="CandidateName", VotesCount = 0 });

        mockCastVoteRepo.Setup(repo => repo.CastVote(newVote))
            .Returns(true);

        // Act
        var result = await controller.CastVote(newVote);

        // Assert
        Assert.IsNotNull(result);
        Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult));
        Assert.IsNotInstanceOfType(result.Result, typeof(BadRequestResult));
    }
}
