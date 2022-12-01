using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOrders.DAL.Interfaces.IModels
{
    public interface IDistinctValues
    {
        public List<string>? Number { get; set; }
        public List<string>? ItemName { get; set; }
        public List<string>? ProviderName { get; set; }
        public List<string>? Unit { get; set; }
    }
}
