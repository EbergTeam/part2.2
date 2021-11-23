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
        // обработка запросов методами объекта IApplicationBuilder по принципу конвейера. Компоненты конвейера - middleware
        // для конфигурации конвейера обработки запроса применяются методы Run, Map и Use
        public void Configure(IApplicationBuilder app)
        {
            // USE - добавление компонента middleware в конвейер, определенный как анонимный метод
            // может передать управление след. компоненту через await next.Invoke(), но после выполнится код после await next.Invoke()
            app.Use(async (context, next) =>
            {
                await context.Response.WriteAsync("USE BEFORE Invoke() \n");
                await next.Invoke();
                await context.Response.WriteAsync("\nUSE AFTER Invoke()");
            });

            // MAP анализирует путь запроса. Если запрос вида //localhost:44322/about/, то выполняется метод About
            app.Map("/about", About);
            // MapWhen(true/false, About); Если условие истинно, то вызываем метод About()
            
            // Вложенные методы MAP - анализирует путь запроса. В зависимости от пути вызываем нужный...
            app.Map("/index", (appBuilder) =>
            {
                // ..метод
                appBuilder.Map("/help", Help);

                //  ..анонимный метод
                appBuilder.Run(async (context) =>
                {
                    await context.Response.WriteAsync("INDEX");
                });
            });

            // RUN - добавление компонента middleware в конвейер, определенный как анонимный метод
            // не вызывает никакие другие компоненты и дальше обработку запроса не передает (является терминальным)
            app.Run(async (context) =>
            {
                var host = context.Request.Host.Value;
                var path = context.Request.Path;
                var query = context.Request.QueryString.Value;
                await context.Response.WriteAsync("RUN host=" + host + ", path " + path + ", query" + query);              
            });
        }

        private static void About(IApplicationBuilder applicationBuilder)
        {
            applicationBuilder.Run(async (context) =>
            {
                await context.Response.WriteAsync("ABOUT");
            });
        }

        private static void Help(IApplicationBuilder applicationBuilder)
        {
            applicationBuilder.Run(async (context) =>
            {
                await context.Response.WriteAsync("HELP");
            });
        }
    }
}
