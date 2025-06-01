using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.TablesDtos
{
    public class TableDto
    {
        public int id { get; set; }
        public string name { get; set; } = null!;
        public int capacity { get; set; }
        public string status { get; set; } = null!;
    }
}
