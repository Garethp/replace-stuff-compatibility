using Replace_Stuff.NewThing;
using RimWorld;
using Verse;

namespace Replace_Stuff_Compatibility.Comps;

public class TransferBills: IReplacementComp
{
	public void PreAction(Thing newThing, Thing oldThing)
	{
		if (newThing is not Building_WorkTable newTable || oldThing is not Building_WorkTable oldTable) return;
		
		// The base ReplaceStuff already has transferring bills from one table to another for hand/electric tailoring benches
		// and for fueled/electric stove, so we need to skip those
		if ((newTable.def == NewThingDefOf.HandTailoringBench || newTable.def == NewThingDefOf.ElectricTailoringBench) &&
		    (oldTable.def == NewThingDefOf.HandTailoringBench || oldTable.def == NewThingDefOf.ElectricTailoringBench))
			return;
			
		if ((newTable.def == NewThingDefOf.FueledStove || newTable.def == NewThingDefOf.ElectricStove) &&
		    (oldTable.def == NewThingDefOf.FueledStove || oldTable.def == NewThingDefOf.ElectricStove))
			return;
		
		foreach (var bill in oldTable.BillStack)
		{
			newTable.BillStack.AddBill(bill);
		}
	}

	public void PostAction(Thing newThing, Thing oldThing)
	{
	}
}