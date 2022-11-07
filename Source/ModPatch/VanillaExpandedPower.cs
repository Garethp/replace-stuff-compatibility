namespace Replace_Stuff_Compatibility.ModPatch
{
	public class VanillaExpandedPower : AbstractPatch
	{
		protected override string GetRequiredModNames() => "vanillaexpanded.vfepower";

		protected override void AddItems()
		{
			var advancedSolarGenerator = GetDatabaseThing("VFE_AdvancedSolarGenerator");

			var advancedWindTurbine = GetDatabaseThing("VFE_AdvancedWindTurbine");

			var largeWoodGenerator = GetDatabaseThing("VFE_IndustrialWoodFiredGenerator");
			var largeChemfuelGenerator = GetDatabaseThing("VFE_IndustrialChemfuelPoweredGenerator");

			var largeWatermillGenerator = GetDatabaseThing("VFE_AdvancedWatermillGenerator");

			var advancedGeothermalGenerator = GetDatabaseThing("VPE_AdvancedGeothermalGenerator");
			
			MultiModPatch.PoweredGenerators.Add(largeWoodGenerator);
			MultiModPatch.PoweredGenerators.Add(largeChemfuelGenerator);

			MultiModPatch.SolarGenerators.Add(advancedSolarGenerator);
			
		}
	}
}