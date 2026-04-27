using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class MobileInputReader : MonoBehaviour
{
	[Header("config")]
	private float _holdThreshold = 0.35f;
	private float _maxDistance = 25f;

	private bool _isPressing = false;
	private Vector2 _currentPosition;
	private float _pressStartTime;

	//event
	public event Action<Vector2> OnTab;

	private void Update()
	{
#if UNITY_EDITOR
		MouseInput();
#else
		TouchInput();
#endif
	}

	private void MouseInput()
	{
		if(Input.GetMouseButtonDown(0))
		{
			BeginPress(Input.mousePosition);
		}

		if(Input.GetMouseButtonUp(0))
		{
			EndPress(Input.mousePosition);
		}
	}

	private void TouchInput()
	{
		if (Input.touchCount <= 0) return;

		Touch touch = Input.GetTouch(0);
		switch(touch.phase)
		{
			case TouchPhase.Began:
				BeginPress(touch.position);
				break;
			case TouchPhase.Ended:
				EndPress(touch.position);
				break;
			default:
				break;
		}
	}

	private void BeginPress(Vector2 screenPosition)
	{
		if (IsPointerOverUI()) return;

		_isPressing = true;
		_currentPosition = screenPosition;
		_pressStartTime = Time.time;
	}

	private void EndPress(Vector2 screenPosition)
	{
		if (!_isPressing) return;
		_isPressing = false;

		float heldTime = Time.time - _pressStartTime;
		float moveDistance = Vector2.Distance(_currentPosition, screenPosition);

		if(heldTime < _holdThreshold && moveDistance <= _maxDistance)
		{
			OnTab?.Invoke(screenPosition);
		}
	}

	private bool IsPointerOverUI()
	{
		if (EventSystem.current == null) return false;

#if UNITY_EDITOR
		return EventSystem.current.IsPointerOverGameObject();
#else
		return EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId);
#endif
	}
}
