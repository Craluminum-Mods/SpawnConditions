using System.Collections.Generic;
using Vintagestory.API.Common.Entities;

namespace SpawnConditionsMod.Configuration
{
    public class SpawnConditionsConfig
    {
        public Dictionary<string, SpawnConditions> EntityTypes = new() { };

        public SpawnConditionsConfig() { }
        public SpawnConditionsConfig(SpawnConditionsConfig previousConfig)
        {
            foreach (var item in previousConfig.EntityTypes)
            {
                if (EntityTypes.ContainsKey(item.Key)) continue;
                EntityTypes.Add(item.Key, item.Value);
            }
        }
    }
}
