namespace Replace_Stuff_Compatibility.ModPatch
{
	public class VanillaExpandedHelixienGas: AbstractPatch
	{
		protected override string GetRequiredModNames() => "vanillaexpanded.helixiengas";

		protected override void AddItems()
		{
			var biofuelRefinery = GetDatabaseThing("BiofuelRefinery");
			var helixienRefinery = GetDatabaseThing("VHGE_GasPoweredRefinery");
			
			var electricCrematorium = GetDatabaseThing("ElectricCrematorium");
			var helexienCrematorium = GetDatabaseThing("VHGE_GasPoweredCrematorium");
			
			var helixienGenerator = GetDatabaseThing("VHGE_HelixienGenerator");
			var largeHelixienGenerator = GetDatabaseThing("VHGE_IndustrialHelixienGenerator");

			MultiModPatch.Lights.Add(GetDatabaseThing("VHGE_GasLamp"));
			MultiModPatch.Lights.Add(GetDatabaseThing("VHGE_GasFloodlight"));
			
			MultiModPatch.PoweredGenerators.Add(helixienGenerator);
			MultiModPatch.PoweredGenerators.Add(largeHelixienGenerator);
			
			MultiModPatch.Smelters.Add(GetDatabaseThing("VHGE_GasPoweredSmelter"));
			MultiModPatch.Smithys.Add(GetDatabaseThing("VHGE_GasPoweredSmithy"));
			MultiModPatch.Stoves.Add(GetDatabaseThing("VHGE_GasPoweredStove"));

			MultiModPatch.Sunlamps.Add(GetDatabaseThing("VHGE_GasSunLamp"));
			
			AddInterchangeableList(biofuelRefinery, helixienRefinery);
			AddInterchangeableList(electricCrematorium, helexienCrematorium);
		}
	}
}