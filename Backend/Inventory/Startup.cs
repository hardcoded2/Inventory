using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Unicode;
using System.Threading.Tasks;
using Google.Protobuf;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace WebApplication
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context => { await context.Response.WriteAsync("Hello World!"); });
            });
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/testrawstringattach", async context =>
                {
                    var response = context.Response;
                    await response.BodyWriter.WriteAsync(System.Text.Encoding.UTF8.GetBytes("HELLO WORLD"));
                    Console.WriteLine("wrote text body");
                    
                    /*
                    //https://stackoverflow.com/questions/5895684/how-to-send-file-in-httpresponse not async though
                    response.Clear();
                    const string SaveAsFileName = "TestProto";
                    response.Headers.Add("content-disposition", "attachment;filename=" + SaveAsFileName);
                    response.ContentType = "application/octet-stream";
                    response.BinaryWrite(GetTestProto());
                    response.End();
                    */
                    //await context.Response
                    //await context.Response.WriteAsync(GetTestProto());
                });
            });
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/testproto", async context =>
                {
                    var response = context.Response;
                    await response.BodyWriter.WriteAsync(GetTestProtoBytes());
                    Console.WriteLine("wrote proto body");
                    
                    /*
                    //https://stackoverflow.com/questions/5895684/how-to-send-file-in-httpresponse not async though
                    response.Clear();
                    const string SaveAsFileName = "TestProto";
                    response.Headers.Add("content-disposition", "attachment;filename=" + SaveAsFileName);
                    response.ContentType = "application/octet-stream";
                    response.BinaryWrite(GetTestProto());
                    response.End();
                    */
                    //await context.Response
                    //await context.Response.WriteAsync(GetTestProto());
                });
            });
        }

        public static PlayerData GetTestProto()
        {
            return new PlayerData(){DataVersion = 100};
        }
        public static byte[] GetTestProtoBytes()
        {
            return GetTestProto().ToByteArray();
        }
    }
}
