import { UnitAngle } from "@/data/enums/unitangle.enum";
import { UnitAnglePrecision } from "@/data/enums/unitangleprecision.enum";
import { UnitArea } from "@/data/enums/unitarea.enum";
import { UnitAreaPrecision } from "@/data/enums/unitareaprecision.enm";
import { UnitLength } from "@/data/enums/unitlength.enum";
import { UnitLengthPrecision } from "@/data/enums/unitlengthprecision.enum";
import { UnitSystem } from "@/data/enums/unitsystem.enum";
import { UnitVolume } from "@/data/enums/unitvolume.enum";
import { UnitVolumePrecision } from "@/data/enums/unitvolumeprecision.enum";
import { UnitWeight } from "@/data/enums/unitweight.enum";
import { UnitWeightPrecision } from "@/data/enums/unitweigthprecision.enum";
import { z } from "zod";
// We're keeping a simple non-relational schema here.
// IRL, you will have a schema for your data models.
export const unitSchema = z.object({
    projectId: z.number(),
    unitSystem: z.nativeEnum(UnitSystem),
    unitLength: z.nativeEnum(UnitLength),
    unitLengthPrecision: z.nativeEnum(UnitLengthPrecision),
    isCheckMeasurement: z.boolean(),
    unitArea: z.nativeEnum(UnitArea),
    unitAreaPrecision: z.nativeEnum(UnitAreaPrecision),
    unitWeight: z.nativeEnum(UnitWeight),
    unitWeightPrecision: z.nativeEnum(UnitWeightPrecision),
    unitVolume: z.nativeEnum(UnitVolume),
    unitVolumePrecision: z.nativeEnum(UnitVolumePrecision),
    unitAngle: z.nativeEnum(UnitAngle),
    unitAnglePrecision: z.nativeEnum(UnitAnglePrecision),
});

export type Unit = z.infer<typeof unitSchema>;
export const unitDefault: Unit = {
    projectId: 0,
    unitSystem: UnitSystem.Metric,
    unitLength: UnitLength.Meters,
    unitLengthPrecision: UnitLengthPrecision.Zero,
    isCheckMeasurement: false,
    unitArea: UnitArea.SquareMeters,
    unitAreaPrecision: UnitAreaPrecision.OneHundredth,
    unitWeight: UnitWeight.Kilograms,
    unitWeightPrecision: UnitWeightPrecision.OneHundredth,
    unitVolume: UnitVolume.CubicCentimeters,
    unitVolumePrecision: UnitVolumePrecision.OneHundredth,
    unitAngle: UnitAngle.Degrees,
    unitAnglePrecision: UnitAnglePrecision.OneHundredth,
};