using Accessories_PC_Nik.Context.Contracts.Interface;
using Accessories_PC_Nik.Context.Contracts.Models;

namespace Accessories_PC_Nik.Context
{
    public class AccessoriesContext : IAccessoriesContext
    {
        private readonly IList<Clients> clients;
        private readonly IList<Workers> workers;
        private readonly IList<Services> services;
        private readonly IList<Order> order;
        private readonly IList<Delivery> delivery;
        private readonly IList<AccessKey> accessKey;
        private readonly IList<Components> components;

        public AccessoriesContext()
        {
            clients = new List<Clients>();
            workers = new List<Workers>();
            services = new List<Services>();
            order = new List<Order>();
            delivery = new List<Delivery>();
            accessKey = new List<AccessKey>();
            components = new List<Components>();
        }

        IEnumerable<Clients> IAccessoriesContext.Clients => clients;

        IEnumerable<Workers> IAccessoriesContext.Workers => workers;

        IEnumerable<Services> IAccessoriesContext.Services => services;

        IEnumerable<Order> IAccessoriesContext.Order => order;

        IEnumerable<Delivery> IAccessoriesContext.Delivery => delivery;

        IEnumerable<AccessKey> IAccessoriesContext.AccessKey => accessKey;

        IEnumerable<Components> IAccessoriesContext.Components => components;
    }
}
