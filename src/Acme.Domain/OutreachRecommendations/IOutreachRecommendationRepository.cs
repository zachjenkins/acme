using System.Collections.Generic;
using System.Threading.Tasks;

namespace Acme.Domain.OutreachRecommendations
{
    public interface IOutreachRecommendationRepository
    {
        Task<IEnumerable<OutreachRecommendation>> Get(IEnumerable<string> zipCodes, IEnumerable<string> cities);
        Task Update(OutreachRecommendation outreachRecommendation);
    }
}
