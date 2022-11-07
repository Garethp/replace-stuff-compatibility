namespace Replace_Stuff_Compatibility.ModPatch
{
	public class VanillaExpandedSpacer: AbstractPatch
	{
		protected override string GetRequiredModNames() => "vanillaexpanded.vfespacer";
		protected override void AddItems()
		{
			MultiModPatch.Lights.Add(GetDatabaseThing("Light_SpacerLamp"));
			MultiModPatch.Lights.Add(GetDatabaseThing("Spacer_OutdoorLamp"));
			
			MultiModPatch.EndTables.Add(GetDatabaseThing("Table_IlluminatedEndTable"));
			MultiModPatch.Dressers.Add(GetDatabaseThing("Table_IlluminatedDresser"));
			
			MultiModPatch.TVs.Add(GetDatabaseThing("UltrascreenTV"));
		}
	}
}