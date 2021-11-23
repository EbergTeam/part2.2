using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1
{
    // создание своего компонента middleware в виде отдельного класса
    public class TokenMiddleware
    {
        RequestDelegate _next;
        string _pattern;

        // класс middleware должен иметь конструктор, который принимает параметр типа RequestDelegate
        // через этот параметр можно получить ссылку на тот делегат запроса, который стоит следующим в конвейере обработки запроса
        public TokenMiddleware(RequestDelegate next, string pattern)
        {
            _pattern = pattern;
            _next = next;
        }

        // nакже в классе должен быть определен метод, который должен называться либо Invoke, либо InvokeAsync
        // причем этот метод должен возвращать объект Task и принимать в качестве параметра контекст запроса - объект HttpContext
        // данный метод собственно и будет обрабатывать запрос
        public async Task InvokeAsync (HttpContext context)
        {
            if (context.Request.Query["token"] != _pattern)
            {
                await context.Response.WriteAsync("ERROR");
            }
            else
            {
                await _next.Invoke(context);
            }
        }
    }
}
