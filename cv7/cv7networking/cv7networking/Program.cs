using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using MailKit.Net.Smtp;
using Microsoft.VisualBasic;
using MimeKit;

namespace cv7networking
{
    class Forecast
    {
        public string init { get; set; }
        public Item[] dataseries { get; set; }
    }

    class Item
    {
        public int timepoint { get; set; }
        public int temp2m { get; set; }
    }

    class Program
    {
        static async Task Main(string[] args)
        {
            HttpListener listener = new HttpListener();
            listener.Prefixes.Add("http://localhost:8080");
            listener.Start();

            while (true)
            {
                var ctx = listener.GetContext();
                string path = ctx.Request.Url.LocalPath;
                using StreamWriter sw = new StreamWriter(ctx.Response.OutputStream);
                switch (path)
                {
                    case "/":
                        ctx.Response.Headers.Add("Content-Type","text/html");

                        sw.Write("Funguje: <strong> "+ DateTime.Now + "</strong>");
                        sw.Write("<br>"+path);
                        break;

                    case "/products":
                        ctx.Response.Headers.Add("Content-Type","aplication/json");

                        sw.Write(JsonSerializer.Serialize(new object[] {
                        new {name = "kolo", price = 100}}
                        ));
                       
                        break;
                    case "/donwlaod":
                        ctx.Response.Headers.Add("Content-Type","aplication/octet-stream");
                        ctx.Response.Headers.Add("Content-Disposition", "attachment";filename=\"soubor.pdf);
                        using (FileStream fs = new FileStream("asp.net.pdf",FileMode.Open))
                        {
                            fs.CopyTo(ctx.Response.OutputStream);
                        }
                       
                        break;
                    default:
                        ctx.Response.StatusCode = 404;
                        break;

                }

                

            }
            

            /*
            //mailkit
            using MimeMessage msg = new MimeMessage();
            msg.From.Add(new MailboxAddress("Jan","atnet2019@seznam.cz "));
            msg.To.Add(new MailboxAddress("Janoušek","jan.janousek@vsb.cz"));
            msg.Subject = "Předmět test";

            BodyBuilder builder = new BodyBuilder();
            builder.TextBody = "Text emailu";
            builder.HtmlBody = "Text <strong>emailu</strong>";

            using HttpClient httpClient = new HttpClient();
            using Stream stream = await httpClient.GetStreamAsync("https://www.7timer.info/bin/astro.php?lon=18.160005506399536&lat=49.831015379859586&ac=0&unit=metric&output=json&tzshift=0");
            builder.Attachments.Add("test.json", stream);


            msg.Body = builder.ToMessageBody();

            using SmtpClient client = new SmtpClient();
            await client.ConnectAsync("smtp.seznam.cz", 465, true);
            await client.AuthenticateAsync("atnet2019@seznam.cz", "123abcd");

            await client.SendAsync(msg);
            await client.DisconnectAsync(true);
            */




            /*
            //HttpClient
            string url = "https://katedrainformatiky.cz";
            string url2 = "https://www.7timer.info/bin/astro.php?lon=18.160005506399536&lat=49.831015379859586&ac=0&unit=metric&output=json&tzshift=0";
            
            using HttpClient client = new HttpClient();

            //Varianta A
            //string json = await client.getStringAsync(url2);
            
            //Varianta B
            using HttpRequestMessage req = new HttpRequestMessage(HttpMethod.Get, url2);
            using HttpResponseMessage response = await client.SendAsync(req);
            if (response.StatusCode != HttpStatusCode.OK)
            {
                Console.WriteLine("Došlo k chybě");
                return;
            }

            string json = await response.Content.ReadAsStringAsync();

            Forecast forecast = JsonSerializer.Deserialize<Forecast>(json);
            DateTime date = DateTime.ParseExact(forecast.init, "yyyyMMddHH", CultureInfo.InvariantCulture);
            foreach (var item in forecast.dataseries)
            {
                Console.WriteLine($"{date.AddHours(item.timepoint)}: {item.temp2m} C");
            }
            */

            /* pred ukolem 1
            using HttpRequestMessage req = new HttpRequestMessage(HttpMethod.Post, url);
            req.Headers.Add("Accept", "application/json");
            req.Headers.Authorization = AuthenticationHeaderValue.Parse("Bearer DFHJSHFXFF");
            req.Content = new StringContent("{\"a\":5}", Encoding.UTF8, "aplication/json");

            req.Content = new FormUrlEncodedContent(new Dictionary<string?, string?>()
            {
                {"nazevPromene", "hodnota"},
                {"test", "val"}
            });


            //MultipartContent multi = new MultipartContent();
            //multi.Add(new StreamContent(File.OpenRead("test.jpg")),"file1");
            //req.Content = multi;



            using HttpResponseMessage response = await client.SendAsync(req);

            //using HttpResponseMessage response = await client.GetAsync(url);
            if (response.StatusCode != HttpStatusCode.OK)
            {
                Console.WriteLine("Došlo k chybě");
                return;
            }
            string header = response.Headers.GetValues("Server").FirstOrDefault();

            string txt = await response.Content.ReadAsStringAsync();
            //using Stream txt = await client.GetStreamAsync(url);

            //using FileStream fs = new FileStream("test.html", FileMode.Create);
            //await txt.CopyToAsync(fs);
            Console.WriteLine(txt);
            */

        }
    }
}
