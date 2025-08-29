using System.ComponentModel.DataAnnotations;

namespace BuildingWebApisWithAspNet.Attributes
{
    public class SortColumnValidationAttribute<T> : ValidationAttribute where T : class
    {

        
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            string propertyName = value as string;

            Type type = typeof(T);

            var properties = type.GetProperties().Select(c => c.Name).ToHashSet();

            if (properties.Contains(propertyName))
                return ValidationResult.Success;

            return new ValidationResult($"{propertyName} does not exist in {type.Name}");

        }
    }
}
