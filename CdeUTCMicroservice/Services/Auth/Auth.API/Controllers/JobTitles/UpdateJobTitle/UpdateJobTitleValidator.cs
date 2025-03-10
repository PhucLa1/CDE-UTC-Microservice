﻿using Auth.Application.JobTitles.UpdateJobTitle;

namespace Auth.API.Controllers.JobTitles.UpdateJobTitle
{
    public class UpdateJobTitleValidator : AbstractValidator<UpdateJobTitleRequest>
    {
        public UpdateJobTitleValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .WithMessage("Id không được để trống");
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("Tên không được để trống");
        }
    }
}
