using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ControleEstoque.Web.Controllers.Relatorio
{
    public class RelatorioController : Controller
    {
        // GET: Relatorio
        /*
         @todo: Export PDF "Relatorio"
         @body: Create a controller to return a report in pdf
             */


        public ActionResult Index()
        {
            return View();
        }

        //protected FileContentResult ViewPdf(/*string pageTitle, string viewName, object model*/)
        //{
        //    // Render the view html to a string.
        //    string htmlText = this.htmlViewRenderer.RenderViewToString();

        //    // Let the html be rendered into a PDF document through iTextSharp.
        //    byte[] buffer = standardPdfRenderer.Render("<");

        //    // Return the PDF as a binary stream to the client.
        //    return File(buffer, "application/pdf", "file.pdf");
        //}


    }
}