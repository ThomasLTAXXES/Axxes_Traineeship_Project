using Newtonsoft.Json;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Who.DAL.DatabaseInitialize;
using Who.DAL.Repositories;
using Who.Data;

namespace Who.DAL.Setup
{
    public class Program
    {
        // TODO: move to config
        const string PICTURE_URL_FROM_AD = "https://graph.microsoft.com/v1.0/users/{0}/photo/$value";
        const string ALL_USERS_FROM_AD_URL = "https://graph.microsoft.com/v1.0/users";
        const string PICTURE_URL_LOCAL_GET = "http://localhost:60589/api/Pictures/{0}";
        const string PICTURE_URL_LOCAL_SAVE = "http://localhost:60589/api/Pictures?fileName={0}";


        static void Main(string[] args)
        {
            Stopwatch sw = Stopwatch.StartNew();
            ConcurrentBag<Picture> pictures = new ConcurrentBag<Picture>();
            int failedPictures = 0;
            dynamic usersFromAD = GetAllUsersFromAD();
            Parallel.ForEach((IEnumerable<dynamic>)usersFromAD.value, (personObject) =>
            {
                string id = null != personObject.objectId ? personObject.objectId.ToString() : personObject.id.ToString();
                byte[] picture = DownloadPictureFromAD(id);
                if (null != picture)
                {
                    // Save it in our own store (file system api)
                    PostPicture(picture, new Uri(string.Format(PICTURE_URL_LOCAL_SAVE, id)), "multipart/form-data").Wait();
                    pictures.Add(new Picture { Url = string.Format(PICTURE_URL_LOCAL_GET, id), Name = personObject.displayName });
                }
                else
                {
                    ++failedPictures;
                }

            });
            Console.WriteLine("Saving data");
            SavePictures(pictures.ToList());

            sw.Stop();

            Console.WriteLine("Done migrating data");
            Console.WriteLine($"We imported {pictures.Count} pictures");
            Console.WriteLine($"We failed to import {failedPictures} pictures");
            Console.WriteLine($"This operation took {sw.ElapsedMilliseconds} miliseconds");
            Console.ReadKey();

        }

        private static byte[] DownloadPictureFromAD(string id)
        {
            string pictureFromADUrl = string.Format(PICTURE_URL_FROM_AD, id);
            using (WebClient client = new WebClient())
            {
                try
                {
                    AddHeadersToWebClient(client);
                    return client.DownloadData(pictureFromADUrl);
                }
                catch (Exception ex)
                {
                    // Currently we are ignoring it because not everyone has a picture, but we are logging the total failures to the console
                    return null;
                }
            }
        }

        private static async Task<string> PostPicture(byte[] data, Uri url, string contentType)
        {
            HttpContent content = new ByteArrayContent(data);

            content.Headers.ContentType = new MediaTypeHeaderValue(contentType);

            using (var form = new MultipartFormDataContent())
            {
                form.Add(content);

                using (var client = new HttpClient())
                {
                    var response = await client.PostAsync(url, form);
                    return await response.Content.ReadAsStringAsync();
                }
            }
        }

        private static dynamic GetAllUsersFromAD()
        {
            dynamic objects;
            using (WebClient client = new WebClient())
            {
                AddHeadersToWebClient(client);

                string json = client.DownloadString(ALL_USERS_FROM_AD_URL);
                objects = JsonConvert.DeserializeObject<dynamic>(json);
            }

            return objects;
        }

        static void SavePictures(List<Picture> pictures)
        {
            ImageRepository repository = new ImageRepository();
            repository.CreateAll(pictures.Select(p => new ImageEntity { Name = p.Name, Url = p.Url }));
        }

        static void AddHeadersToWebClient(WebClient client)
        {
            client.Headers.Clear();
            client.Headers.Add(HttpHeaderSettings.Accept.NAME, HttpHeaderSettings.Accept.VALUE);
            client.Headers.Add(HttpHeaderSettings.AcceptEncoding.NAME, HttpHeaderSettings.AcceptEncoding.VALUE);
            client.Headers.Add(HttpHeaderSettings.AcceptLanguage.NAME, HttpHeaderSettings.AcceptLanguage.VALUE);
            // ReadMe.txt for extra info regarding the Authorization header/bearer token (Section: How to get a valid bearer token)
            client.Headers.Add(HttpHeaderSettings.Authorization.NAME, HttpHeaderSettings.Authorization.VALUE);

        }
    }
}
