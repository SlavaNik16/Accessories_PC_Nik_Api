using Accessories_PC_Nik.Context.Contracts.Interface;
using Accessories_PC_Nik.Context.Contracts.Models;
using Accessories_PC_Nik.Repositories.Contracts.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accessories_PC_Nik.Repositories.Implementations
{
    public class ComponentsReadRepository : IComponentsReadRepository
    {
        private readonly IAccessoriesContext context;

        public ComponentsReadRepository(IAccessoriesContext context)
        {
            this.context = context;
        }

        Task<List<Components>> IComponentsReadRepository.GetAllAsync(CancellationToken cancellationToken)
            => Task.FromResult(context.Components.Where(x => x.DeleteAt == null)
                .OrderBy(x => x.MaterialType)
                .ToList());

        Task<Components?> IComponentsReadRepository.GetByIdAsync(Guid id, CancellationToken cancellationToken)
            => Task.FromResult(context.Components.FirstOrDefault(x => x.Id == id));

    }
}
