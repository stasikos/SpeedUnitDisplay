using System;
using UnityEngine;
using KSP.UI.Screens.Flight;

namespace SpeedUnitDisplay
{

	[KSPAddon(KSPAddon.Startup.Flight, false)] 
	public class SpeedUnitDisplay : MonoBehaviour
	{

		private SpeedDisplay display;
		private float fontSize;
		private float titleFontSize;
		private float msToKph = 3.6f;

		public SpeedUnitDisplay ()
		{
			// Nothing to bo done here
		}

		public void OnGUI() {
			if (display == null) {
				display = GameObject.FindObjectOfType<SpeedDisplay> ();
				if (display != null) {
					fontSize = display.textSpeed.fontSize;
					titleFontSize = display.textTitle.fontSize;
				}
			}
		}

		public void LateUpdate() {
			// For test...
			if (display != null) {
				FlightGlobals.SpeedDisplayModes mode = FlightGlobals.speedDisplayMode;
				if (mode == FlightGlobals.SpeedDisplayModes.Surface) { 
					// I need IAS in km/h
					// I need SPD in km/h and m/s... so much numbers to fit!!!
					double ias = FlightGlobals.ActiveVessel.indicatedAirSpeed;
					double spd = FlightGlobals.ActiveVessel.srfSpeed;
					double spdKph = FlightGlobals.ActiveVessel.srfSpeed * msToKph;

					string iasStr = ias.ToString ("F1");
					string iasKphStr = (ias * msToKph).ToString ("F1");

					// It works, but it is so small....
					string titleText = "km/h:" + spdKph.ToString ("F1");
					string speedText = "m/s:" + spd.ToString ("F1");


					if (ias > 0) { // Only in air
						titleText += " IAS:" + iasKphStr;
						speedText += " IAS:" + iasStr;
					}

					display.textTitle.text = titleText;
					display.textSpeed.text = speedText;
					display.textTitle.overflowMode = TMPro.TextOverflowModes.Overflow;
					display.textSpeed.overflowMode = TMPro.TextOverflowModes.Overflow;
					display.textTitle.fontSize = titleFontSize / 1.1f;
					display.textSpeed.fontSize = fontSize / 1.3f;


				} else { // Fix font size when it is not in "surface mode"
					display.textTitle.fontSize = titleFontSize;
					display.textSpeed.fontSize = fontSize;
				}
			}
		}

	}
}

