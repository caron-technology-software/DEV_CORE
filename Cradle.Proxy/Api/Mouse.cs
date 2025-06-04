//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using Microsoft.AspNetCore.Mvc;
//using System.Threading.Tasks;

//using ProRob.WebApi.Auth;

//namespace Cradle.Proxy
//{
//    [Route("mouse")]
//    public class MouseController : ProxyApiController
//    {
//        [HttpGet]
//        [Route("mouse")]
//        public IHttpActionResult Mouse(int x, int y)
//        {
//            Console.WriteLine($"[Api] Mouse({x},{y})");

//            Task.Run(() =>
//            {
//                ProRob.Devices.Mouse.ExecuteClick(x, y);
//            });

//            return Ok();
//        }
//    }
//}