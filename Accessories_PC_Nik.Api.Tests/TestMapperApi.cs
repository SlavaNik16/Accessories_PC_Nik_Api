using Accessories_PC_Nik.Api.Infrastructures;
using AutoMapper;
using Xunit;

namespace Accessories_PC_Nik.Api.Tests
{
    /// <summary>
    /// Тест мапперов в Api
    /// </summary>
    public class TestMapperApi
    {
        /// <summary>
        /// Тесты на маппер Api
        /// </summary>
        [Fact]
        public void TestMap()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new ApiProfile());
            });

            config.AssertConfigurationIsValid();
        }
    }
}