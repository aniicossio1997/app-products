using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using app_products.Enums;
using app_products.Exceptions;
using app_products.Repositories.IRepositories;
using app_products.Services;
using app_products.ViewModels;
using app_products.ViewModels.Filters;
using Moq;

namespace UnitTest.Services
{


 
    public partial class ProductsServiceTest
    {
        public class GetByBudgetPrice : ProductsServiceTest
        {
            protected CancellationToken CancellationToken;

            public GetByBudgetPrice()
            {
                CancellationToken = new CancellationToken();
            }



            public static IEnumerable<object[]> GetCasesSuccess()
            {
                yield return new object[]
                {
                    new ProductFilterViewModel
                    {
                        BudgetPrice=70
                    },
                     //Lista toda la lista
                    new List<ProductViewModel>
                    {
                        new ProductViewModel {
                            Category=new CategoryViewModel{ Id=(int)CategoryTypeEnum.Two},
                            Id=2,
                            Price=10,
                        },
                        new ProductViewModel {
                            Category=new CategoryViewModel{ Id=(int)CategoryTypeEnum.One},
                            Id=1,
                            Price=60,
                        },
                        new ProductViewModel {
                            Category=new CategoryViewModel{ Id=(int)CategoryTypeEnum.Two},
                            Id=2,
                            Price=10,
                        },
                       new ProductViewModel {
                            Category=new CategoryViewModel{ Id=(int)CategoryTypeEnum.Two},
                            Id=2,
                            Price=5,
                        },},
                    //Lista esperada
                    new  List<ProductViewModel>
                    {
                                                new ProductViewModel {
                            Category=new CategoryViewModel{ Id=(int)CategoryTypeEnum.Two},
                            Id=2,
                            Price=10,
                        },
                        new ProductViewModel {
                            Category=new CategoryViewModel{ Id=(int)CategoryTypeEnum.One},
                            Id=1,
                            Price=60,
                        },
                    }
                };


            }

            [Theory]
            [MemberData(nameof(GetCasesSuccess))]
            public async Task Should_Return_Prodcuts_To_Budget(
                ProductFilterViewModel testCase,
                List<ProductViewModel> products,
                List<ProductViewModel> expectedResult)
            {
                // Arrange
                var cancellationToken = CancellationToken.None;

                RepositoryMock
                    .Setup(repo => repo.GetByFilter(It.IsAny<ProductFilterViewModel>(), cancellationToken))
                    .ReturnsAsync(products);


                // Act
                var budgets = await ServiceUnderTest.GetByBudgetPrice(
                    testCase,
                    cancellationToken
                );

                // Assert
                RepositoryMock.Verify();
                Assert.True(budgets.Count()== expectedResult.Count);
                Assert.True(budgets.Sum(e=> e.Price) == expectedResult.Sum(e => e.Price));
                Assert.Collection(expectedResult, expectedResult.Select((item, index) => { return new Action<ProductViewModel>((item) => Assert.Equal(item, expectedResult[index])); }).ToArray());

  

            }

            #region Cases Error
            public static IEnumerable<object[]> GetCasesError()
            {
                yield return new object[]
                {
        new ProductFilterViewModel
        {
            BudgetPrice = 60
        },
        // Lista con productos
        new List<ProductViewModel>
        {
            new ProductViewModel {
                Category = new CategoryViewModel{ Id = (int)CategoryTypeEnum.One },
                Id = 1,
                Price = 60,
            },
            new ProductViewModel {
                Category = new CategoryViewModel{ Id = (int)CategoryTypeEnum.Two },
                Id = 2,
                Price = 10,
            },
            new ProductViewModel {
                Category = new CategoryViewModel{ Id = (int)CategoryTypeEnum.Two },
                Id = 3,
                Price = 5,
            },
        },
        // Lista esperada (puedes dejarla como una lista vacía si no se esperan productos)
        new List<ProductViewModel>()
                };

                yield return new object[]
                {
        new ProductFilterViewModel
        {
            BudgetPrice = 1000 // Supuesto filtro que no debería coincidir con ningún producto
        },
        // Lista vacía de productos
        new List<ProductViewModel>(),
        // Lista esperada (vacía)
        new List<ProductViewModel>()
                };
            }

            [Theory]
            [MemberData(nameof(GetCasesError))]
            public async Task Should_Return_Error_To_Budget(
                ProductFilterViewModel testCase,
                List<ProductViewModel> products,
                List<ProductViewModel> expectedResult)
            {
                // Arrange
                var cancellationToken = CancellationToken.None;

                RepositoryMock
                    .Setup(repo => repo.GetByFilter(It.IsAny<ProductFilterViewModel>(), cancellationToken))
                    .ReturnsAsync(products);

                // Act


                // Assert
                RepositoryMock.Verify();

               // Si no se esperan productos, deberíamos recibir una EntityNotFoundException
                await Assert.ThrowsAsync<EntityNotFoundException>(async () =>
                {
                    var budgets = await ServiceUnderTest.GetByBudgetPrice(
                        testCase,
                        cancellationToken
                    );
                });
                


            }
            #endregion

        }

    }
 
}


