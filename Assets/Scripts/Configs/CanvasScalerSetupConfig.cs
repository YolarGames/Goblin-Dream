using UnityEngine;
using UnityEngine.UI;

namespace Configs
{
	[CreateAssetMenu(fileName = "CanvasScalerSetupConfig", menuName = "Configs/CanvasScaler config")]
	public class CanvasScalerSetupConfig : ScriptableObject, IConfig
	{
		public Vector2 ReferenceResolution = new(1920, 1080);
		public CanvasScaler.ScreenMatchMode ScreenMatchMode = CanvasScaler.ScreenMatchMode.MatchWidthOrHeight;
		[Range(0, 1)] public float Match = 0.5f;
	}
}