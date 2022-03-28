using UnityEngine;

public class SetCanvasCamera : MonoBehaviour
{
	[SerializeField] private Canvas _canvas;
	[SerializeField] private string _cameraTag;
	
	private Camera _cameraUI;
	
	
	private void Awake()
	{
		_cameraUI = GameObject.FindWithTag(_cameraTag).GetComponent<Camera>();
		if (_cameraUI)
			_canvas.worldCamera = _cameraUI;
	}
}
