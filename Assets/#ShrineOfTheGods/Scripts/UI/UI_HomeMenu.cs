using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UI_HomeMenu : MonoBehaviour
{
    [Header("Transition")]
    public UI_Transition transition;

    [Header("Delete Button")]
    public Button deleteDataButton;

    [Header("Audio")]
    public TextMeshProUGUI audioText;
    public S_Bool audioOptions;

    [Header("URLS!")]
    public string URL_MoreGames;
    public string URL_Twitter;
    public string URL_INSTAGRAM;

    private void Start()
    {
        if (PlayerPrefs.HasKey("Audio"))
        {
            int savedAudioOption = PlayerPrefs.GetInt("Audio");
            if(savedAudioOption == 1)
            {
                audioOptions.Value = true;
                audioText.text = "Audio On";
            }
            else
            {
                audioOptions.Value = false;
                audioText.text = "Audio Off";
            }
        }
        else
        {
            audioOptions.Value = true;
            audioText.text = "Audio On";
        }


        deleteDataButton.interactable = PlayerPrefs.HasKey("loops");
    }

    public void PlayButton()
    {
        transition.LoadScene(1);
    }

    public void ToggleAudio()
    {
        if (audioOptions.Value)
        {
            audioOptions.Value = false;
            audioText.text = "Audio Off";

            PlayerPrefs.SetInt("Audio",0);
        }
        else
        {
            audioOptions.Value = true;
            audioText.text = "Audio On";
            
            PlayerPrefs.SetInt("Audio",1);
        }
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
