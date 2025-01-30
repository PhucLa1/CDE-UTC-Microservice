namespace Project.Application.Features.Unit.UpdateUnitByProjectId
{
    public class UpdateUnitByProjectIdRequest : ICommand<UpdateUnitByProjectIdResponse>
    {
        public Guid ProjectId { get; set; }
        public UnitSystem UnitSystem { get; set; }
        public UnitLength UnitLength { get; set; }
        public UnitLengthPrecision UnitLengthPrecision { get; set; }
        public bool IsCheckMeasurement { get; set; }
        public UnitArea UnitArea { get; set; }
        public UnitAreaPrecision UnitAreaPrecision { get; set; }
        public UnitWeight UnitWeight { get; set; }
        public UnitWeightPrecision UnitWeightPrecision { get; set; }
        public UnitVolume UnitVolume { get; set; }
        public UnitVolumePrecision UnitVolumePrecision { get; set; }
        public UnitAngle UnitAngle { get; set; }
        public UnitAnglePrecision UnitAnglePrecision { get; set; }
    }
}
