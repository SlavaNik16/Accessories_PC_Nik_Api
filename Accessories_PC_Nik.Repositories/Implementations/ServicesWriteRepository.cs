using Accessories_PC_Nik.Common.Entity.InterfaceDB;
using Accessories_PC_Nik.Context.Contracts.Models;
using Accessories_PC_Nik.Repositories.Anchors;
using Accessories_PC_Nik.Repositories.Contracts.Interface;

namespace Accessories_PC_Nik.Repositories.Implementations
{
    public class ServicesWriteRepository : BaseWriteRepository<Service>,
        IServicesWriteRepository,
        IReadRepositoryAnchor
    {
        /// <summary>
        /// Инициализирует новый экземпляр <see cref="ServicesWriteRepository"/>
        /// </summary>
        public ServicesWriteRepository(IDbWriterContext writerContext)
            : base(writerContext)
        {
        }
    }
}
