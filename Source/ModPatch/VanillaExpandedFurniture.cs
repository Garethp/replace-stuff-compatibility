using RimWorld;
using Verse;

namespace Replace_Stuff_Compatibility.ModPatch
{
	public class VanillaExpandedFurniture: AbstractPatch
	{
		[DefOf]
		public static class BaseGameThingDefs
		{
			public static ThingDef EndTable;
			public static ThingDef Dresser;
		}

		protected override string GetRequiredModNames() => "vanillaexpanded.vfecore";

		protected override void AddItems()
		{
			var campfire = GetDatabaseThing("Campfire");
			var stoneCampfire = GetDatabaseThing("Stone_Campfire");

			var oldRadio = GetDatabaseThing("Radio_Industrial");
			var radio = GetDatabaseThing("Radio_Spacer");
			
			MultiModPatch.Lights.Add(GetDatabaseThing("Light_Streetlamp"));
			MultiModPatch.Lights.Add(GetDatabaseThing("Table_LightEndTable"));
			
			MultiModPatch.EndTables.Add(GetDatabaseThing("Table_LightEndTable"));
			MultiModPatch.EndTables.Add(GetDatabaseThing("Table_RoyalEndTable"));
			
			MultiModPatch.Dressers.Add(GetDatabaseThing("Table_RoyalDresser"));
			
			MultiModPatch.Wardrobes.Add(GetDatabaseThing("Table_Wardrobe"));

			AddInterchangeableList(campfire, stoneCampfire);
			AddInterchangeableList(oldRadio, radio);
		}
	}
}