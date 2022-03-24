using UnityEditor;

#if UNITY_EDITOR
public class CreateAssetBundles
{
	[MenuItem("Assets/Build AssetBundles")]	
	private static void BuildAllAssetBundles()
	{
		BuildPipeline.BuildAssetBundles(
			"Assets/AssetBundles",
			BuildAssetBundleOptions.None,
			BuildTarget.Android);
	}
}
#endif

