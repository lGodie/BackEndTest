using System;
using System.Collections.Generic;
using System.Text;

namespace BackEndTest.Shared.Requests
{
    public class ProductRequest
    {
        public Guid? Id { get; set; }

        public string Name { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public Guid? CategoryId { get; set; }

    }
}
