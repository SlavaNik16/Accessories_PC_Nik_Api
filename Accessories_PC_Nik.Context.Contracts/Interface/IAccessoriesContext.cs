using Accessories_PC_Nik.Context.Contracts.Models;
using Microsoft.EntityFrameworkCore;

namespace Accessories_PC_Nik.Context.Contracts.Interface
{
    /// <summary>
    /// Контекст работы с сущностями
    /// </summary>
    public interface IAccessoriesContext
    {
        /// <summary>Список <inheritdoc cref="Models.Client"/></summary>
        DbSet<Client> Client { get; }

        /// <summary>Список <inheritdoc cref="Models.Worker"/></summary>
        DbSet<Worker> Worker { get; }

        /// <summary>Список <inheritdoc cref="Models.Service"/></summary>
        DbSet<Service> Service { get; }

        /// <summary>Список <inheritdoc cref="Order"/></summary>
        DbSet<Order> Orders { get; }

        /// <summary>Список <inheritdoc cref="Delivery"/></summary>
        DbSet<Delivery> Deliveries { get; }

        /// <summary>Список <inheritdoc cref="AccessKey"/></summary>
        DbSet<AccessKey> AccessKeys { get; }

        /// <summary>Список <inheritdoc cref="Models.Component"/></summary>
        DbSet<Component> Component { get; }
    }
}
