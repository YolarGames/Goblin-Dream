using UnityEngine;

public class SetCanvasCamera : MonoBehaviour
{
	[SerializeField] private Canvas _canvas;
	private Camera _cameraUI;
	
	
	private void Awake()
	{
		_cameraUI = GameObject.FindWithTag("uiCamera").GetComponent<Camera>();
		if (_cameraUI)
			_canvas.worldCamera = _cameraUI;
	}
}
