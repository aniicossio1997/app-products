using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using app_products.Repositories.IRepositories;
using app_products.Services;
using Moq;

namespace UnitTest.Services
{


    public partial class ProductsServiceTest
    {

        protected ProductsService ServiceUnderTest;
        protected Mock<IProductsRepository> RepositoryMock;

        public ProductsServiceTest()
        {
            RepositoryMock = new Mock<IProductsRepository>();
            ServiceUnderTest = new ProductsService(RepositoryMock.Object);
        }

    }



}
