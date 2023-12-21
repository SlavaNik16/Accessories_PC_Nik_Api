using Accessories_PC_Nik.Common.Entity.InterfaceDB;
using Accessories_PC_Nik.Context.Contracts.Models;
using Accessories_PC_Nik.Repositories.Anchors;
using Accessories_PC_Nik.Repositories.Contracts.Interface;

namespace Accessories_PC_Nik.Repositories.Implementations
{
    public class ComponentsWriteRepository : BaseWriteRepository<Component>,
        IComponentsWriteRepository,
        IReadRepositoryAnchor
    {
        /// <summary>
        /// Инициализирует новый экземпляр <see cref="ComponentsWriteRepository"/>
        /// </summary>
        public ComponentsWriteRepository(IDbWriterContext writerContext)
            : base(writerContext)
        {
        }
    }
}
