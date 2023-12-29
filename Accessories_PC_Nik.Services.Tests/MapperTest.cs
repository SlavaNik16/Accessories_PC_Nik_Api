using Accessories_PC_Nik.Services.Automappers;
using AutoMapper;
using Xunit;

namespace Accessories_PC_Nik.Services.Tests
{
    public class MapperTest
    {
        /// <summary>
        /// Тесты на маппер
        /// </summary>
        [Fact]
        public void TestMap()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new ServiceProfile());
            });

            config.AssertConfigurationIsValid();
        }
    }
}