using Acme.Domain.Registration;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Acme.Functions.Queries
{
    public class GetRegistrations
    {
        public class Query : IRequest<IEnumerable<ZipRegistration>>
        {
           
        }

        public class Handler : IRequestHandler<Query, IEnumerable<ZipRegistration>>
        {
            private readonly IRegistrationRepository registrationRepository;

            public Handler(IRegistrationRepository registrationRepository)
            {
                this.registrationRepository = registrationRepository;
            }

            public async Task<IEnumerable<ZipRegistration>> Handle(Query query, CancellationToken cancellationToken)
            {
                return await registrationRepository.GetRegisteredZipCodes();
            }
        }
    }
}
