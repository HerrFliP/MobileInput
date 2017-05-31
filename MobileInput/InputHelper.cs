using UnityEngine;

namespace HerrFliP.MobileInput
{
	public class InputHelper
	{
		public static int touchCount
		{
			get
			{
#if UNITY_EDITOR
				return Input.GetMouseButton(0) ? 1 : 0;
#elif UNITY_ANDROID
				return Input.touchCount;
#endif
			}
		}

		public static Touch[] touches
		{
			get
			{
#if UNITY_EDITOR
				Touch[] touches = new Touch[1];
				
				Debug.Log(Input.mousePosition);
				
				Touch touch = new Touch{position = Input.mousePosition, fingerId = 2};
				touches[0] = touch;
				
				return touches;
#elif UNITY_ANDROID
				return Input.touches;
#endif				
			}
		}
	}
}