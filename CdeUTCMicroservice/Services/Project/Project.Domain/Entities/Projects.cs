

namespace Project.Domain.Entities
{
    public class Projects : BaseEntity<ProjectId>
    {
        public string Name { get; set; } = default!;
        public string ImageUrl { get; set; } = default!;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Description { get; set; } = default!;
        public bool TodoVisibility { get; set; }
        public bool InvitationVisibility { get; set; }
        public bool EmailNotification { get; set; }
        public UnitSystem UnitSystem { get; set; } = UnitSystem.Metric;
        public UnitLength UnitLength { get; set; } = UnitLength.Meters;
        public UnitLengthPrecision UnitLengthPrecision { get; set; } = UnitLengthPrecision.Zero;
        public bool IsCheckMeasurement { get; set; } = true;
        public UnitArea UnitArea { get; set; } = UnitArea.SquareMeters;
        public UnitAreaPrecision UnitAreaPrecision { get; set; } = UnitAreaPrecision.OneHundredth;
        public UnitWeight UnitWeight { get; set; } = UnitWeight.Kilograms;
        public UnitWeightPrecision UnitWeightPrecision { get; set; } = UnitWeightPrecision.OneThousandth;
        public UnitVolume UnitVolume { get; set; } = UnitVolume.CubicCentimeters;
        public UnitVolumePrecision UnitVolumePrecision { get; set; } = UnitVolumePrecision.OneHundredth;
        public UnitAngle UnitAngle { get; set; } = UnitAngle.Degrees;
        public UnitAnglePrecision UnitAnglePrecision { get; set; } = UnitAnglePrecision.OneThousandth;
        public string Ownership { get; set; } = "University Of Transformation Communication"!;
        public TimeSpan DigestTime { get; set; } = new TimeSpan(5,0,0);
        public ActivityType ActivityType { get; set; } = ActivityType.Instant;

        //Relation
        public ICollection<BCFTopic>? BCFTopics { get; set; }
        public ICollection<Status>? Statuses { get; set; }
        public ICollection<Type>? Types { get; set; }
        public ICollection<Priority>? Priorities { get; set; }
        public Projects()
        {
            // Initialize Statuses by passing ProjectId
            Statuses = Status.InitData(Id);
            Types = Type.InitData(Id);
            Priorities = Priority.InitData(Id);

        }

    }
}
