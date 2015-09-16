using SimpleBankingSystem.WebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SimpleBankingSystem.WebApp.Controllers
{
    public class BaseApiController : ApiController
    {
        protected IEnumerable<String> GetErrorsFromModelState()
        {
            return ModelState.Values.SelectMany(x => x.Errors.Select(e => e.ErrorMessage));
        }

        protected IHttpActionResult OkResult()
        {
            return base.Ok(new BaseResultModel { Errors = GetErrorsFromModelState() });
        }

        protected IHttpActionResult OkResult(Exception error)
        {
            var errors = GetErrorsFromModelState().ToList();
            errors.Add(error.Message);

            return base.Ok(new BaseResultModel { Errors = errors });
        }

        protected IHttpActionResult OkResult<T>(T data) where T : class
        {
            return base.Ok(new BaseResultDataModel<T>(data) { Errors = GetErrorsFromModelState() });
        }
    }

}
