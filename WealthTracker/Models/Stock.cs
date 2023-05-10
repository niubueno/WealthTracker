using Amazon.DynamoDBv2.DataModel;

namespace WealthTracker.Models
{
    [DynamoDBTable("wealthtracker-tb-v1")]
    public class Stock
    {
        [DynamoDBHashKey("id")]
        public string? Id { get; set; }

        [DynamoDBProperty("company_name")]
        public string? CompanyName { get; set; }

        [DynamoDBProperty("company_description")]
        public string? CompanyDescription { get; set; }

        [DynamoDBProperty("company_symbol")]
        public string? CompanySymbol { get; set; }

        [DynamoDBProperty("current_price")]
        public float? CurrentPrice { get; set; }
    }
}
