using RimWorld;
using Verse;

namespace Replace_Stuff_Compatibility.Comps;

public class ReplaceTemperatureControl: IReplacementComp
{
	public void PreAction(Thing newThing, Thing oldThing)
	{
		if (newThing is not Building_TempControl newTemp || oldThing is not Building_TempControl oldTemp) return;

		newTemp.compTempControl.targetTemperature = oldTemp.compTempControl.targetTemperature;
	}

	public void PostAction(Thing newThing, Thing oldThing)
	{
	}
}