using UnityEngine;
using UnityEngine.UI;

[BoltGlobalBehaviour]
public class PauseMenu : Bolt.GlobalEventListener
{
	public GameObject PauseSubMenu;
	public GameObject OptionsMenu;
	public Slider VolumeSlider;
	public InputField VolumeInput;

	private void Start()
	{
		VolumeSlider.onValueChanged.AddListener(VolumeSlider_OnValueChanged);
		VolumeInput.onEndEdit.AddListener(VolumeInput_onEndEdit);
	}

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

	public void VolumeSlider_OnValueChanged(float volume)
	{
		VolumeInput.text = volume.ToString();
		AudioListener.volume = volume / 100;
	}

	public void VolumeInput_onEndEdit(string volumeStr)
	{
		int volume;
		if (!int.TryParse(volumeStr, out volume))
			VolumeInput.text = VolumeSlider.value.ToString();
		else
		{
			VolumeSlider.value = volume;
			VolumeInput.text = volume.ToString();
			AudioListener.volume = (float)volume / 100;
		}
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
