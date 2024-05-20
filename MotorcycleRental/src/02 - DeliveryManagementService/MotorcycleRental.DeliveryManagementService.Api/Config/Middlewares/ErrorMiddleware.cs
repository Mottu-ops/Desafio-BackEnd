using Microsoft.EntityFrameworkCore;
using MotorcycleRental.DeliveryManagementService.Api.Config.Models;
using MotorcycleRental.DeliveryManagementService.Service.Exceptions;
using Npgsql;
using System.Net;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace MotorcycleRental.DeliveryManagementService.Api.Config.Middlewares
{
    public class ErrorMiddleware
    {
        private readonly RequestDelegate next;

        public ErrorMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            CustomErrorResponse errorResponse;
            try
            {
                await next(context);
            }
            catch (DbUpdateException ex) when (ex.InnerException is PostgresException postgresEx && postgresEx.SqlState == "23505")
            {
                await HandleExceptionAsync(context, ex, HttpStatusCode.Conflict, ExtractMessageUniqueConstraintViolation(ex.InnerException.Message));                
            }
            catch (DbUpdateException ex) when (ex.InnerException is PostgresException postgresEx && postgresEx.SqlState == "23503")
            {
                await HandleExceptionAsync(context, ex, HttpStatusCode.Conflict, GetTableForeignKeyConstraint(ex.InnerException.Message));
            }
            catch (Exception error)
            {
                switch (error)
                {
                    case DuplicateKeyException e:
                        await HandleExceptionAsync(context, e);
                        break;
                    case NotFoundException e:
                        await HandleExceptionAsync(context, e, HttpStatusCode.NotFound);
                        break;

                    default:
                        await HandleExceptionAsync(context, error);
                        break;
                }
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception ex, HttpStatusCode? statusCode = null, string message = null)
        {
            CustomErrorResponse errorResponse;
            string _message = message == null ? $"{ex.Message} {ex?.InnerException?.Message}" : message;
            HttpStatusCode _statusCode = statusCode ?? HttpStatusCode.InternalServerError;
            if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development" ||
                Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Qa")
            {
                errorResponse =
                    new CustomErrorResponse((int)_statusCode,
                                            new ErrorDetail(new List<string>() { _message }));
            }
            else
            {
                errorResponse =
                    new CustomErrorResponse((int)_statusCode,
                                            new ErrorDetail(new List<string>() { "An internal server error has occurred." }));
            }

            context.Response.StatusCode = (int)_statusCode;

            var result = JsonSerializer.Serialize(errorResponse);
            context.Response.ContentType = "application/json";
            return context.Response.WriteAsync(result);
        }

        private static string GetTableForeignKeyConstraint(string errorMessage)
        {
            string pattern = "\"([^\"]*)\"";

            Match match = Regex.Match(errorMessage, pattern);
            if (match.Success)
                return $"Update or delete on table {match.Groups[1].Value} violates foreign key constraint.";
            else
                return "Foreign key constraint violation.";
        }

        private string ExtractMessageUniqueConstraintViolation(string errorMessage)
        {
            string pattern = "\"([^\"]*)\"";
             Match match = Regex.Match(errorMessage, pattern);
            if (match.Success)
                return $"Duplicate key value violates unique constraint {match.Groups[1].Value}.";
            else
                return "Duplicate key value violates unique constraint";
        }
    }
}
