using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dotnetmvc.DataAccess;
using dotnetmvc.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace dotnetmvctest.Controllers
{
    public class HomeController : Controller
    {
        private readonly int TimesBetterThanWindows;
        private readonly string ConfigSampleString;

        private readonly BuildInfoConfig buildConfig;
        private IBusinessMan businessBro;
        private readonly ISampleDataAccess sampleDataAccess;
        public HomeController(IOptions<TestConfig> config,
            IOptions<BuildInfoConfig> buildConfig,
            ISampleDataAccess dataAccess,
            IBusinessMan businessMan)
        {
            ConfigSampleString = config.Value.SampleString;
            TimesBetterThanWindows = config.Value.SampleInt;
            businessBro = businessMan;
            this.buildConfig = buildConfig.Value;
            sampleDataAccess = dataAccess;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["GitBranch"] = buildConfig.GitBranch;
            ViewData["GitRevision"] = buildConfig.GitHash;
            ViewData["BuildDate"] = buildConfig.BuildDate.ToString("F");

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = string.Format("New MVC is {0} times better than the old one. You can get it for only ${1}",
               TimesBetterThanWindows,
               businessBro.GetSwag());

            ViewData["DbData"] = sampleDataAccess.GetAttachmentName(1200);
           
            try {
                SampleDTO dto = GetPostedData();
                ViewData["SampleResultID"] = dto.id;
                ViewData["SampleResultBody"] = dto.body;
            } catch(Exception e) {
                ViewData["Error"] = e.Message;
            }

            return View();
        }

        private SampleDTO GetPostedData() {
            RestSharpClient client = new RestSharpClient();
            dynamic requestBody = new {
                title = "foo",
                body = "bar",
                userId = 1
            };
            return client.Post<SampleDTO>("/", requestBody);
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
