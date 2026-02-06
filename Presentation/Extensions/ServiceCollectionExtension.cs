using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Presentation.Filters;
using System.Text.Json.Serialization;

namespace Presentation.Extensions
{
    public static class ServiceCollectionExtension
    {
        extension(IServiceCollection services)
        {
            public IServiceCollection AddPresentationControllers()
            {
                services.AddValidators();

                services.AddControllers(options =>
                    {
                        options.Filters.Add<ValidationActionFilter>();
                    })
                    .AddApplicationPart(typeof(AssemblyMarker).Assembly)
                    .AddJsonOptions(options => options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));

                return services;
            }

            private void AddValidators()
            {
                services.AddValidatorsFromAssemblyContaining<AssemblyMarker>();
            }
        }
    }
}