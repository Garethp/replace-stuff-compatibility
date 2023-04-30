using System.Collections.Generic;
using ArmorRacks.ThingComps;
using ArmorRacks.Things;
using RimWorld;
using Verse;

namespace Replace_Stuff_Compatibility.ModPatch
{
	public class ArmourRacks : AbstractPatch
	{
		protected override string GetRequiredModNames() => "khamenman.armorracks";

		protected override void AddItems()
		{
			var armourRack = GetDatabaseThing("ArmorRacks_ArmorRack");
			var mechanisedArmourRack = GetDatabaseThing("ArmorRacks_MechanizedArmorRack");
			var mendingArmourRack = GetDatabaseThing("ArmorRacks_MendingArmorRack");

			// When replacing an armour rack, make sure to copy over it's settings, pawn type, body type and owner
			AddInterchangeableList(new List<ThingDef>() {armourRack, mechanisedArmourRack, mendingArmourRack},
				preAction: (newThing, oldThing) =>
				{
					var newRack = (ArmorRack) newThing;
					var oldRack = (ArmorRack) oldThing;
					
					newRack.Settings.CopyFrom(oldRack.GetStoreSettings());
					if (newRack.PawnKindDef != oldRack.PawnKindDef)
						newRack.PawnKindDef = oldRack.PawnKindDef;
					else
						newRack.BodyTypeDef = oldRack.BodyTypeDef;
					
					oldRack.InnerContainer.TryTransferAllToContainer(newRack.InnerContainer);
					
					if (oldRack.GetAssignedPawn() != null) 
						newRack.TryAssignPawn(oldRack.GetAssignedPawn());
				});
		}
	}
}