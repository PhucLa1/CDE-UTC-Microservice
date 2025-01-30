using FluentValidation;
using Project.Application.Features.Unit.UpdateUnitByProjectId;

namespace Project.API.Endpoint.Unit.UpdateUnitByProjectId
{
    public class UpdateUnitByProjectIdValidator : AbstractValidator<UpdateUnitByProjectIdRequest>
    {
        public UpdateUnitByProjectIdValidator()
        {
            RuleFor(x => x.ProjectId)
            .NotEmpty().WithMessage("ProjectId không được để trống.");

            RuleFor(x => x.UnitSystem)
                .NotNull().WithMessage("UnitSystem không được để trống.")
                .IsInEnum().WithMessage("UnitSystem phải là giá trị trong enum.");

            RuleFor(x => x.UnitLength)
                .NotNull().WithMessage("UnitLength không được để trống.")
                .IsInEnum().WithMessage("UnitLength phải là giá trị trong enum.");

            RuleFor(x => x.UnitLengthPrecision)
                .NotNull().WithMessage("UnitLengthPrecision không được để trống.")
                .IsInEnum().WithMessage("UnitLengthPrecision phải là giá trị trong enum.");

            RuleFor(x => x.UnitArea)
                .NotNull().WithMessage("UnitArea không được để trống.")
                .IsInEnum().WithMessage("UnitArea phải là giá trị trong enum.");

            RuleFor(x => x.UnitAreaPrecision)
                .NotNull().WithMessage("UnitAreaPrecision không được để trống.")
                .IsInEnum().WithMessage("UnitAreaPrecision phải là giá trị trong enum.");

            RuleFor(x => x.UnitWeight)
                .NotNull().WithMessage("UnitWeight không được để trống.")
                .IsInEnum().WithMessage("UnitWeight phải là giá trị trong enum.");

            RuleFor(x => x.UnitWeightPrecision)
                .NotNull().WithMessage("UnitWeightPrecision không được để trống.")
                .IsInEnum().WithMessage("UnitWeightPrecision phải là giá trị trong enum.");

            RuleFor(x => x.UnitVolume)
                .NotNull().WithMessage("UnitVolume không được để trống.")
                .IsInEnum().WithMessage("UnitVolume phải là giá trị trong enum.");

            RuleFor(x => x.UnitVolumePrecision)
                .NotNull().WithMessage("UnitVolumePrecision không được để trống.")
                .IsInEnum().WithMessage("UnitVolumePrecision phải là giá trị trong enum.");

            RuleFor(x => x.UnitAngle)
                .NotNull().WithMessage("UnitAngle không được để trống.")
                .IsInEnum().WithMessage("UnitAngle phải là giá trị trong enum.");

            RuleFor(x => x.UnitAnglePrecision)
                .NotNull().WithMessage("UnitAnglePrecision không được để trống.")
                .IsInEnum().WithMessage("UnitAnglePrecision phải là giá trị trong enum.");

            RuleFor(x => x.IsCheckMeasurement)
                .NotNull().WithMessage("IsCheckMeasurement must be specified.");
        }

    }
}
