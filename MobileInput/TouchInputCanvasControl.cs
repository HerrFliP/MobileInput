using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace HerrFliP.MobileInput
{
	public class TouchInputCanvasControl : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
	{
		[SerializeField] private float _tapTime = 0.1f;
		[SerializeField] private float _doubleTapTime = 0.3f;

		public static Action OnTap = () => { /*DebugConsole.Log("[TAP]");*/ };
		public static Action OnDoubleTap = () => { /*DebugConsole.Log("[DOUBLE_TAP]");*/ };
		public static Action OnTapAndHold = () => { /*DebugConsole.Log("[TAP_AND_HOLD]");*/ };

		public static Action<Vector2> OnTouchDrag = (delta) => { /*DebugConsole.Log("[TOUCH_DRAG]");*/ };

		public static Action OnSwipe = () => { /*DebugConsole.Log("[SWIPE]");*/ };

		public static Action OnTouch = () => { /*DebugConsole.Log("[TOUCH]");*/ };
		public static Action OnRelease = () => { /*DebugConsole.Log("[RELEASE]");*/ };

		private float _pointerDownTime;				// last time the pointer was down
		private Vector2 _pointerDownPosition;		// position of pointer
		private float _lastClick;					// last time of recognised tap

		#region public
		
		public void OnDrag(PointerEventData data)
		{
			OnTouchDrag(new Vector2(data.delta.x, data.delta.y));
		}

		public void OnPointerDown(PointerEventData data)
		{
			_pointerDownTime = Time.time;

			if (Time.time - _lastClick < _doubleTapTime)
			{
				OnTapAndHold();
			}
			else
			{
				OnTouch();	
			}
		}

		public void OnPointerUp(PointerEventData data)
		{
			OnRelease();
			if (Time.time - _pointerDownTime < _tapTime)
			{
				OnPointerClick(data);
			}
		}

		#endregion

		#region private

		private void OnPointerClick(PointerEventData data)
		{
			if (Time.time - _lastClick < _doubleTapTime)
			{
				OnDoubleTap();
			}
			else
			{
				OnTap();
				RayCastTap(data);
			}

			_lastClick = Time.time;
		}

		private void RayCastTap(PointerEventData data)
		{
			RaycastHit hit = new RaycastHit();
			Transform cam = Camera.main.transform;
			Ray ray = Camera.main.ScreenPointToRay (UnityEngine.Input.mousePosition);
			LayerMask layerMask = LayerMask.GetMask( Enum.GetName(typeof(Layer), Layer.Interactable));


			if (Physics.Raycast(ray, out hit, 1000, layerMask))
			{
				Debug.DrawLine(cam.position, hit.point, Color.green, 2f);

				Interactable interactable = hit.collider.GetComponent<Interactable>();
				if (interactable)
				{
					interactable.Interact(ray.origin);
				}
			}
			else
			{
				Debug.DrawRay(ray.origin, ray.direction, Color.red, 2f);
			}
		}

		#endregion
	}

	public enum Direction
	{
		Left,
		Right,
		Up,
		Down
	}
}