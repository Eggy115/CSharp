using System;
using Microsoft.AspNetCore.Mvc;

class Program {
    static void Main(string[] args) {
        var host = new WebHostBuilder()
            .UseKestrel()
            .UseStartup<Startup>()
            .Build();
        host.Run();
    }
}

class Startup {
    public void ConfigureServices(IServiceCollection services) {
        services.AddMvc();
    }

    public void Configure(IApplicationBuilder app) {
        app.UseMvc();
    }
}

[Route("api/[controller]")]
public class HelloWorldController : Controller {
    [HttpGet]
    public string Get() {
        return "Hello, world!";
    }
}
