using System;
using System.Collections.Generic;
using System.Text;
using Acme.Domain.OutreachRecommendations;
using Acme.Domain.Registration;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Attributes;

namespace Acme.Infrastructure.Repositories
{

    public static class MongoMaps
    {
        public static void RegisterMaps()
        {
            BsonClassMap.RegisterClassMap<OutreachRecommendation>(cm =>
            {
                cm.AutoMap();
                cm.MapProperty(p => p.Recommendations);
                cm.SetIgnoreExtraElements(true);
            });

            BsonClassMap.RegisterClassMap<ZipRegistration>(cm =>
            {
                cm.AutoMap();
                cm.SetIgnoreExtraElements(true);
            });
        }
    }
}
