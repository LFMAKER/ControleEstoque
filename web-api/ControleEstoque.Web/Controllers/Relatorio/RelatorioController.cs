using ControleEstoque.Web.Dal.Grafico;
using ControleEstoque.Web.Models.Domain;
using System;
using Rotativa;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using NPOI;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System.IO;
using NPOI.HSSF.UserModel;
using NPOI.HSSF.Util;
using ControleEstoque.Web.Dal.Relatorio;

namespace ControleEstoque.Web.Controllers.Relatorio
{
    public class RelatorioController : Controller
    {
        
        //RELATORIO ENTRADAS
        public ActionResult RelatorioEntradasIndex()
        {
            return View();
        }

        [HttpPost]
        public ActionResult RelatorioEntradasFiltro(DateTime txt_data_inicio, DateTime txt_data_fim, int cbx_tipo)
        {
            //cbx_tipo = 1, significa gerar PDF
            //cbx_tipo = 2, significa gerar XLSX

            if (cbx_tipo == 1)
            {
                //Criando o PDF
                var relatorioPDF = new ViewAsPdf
                {
                    ViewName = "RelatorioEntradasPDF",
                    IsGrayScale = false,
                    FileName = "RelatorioEntradasPDF.pdf",
                    Model = EntradaESaidaRelatorioDao.GetEntradasRelatorioFiltro(txt_data_inicio, txt_data_fim)
                };
                return relatorioPDF;
            }
            else
            {
                List<EntradaGraficos> entradas = EntradaESaidaRelatorioDao.GetEntradasRelatorioFiltro(txt_data_inicio, txt_data_fim);
                //Criando o XLSX
                IWorkbook workbook = new XSSFWorkbook();
                ISheet sheet = workbook.CreateSheet("Relatório Entradas");
                int rowNumer = 0;

                //---- HEADER
                IRow row = sheet.CreateRow(rowNumer);
                ICell cellData;
                ICell cellTotal;
                ICellStyle styleData = workbook.CreateCellStyle();
                styleData.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.Violet.Index;
                styleData.Alignment = HorizontalAlignment.Center;

                ICellStyle styleTotal = workbook.CreateCellStyle();
                styleTotal.Alignment = HorizontalAlignment.Right;
                ICreationHelper createHelper = workbook.GetCreationHelper();
                styleTotal.DataFormat = (createHelper.CreateDataFormat().GetFormat("R$ #.##0;-R$ #.##0"));


                cellData = row.CreateCell(0);
                cellData.SetCellValue("Data");
                cellData.CellStyle = styleData;

                cellTotal = row.CreateCell(1);
                cellTotal.SetCellValue("Total");
                cellTotal.CellStyle = styleTotal;
                if (entradas.Count > 0 ){
                    foreach (var item in entradas)
                    {
                        rowNumer++;
                        row = sheet.CreateRow(rowNumer);
                        cellData = row.CreateCell(0);
                        cellData.SetCellValue(item.Data.ToString("dd/MM/yyyy"));
                        cellData.CellStyle = styleData;

                        cellTotal = row.CreateCell(1);
                        cellTotal.SetCellValue((double)item.total);
                        cellTotal.CellStyle = styleTotal;
                    }
                }else
                {
                    rowNumer++;
                    row = sheet.CreateRow(rowNumer);
                    cellData = row.CreateCell(0);
                    cellData.SetCellValue("SEM DADOS NESSE PERIODO");
                    cellData.CellStyle = styleData;

                    cellTotal = row.CreateCell(1);
                    cellTotal.SetCellValue(0.00);
                    cellTotal.CellStyle = styleTotal;
                }
                //Tamanho das colunas
                sheet.SetColumnWidth(0, 40 * 256);
                sheet.SetColumnWidth(1, 20 * 256);



                MemoryStream stream = new MemoryStream();
                workbook.Write(stream);
                return File(stream.ToArray(), //The binary data of the XLS file
        "application/vnd.ms-excel", //MIME type of Excel files
        string.Format("RelatorioEntradas_{0}.xlsx", DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss"))); //Suggested file name in the "Save as" dialog which will be displayed to the end user
            }
        }


        public async Task<ActionResult> RelatorioEntradas()
        {
            List<EntradaGraficos> entradas = await EntradaESaidaGraficoDao.GetEntradasGrafico();
            return View(entradas);
        }




        //RELATÓRIO SAIDA

        public ActionResult RelatorioSaidasIndex()
        {
            return View();
        }


        //GetEntradasGraficoFiltro
        [HttpPost]
        public ActionResult RelatorioSaidasFiltro(DateTime txt_data_inicio, DateTime txt_data_fim, int cbx_tipo)
        {
            //cbx_tipo = 1, significa gerar PDF
            //cbx_tipo = 2, significa gerar XLSX

            if (cbx_tipo == 1)
            {
                //Criando o PDF
                var relatorioPDF = new ViewAsPdf
                {
                    ViewName = "RelatorioSaidasPDF",
                    IsGrayScale = false,
                    FileName = "RelatorioSaidasPDF.pdf",
                    Model = EntradaESaidaRelatorioDao.GetSaidasRelatorioFiltro(txt_data_inicio, txt_data_fim)
                };
                return relatorioPDF;
            }
            else
            {
                List<SaidaGraficos> saidas = EntradaESaidaRelatorioDao.GetSaidasRelatorioFiltro(txt_data_inicio, txt_data_fim);
                //Criando o XLSX
                IWorkbook workbook = new XSSFWorkbook();
                ISheet sheet = workbook.CreateSheet("Relatório Entradas");
                int rowNumer = 0;

                //---- HEADER
                IRow row = sheet.CreateRow(rowNumer);
                ICell cellData;
                ICell cellTotal;
                ICellStyle styleData = workbook.CreateCellStyle();
                styleData.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.Violet.Index;
                styleData.Alignment = HorizontalAlignment.Center;

                ICellStyle styleTotal = workbook.CreateCellStyle();
                styleTotal.Alignment = HorizontalAlignment.Right;
                ICreationHelper createHelper = workbook.GetCreationHelper();
                styleTotal.DataFormat = (createHelper.CreateDataFormat().GetFormat("R$ #.##0;-R$ #.##0"));


                cellData = row.CreateCell(0);
                cellData.SetCellValue("Data");
                cellData.CellStyle = styleData;

                cellTotal = row.CreateCell(1);
                cellTotal.SetCellValue("Total");
                cellTotal.CellStyle = styleTotal;
                if (saidas.Count > 0)
                {
                    foreach (var item in saidas)
                    {
                        rowNumer++;
                        row = sheet.CreateRow(rowNumer);
                        cellData = row.CreateCell(0);
                        cellData.SetCellValue(item.Data.ToString("dd/MM/yyyy"));
                        cellData.CellStyle = styleData;

                        cellTotal = row.CreateCell(1);
                        cellTotal.SetCellValue((double)item.total);
                        cellTotal.CellStyle = styleTotal;
                    }
                }
                else
                {
                    rowNumer++;
                    row = sheet.CreateRow(rowNumer);
                    cellData = row.CreateCell(0);
                    cellData.SetCellValue("SEM DADOS NESSE PERIODO");
                    cellData.CellStyle = styleData;

                    cellTotal = row.CreateCell(1);
                    cellTotal.SetCellValue(0.00);
                    cellTotal.CellStyle = styleTotal;
                }
                //Tamanho das colunas
                sheet.SetColumnWidth(0, 40 * 256);
                sheet.SetColumnWidth(1, 20 * 256);



                MemoryStream stream = new MemoryStream();
                workbook.Write(stream);
                return File(stream.ToArray(), //The binary data of the XLS file
        "application/vnd.ms-excel", //MIME type of Excel files
        string.Format("RelatorioSaidas_{0}.xlsx", DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss"))); //Suggested file name in the "Save as" dialog which will be displayed to the end user
            }
        }


        public async Task<ActionResult> RelatorioSaidas()
        {
            List<SaidaGraficos> saidas = await EntradaESaidaGraficoDao.GetSaidasGrafico();
            return View(saidas);
        }




    }
}