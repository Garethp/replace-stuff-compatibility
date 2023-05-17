using System.Collections.Generic;
using Verse;

namespace Replace_Stuff_Compatibility;

public class InterchangableItems : Def
{
	public List<ReplaceList> replaceLists = new();
}

public class ReplaceList
{
	public bool isWall = false;
	
	public bool isWorkbench = false;
	
	public string category = "";
	
	public List<ThingDef> items = new();

	public List<string> comps = new();
}