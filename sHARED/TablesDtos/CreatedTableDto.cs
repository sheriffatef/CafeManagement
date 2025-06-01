using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.TablesDtos
{
    public class CreatedTableDto
    {
        public string name { get; set; } = null!;
        public int Capacity { get; set; }

        public string status { get; set; }
    }
}
