using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Relatorio_api.Models;
using Relatorio_api.DAO;
using Relatorio_api.Repositorio;
using System.Web.Http.Cors;
using System.Threading.Tasks;
using System.Web;
using System.IO;

namespace Relatorio_api.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class ValoresController : ApiController
    { 
        ReturnJsString jsString;
        PathFile pathFile; 

        public ValoresController()
        {
            jsString = new ReturnJsString();
            pathFile = new PathFile();
        }

        [HttpPost()]
        public async Task<string> Post()
        {
            string message = null;
            var ctx = HttpContext.Current;

            var root = ctx.Server.MapPath("~/App_Data");
            var provider =
                new MultipartFormDataStreamProvider(root);

            try
            {
                await Request.Content.ReadAsMultipartAsync(provider);

                foreach (var file in provider.FileData)
                {
                    var name = file.Headers
                        .ContentDisposition
                        .FileName;

                    name = name.Trim('"');

                    var localFileName = file.LocalFileName;
                    var filePath = System.IO.Path.Combine(root, name);

                    File.Move(localFileName, filePath);

                    pathFile.path = filePath;

                    message = jsString.returnData(pathFile);
                }
            }
            catch (Exception ex)
            {
                return ex.Message.ToString();
            }

            return message;
        }
    }
}
