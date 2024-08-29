
using System.ComponentModel.DataAnnotations;

namespace Construction.Domain.Validation
{

    public class AllowedNamesAttribute : ValidationAttribute
    {
        private readonly string[] _allowedNames;

        public AllowedNamesAttribute(params string[] allowedNames)
        {
            _allowedNames = allowedNames;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is string stringValue)
            {
                if (Array.Exists(_allowedNames, name => name.Equals(stringValue, StringComparison.OrdinalIgnoreCase)))
                {
                    return ValidationResult.Success;
                }
            }

            return new ValidationResult($"The field {validationContext.DisplayName} must be one of the following values: {string.Join(", ", _allowedNames)}.");
        }
    }
}
