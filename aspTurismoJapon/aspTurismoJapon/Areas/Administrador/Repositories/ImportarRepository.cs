using aspTurismoJapon.Areas.Administrador.Models.ViewModels;
using aspTurismoJapon.Models;
using aspTurismoJapon.Repositories;
using Microsoft.AspNetCore.Http;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace aspTurismoJapon.Areas.Administrador.Repositories
{
    public class ImportarRepository : Repository<ImportarViewModel>
    {
        public void Importar(int Tipo, IFormFile file, string path)
        {
            FileStream fileStream = File.Create($"{path}/temp/{Path.GetFileName(Path.GetTempFileName())}");
            try
            {
                file.CopyTo(fileStream);

                if (file.ContentType == "text/csv" || file.FileName.EndsWith(".csv"))
                {
                    ImportarCSV(Tipo, fileStream, path);
                }
                else
                {
                    ImportarExcel(Tipo, fileStream, path);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (Context.Database.CurrentTransaction != null)
                {
                    Context.Database.RollbackTransaction();
                }
                fileStream.Close();
                File.Delete(fileStream.Name);
            }
        }

        public void ImportarCSV(int Tipo, FileStream file, string ruta)
        {
            file.Position = 0;

            var reader = new StreamReader(file);

            WebClient webClient = new WebClient();

            /*
             * Tipos
             * 1 - Ciudad
             * 2 - Comida
             * 3 - Tipo de Atraccion
             * 4 - Atraccion
             * */
            string headers;
            CiudadesRepository ciudadesRepository;
            ComidasRepository comidasRepository;
            TipoAtraccionRepository tipoAtraccionRepository;
            AtraccionesRepository atraccionesRepository;

            switch (Tipo)
            {
                case 1:
                    headers = reader.ReadLine();
                    if (headers != "Nombre,Portada,Contenido")
                    {
                        throw new Exception("El orden de las columnas no es el correcto [ Nombre,Portada,Contenido ] (La portada debe ser un link de una imagen JPG)");
                    }

                    ciudadesRepository = new CiudadesRepository();

                    Context.Database.BeginTransaction();

                    while (!reader.EndOfStream)
                    {
                        var line = reader.ReadLine();
                        var values = line.Split(',');

                        if (ciudadesRepository.GetCiudadesByNombre(values[0]) == null)
                        {
                            Ciudades ciudades = new Ciudades()
                            {
                                Nombre = values[0],
                                Contenido = values[2]
                            };

                            Context.Add(ciudades);
                            Context.SaveChanges();

                            webClient.DownloadFile(values[1], $"{ruta}/images/ciudades/{ciudades.Id}.jpg");
                        }
                        else
                        {
                            throw new Exception($"Ya existe una ciudad con el nombre de [ {values[0]} ]");
                        }
                    }

                    Context.Database.CommitTransaction();
                    break;

                case 2:
                    headers = reader.ReadLine();
                    if (headers != "Nombre,Portada,Descripcion,Ciudad")
                    {
                        throw new Exception("El orden de las columnas no es el correcto [ Nombre,Portada,Descripcion,Ciudad ] (La portada debe ser un link de una imagen JPG)");
                    }

                    ciudadesRepository = new CiudadesRepository();
                    comidasRepository = new ComidasRepository();

                    Context.Database.BeginTransaction();

                    while (!reader.EndOfStream)
                    {
                        var line = reader.ReadLine();
                        var values = line.Split(',');
                        var ciudad = ciudadesRepository.GetCiudadesByNombre(values[3]);

                        if (comidasRepository.GetComidaByNombre(values[0]) == null)
                        {
                            if (ciudad == null)
                            {
                                throw new Exception($"No existe una ciudad con el nombre de [ {values[3]} ]");
                            }

                            Comidas comidas = new Comidas()
                            {
                                Nombre = values[0],
                                Descripcion = values[2],
                                IdCiudad = ciudad.Id
                            };

                            Context.Add(comidas);
                            Context.SaveChanges();

                            webClient.DownloadFile(values[1], $"{ruta}/images/comidas/{comidas.Id}.jpg");
                        }
                        else
                        {
                            throw new Exception($"Ya existe una comida con el nombre de [ {values[0]} ]");
                        }
                    }

                    Context.Database.CommitTransaction();
                    break;

                case 3:
                    headers = reader.ReadLine();
                    if (headers != "Tipo,Icono")
                    {
                        throw new Exception("El orden de las columnas no es el correcto [ Tipo,Icono ] (El icono debe ser un link de una imagen JPG)");
                    }

                    tipoAtraccionRepository = new TipoAtraccionRepository();

                    Context.Database.BeginTransaction();

                    while (!reader.EndOfStream)
                    {
                        var line = reader.ReadLine();
                        var values = line.Split(',');

                        if (tipoAtraccionRepository.GetTipoAtraccionByTipo(values[0]) == null)
                        {
                            Tipoatraccion tipoatraccion = new Tipoatraccion()
                            {
                                Tipo = values[0]
                            };

                            Context.Add(tipoatraccion);
                            Context.SaveChanges();

                            webClient.DownloadFile(values[1], $"{ruta}/images/tipoatraccion/{tipoatraccion.Id}.jpg");
                        }
                        else
                        {
                            throw new Exception($"Ya existe este tipo de atraccion con el nombre de [ {values[0]} ]");
                        }
                    }

                    Context.Database.CommitTransaction();
                    break;

                case 4:
                    headers = reader.ReadLine();
                    if (headers != "Titulo,Portada,Contenido,Ciudad,Tipo")
                    {
                        throw new Exception("El orden de las columnas no es el correcto [ Titulo,Portada,Contenido,Ciudad,Tipo ] (La portada debe ser un link de una imagen JPG)");
                    }

                    atraccionesRepository = new AtraccionesRepository();
                    ciudadesRepository = new CiudadesRepository();
                    tipoAtraccionRepository = new TipoAtraccionRepository();

                    Context.Database.BeginTransaction();

                    while (!reader.EndOfStream)
                    {
                        var line = reader.ReadLine();
                        var values = line.Split(',');
                        var ciudad = ciudadesRepository.GetCiudadesByNombre(values[3]);
                        var tipoatraccion = tipoAtraccionRepository.GetTipoAtraccionByTipo(values[4]);

                        if (atraccionesRepository.GetAtraccionesByTitulo(values[0]) == null)
                        {
                            if (ciudad == null)
                            {
                                throw new Exception($"No existe una ciudad con el nombre de [ {values[3]} ]");
                            }

                            if (tipoatraccion == null)
                            {
                                throw new Exception($"No existe un tipo de atraccion con el nombre de [ {values[4]} ]");
                            }

                            Atracciones atracciones = new Atracciones()
                            {
                                Titulo = values[0],
                                Contenido = values[2],
                                IdCiudad = ciudad.Id,
                                IdTipo = tipoatraccion.Id
                            };

                            Context.Add(atracciones);
                            Context.SaveChanges();

                            webClient.DownloadFile(values[1], $"{ruta}/images/atracciones/{atracciones.Id}.jpg");
                        }
                        else
                        {
                            throw new Exception($"Ya existe una atraccion con el titulo de [ {values[0]} ]");
                        }
                    }

                    Context.Database.CommitTransaction();
                    break;

                default:
                    throw new Exception("Seleccione un tipo de registro valido");
            }
        }

        public void ImportarExcel(int Tipo, FileStream file, string ruta)
        {
            file.Position = 0;

            IWorkbook workbook = new XSSFWorkbook(file);
            ISheet sheet = workbook.GetSheetAt(0);

            var primerRow = sheet.GetRow(0);

            /*
             * Tipos
             * 1 - Ciudad
             * 2 - Comida
             * 3 - Tipo de Atraccion
             * 4 - Atraccion
             * */

            switch (Tipo)
            {
                case 1:
                    if (primerRow.GetCell(0).StringCellValue == "Nombre" &&
                        primerRow.GetCell(1).StringCellValue == "Portada" &&
                        primerRow.GetCell(2).StringCellValue == "Contenido")
                    {
                        int lastRow = sheet.LastRowNum;
                        CiudadesRepository ciudadesRepository = new CiudadesRepository();

                        Context.Database.BeginTransaction();

                        WebClient webClient = new WebClient();

                        for (int i = 1; i <= sheet.LastRowNum; i++)
                        {
                            if (ciudadesRepository.GetCiudadesByNombre(sheet.GetRow(i).GetCell(0).StringCellValue) == null)
                            {
                                Ciudades ciudades = new Ciudades()
                                {
                                    Nombre = sheet.GetRow(i).GetCell(0).StringCellValue,
                                    Contenido = sheet.GetRow(i).GetCell(2).StringCellValue
                                };

                                Context.Add(ciudades);
                                Context.SaveChanges();

                                webClient.DownloadFile(sheet.GetRow(i).GetCell(1).StringCellValue, $"{ruta}/images/ciudades/{ciudades.Id}.jpg");
                            }
                            else
                            {
                                throw new Exception($"Ya existe una ciudad con el nombre de [ {sheet.GetRow(i).GetCell(0).StringCellValue} ]");
                            }                            
                        }
                        Context.Database.CommitTransaction();
                    }
                    else
                    {
                        workbook.Close();
                        throw new Exception("El archivo de Excel no tiene los datos en el orden correcto [ Nombre,Portada,Contenido ] (La portada debe ser un link de una imagen JPG)");
                    }
                    break;

                case 2:
                    if (primerRow.GetCell(0).StringCellValue == "Nombre" &&
                        primerRow.GetCell(1).StringCellValue == "Portada" &&
                        primerRow.GetCell(2).StringCellValue == "Descripcion" &&
                        primerRow.GetCell(3).StringCellValue == "Ciudad")
                    {
                        int lastRow = sheet.LastRowNum;
                        CiudadesRepository ciudadesRepository = new CiudadesRepository();
                        ComidasRepository comidasRepository = new ComidasRepository();

                        Context.Database.BeginTransaction();

                        WebClient webClient = new WebClient();

                        for (int i = 1; i <= sheet.LastRowNum; i++)
                        {
                            if (comidasRepository.GetComidaByNombre(sheet.GetRow(i).GetCell(0).StringCellValue) == null)
                            {
                                Ciudades ciudad = ciudadesRepository.GetCiudadesByNombre(sheet.GetRow(i).GetCell(3).StringCellValue);

                                if (ciudad == null)
                                {
                                    throw new Exception($"No existe una ciudad con el nombre de [ {sheet.GetRow(i).GetCell(3).StringCellValue} ]");
                                }

                                Comidas comidas = new Comidas()
                                {
                                    Nombre = sheet.GetRow(i).GetCell(0).StringCellValue,
                                    Descripcion = sheet.GetRow(i).GetCell(2).StringCellValue,
                                    IdCiudad = ciudad.Id
                                };

                                Context.Add(comidas);
                                Context.SaveChanges();

                                webClient.DownloadFile(sheet.GetRow(i).GetCell(1).StringCellValue, $"{ruta}/images/comidas/{comidas.Id}.jpg");
                            }
                            else
                            {
                                throw new Exception($"Ya existe una comida con el nombre de [ {sheet.GetRow(i).GetCell(0).StringCellValue} ]");
                            }
                        }
                        Context.Database.CommitTransaction();
                    }
                    else
                    {
                        workbook.Close();
                        throw new Exception("El archivo de Excel no tiene los datos en el orden correcto [ Nombre,Portada,Descripcion,Ciudad ] (La portada debe ser un link de una imagen JPG)");
                    }
                    break;

                case 3:
                    if (primerRow.GetCell(0).StringCellValue == "Tipo" &&
                        primerRow.GetCell(1).StringCellValue == "Icono")
                    {
                        int lastRow = sheet.LastRowNum;
                        TipoAtraccionRepository tipoAtraccionRepository= new TipoAtraccionRepository();

                        Context.Database.BeginTransaction();

                        WebClient webClient = new WebClient();

                        for (int i = 1; i <= sheet.LastRowNum; i++)
                        {
                            if (tipoAtraccionRepository.GetTipoAtraccionByTipo(sheet.GetRow(i).GetCell(0).StringCellValue) == null)
                            {
                                Tipoatraccion tipoatraccion = new Tipoatraccion()
                                {
                                    Tipo = sheet.GetRow(i).GetCell(0).StringCellValue
                                };

                                Context.Add(tipoatraccion);
                                Context.SaveChanges();

                                webClient.DownloadFile(sheet.GetRow(i).GetCell(1).StringCellValue, $"{ruta}/images/tipoatraccion/{tipoatraccion.Id}.jpg");
                            }
                            else
                            {
                                throw new Exception($"Ya existe este tipo de atraccion con el nombre de [ {sheet.GetRow(i).GetCell(0).StringCellValue} ]");
                            }
                        }
                        Context.Database.CommitTransaction();
                    }
                    else
                    {
                        workbook.Close();
                        throw new Exception("El archivo de Excel no tiene los datos en el orden correcto [ Tipo,Icono ] (El icono debe ser un link de una imagen JPG)");
                    }
                    break;

                case 4:
                    if (primerRow.GetCell(0).StringCellValue == "Titulo" &&
                        primerRow.GetCell(1).StringCellValue == "Portada" &&
                        primerRow.GetCell(2).StringCellValue == "Contenido" &&
                        primerRow.GetCell(3).StringCellValue == "Ciudad" &&
                        primerRow.GetCell(4).StringCellValue == "Tipo")
                    {
                        int lastRow = sheet.LastRowNum;
                        AtraccionesRepository atraccionesRepository = new AtraccionesRepository();
                        CiudadesRepository ciudadesRepository = new CiudadesRepository();
                        TipoAtraccionRepository tipoAtraccionRepository = new TipoAtraccionRepository();

                        Context.Database.BeginTransaction();

                        WebClient webClient = new WebClient();

                        for (int i = 1; i <= sheet.LastRowNum; i++)
                        {
                            if (atraccionesRepository.GetAtraccionesByTitulo(sheet.GetRow(i).GetCell(0).StringCellValue) == null)
                            {
                                Ciudades ciudad = ciudadesRepository.GetCiudadesByNombre(sheet.GetRow(i).GetCell(3).StringCellValue);

                                if (ciudad == null)
                                {
                                    throw new Exception($"No existe una ciudad con el nombre de [ {sheet.GetRow(i).GetCell(3).StringCellValue} ]");
                                }

                                Tipoatraccion tipoatraccion = tipoAtraccionRepository.GetTipoAtraccionByTipo(sheet.GetRow(i).GetCell(4).StringCellValue);

                                if (tipoatraccion == null)
                                {
                                    throw new Exception($"No existe un tipo de atraccion con el nombre de [ {sheet.GetRow(i).GetCell(4).StringCellValue} ]");
                                }

                                Atracciones atracciones = new Atracciones()
                                {
                                    Titulo = sheet.GetRow(i).GetCell(0).StringCellValue,
                                    Contenido = sheet.GetRow(i).GetCell(2).StringCellValue,
                                    IdCiudad = ciudad.Id,
                                    IdTipo = tipoatraccion.Id
                                };

                                Context.Add(atracciones);
                                Context.SaveChanges();

                                webClient.DownloadFile(sheet.GetRow(i).GetCell(1).StringCellValue, $"{ruta}/images/atracciones/{atracciones.Id}.jpg");
                            }
                            else
                            {
                                throw new Exception($"Ya existe una atraccion con el titulo de [ {sheet.GetRow(i).GetCell(0).StringCellValue} ]");
                            }
                        }
                        Context.Database.CommitTransaction();
                    }
                    else
                    {
                        workbook.Close();
                        throw new Exception("El archivo de Excel no tiene los datos en el orden correcto [ Titulo,Portada,Contenido,Ciudad,Tipo ] (La portada debe ser un link de una imagen JPG)");
                    }
                    break;

                default:
                    throw new Exception("Seleccione un tipo de registro valido");
            }
        }
    }
}
