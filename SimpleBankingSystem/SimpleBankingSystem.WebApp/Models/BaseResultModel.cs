using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SimpleBankingSystem.WebApp.Models
{
    public class BaseResultModel
    {
        public BaseResultModel()
        {
            Errors = new List<String>();
        }

        public BaseResultModel(IEnumerable<String> errors)
        {
            Errors = errors;
        }

        public IEnumerable<String> Errors { get; set; }

        public Boolean IsValid { get { return !Errors.Any(); } }
    }

}