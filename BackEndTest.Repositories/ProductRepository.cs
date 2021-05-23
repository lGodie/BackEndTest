using BackEndTest.Shared.Repositories.Generic;
using presupuestoback.Shared.v1.repositories;
using Repositories.Shared.Core.Entities;
using Repositories.Shared.Repositories.Generic;

namespace BackEndTest.Repositories
{
    public class ProductRepository : BaseRepository<Product>, IProductRepository
    {
        public ProductRepository(IUnitOfWork _unitOfWork)
            : base(_unitOfWork)
        { }
    }
}
