﻿using Microsoft.AspNetCore.Builder;

namespace PlaygroundShared.RabbitMq
{
    public static class RawRabbitExtensions
    {
        public static IBusSubscriber UseRabbitMq(this IApplicationBuilder app)
            => new BusSubscriber(app);
    }
}