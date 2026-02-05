namespace Presentation.Constants;

internal static class PathConstants
{
    private const string ApiBase = "api";

    public static class Products
    {
        public const string Controller = ApiBase + "/Product";
        
        public const string GetById = "{languageCode}/{id}";
        public const string Create = "";
        public const string Update = "{productId}";
        public const string Delete = "{productId}";
        public const string GetByGroup = "{languageCode}/group/{groupId}";
    }

    public static class ProductGroups
    {
        public const string Controller = ApiBase + "/ProductGroup";

        public const string GetById = "{languageCode}/{id:guid}";
        public const string Create = "";
        public const string Update = "{productGroupId:guid}";
        public const string Delete = "{productGroupId:guid}";
        public const string GetAll = "{languageCode}";
    }

    public static class Transactions
    {
        public const string Controller = ApiBase + "/Transaction";
    }

    public static class User
    {
        public const string Controller = ApiBase + "/User";

        public const string GetCurrentScoresAmount = "current-scores-amount";
    }
}