using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Utils;

public class GoToMainMenu : MonoBehaviour
{
	[SerializeField] private Button _gotoMenu;

	private void Start()
	{
		_gotoMenu.onClick.AddListener(MenuManager.LoadMainMenuScene);
	}
}
