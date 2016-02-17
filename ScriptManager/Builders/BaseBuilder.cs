using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScrtiptManager.Models.Builders
{
    public class BaseBuilder<T>
    {
        public T CurrentObject { get; set; }
        public BaseBuilder()
        {
           
        }

        public T Build()
        {
            return CurrentObject;
        }
    }
}
