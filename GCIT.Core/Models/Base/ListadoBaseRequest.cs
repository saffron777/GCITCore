using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace GCIT.Core.Models.Base
{
    /// <summary>
    /// Request base para listados paginados
    /// </summary>
    public class ListadoBaseRequest : BaseRequest
    {
        /// <summary>
        /// Número de página a retornar
        /// </summary>
        [Range(1, int.MaxValue)]
        [DefaultValue(1)]
        public virtual int? Pagina { get; set; } = 1;

        /// <summary>
        /// Cantidad de registros por página
        /// Utilizar 10000 cuando se quiera obtener todos los registros
        /// </summary>
        [Range(1, 10000)]
        [DefaultValue(10)]
        public virtual int? RegistrosPorPagina { get; set; } = 10;

        /// <summary>
        /// Opcional: Término de búsqueda
        /// Se utiliza para filtrar los registros de acuerdo a muliples campos
        /// </summary>
        public virtual string? Buscar { get; set; }

        /// <summary>
        /// Opcional: Filtros para la consulta
        /// Se utiliza para aplicar filtros adicionales a la consulta
        /// </summary>
        public virtual List<FiltroCampo>? Filtros { get; set; }
    }

    public enum OrdenDireccion
    {
        [Description("ASC")]
        Ascendente,
        [Description("DESC")]
        Descendente
    }

    /// <summary>
    /// Request base para listados paginados con ordenamiento opcional
    /// </summary>
    public class ListadoBaseConOrdenRequest : ListadoBaseRequest
    {
        public string? OrdenarPor { get; set; }
        public OrdenDireccion Orden { get; set; } = OrdenDireccion.Ascendente;

    }


    public class FiltroCampo
    {
        /// <summary>
        /// Campo a filtrar
        /// </summary>        
        public string Campo { get; set; } = string.Empty;

        /// <summary>
        /// Valor a filtrar
        /// </summary>
        public object? Valor { get; set; } 

    }
}
