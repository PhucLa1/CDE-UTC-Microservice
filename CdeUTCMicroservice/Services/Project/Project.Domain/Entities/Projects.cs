

namespace Project.Domain.Entities
{
    public class Projects : Aggregate<ProjectId>
    {
        public string Name { get; private set; } = default!;
        public string ImageUrl { get; private set; } = default!;
        public DateTime StartDate { get; private set; }
        public DateTime EndDate { get; private set; }
        public string Description { get; private set; } = default!;
        public bool TodoVisibility { get; private set; }
        public bool InvitationVisibility { get; private set; }
        public bool EmailNotification { get; private set; }
        public UnitSystem UnitSystem { get; private set; } = UnitSystem.Metric;
        public UnitLength UnitLength { get; private set; } = UnitLength.Meters;
        public UnitLengthPrecision UnitLengthPrecision { get; private set; } = UnitLengthPrecision.Centimeters;
        public bool IsCheckMeasurement { get; private set; } = true;
        public UnitArea UnitArea { get; private set; } = UnitArea.SquareMeters;
        public UnitAreaPrecision UnitAreaPrecision { get; private set; } = UnitAreaPrecision.SquareCentimeters;
        public UnitWeight UnitWeight { get; private set; } = UnitWeight.Kilograms;
        public UnitWeightPrecision UnitWeightPrecision { get; private set; } = UnitWeightPrecision.Grams;
        public UnitVolume UnitVolume { get; private set; } = UnitVolume.Liters;
        public UnitVolumePrecision UnitVolumePrecision { get; private set; } = UnitVolumePrecision.CubicInches;
        public UnitAngle UnitAngle { get; private set; } = UnitAngle.Degrees;
        public UnitAnglePrecision UnitAnglePrecision { get; private set; } = UnitAnglePrecision.Minutes;
        public string Ownership { get; private set; } = "University Of Transformation Communication"!;
        public TimeSpan DigestTime { get; private set; } = new TimeSpan(5,0,0);
        public ActivityType ActivityType { get; private set; } = ActivityType.Instant;

        public static Projects Create(string name, string imageUrl, DateTime startDate, DateTime endDate, string description, bool todoVisibility, bool invitationVisibility, bool emailNotification)
        {
            if (startDate < endDate)
                throw new ArgumentOutOfRangeException("Start Date cannot max than End Date");
            var project = new Projects
            {
                Name = name,
                ImageUrl = imageUrl,
                StartDate = startDate,
                EndDate = endDate,
                Description = description,
                TodoVisibility = todoVisibility,
                InvitationVisibility = invitationVisibility,
                EmailNotification = emailNotification
            };
            return project;
        }
    }
}
