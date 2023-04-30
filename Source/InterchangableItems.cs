using System.Collections.Generic;
using Verse;

namespace Replace_Stuff_Compatibility;

public class InterchangableItems : Def
{
	public List<ReplaceList> ReplaceLists = new();
}

public class ReplaceList
{
	public bool IsWall = false;
	
	public bool IsWorkbench = false;
	
	public string Category = "";
	
	public List<ThingDef> Items = new();

	public List<string> comps = new();
}