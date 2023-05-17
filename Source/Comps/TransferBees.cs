using RimBees;
using Verse;

namespace Replace_Stuff_Compatibility.Comps;

public class TransferBees: IReplacementComp
{
	public void PreAction(Thing newThing, Thing oldThing)
	{
		if (newThing is not Building_Beehouse newHouse || oldThing is not Building_Beehouse oldHouse) return;
		
		oldHouse.innerContainerDrones.TryTransferAllToContainer(newHouse.innerContainerDrones);
		oldHouse.innerContainerQueens.TryTransferAllToContainer(newHouse.innerContainerQueens);
	}

	public void PostAction(Thing newThing, Thing oldThing)
	{
	}
}