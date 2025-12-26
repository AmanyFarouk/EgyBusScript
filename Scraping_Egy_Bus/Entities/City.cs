using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scraping_Egy_Bus.Entities
{
    public class City : BaseModel
    {
        public int CityId { get; set; }
        public string CityName { get; set; } = string.Empty;


        public ICollection<Station> Stations { get; set; } = new List<Station>();
    }
}
