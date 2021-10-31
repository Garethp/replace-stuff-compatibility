﻿namespace Replace_Stuff_Compatibility
{
	public class IdeologyUpgrades: AbstractPatch
	{
		protected override string GetRequiredModNames() => "ludeon.rimworld.ideology";

		protected override void AddItems()
		{
			MultiModPatch.Lights.Add(GetDatabaseThing("Darklamp"));
			MultiModPatch.Lights.Add(GetDatabaseThing("Darktorch"));
			MultiModPatch.Lights.Add(GetDatabaseThing("DarktorchFungus"));
		}
	}
}