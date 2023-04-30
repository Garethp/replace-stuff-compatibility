using System.Collections.Generic;
using System.Linq;
using Replace_Stuff.NewThing;
using RimWorld;
using Verse;

namespace Replace_Stuff_Compatibility
{
	public class MultiModPatch : AbstractPatch
	{
		protected override string GetRequiredModNames() => "";

		public static List<ThingDef> Smelters = new List<ThingDef>() { GetDatabaseThing("ElectricSmelter") };

		public static List<ThingDef> Smithys = new List<ThingDef>()
			{ GetDatabaseThing("FueledSmithy"), GetDatabaseThing("ElectricSmithy") };

		public static List<ThingDef> MachiningTables = new List<ThingDef>() { GetDatabaseThing("TableMachining") };

		public static List<ThingDef> Stoves = new List<ThingDef>()
			{ GetDatabaseThing("FueledStove"), GetDatabaseThing("ElectricStove") };

		public static List<ThingDef> TailoringBenches = new List<ThingDef>()
			{ GetDatabaseThing("HandTailoringBench"), GetDatabaseThing("ElectricTailoringBench") };
		
		public static List<ThingDef> Fabricators = new List<ThingDef>() { GetDatabaseThing("FabricationBench") };

		public static List<ThingDef> ArtTables = new List<ThingDef>() { GetDatabaseThing("TableSculpting") };
		
		protected override void AddItems()
		{
			// Allow all "plant growable items" to replace each other, and when they do attempt to set the growing plant type
			NewThingReplacement.replacements.Add(new NewThingReplacement.Replacement(
				building => typeof(IPlantToGrowSettable).IsAssignableFrom(building.thingClass),
				postAction: (newItem, oldItem) =>
				{
					((IPlantToGrowSettable)newItem).SetPlantDefToGrow(((IPlantToGrowSettable)oldItem).GetPlantDefToGrow());
				}));

			NewThingReplacement.replacements.Add(new NewThingReplacement.Replacement(
				building => typeof(Building_Battery).IsAssignableFrom(building.thingClass)));

			// We can use placeWorkers and comps to check what kind of power is being generated so that we don't have to worry
			// about each item individually
			NewThingReplacement.replacements.Add(new NewThingReplacement.Replacement(
				building => building.placeWorkers?.Any(placeWorker =>
					placeWorker == typeof(PlaceWorker_WatermillGenerator)) ?? false));
			NewThingReplacement.replacements.Add(new NewThingReplacement.Replacement(
				building => building.placeWorkers?.Any(placeWorker =>
					placeWorker == typeof(PlaceWorker_WindTurbine)) ?? false));
			NewThingReplacement.replacements.Add(new NewThingReplacement.Replacement(
				building => building.placeWorkers?.Any(placeWorker =>
					placeWorker == typeof(PlaceWorker_OnSteamGeyser)) ?? false));
			
			AddInterchangeableWorkbenches(Smelters);
			AddInterchangeableWorkbenches(Smithys);
			AddInterchangeableWorkbenches(Fabricators);
			AddInterchangeableWorkbenches(TailoringBenches);
			AddInterchangeableWorkbenches(Stoves);
			AddInterchangeableWorkbenches(MachiningTables);
			AddInterchangeableWorkbenches(ArtTables);
			
			AddItemsFromDef();
		}

		protected void AddItemsFromDef()
		{
			var categories = new Dictionary<string, List<ThingDef>>();
			var workbenchCategories = new Dictionary<string, List<ThingDef>>();
			var uncategorized = new List<List<ThingDef>>();

			foreach (var def in DefDatabase<InterchangableItems>.AllDefs)
			{
				foreach (var list in def.ReplaceLists)
				{
					if (list.Category.Any() && !list.IsWorkbench)
					{
						if (!categories.ContainsKey(list.Category)) categories.Add(list.Category, new List<ThingDef>());

						foreach (var thing in list.Items)
						{
							categories[list.Category].Add(thing);
						}
					}
					else if (list.Category.Any() && list.IsWorkbench)
					{
						if (!workbenchCategories.ContainsKey(list.Category))
							workbenchCategories.Add(list.Category, new List<ThingDef>());

						foreach (var thing in list.Items)
						{
							workbenchCategories[list.Category].Add(thing);
						}
					}
					else
					{
						uncategorized.Add(list.Items);
					}
				}

				foreach (var itemList in uncategorized)
				{
					AddInterchangeableList(itemList);
				}

				foreach (var category in categories)
				{
					AddInterchangeableList(category.Value);
				}

				foreach (var category in workbenchCategories)
				{
					AddInterchangeableWorkbenches(category.Value);
				}
			}
		}
	}
}