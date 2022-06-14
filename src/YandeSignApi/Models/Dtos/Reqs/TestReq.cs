using System.ComponentModel.DataAnnotations;

namespace YandeSignApi.Models.Dtos.Reqs
{
    public class TestReq
    {
        [Display(Name = "id"), Required(ErrorMessage = "{0}不能为空")]
        public string Id { get; set; }
    }
}
