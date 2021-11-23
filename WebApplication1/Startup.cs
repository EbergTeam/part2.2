using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1
{
    public class Startup
    {
        public void Configure(IApplicationBuilder app)
        {
            // вызываем метод расширения и передаем образец токена для сравнения
            app.UseToken("1234");

            app.Run(async (context) =>
            {
                context.Response.ContentType = "text/plain;charset=utf-8";
                await context.Response.WriteAsync("It work!!!");
            });
        }
    }
}
