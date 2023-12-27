using Accessories_PC_Nik.Common.Entity.InterfaceDB;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System.Diagnostics;

namespace Accessories_PC_Nik.Context.Tests
{
    /// <summary>
    /// Класс <see cref="AccessoriesContext"/> для тестов с базой в памяти. Один контекст на тест
    /// </summary>
    public abstract class AccessoriesContextInMemory : IAsyncDisposable
    {
        protected readonly CancellationToken CancellationToken;
        private readonly CancellationTokenSource cancellationTokenSource;

        /// <summary>
        /// Контекст <see cref="AccessoriesContext"/>
        /// </summary>
        protected AccessoriesContext Context { get; }

        /// <inheritdoc cref="IUnitOfWork"/>
        protected IUnitOfWork UnitOfWork => Context;

        /// <inheritdoc cref="IDbRead"/>
        protected IDbRead Reader => Context;

        protected IDbWriterContext WriterContext => new TestWriteContext(Context, UnitOfWork);

        protected AccessoriesContextInMemory()
        {
            cancellationTokenSource = new CancellationTokenSource();
            CancellationToken = cancellationTokenSource.Token;
            var optionsBuilder = new DbContextOptionsBuilder<AccessoriesContext>()
                .UseInMemoryDatabase($"MemoryTests{Guid.NewGuid()}")
                .ConfigureWarnings(w => w.Ignore(InMemoryEventId.TransactionIgnoredWarning));
            Context = new AccessoriesContext(optionsBuilder.Options);
        }

        public async ValueTask DisposeAsync()
        {
            cancellationTokenSource.Cancel();
            cancellationTokenSource.Dispose();
            try
            {
                await Context.Database.EnsureDeletedAsync();
                await Context.DisposeAsync();
            }
            catch (ObjectDisposedException ex)
            {
                Trace.TraceError(ex.Message);
            }
        }
    }
}
