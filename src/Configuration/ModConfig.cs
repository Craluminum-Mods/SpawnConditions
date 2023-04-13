using Vintagestory.API.Common;

namespace SpawnConditionsMod.Configuration;

static class ModConfig
{
    private const string jsonConfig = "ConfigureEverything/SpawnConditions.json";
    private static SpawnConditionsConfig config;

    public static void ReadConfig(ICoreAPI api)
    {
        try
        {
            config = LoadConfig(api);
            config = api.FillConfigWithEntityTypes(config);

            if (config == null)
            {
                GenerateConfig(api);
                config = LoadConfig(api);
                config = api.FillConfigWithEntityTypes(config);
            }
            else
            {
                GenerateConfig(api, config);
                config = api.FillConfigWithEntityTypes(config);
            }
        }
        catch
        {
            GenerateConfig(api);
            config = LoadConfig(api);
            config = api.FillConfigWithEntityTypes(config);
        }

        api.ApplyPatches(config);
    }

    private static SpawnConditionsConfig LoadConfig(ICoreAPI api) => api.LoadModConfig<SpawnConditionsConfig>(jsonConfig);
    private static void GenerateConfig(ICoreAPI api) => api.StoreModConfig(new SpawnConditionsConfig(), jsonConfig);
    private static void GenerateConfig(ICoreAPI api, SpawnConditionsConfig previousConfig) => api.StoreModConfig(new SpawnConditionsConfig(previousConfig), jsonConfig);
}