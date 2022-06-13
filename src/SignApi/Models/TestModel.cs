using SignApi.AuthSecurityBinder.RsaBinder;
using System.ComponentModel.DataAnnotations;

namespace SignApi.Models
{
    [RsaModelParse]
    public class TestModel
    {
        [Display(Name = "id"), Required(ErrorMessage = "{0}不能为空")]
        public string Id { get; set; }
    }
}
