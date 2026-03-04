using Polly.CircuitBreaker;

namespace Presentation.Constants;

internal static class PathConstants
{
    private const string ApiBase = "api";

    public static class Users
    {
        public const string Controller = ApiBase + "/user";

        public const string GetUserBalance = "balance";
        public const string GetOrders = "{id:guid}/orders";
    }

    public static class Orders
    {
        public const string Controller = ApiBase + "/order";

        public const string Create = "";
        public const string Cancel = "{id:guid}/cancel";
        public const string GetById = "{id:guid}";
    }
    
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

    public static class Files
    {
        public const string Controller = ApiBase + "/File";
        public const string Upload = "upload";
    }

    public static class OrderAudits
    {
        public const string Controller = ApiBase + "/orderAudit";
        public const string GetByOrderId = "{orderId:guid}";
    }

    public static class ProductQuantity
    {
        public const string Controller = ApiBase + "/productQuantity";
        public const string GetAvailable = "available/{productSizeId:guid}";
        public const string GetHistory = "history/{productSizeId:guid}";
        public const string Add = "";

    }
}