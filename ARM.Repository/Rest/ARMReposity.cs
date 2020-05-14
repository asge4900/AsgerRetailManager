using System;
using System.Collections.Generic;
using System.Text;

namespace ARM.Repository
{
    /// <summary>
    /// Contains methods for interacting with the app backend using REST. 
    /// </summary>
    public class ARMReposity : IARMReposity
    {

        private readonly string _url;

        public ARMReposity(string url)
        {
            _url = url;
        }

        public ILoginRepository Login => new LoginRepository(_url);

        public IProductRepository Product => new ProductRepository(_url);

        public ISaleRepository Sale => new SaleRepository(_url);

    }
}
