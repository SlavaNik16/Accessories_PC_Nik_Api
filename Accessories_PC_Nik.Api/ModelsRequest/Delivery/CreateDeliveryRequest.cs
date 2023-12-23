﻿namespace Accessories_PC_Nik.Api.ModelsRequest.Person
{
    public class CreateDeliveryRequest
    {
        /// <summary>
        /// Откуда привезти
        /// </summary>
        public DateTimeOffset From { get; set; }

        /// <summary>
        /// Куда привезти
        /// </summary>
        public DateTimeOffset To { get; set; }

        /// <summary>
        /// Стоимость доставки
        /// </summary>
        public decimal Price { get; set; }
    }
}
