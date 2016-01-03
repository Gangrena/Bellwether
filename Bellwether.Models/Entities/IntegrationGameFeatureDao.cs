﻿namespace Bellwether.Models.Entities
{
    public class IntegrationGameFeatureDao
    {
        public int Id { get; set; }
        public virtual IntegrationGameDao IntegrationGame { get; set; }
        public virtual GameFeatureDao GameFeature { get; set; }
        public virtual GameFeatureDetailDao GameFeatureDetail { get; set; }
    }
}
