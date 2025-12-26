using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scraping_Egy_Bus.Entities
{
    public class Company : BaseModel
    {
        public int CompanyId { get; set; }
        public string CompanyName { get; set; } = string.Empty;
        public string Website { get; set; }
        public string? PhoneNumber { get; set; }
        public string? LogoUrl { get; set; }
        public bool IsActive { get; set; } = true;

        // Navigation Properties - One Company has Many...
        public ICollection<Station>? Stations { get; set; } = new List<Station>();
        public ICollection<BusType>? BusTypes { get; set; } = new List<BusType>();

        // public ICollection<Route>? Routes { get; set; } = new List<Route>();
        public ICollection<Trip>? Trips { get; set; } = new List<Trip>();
    }
}
