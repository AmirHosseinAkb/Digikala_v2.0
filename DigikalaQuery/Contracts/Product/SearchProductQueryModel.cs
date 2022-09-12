using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigikalaQuery.Contracts.Product
{
    public class SearchProductQueryModel
    {
        public int PageId { get; set; } = 1;
        public string Title { get; set; } = "";
        public string OrderBy { get; set; } = "";
        public bool IsInStock { get; set; } = false;
        public int StartPrice { get; set; } = 0;
        public int EndPrice { get; set; } = 0;
        public List<string> Colors { get; set; } = new List<string>();
        public List<int> Brands { get; set; } = new List<int>();
    }
}
