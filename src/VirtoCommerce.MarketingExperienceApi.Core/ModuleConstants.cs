namespace VirtoCommerce.MarketingExperienceApi.Core;

public static class ModuleConstants
{
    public static class Security
    {
        public static class Permissions
        {
            public const string Create = "MarketingExperienceApi:create";
            public const string Read = "MarketingExperienceApi:read";
            public const string Update = "MarketingExperienceApi:update";
            public const string Delete = "MarketingExperienceApi:delete";

            public static string[] AllPermissions { get; } =
            {
                Create,
                Read,
                Update,
                Delete,
            };
        }
    }
}
