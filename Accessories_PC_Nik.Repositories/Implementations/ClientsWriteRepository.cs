using Accessories_PC_Nik.Common.Entity.InterfaceDB;
using Accessories_PC_Nik.Context.Contracts.Models;
using Accessories_PC_Nik.Repositories.Anchors;
using Accessories_PC_Nik.Repositories.Contracts.Interface;

namespace Accessories_PC_Nik.Repositories.Implementations
{
    public class ClientsWriteRepository : BaseWriteRepository<Client>,
        IClientsWriteRepository,
        IReadRepositoryAnchor
    {
        /// <summary>
        /// Инициализирует новый экземпляр <see cref="ClientsWriteRepository"/>
        /// </summary>
        public ClientsWriteRepository(IDbWriterContext writerContext)
            : base(writerContext)
        {
        }
    }
}
