using RimWorld;
using Verse;

namespace Replace_Stuff_Compatibility.Comps;

public class CopyStorageSettingsComp: IReplacementComp
{
	public void PreAction(Thing newThing, Thing oldThing)
	{
		if (newThing is not Building_Storage newStorage || oldThing is not Building_Storage oldStorage) return;
		
		newStorage.settings.CopyFrom(oldStorage.settings);
	}

	public void PostAction(Thing newThing, Thing oldThing)
	{
	}
}