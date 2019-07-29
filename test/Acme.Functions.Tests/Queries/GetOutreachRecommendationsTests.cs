using Acme.Domain.OutreachRecommendations;
using Acme.Domain.Tests.OutreachReommendations;
using Acme.Functions.Queries;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Acme.Functions.Tests.Queries
{
    [Trait("Functions", nameof(GetOutreachRecommendations))]
    public class GetOutreachRecommendationsTests
    {
        private readonly GetOutreachRecommendations.Handler handler;

        private readonly Mock<IOutreachRecommendationRepository> mockOutreachRecommendationsRepository
            = new Mock<IOutreachRecommendationRepository>(MockBehavior.Strict);

        public GetOutreachRecommendationsTests()
        {
            handler = new GetOutreachRecommendations.Handler(mockOutreachRecommendationsRepository.Object);
        }

        [Fact]
        public async Task Handle_Returns_Recommendations_ByZip()
        {
            // Arrange
            var existingRecommendations = OutreachRecommendationsBuilder
                .Randomized()
                .BuildAsList();

            var query = new GetOutreachRecommendations.Query
            {
                ZipCodes = new List<string> { "54022", "55401" },
                Cities = new List<string> { "River Falls", "Minneapolis" }
            };

            mockOutreachRecommendationsRepository
                .Setup(r => r.Get(query.ZipCodes, query.Cities))
                .ReturnsAsync(existingRecommendations);

            // Act
            var returnedRecommendations = await handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.Equal(existingRecommendations, returnedRecommendations);
            mockOutreachRecommendationsRepository.Verify();
        }

        [Fact]
        public async Task Handle_ThrowsArgumentException_NoSearchCriteria()
        {
            // Arrange
            var query = new GetOutreachRecommendations.Query
            {
                ZipCodes = Enumerable.Empty<string>(),
                Cities = Enumerable.Empty<string>()
            };

            // Act
            var exception = await Assert.ThrowsAsync<ArgumentException>(async () 
                => await handler.Handle(query, CancellationToken.None));

            // Assert
            Assert.Equal("Must provide at least one city or zipCode", exception.Message);
        }
    }
}
