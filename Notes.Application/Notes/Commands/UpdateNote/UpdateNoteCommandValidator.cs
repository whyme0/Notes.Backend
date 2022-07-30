using FluentValidation;

namespace Notes.Application.Commands
{
    public class UpdateNoteCommandValidator : AbstractValidator<UpdateNoteCommand>
    {
        public UpdateNoteCommandValidator()
        {
            RuleFor(command => command.Id)
                .NotEqual(Guid.Empty);
            RuleFor(command => command.UserId)
                .NotEqual(Guid.Empty);
            RuleFor(command => command.Title)
                .NotEmpty().MaximumLength(250);
        }
    }
}
