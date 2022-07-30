﻿using FluentValidation;


namespace Notes.Application.Commands
{
    public class DeleteNoteCommandValidator : AbstractValidator<DeleteNoteCommand>
    {
        public DeleteNoteCommandValidator()
        {
            RuleFor(command => command.Id)
                .NotEqual(Guid.Empty);
            RuleFor(command => command.UserId)
                .NotEqual(Guid.Empty);
        }
    }
}
