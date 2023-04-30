namespace Replace_Stuff_Compatibility.ModPatch
{
	public class VanillaExpandedPower : AbstractPatch
	{
		protected override string GetRequiredModNames() => "vanillaexpanded.vfepower";

		protected override void AddItems()
		{
			var advancedWindTurbine = GetDatabaseThing("VFE_AdvancedWindTurbine");
			
			var largeWatermillGenerator = GetDatabaseThing("VFE_AdvancedWatermillGenerator");

			var advancedGeothermalGenerator = GetDatabaseThing("VPE_AdvancedGeothermalGenerator");
		}
	}
}