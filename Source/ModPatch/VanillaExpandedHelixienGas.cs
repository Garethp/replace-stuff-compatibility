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
			
			MultiModPatch.Smelters.Add(GetDatabaseThing("VHGE_GasPoweredSmelter"));
			MultiModPatch.Smithys.Add(GetDatabaseThing("VHGE_GasPoweredSmithy"));
			MultiModPatch.Stoves.Add(GetDatabaseThing("VHGE_GasPoweredStove"));
			
			AddInterchangeableList(biofuelRefinery, helixienRefinery);
			AddInterchangeableList(electricCrematorium, helexienCrematorium);
		}
	}
}