using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using MongoDB.Driver;
using Acme.Domain.OutreachRecommendations;

namespace Acme.Infrastructure.Repositories
{
    public class MongoOutreachRecommendationsRepository : IOutreachRecommendationRepository
    {
        private readonly IMongoCollection<OutreachRecommendation> recommendationsCollection;

        public MongoOutreachRecommendationsRepository(IMongoCollection<OutreachRecommendation> recommendationsCollection)
        {
            this.recommendationsCollection = recommendationsCollection;
        }

        public async Task<IEnumerable<OutreachRecommendation>> Get(IEnumerable<string> zipCodes, IEnumerable<string> cities)
        {
            var filter = FilterDefinition<OutreachRecommendation>.Empty;

            if (zipCodes.Any())
                filter &= Builders<OutreachRecommendation>.Filter.In(r => r.ZipCode, zipCodes);

            if (cities.Any())
                filter &= Builders<OutreachRecommendation>.Filter.In(r => r.CityName, cities);

            var results = await recommendationsCollection.FindAsync(filter);

            return await results.ToListAsync();
        }

        public async Task Update(OutreachRecommendation outreachRecommendation)
        {
            try
            {
                var options = new FindOneAndReplaceOptions<OutreachRecommendation>()
                {
                    IsUpsert = true
                };

                var zipFilter = Builders<OutreachRecommendation>.Filter.Eq(r => r.ZipCode, outreachRecommendation.ZipCode);

                await recommendationsCollection.FindOneAndReplaceAsync(zipFilter, outreachRecommendation, options);
            }
            catch (MongoWriteException mongoWriteException)
            {
                // TODO
            }
        }
    }
}
