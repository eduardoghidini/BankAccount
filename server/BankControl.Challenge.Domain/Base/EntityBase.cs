using BankAccount.Warren.Domain.Abstractions;
using FluentValidation;
using FluentValidation.Results;
using System.ComponentModel.DataAnnotations.Schema;

namespace BankAccount.Warren.Domain.Base
{
    public abstract class EntityBase : IEntity
    {
        public virtual int Id { get; set; }

        [NotMapped]
        public bool Valid { get; private set; }
        
        [NotMapped]
        public bool Invalid => !Valid;

        [NotMapped]
        public ValidationResult ValidationResult { get; private set; }

        protected bool Validate<TModel>(TModel model, AbstractValidator<TModel> validator)
        {
            ValidationResult = validator.Validate(model);

            return Valid = ValidationResult.IsValid;
        }
    }
}