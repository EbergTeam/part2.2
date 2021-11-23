using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1
{
    public static class TokenExtensions
    {
        // создаем метод расширения для типа IApplicationBuilder
        public static IApplicationBuilder UseToken(this IApplicationBuilder applicationBuilder, string pattern)
        {
            // добавление компонента middleware, который представляет класс, в конвейер обработки запроса применяется метод UseMiddleware()
            return applicationBuilder.UseMiddleware<TokenMiddleware>(pattern);
        }
    }
}
