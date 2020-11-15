using System;
using System.Collections.Generic;
using System.Text;

namespace MyIoTService.Domain
{
    public class DataType
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int RangeFrom { get; set; }
        public int RangeTo { get; set; }
    }
}
