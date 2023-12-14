using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotnetSteps.Middlewares;

namespace DotnetSteps.Extensions;

public static class OffensiveWordsMiddlewareExtension
{
    public static IApplicationBuilder UseOffensiveWordsFilter(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<OffensiveWordsMiddleware>();
    }
}