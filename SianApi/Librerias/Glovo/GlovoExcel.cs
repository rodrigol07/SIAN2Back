using Microsoft.Office.Interop.Excel;
using SianApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SianApi.Librerias.Glovo
{
    public class GlovoExcel
    {
        //start excel
        Application excel = new Application();
        public string generarExcel(string fileName, string marca, List<sp_MenuIndexSybaseExcelGlovo> listaExcel)
        {
            // Crear columnas
            excel.Workbooks.Add();
            Worksheet workSheet = excel.ActiveSheet;

            var groups = from p in listaExcel
                         where (p.Collection != null)
                         orderby p.OrderPos
                         orderby p.OrderPos, p.ProdPos
                         orderby p.OrderPos, p.ProdPos, p.OrdenPregunta
                         orderby p.OrderPos, p.ProdPos, p.OrdenPregunta, p.OrdenRespuesta
                         group p by new
                         {
                             p.OrderPos,
                             p.ProdPos,
                             p.OrdenPregunta,
                             p.OrdenRespuesta
                         }
                         into s
                         select s.First();

            try
            {
                // Crear cabeceras
                workSheet.Cells[1, "A"] = "Empresa";
                workSheet.Cells[1, "B"] = "Super collection";
                workSheet.Cells[1, "C"] = "Collection";
                workSheet.Cells[1, "D"] = "Sección";
                workSheet.Cells[1, "E"] = "Código del producto principal";
                workSheet.Cells[1, "F"] = "Nombre producto principal";
                workSheet.Cells[1, "G"] = "Precio producto principal";
                workSheet.Cells[1, "H"] = "Fecha inicio";
                workSheet.Cells[1, "I"] = "Fecha fin";
                workSheet.Cells[1, "J"] = "Descripción del producto principal";
                workSheet.Cells[1, "K"] = "Código de pregunta";
                workSheet.Cells[1, "L"] = "Nombre de pregunta";
                workSheet.Cells[1, "M"] = "Minimo de respuestas a elegir";
                workSheet.Cells[1, "N"] = "Maximo respuestas a elegir";
                workSheet.Cells[1, "O"] = "Código del producto respuesta";
                workSheet.Cells[1, "P"] = "Nombre del producto respuesta";
                workSheet.Cells[1, "Q"] = "Precio del producto respuesta";
                workSheet.Cells[1, "R"] = "Imagen";


                // Para completar desde la segunda posición
                int row = 2;
                foreach (var i in groups)
                {
                    workSheet.Cells[row, "A"] = marca.ToUpper();
                    workSheet.Cells[row, "B"] = i.SuperCollection;
                    workSheet.Cells[row, "C"] = i.Collection;
                    workSheet.Cells[row, "D"] = i.Seccion;
                    workSheet.Cells[row, "E"] = i.CodigoPadre;
                    workSheet.Cells[row, "F"] = i.ProductoPadre;
                    workSheet.Cells[row, "G"] = i.PrecioPadre;
                    workSheet.Cells[row, "H"] = i.VigenciaFechaInicio.ToString("dd/MM/yyyy");
                    workSheet.Cells[row, "I"] = i.VigenciaFechaFin.ToString("dd/MM/yyyy");
                    workSheet.Cells[row, "J"] = i.DescripcionProductoPadreGlovo;
                    workSheet.Cells[row, "K"] = i.CodigoPregunta;
                    workSheet.Cells[row, "L"] = i.Pregunta;
                    workSheet.Cells[row, "M"] = i.Minimo;
                    workSheet.Cells[row, "N"] = i.Maximo;
                    workSheet.Cells[row, "O"] = i.CodigoRespuesta;
                    workSheet.Cells[row, "P"] = i.Respuesta;
                    workSheet.Cells[row, "Q"] = i.PrecioRespuesta;
                    workSheet.Cells[row, "R"] = i.ImagenGlovo;
                    row++;
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