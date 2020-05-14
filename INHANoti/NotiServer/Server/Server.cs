// This source code is a part of Inha Univ AlarmBot.
// Copyright (C) 2020. rollrat. Licensed under the MIT Licence.

using EmbedIO;
using EmbedIO.Actions;
using EmbedIO.Routing;
using EmbedIO.WebApi;
using NotiServer.Utils;
using Swan.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace NotiServer.Server
{
    /// <summary>
    /// Inha Alarmbot(Notification Server) HTTP Server for API
    /// </summary>
    public class Server : ILazy<Server>
    {
        /// <summary>
        /// Enter the loop-back address of nginx.
        /// </summary>
        /// <param name="port"></param>
        public async void StartServer(int port)
        {
            Logger.UnregisterLogger<ConsoleLogger>();
            using (var server = CreateWebServer($"http://0.0.0.0:{port}/"))
            {
                await server.RunAsync();
            }
        }

        private static WebServer CreateWebServer(string url)
        {
            var server = new WebServer(o => o
                    .WithUrlPrefix(url)
                    .WithMode(HttpListenerMode.EmbedIO))
                .WithLocalSessionManager()
                .WithWebApi("/api", m => m
                    .WithController<ServerAPI>())
                .WithModule(new ActionModule("/", HttpVerbs.Any, ctx => ctx.SendStringAsync("Inha Alarmbot(Notification Server) HTTP Server", "text/html", Encoding.UTF8)));

            return server;
        }
    }

    public class ServerAPI : WebApiController
    {
        [Route(HttpVerbs.Get, "/test")]
        public void GetData()
        {
            var dd = HttpContext.GetRequestBodyAsStringAsync().Result;
            HttpContext.SendStringAsync("Test data! " + dd, "text", Encoding.UTF8);
        }

        [Route(HttpVerbs.Get, "/mail")]
        public void GetMailList()
        {
            //var dd = HttpContext.GetRequestBodyAsStringAsync().Result;
            //if (string.IsNullOrEmpty(dd.Trim()))
            //{
            //    var mailbox_path = Path.Combine(Program.ApplicationPath, "mailbox");

            //    if (Directory.Exists(mailbox_path))
            //    {
            //        var items = MailServer.DataBase.QueryAll();
            //        HttpContext.SendStringAsync(string.Join("</br>", items.Select(x => $"From='{x.From}', To='{x.To}', Subject='{x.Title}', When='{new DateTime(Convert.ToInt64(x.DateTime), DateTimeKind.Utc)}', MailBox='{x.FileName}'")), "text/html", Encoding.UTF8);
            //    }
            //    else
            //        HttpContext.SendStringAsync("Empty", "text", Encoding.UTF8);
            //}
        }
    }
}
