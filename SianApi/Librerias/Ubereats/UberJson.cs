using Newtonsoft.Json.Linq;
using SianApi.Models;
using System.Collections.Generic;
using System.Linq;

namespace SianApi.Librerias.Ubereats
{
    public class UberJson
    {
        string ruta;
        JProperty horario;

        public JObject generarListaJson(List<tbl_PizarraMarcaDetalle> listaPizarraMarcaDetalle, string marca, int indexSybase, IEnumerable<tbl_AgregadorHorario> grupoHorario = null)
        {
            var subsections = from p in listaPizarraMarcaDetalle
                              where (p.sOrdenMenu != "")
                              orderby p.nOrderPos
                              group p by p.sOrdenMenu into s
                              select s.First();

            var subsectionsItems = from p in listaPizarraMarcaDetalle
                                   where (((p.sProductoPadre != "" || p.sProductoPadre != null) && (p.sPregunta != "" || p.sPregunta != null)) || (p.sOrdenMenu == "Complementos") || (p.sOrdenMenu == "Postres") || ((p.sOrdenMenu == "Bebidas") && (p.nIndexSybase == 22019 || p.nIndexSybase == 22023)) || ((p.sOrdenMenu == "Adicionales") && (p.nIndexSybase == 22019 || p.nIndexSybase == 22023)) || ((p.sOrdenMenu == "Hamburguesas") && (p.nIndexSybase == 22074)))
                                   orderby p.nOrderPos
                                   //group p by p.sProductoPadre into si
                                   group p by new
                                   {
                                       p.sOrdenMenu,
                                       p.sProductoPadre,
                                   } into sit
                                   select sit.First();

            var customizations = from p in listaPizarraMarcaDetalle
                                 where (p.sPregunta != "" || p.sPregunta != null)
                                 group p by new
                                     {
                                         p.sCodigoPadre,
                                         p.sPregunta,
                                     } into c
                                 select c.First();

            //Para rutas de imágenes según marca
            /*
            switch (marca)
            {
                case "popeyes":
                    ruta = "https://190.223.40.173/imagenes/popeyes/uber/";
                    break;
                case "bembos":
                    ruta = "https://190.223.40.173/imagenes/bembos/uber/";
                    break;
                case "papa johns":
                    ruta = "https://190.223.40.173/imagenes/papajohns/uber/";
                    break;
                default:
                    ruta = "https://190.223.40.173/imagenes/popeyes/uber/";
                    break;
            }
            */
            ruta = "http://190.223.40.173/glovo/menu/";

            if (grupoHorario == null)
            {
                horario = 
                    new JProperty("service_availability",
                        new JArray(
                            new JObject(
                                new JProperty("day_of_week", "sunday"),
                                new JProperty("enabled", true),
                                new JProperty("time_periods",
                                    new JArray(
                                        new JObject(
                                            new JProperty("start_time", "08:00"),
                                            new JProperty("end_time", "23:00")
                                        )
                                    )
                                )
                            ),
                            new JObject(
                                new JProperty("day_of_week", "monday"),
                                new JProperty("enabled", true),
                                new JProperty("time_periods",
                                    new JArray(
                                        new JObject(
                                            new JProperty("start_time", "08:00"),
                                            new JProperty("end_time", "23:00")
                                        )
                                    )
                                )
                            ),
                            new JObject(
                                new JProperty("day_of_week", "tuesday"),
                                new JProperty("enabled", true),
                                new JProperty("time_periods",
                                    new JArray(
                                        new JObject(
                                            new JProperty("start_time", "08:00"),
                                            new JProperty("end_time", "23:00")
                                        )
                                    )
                                )
                            ),
                            new JObject(
                                new JProperty("day_of_week", "wednesday"),
                                new JProperty("enabled", true),
                                new JProperty("time_periods",
                                    new JArray(
                                        new JObject(
                                            new JProperty("start_time", "08:00"),
                                            new JProperty("end_time", "23:00")
                                        )
                                    )
                                )
                            ),
                            new JObject(
                                new JProperty("day_of_week", "thursday"),
                                new JProperty("enabled", true),
                                new JProperty("time_periods",
                                    new JArray(
                                        new JObject(
                                            new JProperty("start_time", "08:00"),
                                            new JProperty("end_time", "23:00")
                                        )
                                    )
                                )
                            ),
                            new JObject(
                                new JProperty("day_of_week", "friday"),
                                new JProperty("enabled", true),
                                new JProperty("time_periods",
                                    new JArray(
                                        new JObject(
                                            new JProperty("start_time", "08:00"),
                                            new JProperty("end_time", "23:59")
                                        )
                                    )
                                )
                            ),
                            new JObject(
                                new JProperty("day_of_week", "saturday"),
                                new JProperty("enabled", true),
                                new JProperty("time_periods",
                                    new JArray(
                                        new JObject(
                                            new JProperty("start_time", "08:00"),
                                            new JProperty("end_time", "23:59")
                                        )
                                    )
                                )
                            )
                        )
                    );
            }
            else
            {
                horario = new JProperty("service_availability",
                              new JArray(
                                  from h in grupoHorario
                                  select new JObject(
                                      new JProperty("day_of_week", h.sDiaNombre.ToString().Trim()),
                                      new JProperty("enabled", true),
                                      new JProperty("time_periods",
                                          new JArray(
                                              new JObject(
                                                  new JProperty("start_time", h.sHoraInicio.ToString().Trim()),
                                                  new JProperty("end_time", h.sHoraFin.ToString().Trim())
                                              )
                                          )
                                      )
                                  )
                             )
                         );
            }

            JObject json =
                new JObject(
                    new JProperty("sections",
                        new JArray(
                            new JObject(
                                new JProperty("title", "Uber " + marca + " - " + indexSybase),
                                horario,
                                new JProperty("subsections",
                                    new JArray(
                                        from s in subsections
                                        select new JObject(
                                            new JProperty("title", s.sOrdenMenu),
                                            new JProperty("items",
                                                new JArray(
                                                    from si in subsectionsItems
                                                    where si.sOrdenMenu == s.sOrdenMenu
                                                    orderby si.nProdPos
                                                    select new JObject(
                                                        new JProperty("title", si.sProductoPadre),
                                                        new JProperty("price", si.nPrecioPadre),
                                                        new JProperty("currency_code", "PEN"),
                                                        new JProperty("external_id", si.sCodigoPadre.ToString()),
                                                        new JProperty("tax_rate", 0),
                                                        new JProperty("item_description", si.sDescripcionProductoPadreUber),
                                                        new JProperty("image_url", (si.sImagenUber == "" || si.sImagenUber == null) ? ruta + "default.jpg" : ruta + si.sImagenUber.ToLower()),
                                                        // new JProperty("image_url", "https://res.cloudinary.com/ngr/image/upload/v1567722383/Test/brownie.jpg"),
                                                        // Solo si tiene opciones de combinacion ()
                                                        new JProperty("customizations",
                                                            new JArray(
                                                                from c in customizations
                                                                where c.sProductoPadre == si.sProductoPadre && c.sOrdenMenu == si.sOrdenMenu && c.sRespuesta != null
                                                                orderby c.nOrdenPregunta
                                                                select new JObject(
                                                                    new JProperty("title", c.sPregunta),
                                                                    new JProperty("min_permitted", c.nMinimo),
                                                                    new JProperty("max_permitted", c.nMaximo),
                                                                    new JProperty("customization_options",
                                                                        new JArray(
                                                                            from j in listaPizarraMarcaDetalle
                                                                            where (j.sCodigoPadre == c.sCodigoPadre && j.sCodigoPregunta == c.sCodigoPregunta && j.sOrdenMenu == c.sOrdenMenu)
                                                                            orderby j.nOrdenRespuesta
                                                                            select new JObject(
                                                                                new JProperty("title", j.sRespuesta),
                                                                                new JProperty("price", j.nPrecioRespuesta),
                                                                                new JProperty("external_id", j.sCodigoRespuesta.ToString())
                                                                            )
                                                                        )
                                                                    )
                                                                )
                                                            )
                                                        )///
                                                    )
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