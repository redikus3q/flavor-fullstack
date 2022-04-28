using Shisha.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shisha.Repositories
{
    public interface IFlavorRepository : IGenericRepository<Flavor>
    {
        Task<List<Flavor>> GetAllFlavorsNamePhoto();
        Task<Flavor> GetFlavorWithComments(int id);
    }
}
