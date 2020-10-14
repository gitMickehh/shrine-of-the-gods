using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Events;

public class UI_Transition : MonoBehaviour
{
    public List<Sprite> sprites;
    public Image transitionImage;

    [Header("Transition Settings")]
    public float transitionTime = 1.5f;
    public float uniformScale = 2f;
    public Ease transitionEasing;

    [Header("Transition Over Event")]
    public UnityEvent TransitionFinished;

    private void Start()
    {
        StartScene();
    }

    private Sprite GetRandomSprite()
    {
        if (sprites.Count == 1)
            return sprites[0];

        int r = Random.Range(0, sprites.Count);
        return sprites[r];
    }

    private void StartScene()
    {
        transitionImage.gameObject.SetActive(true);
        transitionImage.sprite = GetRandomSprite();

        transitionImage.rectTransform.localScale = Vector3.one * uniformScale;
        transitionImage.rectTransform.DOScale(Vector3.zero, transitionTime).SetEase(transitionEasing)
            .OnComplete(() =>
            {
                transitionImage.gameObject.SetActive(false);
                TransitionFinished.Invoke();
            });
    }

    public void LoadScene(int sceneIndex)
    {

        transitionImage.gameObject.SetActive(true);
        transitionImage.sprite = GetRandomSprite();

        transitionImage.rectTransform.localScale = Vector3.zero;
        transitionImage.rectTransform.DOScale(Vector3.one * uniformScale, transitionTime).SetEase(transitionEasing)
            .OnComplete(() =>
            {
                SceneManager.LoadScene(sceneIndex);
            });
    }

}
