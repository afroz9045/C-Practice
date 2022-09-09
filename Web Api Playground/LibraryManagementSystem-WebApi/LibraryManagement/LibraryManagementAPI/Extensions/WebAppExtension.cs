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
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseHttpsRedirection();
            app.UseSerilogRequestLogging();
            app.UseAuthorization();

            app.MapControllers();
        }
    }
}