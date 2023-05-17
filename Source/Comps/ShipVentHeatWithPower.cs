using RimWorld;
using Verse;

namespace Replace_Stuff_Compatibility.Comps;

public class ShipVentHeatWithPower: IReplacementComp
{
	public void PreAction(Thing newThing, Thing oldThing)
	{
		if (newThing is not Building_ShipVent newVent || oldThing is not Building_ShipVent oldVent) return;

		newVent.heatWithPower = oldVent.heatWithPower;
	}

	public void PostAction(Thing newThing, Thing oldThing)
	{
	}
}