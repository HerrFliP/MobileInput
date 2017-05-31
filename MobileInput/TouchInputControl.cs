using UnityEngine;

namespace HerrFliP.MobileInput
{
	public class TouchInputControl : MonoBehaviour
	{
		private void Update()
		{
			if (InputHelper.touchCount > 0)
			{
				string output = "Input:";

				for (int i = 0; i < InputHelper.touchCount; i++)
				{
					Touch touch = InputHelper.touches[i];

					output += " [" + touch.fingerId + "] " + touch.position + " |";
				}
				
				DebugConsole.Log(output);
			}
		}
	}
}