using System;
using System.Collections.Generic;
using System.Text;

namespace GCIT.Core.Models.Base
{
    /// <summary>
    /// Response base para respuestas de listados paginados.
    /// </summary>
    public class ListadoBaseResponse : BaseResponse
    {
        /// <summary>
        /// Página actual de la respuesta.
        /// </summary>
        public virtual int? Pagina { get; set; }

        /// <summary>
        /// Tamaño de página utilizado en la consulta.
        /// </summary>
        public virtual int? TamanoPagina { get; set; }

        /// <summary>
        /// Total de elementos en la consulta sin paginación.
        /// </summary>
        public virtual int TotalElementos { get; set; }

        /// <summary>
        /// Total de páginas disponibles.
        /// </summary>
        public virtual int TotalPaginas { get; set; }

        /// <summary>
        /// Campo por el cual se ordenó la consulta.
        /// </summary>
        public virtual string? OrdenarPor { get; set; }

        /// <summary>
        /// Orden de la consulta (Asc/Desc).
        /// </summary>
        public virtual string? Orden { get; set; }

        /// <summary>
        /// Filtros aplicados en la consulta.
        /// </summary>
        public virtual List<FiltroCampo>? FiltrosAplicados { get; set; }

        /// <summary>
        /// Crea una respuesta exitosa para un listado paginado.
        /// </summary>
        /// <param name="pagina">Página actual.</param>
        /// <param name="tamanoPagina">Tamaño de página.</param>
        /// <param name="totalElementos">Total de elementos sin paginación.</param>
        /// <param name="ordenarPor">Campo por el cual se ordenó.</param>
        /// <param name="orden">Orden de la consulta.</param>
        /// <param name="filtrosAplicados">Filtros aplicados.</param>
        /// <param name="message">Mensaje opcional.</param>
        /// <param name="status">Código de estado HTTP.</param>
        /// <returns>Una respuesta de listado exitosa.</returns>
        public static ListadoBaseResponse Success(
            int pagina,
            int tamanoPagina,
            int totalElementos,
            string? ordenarPor = null,
            string? orden = null,
            List<FiltroCampo>? filtrosAplicados = null,
            string? message = null)
        {
            var totalPaginas = (int)Math.Ceiling((double)totalElementos / tamanoPagina);

            return new ListadoBaseResponse
            {
                Pagina = pagina,
                TamanoPagina = tamanoPagina,
                TotalElementos = totalElementos,
                TotalPaginas = totalPaginas,
                OrdenarPor = ordenarPor,
                Orden = orden,
                FiltrosAplicados = filtrosAplicados ?? new List<FiltroCampo>(),
                Mensaje = message ?? "Listado obtenido exitosamente",
                Errores = null,
                CodigoError = null,
                EsExitoso = true
            };
        }

        /// <summary>
        /// Crea una respuesta exitosa para un listado paginado con datos.
        /// </summary>
        /// <typeparam name="T">Tipo de datos del listado.</typeparam>
        /// <param name="data">Datos del listado.</param>
        /// <param name="pagina">Página actual.</param>
        /// <param name="tamanoPagina">Tamaño de página.</param>
        /// <param name="totalElementos">Total de elementos sin paginación.</param>
        /// <param name="ordenarPor">Campo por el cual se ordenó.</param>
        /// <param name="orden">Orden de la consulta.</param>
        /// <param name="filtrosAplicados">Filtros aplicados.</param>
        /// <param name="message">Mensaje opcional.</param>
        /// <param name="status">Código de estado HTTP.</param>
        /// <returns>Una respuesta de listado exitosa con datos.</returns>
        public static ListadoBaseResponse<T> Success<T>(
            T data,
            int pagina,
            int tamanoPagina,
            int totalElementos,
            string? ordenarPor = null,
            string? orden = null,
            List<FiltroCampo>? filtrosAplicados = null,
            string? message = null)
        {
            var totalPaginas = (int)Math.Ceiling((double)totalElementos / tamanoPagina);

            return new ListadoBaseResponse<T>
            {
                Datos = new List<T> { data },
                Pagina = pagina,
                TamanoPagina = tamanoPagina,
                TotalElementos = totalElementos,
                TotalPaginas = totalPaginas,
                OrdenarPor = ordenarPor,
                Orden = orden,
                FiltrosAplicados = filtrosAplicados ?? new List<FiltroCampo>(),
                Mensaje = message ?? "Listado obtenido exitosamente",
                Errores = null,
                CodigoError = null,
                EsExitoso = true
            };
        }
    }

    /// <summary>
    /// Response base genérica para respuestas de listados paginados con datos.
    /// </summary>
    /// <typeparam name="T">Tipo de datos del listado.</typeparam>
    public class ListadoBaseResponse<T> : ListadoBaseResponse
    {
        /// <summary>
        /// Datos del listado.
        /// </summary>
        public virtual List<T>? Datos { get; set; }
    }
}
