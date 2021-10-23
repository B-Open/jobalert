using Shared.Models;
using System;
using HtmlAgilityPack;
using HtmlAgilityPack.CssSelectors.NetCore;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Shared.Services
{
    public class ScraperServiceFactory
    {

        public string ScraperProvider { get; set; }

        public IScraperService build()
        {
            if (this.ScraperProvider == "jobcenter") {
                return new JobcenterScraperService();
            }
            return new JobcenterScraperService();
        }
    }
}