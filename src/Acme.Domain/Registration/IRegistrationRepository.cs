using System.Collections.Generic;
using System.Threading.Tasks;

namespace Acme.Domain.Registration
{
    public interface IRegistrationRepository
    {
        Task<IEnumerable<ZipRegistration>> GetRegisteredZipCodes();
        Task RegisterZipCode(ZipRegistration zipRegistration);
    }
}
