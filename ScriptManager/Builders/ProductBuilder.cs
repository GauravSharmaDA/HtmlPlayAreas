using ScriptManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScrtiptManager.Models.Builders
{

    public class ProductBuilder : BaseBuilder<Product>
    {
        Product _product;
        public ProductBuilder()
        {
            _product = new Product();
            this.CurrentObject = _product;
        }
        public ProductBuilder WithName(string Name)
        {
            _product.Name = Name;
            return this;
        }
        public ProductBuilder WithId(int Id)
        {
            _product.Id = Id;
            return this;
        }
        
    }
}
