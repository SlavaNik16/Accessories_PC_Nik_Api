using Accessories_PC_Nik.Common;
using Accessories_PC_Nik.Common.Entity.InterfaceDB;
using Moq;

namespace Accessories_PC_Nik.Context.Tests
{
    internal class TestWriteContext : IDbWriterContext
    {
        public IDbWriter Writer { get; }

        public IUnitOfWork UnitOfWork { get; }

        public IDateTimeProvider DateTimeProvider { get; }

        public string UserName { get; }

        public TestWriteContext(IDbWriter writer,
            IUnitOfWork unitOfWork)
        {
            Writer = writer;
            UnitOfWork = unitOfWork;
            var dateTimeProviderMock = new Mock<IDateTimeProvider>();
            dateTimeProviderMock.Setup(x => x.UtcNow).Returns(DateTimeOffset.UtcNow);
            DateTimeProvider = dateTimeProviderMock.Object;
            UserName = "UserForTests";
        }
    }
}
