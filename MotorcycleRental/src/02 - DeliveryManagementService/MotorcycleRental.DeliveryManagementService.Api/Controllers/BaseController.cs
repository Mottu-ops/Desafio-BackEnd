using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc;
using MotorcycleRental.DeliveryManagementService.Api.Config.Models;

namespace MotorcycleRental.DeliveryManagementService.Api.Controllers
{
    [ApiController]
    public abstract class BaseController : ControllerBase
    {
        public ICollection<string> Erros = new List<string>();
        public ICollection<Dictionary<string, string[]>> ErrorsValidation = new List<Dictionary<string, string[]>>();
        protected ActionResult CustomResponse(object result = null)
        {
            if (Isvalid())
                return Ok(result);

            return BadRequest(new CustomErrorResponse(400, new ErrorDetail(Erros.ToList())));
        }

        protected ActionResult CustomResponse(ModelStateDictionary modelState)
        {
            var erros = modelState.Values.SelectMany(e => e.Errors);
            foreach (var error in erros)
            {
                AddError(error.ErrorMessage);
            }
            return CustomResponse();
        }

        protected bool Isvalid()
        {
            if (Erros.Any() || ErrorsValidation.Any())
                return false;
            return true;
        }

        protected bool IsIdValid(string id) {
            if (string.IsNullOrEmpty(id))
            {
                AddError("The id parameter must be informed!");
                return false;
            }
            return true;
        }

        protected void AddError(string erro)
        {
            Erros.Add(erro);
        }

        protected void ClearErrors()
        {
            Erros.Clear();
        }
    }
}
