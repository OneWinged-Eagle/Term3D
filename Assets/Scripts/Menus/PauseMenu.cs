using UnityEngine;
using UnityEngine.UI;

[BoltGlobalBehaviour]
public class PauseMenu : Bolt.GlobalEventListener
{
	public GameObject PauseSubMenu;
	public GameObject OptionsMenu;

	private void Start() {}

	private void Update() {}

	public void OptionsBtn()
	{
		PauseSubMenu.SetActive(false);
		OptionsMenu.SetActive(true);
	}

	public void ExitBtn()
	{
		// TODO
	}

	public void BackBtn()
	{
		OptionsMenu.SetActive(false);
		PauseSubMenu.SetActive(true);
	}

	public void CloseBtn()
	{
		BackBtn();
		gameObject.SetActive(false);
	}
}
