using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackEndTest.Shared.Parameters
{
    public class InvoiceParameters : PagingParameters
    {        
        public Guid? Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
    }
}
