using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using DinkToPdf;
using DinkToPdf.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace PdfGenerator.Controllers
{
    [Route("api/pdfcreator")]
    [ApiController]
    public class PdfCreatorController : ControllerBase
    {
        private IConverter _converter;

        public PdfCreatorController(IConverter converter)
        {
            _converter = converter;
        }

        [HttpGet]
        public IActionResult CreatePDF()
        {
            var globalSettings = new GlobalSettings
            {
                ColorMode = ColorMode.Color,
                Orientation = Orientation.Portrait,
                PaperSize = PaperKind.A4,
                Margins = new MarginSettings { Top = 20 },
                DocumentTitle = "Demand EO",
            };

            var objectSettings = new ObjectSettings
            {
              
                Page = "https://localhost:44338/Home/Page1", 
                WebSettings = { DefaultEncoding = "utf-8" },
                HeaderSettings = { HtmUrl = Path.Combine(Directory.GetCurrentDirectory(), "PageHeaderFooter", "DemandEoHeader.html")},
                FooterSettings = { HtmUrl= Path.Combine(Directory.GetCurrentDirectory(), "PageHeaderFooter", "DemandEoFooter.html") }
            };

            var pdf = new HtmlToPdfDocument()
            {
                GlobalSettings = globalSettings,
                Objects = { objectSettings }
            };

            var file = _converter.Convert(pdf);

            return File(file, "application/pdf", "DemandEO.pdf");
            
        }
    }
}