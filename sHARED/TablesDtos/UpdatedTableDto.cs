using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.TablesDtos
{
    public class UpdatedTableDto
    {
        public string TableName { get; set; } = null!;
        public int Capacity { get; set; }
    }
}
