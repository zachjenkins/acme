using Acme.Domain.OutreachRecommendations;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Acme.Functions.Queries
{
    public class GetOutreachRecommendations
    {
        public class Query : IRequest<IEnumerable<OutreachRecommendation>>
        {
            public IEnumerable<string> ZipCodes { get; set; } = new List<string>();
            public IEnumerable<string> Cities { get; set; } = new List<string>();
        }

        public class Handler : IRequestHandler<Query, IEnumerable<OutreachRecommendation>>
        {
            private readonly IOutreachRecommendationRepository outreachRecommendationRepository;

            public Handler(IOutreachRecommendationRepository outreachRecommendationRepository)
            {
                this.outreachRecommendationRepository = outreachRecommendationRepository;
            }

            public async Task<IEnumerable<OutreachRecommendation>> Handle(Query query, CancellationToken cancellationToken)
            {
                if (!query.ZipCodes.Any() && !query.Cities.Any())
                    throw new ArgumentException("Must provide at least one city or zipCode"); 

                return await outreachRecommendationRepository.Get(query.ZipCodes, query.Cities);
            }
        }
    }
}
