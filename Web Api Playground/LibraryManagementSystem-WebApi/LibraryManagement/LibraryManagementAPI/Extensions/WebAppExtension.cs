using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Serilog;

namespace LibraryManagement.Api.Extensions
{
    public static class WebAppExtension
    {
        public static void CreateMiddlewarePipeline(this WebApplication app)
        {
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                var provider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();
                app.UseSwagger();
                app.UseSwaggerUI(options =>
                {
                    foreach (var description in provider.ApiVersionDescriptions)
                    {
                        options.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json", description.GroupName.ToUpperInvariant());
                    }
                });
            }
            app.UseHttpsRedirection();
            app.UseSerilogRequestLogging();
            app.UseAuthorization();
            app.MapControllers();
        }
    }
}