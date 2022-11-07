namespace Replace_Stuff_Compatibility.ModPatch
{
	public class VanillaExpandedSecurity : AbstractPatch
	{
		protected override string GetRequiredModNames() => "vanillaexpanded.vfesecurity";

		protected override void AddItems()
		{
			var sandbags = GetDatabaseThing("Sandbags");
			var barbedWire = GetDatabaseThing("VFES_BarbedWire");
			var barricade = GetDatabaseThing("Barricade");
			var deployableBarrier = GetDatabaseThing("VFES_DeployableBarrier");

			var turret = GetDatabaseThing("Turret_MiniTurret");
			var deployableTurret = GetDatabaseThing("VFES_Turret_FloorTurret");
			var militaryTurret = GetDatabaseThing("VFES_Turret_MilitaryTurret");
			var searchLight = GetDatabaseThing("VFES_Turret_Searchlight");
			var flameTurret = GetDatabaseThing("VFES_Turret_Flame");
			var sentryGun = GetDatabaseThing("VFES_Turret_SentryGun");
			var chargeTurret = GetDatabaseThing("VFES_Turret_ChargeTurret");

			var spikeTrap = GetDatabaseThing("TrapSpike");
			var bearTrap = GetDatabaseThing("VFES_BearTrap");

			var ballista = GetDatabaseThing("VFES_Turret_Ballista");
			var autocannon = GetDatabaseThing("Turret_Autocannon");
			var uraniumSlugTurret = GetDatabaseThing("Turret_Sniper");
			var doubleAutocannon = GetDatabaseThing("VFES_Turret_AutocannonDouble");
			var chargeRailgun = GetDatabaseThing("VFES_Turret_ChargeRailgunTurret");
			var empTurret = GetDatabaseThing("VFES_Turret_EMPTurret");

			var smallShield = GetDatabaseThing("VFES_ShieldGenerator_Small");
			var largeShield = GetDatabaseThing("VFES_ShieldGenerator_Large");

			AddInterchangeableList(sandbags, barbedWire, barricade, deployableBarrier);
			AddInterchangeableList(
				turret, deployableTurret, militaryTurret, searchLight, flameTurret, sentryGun, chargeTurret);
			AddInterchangeableList(spikeTrap, bearTrap);
			AddInterchangeableList(ballista, autocannon, uraniumSlugTurret, doubleAutocannon, chargeRailgun, empTurret);
			AddInterchangeableList(spikeTrap, bearTrap);
			AddInterchangeableList(smallShield, largeShield);
		}
	}
}