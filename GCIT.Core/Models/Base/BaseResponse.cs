using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace GCIT.Core.Models.Base
{
    public class BaseResponse
    {
        /// <summary>
        /// Código de error de la respuesta.
        /// </summary>
        public virtual string? CodigoError { get; set; }

        /// <summary>
        /// Mensaje opcional describiendo la respuesta.
        /// </summary>
        public virtual string? Mensaje { get; set; }

        /// <summary>
        /// Lista de errores relacionados con la respuesta, si hay alguno.
        /// </summary>
        public virtual List<string>? Errores { get; set; }

        /// <summary>
        /// Indica si la respuesta es exitosa.
        /// Por defecto es false. Solo se establece en true en respuestas exitosas.
        /// </summary>
        public virtual bool EsExitoso { get; set; } = false;

        /// <summary>
        /// Información adicional sobre la respuesta.
        /// </summary>
        public virtual Dictionary<string, object> InformacionAdicional { get; set; } = new();

        /// <summary>
        /// Crea una respuesta de error genérico (400) con un mensaje de error.
        /// </summary>
        /// <param name="message">Mensaje que describe el error.</param>
        /// <param name="errorCode">Código de error opcional para la respuesta.</param>
        /// <returns>Un objeto BaseResponse con el estado de error.</returns>
        public static BaseResponse Error(string message, string? errorCode = null)
        {
            return new BaseResponse
            {
                Errores = [message],
                Mensaje = message,
                CodigoError = errorCode ?? "ERROR"
            };
        }

        /// <summary>
        /// Crea una respuesta de error genérico (400) con una lista de errores.
        /// </summary>
        /// <param name="message">Mensaje principal que describe el error.</param>
        /// <param name="errors">Lista de errores adicionales.</param>
        /// <param name="errorCode">Código de error opcional para la respuesta.</param>
        /// <returns>Un objeto BaseResponse con el estado de error.</returns>
        public static BaseResponse Error(string message, List<string> errors, string? errorCode = null)
        {
            return new BaseResponse
            {
                Errores = errors,
                Mensaje = message,
                CodigoError = errorCode ?? "ERROR"
            };
        }

        /// <summary>
        /// Crea una respuesta de error genérico (400) con un mensaje de error y detalles adicionales.
        /// </summary>
        /// <param name="message">Mensaje que describe el error.</param>
        /// <param name="details">Detalles adicionales del error.</param>
        /// <param name="errorCode">Código de error opcional para la respuesta.</param>
        /// <returns>Un objeto BaseResponse con el estado de error.</returns>
        public static BaseResponse Error(string message, object details, string? errorCode = null)
        {
            return new BaseResponse
            {
                Errores = [message],
                Mensaje = message,
                CodigoError = errorCode ?? "ERROR",
                InformacionAdicional = new Dictionary<string, object> { ["details"] = details }
            };
        }
    }

    public class BaseResponse<T> : BaseResponse
    {
        /// <summary>
        /// Datos de la respuesta.
        /// </summary>
        public virtual T? Datos { get; set; }

        /// <summary>
        /// Crea una respuesta exitosa con datos, asignando el código de estado HTTP 2XX OK.
        /// </summary>
        /// <param name="data">Los datos de la respuesta.</param>
        /// <param name="message">Mensaje opcional describiendo la respuesta.</param>
        /// <param name="status">Código de estado HTTP para la respuesta. Debe estar en el rango 2XX (por defecto es 200 OK).</param>
        /// <returns>Un objeto BaseResponse que contiene los datos y el estado de éxito.</returns>
        /// <exception cref="ArgumentException">Se lanza si el código de estado no está en el rango 2XX.</exception>
        public static BaseResponse<T> Success(T? data, string? message = null, HttpStatusCode status = HttpStatusCode.OK)
        {
            if ((int)status < 200 || (int)status >= 300)
                throw new ArgumentException("The status code for Success must be in the 2xx range.", nameof(status));

            return new BaseResponse<T>
            {
                Datos = data,
                Mensaje = message ?? "Operación exitosa",
                Errores = null,
                CodigoError = null,
                EsExitoso = true
            };
        }

        /// <summary>
        /// Crea una respuesta exitosa con datos, asignando el código de estado HTTP 2XX OK.
        /// </summary>
        /// <typeparam name="TResponse">Tipo de respuesta a devolver.</typeparam>
        /// <param name="data">Los datos de la respuesta.</param>
        /// <param name="message">Mensaje opcional describiendo la respuesta.</param>
        /// <param name="status">Código de estado HTTP para la respuesta. Debe estar en el rango 2XX (por defecto es 200 OK).</param>
        /// <returns>Un objeto BaseResponse que contiene los datos y el estado de éxito.</returns>
        /// <exception cref="ArgumentException">Se lanza si el código de estado no está en el rango 2XX.</exception>
        public static TResponse Success<TResponse>(T data, string? message = null, HttpStatusCode status = HttpStatusCode.OK)
            where TResponse : BaseResponse<T>, new()
        {
            if ((int)status < 200 || (int)status >= 300)
                throw new ArgumentException("The status code for Success must be in the 2xx range.", nameof(status));

            var response = new TResponse
            {
                Datos = data,
                Mensaje = message,
                Errores = null,
                CodigoError = null,
                EsExitoso = true
            };

            return response;
        }


        /// <summary>
        /// Crea una respuesta de recurso no encontrado (404) con una lista de errores.
        /// </summary>
        /// <param name="errors">Lista de errores opcionales que explican por qué el recurso no fue encontrado. Por defecto es "Recurso no encontrado".</param>
        /// <param name="message">Mensaje opcional que describe la respuesta.</param>
        /// <param name="errorCode">Código de error opcional para la respuesta.</param>
        /// <returns>Un objeto BaseResponse con el estado 404 y los errores proporcionados.</returns>
        public static BaseResponse<T> NotFound(List<string>? errors = null, string? message = null, string? errorCode = null)
        {
            return new BaseResponse<T>
            {
                Errores = errors ?? ["Recurso no encontrado"],
                Mensaje = message,
                Datos = default,
                CodigoError = errorCode ?? "NOT_FOUND"
            };
        }

        /// <summary>
        /// Crea una respuesta de recurso no encontrado (404) con un solo error.
        /// </summary>
        /// <param name="error">Error que explica por qué el recurso no fue encontrado.</param>
        /// <param name="message">Mensaje opcional que describe la respuesta.</param>
        /// <param name="errorCode">Código de error opcional para la respuesta.</param>
        /// <returns>Un objeto BaseResponse con el estado 404 y el error proporcionado.</returns>
        public static BaseResponse<T> NotFound(string error, string? message = null, string? errorCode = null)
        {
            return NotFound([error], message, errorCode);
        }

        /// <summary>
        /// Creates an Unauthorized (401) response with a list of errors.
        /// </summary>
        /// <param name="errors">List of errors explaining why the request is unauthorized.</param>
        /// <param name="message">Optional additional message describing the response.</param>
        /// <param name="errorCode">Optional error code for the response.</param>
        /// <returns>A BaseResponse object with status 401 and the provided errors.</returns>
        public static BaseResponse<T> Unauthorized(List<string> errors, string? message = null, string? errorCode = null)
        {
            return new BaseResponse<T>
            {
                Errores = errors,
                Mensaje = message,
                Datos = default,
                CodigoError = errorCode ?? "UNAUTHORIZED"
            };
        }

        /// <summary>
        /// Creates an Unauthorized (401) response with a single error.
        /// </summary>
        /// <param name="error">Single error explaining why the request is unauthorized.</param>
        /// <param name="message">Optional additional message describing the response.</param>
        /// <param name="errorCode">Optional error code for the response.</param>
        /// <returns>A BaseResponse object with status 401 and the provided error.</returns>
        public static BaseResponse<T> Unauthorized(string error, string? message = null, string? errorCode = null)
        {
            // Internally calls the method that accepts a list of errors
            return Unauthorized([error], message, errorCode);
        }

        /// <summary>
        /// Creates a Forbidden (403) response with a list of errors.
        /// </summary>
        /// <param name="errors">Optional list of errors explaining why access is forbidden. Defaults to "Access denied".</param>
        /// <param name="message">Optional additional message describing the response.</param>
        /// <param name="errorCode">Optional error code for the response.</param>
        /// <returns>A BaseResponse object with status 403 and the provided errors.</returns>
        public static BaseResponse<T> Forbidden(List<string>? errors = null, string? message = null, string? errorCode = null)
        {
            return new BaseResponse<T>
            {
                Errores = errors ?? ["Access denied"],
                Mensaje = message,
                Datos = default,
                CodigoError = errorCode ?? "FORBIDDEN"
            };
        }

        /// <summary>
        /// Creates a Forbidden (403) response with a single error.
        /// </summary>
        /// <param name="error">Single error explaining why access is forbidden.</param>
        /// <param name="message">Optional additional message describing the response.</param>
        /// <param name="errorCode">Optional error code for the response.</param>
        /// <returns>A BaseResponse object with status 403 and the provided error.</returns>
        public static BaseResponse<T> Forbidden(string error, string? message = null, string? errorCode = null)
        {
            // Internally calls the method that accepts a list of errors
            return Forbidden([error], message, errorCode);
        }


        /// <summary>
        /// Crea una respuesta con un código de estado HTTP personalizado.
        /// Este método se puede usar cuando la respuesta no encaja en las categorías predefinidas.
        /// </summary>
        /// <param name="data">Datos de la respuesta opcionales.</param>
        /// <param name="message">Mensaje opcional que describe la respuesta.</param>
        /// <param name="status">Código de estado HTTP para la respuesta.</param>
        /// <param name="errors">Lista de errores opcionales asociados con la respuesta.</param>
        /// <param name="errorCode">Código de error opcional para la respuesta.</param>
        /// <returns>Un objeto BaseResponse con el estado, datos, mensaje y errores especificados.</returns>
        public static BaseResponse<T> CustomStatus(HttpStatusCode status, T? data = default, string? message = null, List<string>? errors = null, string? errorCode = null)
        {
            return new BaseResponse<T>
            {
                Datos = data,
                Mensaje = message,
                Errores = errors,
                CodigoError = errorCode
            };
        }

        /// <summary>
        /// C   rea una respuesta de error de validación (422) con una lista de errores.
        /// </summary>
        /// <param name="errors">Lista de errores que explican el error de validación.</param>
        /// <param name="message">Mensaje opcional que describe la respuesta.</param>
        /// <param name="errorCode">Código de error opcional para la respuesta.</param>
        /// <returns>Un objeto BaseResponse con el estado 422 y los errores proporcionados.</returns>
        public static BaseResponse<T> ValidationError(List<string> errors, string? message = null, string? errorCode = null)
        {
            return new BaseResponse<T>
            {
                Errores = errors,
                Mensaje = message ?? "Error de validación",
                Datos = default,
                CodigoError = errorCode ?? "VALIDATION_ERROR"
            };
        }

        /// <summary>
        /// Crea una respuesta de error genérico (400) con un mensaje de error.
        /// </summary>
        /// <param name="message">Mensaje que describe el error.</param>
        /// <param name="errorCode">Código de error opcional para la respuesta.</param>
        /// <returns>Un objeto BaseResponse con el estado de error.</returns>
        public static BaseResponse<T> Error(string message, string? errorCode = null)
        {
            return new BaseResponse<T>
            {
                Errores = [message],
                Mensaje = message,
                Datos = default,
                CodigoError = errorCode ?? "ERROR"
            };
        }

        /// <summary>
        /// Crea una respuesta de error genérico (400) con una lista de errores.
        /// </summary>
        /// <param name="message">Mensaje principal que describe el error.</param>
        /// <param name="errors">Lista de errores adicionales.</param>
        /// <param name="errorCode">Código de error opcional para la respuesta.</param>
        /// <returns>Un objeto BaseResponse con el estado de error.</returns>
        public static BaseResponse<T> Error(string message, List<string> errors, string? errorCode = null)
        {
            return new BaseResponse<T>
            {
                Errores = errors,
                Mensaje = message,
                Datos = default,
                CodigoError = errorCode ?? "ERROR"
            };
        }

        /// <summary>
        /// Crea una respuesta de error genérico (400) con un mensaje de error y detalles adicionales.
        /// </summary>
        /// <param name="message">Mensaje que describe el error.</param>
        /// <param name="details">Detalles adicionales del error.</param>
        /// <param name="errorCode">Código de error opcional para la respuesta.</param>
        /// <returns>Un objeto BaseResponse con el estado de error.</returns>
        public static BaseResponse<T> Error(string message, object details, string? errorCode = null)
        {
            return new BaseResponse<T>
            {
                Errores = [message],
                Mensaje = message,
                Datos = default,
                CodigoError = errorCode ?? "ERROR",
                InformacionAdicional = new Dictionary<string, object> { ["details"] = details }
            };
        }
    }
}
