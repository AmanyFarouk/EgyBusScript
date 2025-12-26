using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scraping_Egy_Bus.Entities
{
    public class BusType : BaseModel
    {
        public int BusTypeId { get; set; }
        public int CompanyId { get; set; }

        public string TypeName { get; set; } = string.Empty;

 
        public string Description { get; set; } = string.Empty;

   
        public Company Company { get; set; } = null!;

   
        public ICollection<Trip>? Trips { get; set; } = new List<Trip>();
    }
}
