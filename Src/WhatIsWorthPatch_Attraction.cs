using HarmonyLib;
using Parkitect.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

namespace WhatIsWorth {
	[HarmonyPatch]
	public class WhatIsWorthPatch_Attraction {
		static MethodBase TargetMethod() => AccessTools.Method(typeof(Attraction), "calculateValueFor", new[] { typeof(Person) });

		[HarmonyPostfix]
		public static void Postfix(Attraction __instance, float __result, Person person) {
			var value = __result;
			var mult = WhatIsWorth._config.multiplier;
			if (mult > 0) {
				value = Mathf.Floor(value / mult) * mult;
			}
			person.think(new Thought(
				__instance.getCustomizedColorizedName() + " looks like $" + value.ToString("0.##"),
				Thought.Emotion.INFO, __instance
			));
		}
	}
}
