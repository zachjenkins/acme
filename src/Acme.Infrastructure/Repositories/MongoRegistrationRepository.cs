using Acme.Domain.Registration;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Acme.Infrastructure.Repositories
{
    public class MongoRegistrationRepository : IRegistrationRepository
    {
        private readonly IMongoCollection<ZipRegistration> registrationCollection;

        public MongoRegistrationRepository(IMongoCollection<ZipRegistration> registrationCollection)
        {
            this.registrationCollection = registrationCollection;
        }

        public async Task<IEnumerable<ZipRegistration>> GetRegisteredZipCodes()
        {
            var results = await registrationCollection.FindAsync(Builders<ZipRegistration>.Filter.Empty);

            return await results.ToListAsync();
        }

        public async Task RegisterZipCode(ZipRegistration zipRegistration)
        {
            try
            {
                var options = new FindOneAndReplaceOptions<ZipRegistration>
                {
                    IsUpsert = true
                };

                var filter = Builders<ZipRegistration>.Filter.Eq(r => r.ZipCode, zipRegistration.ZipCode);

                await registrationCollection.FindOneAndReplaceAsync(filter, zipRegistration, options);
            }
            catch (MongoException mongoException)
            {
                // TODO
            }
        }
    }
}
