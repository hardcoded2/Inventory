using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Pipelines;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.WebUtilities;

namespace WebApplication
{
    public class MultipartResponse
    {
        private static readonly Encoding encoding = Encoding.UTF8;
        private HttpResponse m_Response;

        public MultipartResponse(HttpResponse response)
        {
            m_Response = response;
        }

        public async Task AsBody(byte[] bytes)
        {
            await m_Response.BodyWriter.WriteAsync(bytes);
        }

        public async Task AsBody(string str)
        {
            var strBytes = System.Text.Encoding.UTF8.GetBytes(str);
            await AsBody(strBytes);
        }


        public async Task SendAsFileBody(string stringData)
        {
            await SendAsFileBody(encoding.GetBytes(stringData));
        }
        public async Task SendAsFileBody(byte[] data)
        {
            HeadersToOctetStream(m_Response.Headers);
            await m_Response.BodyWriter.WriteAsync(data);

            await m_Response.CompleteAsync();
        }
        //not working, not sure how to get multipartformdatacontent to work with the new piep
        //example of expected output: https://ec.haxx.se/http/http-multipart
        public async Task SendAsMultipartBytes(string fileName,byte[] data)
        {
            MultipartFormDataContent formContent = FileContentAsForm(fileName,data);
            var stringbytes2 = await formContent.ReadAsByteArrayAsync();

            HeadersToOctetStream(m_Response.Headers);
            await m_Response.WriteAsync(encoding.GetString(stringbytes2));

            await m_Response.CompleteAsync();
        }

        private MultipartFormDataContent FileContentAsForm(string fileName,byte[] data)
        {
            MultipartFormDataContent formContent = new MultipartFormDataContent("--");
            ByteArrayContent byteArray = new ByteArrayContent(data);
            formContent.Add(byteArray, "file", fileName);
            return formContent;
        }
        private async Task DebugPrintForm(MultipartFormDataContent formContent)
        {
            var stringbytes2 = await formContent.ReadAsByteArrayAsync();
            Console.WriteLine(encoding.GetString(stringbytes2));
        }
        //not multipart
        private void HeadersToOctetStream(IHeaderDictionary headers)
        {
            const string contentType = "Content-Type";
            const string octetStream = "application/octet-stream";
            if(headers.ContainsKey(contentType))
                headers.Remove(contentType);
            headers.Add(contentType, octetStream);
        }
        public async Task SendAsMultipartBytes(string fileName,string data)
        {
            await SendAsMultipartBytes(fileName, encoding.GetBytes(data));
        }
    }
}
