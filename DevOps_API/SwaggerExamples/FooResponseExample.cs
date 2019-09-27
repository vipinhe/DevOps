using System;
using DevOps_API.Models;
using Swashbuckle.AspNetCore.Filters;

namespace DevOps_API.SwaggerExamples
{
    public class FooResponseExample : IExamplesProvider
    {
        public object GetExamples()
        {
            return new Foo
            {
                Id = new Random().Next(),
                Value = Guid.NewGuid().ToString().Remove(6)
            };
        }
    }
}