using Verse;

namespace Replace_Stuff_Compatibility.Comps;

public interface IReplacementComp
{
	abstract void PreAction(Thing newThing, Thing oldThing);

	abstract void PostAction(Thing newThing, Thing oldThing);
}