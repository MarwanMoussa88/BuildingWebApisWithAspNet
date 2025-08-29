using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using BuildingWebApisWithAspNet.Attributes;

namespace BuildingWebApisWithAspNet.Models
{
    public class RequestDTO<T> : IValidatableObject where T : class
    {
        [DefaultValue(value: 0)]
        [Range(minimum: 0, 100, ErrorMessage = "Value is invalid")]
        public int PageIndex { get; set; }
        [Range(minimum: 1, 100, ErrorMessage = "Value is invalid")]
        [DefaultValue(value: 1)]
        public int PageSize { get; set; }
        public string FilterQuery { get; set; }
        [DefaultValue(value: "Name")]
        public string SortColumn { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var sortColumnValidator = new SortColumnValidationAttribute<T>();
            var result = sortColumnValidator.GetValidationResult(SortColumn, validationContext);

            return result is null ? [result] : [];

        }
    }
}
