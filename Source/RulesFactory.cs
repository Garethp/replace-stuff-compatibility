﻿using System;
using System.Collections.Generic;
using System.Linq;
using Replace_Stuff;
using Replace_Stuff_Compatibility.Comps;
using Replace_Stuff.NewThing;
using RimWorld;
using Verse;

namespace Replace_Stuff_Compatibility
{
	public class RulesFactory
	{
		private Dictionary<string, IReplacementComp> compCache = new();
		
		public void AddGenericRules()
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
		}

		public void AddRulesFromXML()
		{
			var categories = new Dictionary<string, ReplaceList>();
			var workbenchCategories = new Dictionary<string, ReplaceList>();
			var uncategorized = new List<ReplaceList>();
			var wallInterchangeable = new List<ThingDef>();

			foreach (var def in DefDatabase<InterchangableItems>.AllDefs)
			{
				foreach (var list in def.ReplaceLists)
				{
					if (list.IsWall)
					{
						wallInterchangeable.AddRange(list.Items);
					}

					if (list.Category.Any() && !list.IsWorkbench)
					{
						if (!categories.ContainsKey(list.Category)) categories.Add(list.Category, new ReplaceList());

						categories[list.Category].Items.AddRange(list.Items);
					}
					else if (list.Category.Any() && list.IsWorkbench)
					{
						if (!workbenchCategories.ContainsKey(list.Category))
						{
							workbenchCategories.Add(list.Category, new ReplaceList());
							workbenchCategories[list.Category].IsWorkbench = true;
						}

						workbenchCategories[list.Category].Items.AddRange(list.Items);
					}
					else
					{
						uncategorized.Add(list);
					}
				}
			}

			// All Wall-Like items should be interchangeable
			NewThingReplacement.replacements.Add(new NewThingReplacement.Replacement(
				item => item.IsWall() || wallInterchangeable.Contains(item)
			));

			foreach (var itemList in uncategorized)
			{
				if (itemList.IsWorkbench && !itemList.comps.Contains("Replace_Stuff_Compatibility.Comps.TransferBills"))
					itemList.comps.Add("Replace_Stuff_Compatibility.Comps.TransferBills");
				
				foreach (var compName in itemList.comps)
				{
					if (compCache.ContainsKey(compName)) continue;

					var type = Type.GetType(compName);
					if (type is null) continue;
					
					var comp = (IReplacementComp)Activator.CreateInstance(type);
					if (comp is null) continue;
					
					compCache.Add(compName, comp);
				}
			}
			
			foreach (var category in categories)
			{
				AddInterchangeableItems(category.Value);
			}

			foreach (var category in workbenchCategories)
			{
				AddInterchangeableItems(category.Value);
			}

			foreach (var itemList in uncategorized)
			{
				AddInterchangeableItems(itemList);
			}
		}

		protected void AddInterchangeableItems(ReplaceList items)
		{
			List<string> comps = new();
			
			if (items.comps.Any())
			{
				comps.AddRange(items.comps
					.Where(compName => compCache.ContainsKey(compName))
				);
			}

			AddInterchangeableList(
				items.Items,
				preAction: (newThing, oldThing) => { comps.ForEach(comp => compCache[comp].PreAction(newThing, oldThing)); },
				postAction: (newThing, oldThing) => { comps.ForEach(comp => compCache[comp].PostAction(newThing, oldThing)); }
			);
		}

		protected static Predicate<ThingDef> ListContainsThingDef(HashSet<ThingDef> list) => list.Contains;

		protected static void AddInterchangeableList(List<ThingDef> items, Action<Thing, Thing> preAction = null,
			Action<Thing, Thing> postAction = null)
		{
			NewThingReplacement.replacements.Add(
				new NewThingReplacement.Replacement(
					ListContainsThingDef(new HashSet<ThingDef>(items)),
					preAction: preAction,
					postAction: postAction
				)
			);
		}
	}
}