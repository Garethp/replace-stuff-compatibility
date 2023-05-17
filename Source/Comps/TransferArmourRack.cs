using ArmorRacks.ThingComps;
using ArmorRacks.Things;
using RimWorld;
using Verse;

namespace Replace_Stuff_Compatibility.Comps;

public class TransferArmourRack: IReplacementComp
{
	public void PreAction(Thing newThing, Thing oldThing)
	{
		if (newThing is not ArmorRack newRack || oldThing is not ArmorRack oldRack) return;

		newRack.Settings.CopyFrom(oldRack.GetStoreSettings());
		if (newRack.PawnKindDef != oldRack.PawnKindDef)
			newRack.PawnKindDef = oldRack.PawnKindDef;
		else
			newRack.BodyTypeDef = oldRack.BodyTypeDef;
					
		oldRack.InnerContainer.TryTransferAllToContainer(newRack.InnerContainer);
					
		if (oldRack.GetAssignedPawn() != null) 
			newRack.TryGetComp<CompAssignableToPawn_ArmorRacks>().TryAssignPawn(oldRack.GetAssignedPawn());
	}

	public void PostAction(Thing newThing, Thing oldThing)
	{
	}
}