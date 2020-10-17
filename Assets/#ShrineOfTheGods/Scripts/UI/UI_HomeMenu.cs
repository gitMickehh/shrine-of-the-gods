using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UI_HomeMenu : MonoBehaviour
{
    [Header("Transition")]
    public UI_Transition transition;

    [Header("Random Seed")]
    public GameObject randomSeedObject;
    public S_Bool seedOption;
    public S_Int randomSeed;
    public TMP_InputField randomSeedInputField;

    [Header("Audio")]
    public S_Bool audioOptions;
    public Toggle audioOptionToggle;

    [Header("URLS!")]
    public string URL_MoreGames;
    public string URL_Twitter;
    public string URL_INSTAGRAM;

    private void Start()
    {
        CheckAudio();
        CheckSeed();
    }

    private void CheckAudio()
    {
        if (PlayerPrefs.HasKey("Audio"))
        {
            int savedAudioOption = PlayerPrefs.GetInt("Audio");
            if (savedAudioOption == 1)
            {
                audioOptions.Value = true;
            }
            else
            {
                audioOptions.Value = false;
            }
        }
        else
        {
            audioOptions.Value = true;
        }

        audioOptionToggle.isOn = audioOptions.Value;

    }

    private void CheckSeed()
    {
        if (PlayerPrefs.HasKey("Played"))
        {
            randomSeedObject.SetActive(true);
        }
        else
        {
            randomSeedObject.SetActive(false);
        }

    }

    public void PlayButton()
    {
        PlayerPrefs.SetInt("Played", 0);
        transition.LoadScene(1);
    }

    public void ToggleAudio(bool isOn)
    {
        audioOptions.Value = isOn;

        if (audioOptions.Value)
        {
            PlayerPrefs.SetInt("Audio", 1);
        }
        else
        {
            PlayerPrefs.SetInt("Audio", 0);
        }
    }

    public void ToggleRandomSeed(bool isOn)
    {
        seedOption.Value = isOn;


        randomSeedInputField.interactable = isOn;
        randomSeedInputField.text = "";
    }

    public void ModifyRandomSeed(string seedString)
    {
        randomSeed.Value = int.Parse(seedString);
    }

    public void MoreGames()
    {
        Application.OpenURL(URL_MoreGames);
    }

    public void Twitter()
    {
        Application.OpenURL(URL_Twitter);
    }

    public void Instagram()
    {
        Application.OpenURL(URL_INSTAGRAM);
    }

    public void QuitButton()
    {
        Application.Quit();
    }

    [ContextMenu("Remove save")]
    public void RemovePastData()
    {
#if UNITY_EDITOR
        Debug.Log("Removed Saves");
#endif
        PlayerPrefs.DeleteAll();
    }
}
