﻿using System;
using System.Collections.Generic;
using System.Linq;
using HarmonyLib;
using JetBrains.Annotations;
using Replace_Stuff.NewThing;
using Verse;

namespace Replace_Stuff_Compatibility
{
	public abstract class AbstractPatch
	{
		[NotNull]
		abstract protected string GetRequiredModNames();

		abstract protected void AddItems();

		public void Patch()
		{
			var requiredModName = GetRequiredModNames();

			if (!LoadedModManager.RunningModsListForReading.Exists(pack => pack.PackageId == requiredModName))
			{
				return;
			}

			AddItems();
		}

		protected static ThingDef GetDatabaseThing(string name) => DefDatabase<ThingDef>.GetNamed(name);

		protected static Predicate<ThingDef> ListContainsThingDef(List<ThingDef> list)
		{
			return product => list.Exists(n => n == product);
		}

		protected static void AddInterchangeableList(params ThingDef[] items)
		{
			NewThingReplacement.replacements.Add(new NewThingReplacement.Replacement(ListContainsThingDef(items.ToList())));
		}
		
		protected static void AddInterchangeableList(List<ThingDef> items)
		{
			NewThingReplacement.replacements.Add(new NewThingReplacement.Replacement(ListContainsThingDef(items)));
		}
	}
}