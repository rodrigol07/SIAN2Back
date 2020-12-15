using Newtonsoft.Json.Linq;
using SianApi.Models;
using System.Collections.Generic;
using System.Linq;

/*
-- ==================================================
-- Autor:      				Jorge Solis Mc Lellan
-- Fecha de creación: 		06/07/2019
-- Versión: 				0.2
-- Tipo:					Procedimiento almacenado
-- Descripción: 			Genera reporte de envios a PRD por marca y agregador.
--
-- Historial de cambios:
--   06/07/2019 v0.1 Jorge Solis: Primera versión.
--   31/07/2019 v0.2 Jorge Solis: Depurados errores en campos null para SianApi.
--
-- ==================================================
*/

namespace SianApi.Librerias.Glovo
{
    public class GlovoJson
    {
        string ruta;
        public JObject generarListaJson(List<tbl_PizarraMarcaDetalle> listaPizarraMarcaDetalle, string marca)
        {
            var attributes = from p in listaPizarraMarcaDetalle
                             where (p.sCodigoRespuesta != null)
                             orderby p.nOrdenPregunta
                             select p;

            var attributeGroups = from p in listaPizarraMarcaDetalle
                                  where (p.sCodigoPregunta != null || p.sPregunta != null)
                                  orderby p.nOrdenPregunta, p.sCodigoPregunta
                                  group p by p.sCodigoPregunta into ag
                                  select ag.First();
            
            var products = from p in listaPizarraMarcaDetalle
                           where (p.sCodigoPadre != "" || p.sCodigoPadre != null)
                           orderby p.nProdPos
                           group p by p.sCodigoPadre into pd
                           select pd.First();
            
            var attributesGroupsItems = from p in listaPizarraMarcaDetalle
                                        where (p.sCodigoPadre != null || p.sCodigoPregunta != null)
                                        orderby p.nOrdenRespuesta
                                        group p by new
                                           {
                                               p.sCodigoPadre,
                                               p.sCodigoPregunta
                                           } into pd
                                       select pd.First();
            
            var collections = from p in listaPizarraMarcaDetalle
                              where (p.sOrdenMenu != null)
                              orderby p.nOrderPos
                              group p by p.sOrdenMenu into c
                              select c.First();
            
            var sections = from p in listaPizarraMarcaDetalle
                           where (p.sOrdenMenu != null)
                           orderby p.nOrderPos
                           group p by p.sOrdenMenu into s
                           select s.First();

            var sectionItems = from p in listaPizarraMarcaDetalle
                               where (p.sCodigoPadre != null || p.sCodigoPregunta != null)
                               orderby p.nProdPos
                               group p by p.sCodigoPadre into si
                               select si.First();


            //Para rutas de imágenes según marca
            /*
            switch (marca)
            {
                case "popeyes":
                    ruta = "http://190.223.40.173/imagenes/popeyes/glovo/";
                    break;
                case "bembos":
                    ruta = "http://190.223.40.173/imagenes/bembos/glovo/";
                    break;
                case "papa johns":
                    ruta = "http://190.223.40.173/imagenes/papajohns/glovo/";
                    break;
                default:
                    ruta = "http://190.223.40.173/imagenes/popeyes/glovo/";
                    break;
            }
            */
            ruta = "http://190.223.40.173/glovo/menu/";

            JObject json =
                new JObject(
                    new JProperty("attributes",
                        new JArray(
                            from a in attributes
                            select new JObject(
                                new JProperty("id", a.sCodigoRespuesta),
                                new JProperty("name", a.sRespuesta),
                                new JProperty("price_impact", a.nPrecioRespuesta)
                            )
                        )
                    ),
                    new JProperty("attribute_groups",
                        new JArray(
                            from ag in attributeGroups
                            where (ag.sPregunta != null)
                            select new JObject(
                                new JProperty("id", ag.sCodigoPregunta),
                                new JProperty("name", ag.sPregunta),
                                new JProperty("min", ag.nMinimo),
                                new JProperty("max", ag.nMaximo),
                                new JProperty("collapse", "false"),
                                new JProperty("attributes",
                                    new JArray(
                                        from a in attributes
                                        where a.sCodigoPregunta == ag.sCodigoPregunta && a.sCodigoPadre == ag.sCodigoPadre
                                        orderby a.nOrdenPregunta
                                        select a.sCodigoRespuesta.ToString()
                                    )
                                )
                            )
                        )
                    ),
                    new JProperty("products",
                        new JArray(
                            from p in products
                            select new JObject(
                                new JProperty("id", p.sCodigoPadre),
                                new JProperty("name", p.sProductoPadre),
                                new JProperty("price", p.nPrecioPadre),
                                new JProperty("image_url", (p.sImagenGlovo == null) ? ruta + "default.jpg" : ruta + p.sImagenGlovo.ToLower()),
                                new JProperty("description", (p.sDescripcionProductoPadreGlovo == null) ? "" : p.sDescripcionProductoPadreGlovo),
                                new JProperty("attributes_groups",
                                    new JArray(
                                        from ag in attributesGroupsItems
                                        where ((ag.sProductoPadre == p.sProductoPadre) && (ag.sCodigoPregunta != null) && (ag.sOrdenMenu == p.sOrdenMenu))
                                        orderby ag.nOrdenPregunta, ag.nOrdenRespuesta
                                        select ag.sCodigoPregunta
                                    )
                                )
                            )
                        )
                    ),
                    new JProperty("collections",
                        new JArray(
                            from c in collections
                            select new JObject(
                                new JProperty("name", c.sOrdenMenu),
                                new JProperty("position", c.nOrderPos),
                                new JProperty("sections", 
                                    new JArray(
                                        new JObject(
                                            new JProperty("name", c.sOrdenMenu),
                                            new JProperty("position", c.nOrderPos),
                                            new JProperty("products",
                                                new JArray(
                                                    from si in sectionItems
                                                    where si.sOrdenMenu == c.sOrdenMenu
                                                    orderby si.nProdPos
                                                    select si.sCodigoPadre
                                                )
                                            )
                                        )
                                    )
                                )
                            )
                        )
                    )
                );
            return json;
        }
    }
}