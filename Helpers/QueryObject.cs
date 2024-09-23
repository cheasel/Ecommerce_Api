using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace eCommerceApi.Helpers
{
    public class QueryObject
    {
        public string? Name { get; set; } = null;
        public int LowPrice { get; set; } = 0;
        public int? HighPrice { get; set; } = null;
        public SortOptions? SortBy { get; set; } = null;
        public bool IsDescending { get; set; } = false;
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 20;
    }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum SortOptions{
        Name,
        Price
    }
}