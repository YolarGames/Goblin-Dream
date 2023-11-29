﻿using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityToolbarExtender;
using Utils;

namespace Editor
{
	[InitializeOnLoad]
	public static class EditorToolbar
	{
		private static bool _bootstrapEnabled;

		private static bool BootstrapEnabled
		{
			get => _bootstrapEnabled;
			set
			{
				_bootstrapEnabled = value;
				EditorSceneManager.playModeStartScene = LoadBootstrapSceneAsset(
					_bootstrapEnabled
						? Scenes.MAIN_MENU
						: Scenes.ActiveSceneName);
			}
		}

		static EditorToolbar()
		{
			ToolbarExtender.LeftToolbarGUI.Add(DrawLeftGUI);
			BootstrapEnabled = true;
		}

		private static void DrawLeftGUI()
		{
			GUILayout.FlexibleSpace();

			GUI.changed = false;

			GUILayout.Toggle(_bootstrapEnabled, "Start with Bootstrap");
			if (GUI.changed)
				BootstrapEnabled = !BootstrapEnabled;
		}

		private static SceneAsset LoadBootstrapSceneAsset(string sceneName) =>
			AssetDatabase.LoadAssetAtPath<SceneAsset>(Scenes.GetScenePath(sceneName));

		// private static void DrawRightGUI()
		// {
		// 	GUILayout.FlexibleSpace();
		//
		// 	if (GUILayout.Button("Focus on Player"))
		// 		FocusOnPlayer();
		//
		// 	GUILayout.Space(250);
		// }
		//
		// private static void FocusOnPlayer()
		// {
		// 	GameObject player = GameObject.FindWithTag("Player");
		//
		// 	if (player == null)
		// 		return;
		//
		// 	Selection.activeGameObject = player;
		// 	EditorGUIUtility.PingObject(player);
		// }
	}
}