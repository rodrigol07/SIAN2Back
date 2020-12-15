using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Office.Interop.Excel;
using SianApi.Models;

namespace SianApi.Librerias.Rappi
{
    public class RappiExcel
    {
        //start excel
        Application excel = new Application();

        public string generarExcel(string fileName, string marca, List<tbl_PizarraMarcaDetalle> lista)
        {
            // Crear columnas
            excel.Workbooks.Add();
            Worksheet workSheet = excel.ActiveSheet;

            var groups = from p in lista
                              where (p.sOrdenMenu != null)
                              orderby p.nOrderPos
                              group p by new
                              {
                                  p.sOrdenMenu,
                                  p.sCodigoPadre,
                                  p.sProductoPadre
                              } into s
                              select s.First();

            try
            {
                // Crear cabeceras
                workSheet.Cells[1, "A"] = "AGRUPACION";
                workSheet.Cells[1, "B"] = "STOREID";
                workSheet.Cells[1, "C"] = "DESCRIPCION PRODUCTO";
                workSheet.Cells[1, "D"] = "CODIGO PRODUCTO";
                workSheet.Cells[1, "E"] = "PRECIO";
                workSheet.Cells[1, "F"] = "CODIGO PRODUCTO PADRE";

                // Para completar desde la segunda posición
                int row = 2;
                foreach (var i in groups)
                {
                    workSheet.Cells[row, "A"] = i.sOrdenMenu;
                    workSheet.Cells[row, "B"] = marca;
                    workSheet.Cells[row, "C"] = i.sProductoPadre;
                    workSheet.Cells[row, "D"] = i.sCodigoPadre;
                    workSheet.Cells[row, "E"] = i.nPrecioPadre;
                    workSheet.Cells[row, "F"] = "NULL";
                    row++;

                    
                    foreach (var j in lista.OrderBy(p => p.nOrdenPregunta))
                    {
                        if (j.sOrdenMenu == i.sOrdenMenu && j.sCodigoPadre == i.sCodigoPadre)
                        {
                            workSheet.Cells[row, "A"] = j.sOrdenMenu;
                            workSheet.Cells[row, "B"] = marca;
                            workSheet.Cells[row, "C"] = j.sRespuesta;
                            workSheet.Cells[row, "D"] = j.sCodigoRespuesta;
                            workSheet.Cells[row, "E"] = j.nPrecioRespuesta;
                            workSheet.Cells[row, "F"] = j.sCodigoPadre;
                            row++;
                        }
                    }
                }

                // Guardar
                workSheet.SaveAs(fileName);
                return "ok";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}