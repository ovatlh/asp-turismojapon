using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCore.Reporting;
using aspTurismoJapon.Areas.Administrador.Models.ViewModels;
using aspTurismoJapon.Areas.Administrador.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace aspTurismoJapon.Areas.Administrador.Controllers
{
    [Area("Administrador")]
    //[Authorize(Roles = "Admin")]
    public class HomeController : Controller
    {
        public IHostingEnvironment Environment { get; set; }

        public HomeController(IHostingEnvironment env)
        {
            Environment = env;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Importar()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Importar(ImportarViewModel importarViewModel)
        {
            if (importarViewModel.Tipo == 0)
            {
                ModelState.AddModelError("", "Seleccione el TIPO de registro que desea importar");
                return View(importarViewModel);
            }
            else if (importarViewModel.Archivo == null)
            {
                ModelState.AddModelError("", "Seleccione un ARCHIVO");
                return View(importarViewModel);
            }
            else if(importarViewModel.Archivo.ContentType != "text/csv" && !importarViewModel.Archivo.FileName.EndsWith(".csv")
                && importarViewModel.Archivo.ContentType != "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet" && !importarViewModel.Archivo.Name.EndsWith(".xlsx"))
            {
                ModelState.AddModelError("", "Seleccione el ARCHIVO de registros que desea importar (Excel o CSV)");
                return View(importarViewModel);
            }
            else if (importarViewModel.Archivo.Length > 1024 * 1024 * 5)
            {
                ModelState.AddModelError("", "El tamaño MAXIMO del archivo es de [ 5 MB ]");
                return View(importarViewModel);
            }
            else
            {
                try
                {
                    //Todo bien con el archivo
                    ImportarRepository importarRepository = new ImportarRepository();
                    importarRepository.Importar(importarViewModel.Tipo ,importarViewModel.Archivo, Environment.WebRootPath);

                    ModelState.AddModelError("", "Importacion realizada con exito");
                    return View(importarViewModel);
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                    return View(importarViewModel);
                }
            }
        }

        public IActionResult Exportar()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Exportar(ExportarViewModel exportarViewModel)
        {
            if (exportarViewModel.Registro == 0)
            {
                ModelState.AddModelError("", "Seleccione el tipo de registro que desea exportar");
                return View(exportarViewModel);
            }
            
            if (exportarViewModel.Tipo == 0)
            {
                ModelState.AddModelError("", "Seleccione el tipo de exportacion");
                return View(exportarViewModel);
            }

            System.Text.UTF8Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
            ExportarRepository exportarRepository = new ExportarRepository();
            LocalReport localReport;
            ReportResult reportResult;

            /*
             * Tipos
             * 1 - Ciudades
             * 2 - Comidas
             * 3 - Tipo de Atracciones
             * 4 - Atracciones
             * */
            try
            {
                switch (exportarViewModel.Registro)
                {
                    case 1:
                        localReport = new LocalReport($"{Environment.WebRootPath}/reportes/reportCiudades.rdlc");
                        var datosCiudades = exportarRepository.GetReporteDatos_Ciudades();
                        localReport.AddDataSource("dsCiudades", datosCiudades);

                        if (exportarViewModel.Tipo == 1)
                        {
                            reportResult = localReport.Execute(RenderType.Pdf);
                            ModelState.AddModelError("", "Exportacion de [ Ciudades ] realizada con exito");
                            //return File(reportResult.MainStream, "application/pdf", "ciudades.pdf");
                            return File(reportResult.MainStream, "application/pdf");
                        }
                        else
                        {
                            //reportResult = localReport.Execute(RenderType.Excel);
                            reportResult = localReport.Execute(RenderType.ExcelOpenXml);
                            ModelState.AddModelError("", "Exportacion de [ Ciudades ] realizada con exito");
                            return File(reportResult.MainStream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "ciudades.xlsx");
                        }
                    case 2:
                        localReport = new LocalReport($"{Environment.WebRootPath}/reportes/reportComidas.rdlc");
                        var datosComidas = exportarRepository.GetReporteDatos_Comidas();
                        localReport.AddDataSource("dsComidas", datosComidas);

                        if (exportarViewModel.Tipo == 1)
                        {
                            reportResult = localReport.Execute(RenderType.Pdf);
                            ModelState.AddModelError("", "Exportacion de [ Comidas ] realizada con exito");
                            //return File(reportResult.MainStream, "application/pdf", "comidas.pdf");
                            return File(reportResult.MainStream, "application/pdf");
                        }
                        else
                        {
                            //reportResult = localReport.Execute(RenderType.Excel);
                            reportResult = localReport.Execute(RenderType.ExcelOpenXml);
                            ModelState.AddModelError("", "Exportacion de [ Comidas ] realizada con exito");
                            return File(reportResult.MainStream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "comidas.xlsx");
                        }
                    case 3:
                        localReport = new LocalReport($"{Environment.WebRootPath}/reportes/reportTipoAtracciones.rdlc");
                        var datosTipoAtracciones = exportarRepository.GetReporteDatos_TipoAtracciones();
                        localReport.AddDataSource("dsTipoAtracciones", datosTipoAtracciones);

                        if (exportarViewModel.Tipo == 1)
                        {
                            reportResult = localReport.Execute(RenderType.Pdf);
                            ModelState.AddModelError("", "Exportacion de [ Tipos de Atracciones ] realizada con exito");
                            //return File(reportResult.MainStream, "application/pdf", "tipoatracciones.pdf");
                            return File(reportResult.MainStream, "application/pdf");
                        }
                        else
                        {
                            //reportResult = localReport.Execute(RenderType.Excel);
                            reportResult = localReport.Execute(RenderType.ExcelOpenXml);
                            ModelState.AddModelError("", "Exportacion de [ Tipos de Atracciones ] realizada con exito");
                            return File(reportResult.MainStream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "tipoatracciones.xlsx");
                        }
                    case 4:
                        localReport = new LocalReport($"{Environment.WebRootPath}/reportes/reportAtracciones.rdlc");
                        var datosAtracciones = exportarRepository.GetReporteDatos_Atracciones();
                        localReport.AddDataSource("dsAtracciones", datosAtracciones);

                        if (exportarViewModel.Tipo == 1)
                        {
                            reportResult = localReport.Execute(RenderType.Pdf);
                            ModelState.AddModelError("", "Exportacion de [ Atracciones ] realizada con exito");
                            //return File(reportResult.MainStream, "application/pdf", "atracciones.pdf");
                            return File(reportResult.MainStream, "application/pdf");
                        }
                        else
                        {
                            //reportResult = localReport.Execute(RenderType.Excel);
                            reportResult = localReport.Execute(RenderType.ExcelOpenXml);
                            ModelState.AddModelError("", "Exportacion de [ Atracciones ] realizada con exito");
                            return File(reportResult.MainStream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "atracciones.xlsx");
                        }
                    default:
                        ModelState.AddModelError("", "Seleccione tipos validos");
                        return View(exportarViewModel);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(exportarViewModel);
            }
        }
    }
}