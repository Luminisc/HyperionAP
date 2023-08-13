using System.ComponentModel.DataAnnotations;

namespace HSAT.Validators
{
    public sealed class LessThanAttribute : ValidationAttribute
    {
        private readonly string errorMessage;
        private readonly bool orEqual;

        public LessThanAttribute(string propertyName, string errorMessage = "", bool orEqual = false)
        {
            PropertyName = propertyName;
            this.errorMessage = errorMessage;
            this.orEqual = orEqual;
        }

        public string PropertyName { get; }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return ValidationResult.Success;
            }

            var instance = validationContext.ObjectInstance;
            var otherValue = instance.GetType().GetProperty(PropertyName).GetValue(instance);

            int comparison = ((IComparable)value).CompareTo(otherValue);
            if (comparison < 0 || (comparison == 0 && orEqual))
            {
                return ValidationResult.Success;
            }

            return new ValidationResult(errorMessage);
        }
    }
}
