using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Pipelines;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
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

        public async Task AsMultipartFile(string str)
        {
            var strBytes = System.Text.Encoding.UTF8.GetBytes(str);
            await m_Response.BodyWriter.WriteAsync(strBytes);
        }

        /*
    private static byte[] GetMultipartFormData(byte[] file, string boundary)
    {
        Stream formDataStream = new System.IO.MemoryStream();
        string filename = "file";
        string fileParam = "fileParam";
        string contentType = "application/octet-stream";
        string header = $"Content-Disposition: form-data; name=\"{fileParam}\"; filename=\"{fileParam}\"Content-Type: {contentType}\r\n\r\n";
        
        bool needsCLRF = false;

        foreach (var param in postParameters)
        {
            // Thanks to feedback from commenters, add a CRLF to allow multiple parameters to be added.
            // Skip it on the first parameter, add it to subsequent parameters.
            if (needsCLRF)
                formDataStream.Write(encoding.GetBytes("\r\n"),0 , encoding.GetByteCount("\r\n"));

            needsCLRF = true;

            if (param.Value is FileParameter)
            {
                FileParameter fileToUpload = (FileParameter)param.Value;

                // Add just the first part of this param, since we will write the file data directly to the Stream
                string header = string.Format("--{0}\r\nContent-Disposition: form-data; name=\"{1}\"; filename=\"{2}\"\r\nContent-Type: {3}\r\n\r\n",
                    boundary,
                    param.Key,
                    fileToUpload.FileName ?? param.Key,
                    fileToUpload.ContentType ?? "application/octet-stream");

                formDataStream.Write(encoding.GetBytes(header), 0, encoding.GetByteCount(header));

                // Write the file data directly to the Stream, rather than serializing it to a string.
                formDataStream.Write(fileToUpload.File,0 , fileToUpload.File.Length);
            }
            else
            {
                string postData = string.Format("--{0}\r\nContent-Disposition: form-data; name=\"{1}\"\r\n\r\n{2}",
                    boundary,
                    param.Key,
                    param.Value);
                formDataStream.Write(encoding.GetBytes(postData),0 , encoding.GetByteCount(postData));
            }
        }

        // Add the end of the request.  Start with a newline
        string footer = "\r\n--" + boundary + "--\r\n";
        formDataStream.Write(encoding.GetBytes(footer),0 , encoding.GetByteCount(footer));

        // Dump the Stream into a byte[]
        formDataStream.Position = 0;
        byte[] formData = new byte[formDataStream.Length];
        formDataStream.Read(formData, 0, formData.Length);
        formDataStream.Close();

        return formData;
    }    
    */
        private async Task Foo()
        {
            string fileName = "File";
            byte[] data=encoding.GetBytes("FOO");
            MultipartFormDataContent formContent = new MultipartFormDataContent();
            ByteArrayContent byteArray = new ByteArrayContent(data);
            formContent.Add(byteArray, "file", fileName);
            m_Response.Body = await formContent.ReadAsStreamAsync();
            await m_Response.Body.FlushAsync();
        }
        public async Task SendAsMultipartBytes(string fileName,byte[] data)
        {
            MultipartFormDataContent formContent = new MultipartFormDataContent();
            ByteArrayContent byteArray = new ByteArrayContent(data);
            formContent.Add(byteArray, "file", fileName);
            
            var formContentStream = await formContent.ReadAsStreamAsync();
            
            byte[] formBytes = new byte[formContentStream.Length];
            
            //assuming read all at once
            await formContentStream.ReadAsync(formBytes);
            var stringbytes2 = await formContent.ReadAsStringAsync();
            Console.WriteLine("TEST1");
            Console.WriteLine(encoding.GetString(formBytes));
            Console.WriteLine("TEST2");
            Console.WriteLine(stringbytes2);
            Console.WriteLine("TEST3");
            m_Response.Body = formContentStream; //is this where it belongs? how about headers?
            await m_Response.StartAsync();
            await m_Response.Body.FlushAsync(); //afaik this should send the response
            await m_Response.CompleteAsync();

            
            Console.WriteLine("STUFF");
        }
        public async Task SendAsMultipartBytes(string fileName,string data)
        {
            await SendAsMultipartBytes(fileName, encoding.GetBytes(data));
        }
    }
}
