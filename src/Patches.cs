using SpawnConditionsMod.Configuration;
using Vintagestory.API.Common;
using Vintagestory.API.Common.Entities;
using Vintagestory.API.Util;

namespace SpawnConditionsMod;

public static class Patches
{
    public static void ApplyPatches(this ICoreAPI api, SpawnConditionsConfig config)
    {
        if (config.EntityTypes == null) return;

        foreach (var entityType in api.World.EntityTypes)
        {
            if (entityType?.Server?.SpawnConditions == null) continue;

            foreach (var eType in config.EntityTypes)
            {
                if (eType.Key == null || eType.Value == null || !entityType.WildcardRegexMatch(eType.Key)) continue;

                entityType.Server.SpawnConditions = eType.Value;
            }
        }
    }

    public static bool WildcardRegexMatch(this EntityProperties properties, string key) => WildcardUtil.Match(new AssetLocation(key), properties.Code);

    public static SpawnConditionsConfig FillConfigWithEntityTypes(this ICoreAPI api, SpawnConditionsConfig config)
    {
        foreach (var entityType in api.World.EntityTypes)
        {
            if (entityType?.Server?.SpawnConditions == null) continue;
            if (config.EntityTypes.ContainsKey(entityType.Code.ToString())) continue;

            config.EntityTypes.Add(entityType.Code.ToString(), entityType.Server.SpawnConditions);
        }

        return config;
    }
}