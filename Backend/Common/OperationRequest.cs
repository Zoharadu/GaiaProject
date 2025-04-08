using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class OperationRequest
    {
        public int Id { get; set; }
        public string Operator { get; set; } = "";
        public double A { get; set; }
        public double B { get; set; }
        public double Result { get; set; }
        public DateTime Timestamp { get; set; }

        public class OperationResult
        {
            public double Result { get; set; }
        }
    }
}
