using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SimpleBankingSystem.WebApp.Models
{
    public class BaseResultDataModel<T> : BaseResultModel where T : class
    {
        public BaseResultDataModel() { }

        public BaseResultDataModel(IEnumerable<String> errors) : base(errors) { }

        public BaseResultDataModel(T data)
            : this()
        {
            Data = data;
        }

        public T Data { get; set; }
    }
}