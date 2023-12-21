using Accessories_PC_Nik.Common.Entity.InterfaceDB;
using Accessories_PC_Nik.Context.Contracts.Models;
using Accessories_PC_Nik.Repositories.Anchors;
using Accessories_PC_Nik.Repositories.Contracts.Interface;

namespace Accessories_PC_Nik.Repositories.Implementations
{
    public class AccessKeyWriteRepository : BaseWriteRepository<AccessKey>,
        IAccessKeyWriteRepository,
        IReadRepositoryAnchor
    {
        /// <summary>
        /// Инициализирует новый экземпляр <see cref="AccessKeyWriteRepository"/>
        /// </summary>
        public AccessKeyWriteRepository(IDbWriterContext writerContext) 
            : base(writerContext)
        {
        }
    }
}
