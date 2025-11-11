using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScreenFade : MonoBehaviour
{
    public static ScreenFade Instance;
    public float fadeDuration = 2f; // thời gian mờ dần
    [SerializeField] private Image fadeImage;
    private float fadeTimer = 0f;
    private bool isFading = false;
    private bool fadeIn = true; 

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
           
        }
    }

    void Start()
    {
        fadeImage = GetComponent<Image>();
        fadeImage.color = new Color(0, 0, 0, 1); 
        StartFadeIn(); 
    }

    void Update()
    {
        if (!isFading) return;

        fadeTimer += Time.deltaTime;
        float t = Mathf.Clamp01(fadeTimer / fadeDuration);

        float alpha = fadeIn ? Mathf.Lerp(1f, 0f, t) : Mathf.Lerp(0f, 1f, t);
        fadeImage.color = new Color(0, 0, 0, alpha);

        if (t >= 1f)
        {
            isFading = false;
            if (fadeIn) fadeImage.enabled = false;
        }
    }

    public void StartFadeIn(float duration = -1f)
    {
        fadeIn = true;
        fadeTimer = 0f;
        isFading = true;
        fadeImage.enabled = true;
        if (duration > 0) fadeDuration = duration;
    }

    public void StartFadeOut(float duration = -1f)
    {
        fadeIn = false;
        fadeTimer = 0f;
        isFading = true;
        fadeImage.enabled = true;
        if (duration > 0) fadeDuration = duration;
    }
    public void StartFadeOutThenIn(float fadeOutDuration = 1f, float fadeInDuration = 2f)
    {
        fadeIn = false;
        fadeTimer = 0f;
        isFading = true;
        fadeImage.enabled = true;

        StartCoroutine(FadeOutInCoroutine(fadeOutDuration, fadeInDuration));
    }

    private IEnumerator FadeOutInCoroutine(float fadeOutDuration, float fadeInDuration)
    {
        // FADE OUT
        while (fadeTimer < fadeOutDuration)
        {
            fadeTimer += Time.deltaTime;
            float alpha = Mathf.Lerp(0f, 1f, fadeTimer / fadeOutDuration);
            fadeImage.color = new Color(0, 0, 0, alpha);
            yield return null;
        }

        // chuẩn bị FADE IN
        fadeIn = true;
        fadeTimer = 0f;

        while (fadeTimer < fadeInDuration)
        {
            fadeTimer += Time.deltaTime;
            float alpha = Mathf.Lerp(1f, 0f, fadeTimer / fadeInDuration);
            fadeImage.color = new Color(0, 0, 0, alpha);
            yield return null;
        }

        // ẩn image khi xong
        fadeImage.enabled = false;
        isFading = false;
    }



}
