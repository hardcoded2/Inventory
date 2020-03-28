using System;
using System.Collections.Generic;
using System.IO.Pipelines;
using System.Linq;
using System.Net.Http;
using System.Text.Unicode;
using System.Threading.Tasks;
using Google.Protobuf;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.WebUtilities;
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
                    await SendString(response.BodyWriter,GetTestString());
                    
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
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/testrawstringheader", async context =>
                {
                    var response = context.Response;
                    var multi = new MultipartContent();
                    //var streamProvider = new FileMultipartSection(new MultipartSection(GetTestProtoBytes()));
                    //await response.Headers. .BodyWriter.WriteAsync(GetTestProtoBytes());
                    using (MultipartFormDataContent multipartFormDataContent = new MultipartFormDataContent())
                    {
                        //multipartFormDataContent.Add(new ByteArrayContent(GetTestProtoBytes()),"File");
                        multipartFormDataContent.Add(new StringContent(GetTestString()),"File");
                        response.BodyWriter.WriteAsync(multipartFormDataContent);
                        /*
                        multipartFormDataContent.Add(new StringContent(JsonConvert.SerializeObject(myFile), Encoding.UTF8, "application/json"), nameof(MyFile));

                        HttpResponseMessage httpResult = await httpClient.PostAsync("api/uploaddownload/upload", multipartFormDataContent).ConfigureAwait(false);

                        httpResult.EnsureSuccessStatusCode();
                        stream = await httpResult.Content.ReadAsStreamAsync().ConfigureAwait(false);
                        */
                    }
                    
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

        private async Task SendString(PipeWriter writer, string str)
        {
            var strBytes = System.Text.Encoding.UTF8.GetBytes(str);
            await writer.WriteAsync(strBytes);
            Console.WriteLine($"wrote text body {str}");
        }
        private async Task SendStringAsAttachment(HttpContext context, string str)
        {
            var strBytes = System.Text.Encoding.UTF8.GetBytes(str);
            await writer.WriteAsync(strBytes);
            Console.WriteLine($"wrote text body {str}");
        }
        public static string GetTestString()
        {
            return "HELLO WORLD";
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
