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
            // âûçûâàåì ìåòîä ðàñøèðåíèÿ è ïåðåäàåì îáðàçåö òîêåíà äëÿ ñðàâíåíèÿ
            app.UseToken("1234");
            // Тест
            app.Run(async (context) =>
            {
                await context.Response.WriteAsync("It work!!!");
            });
        }
    }
}
