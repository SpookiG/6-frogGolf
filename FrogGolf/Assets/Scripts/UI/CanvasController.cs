using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CanvasController : MonoBehaviour
{
    public static CanvasController current;

    [Tooltip("How much time in seconds to transition in and out of scenes")]
    [Range(0f, 2f)]
    public float TransitionDuration = .5f;

    [Tooltip("How much time in seconds to fade in start menu")]
    [Range(0f, 6f)]
    public float FadeInStartMenuDuration = 3f;

    [Tooltip("How much time in seconds to fade out start menu")]
    [Range(0f, 6f)]
    public float FadeOutStartMenuDuration = 1f;

    [Tooltip("How much time in seconds for start menu button prompt flashing")]
    [Range(0f, 6f)]
    public float StartMenuFlashDuration = 2f;

    [Tooltip("How much time in seconds to fade in game over prompt")]
    [Range(0f, 6f)]
    public float FadeInGameOverDuration = 5f;

    [Tooltip("How much time in seconds to fade out game over prompt")]
    [Range(0f, 6f)]
    public float FadeOutGameOverDuration = 3f;

    private TextMeshProUGUI _title;
    private TextMeshProUGUI _startPrompt;
    private TextMeshProUGUI _gameOver;
    private TextMeshProUGUI _gameOverPrompt;
    private RectTransform _fill;

    private IEnumerator _idleCoroutine;

    private void Awake()
    {
        current = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        _title = transform.Find("Title").GetComponent<TextMeshProUGUI>();
        _startPrompt = transform.Find("StartPrompt").GetComponent<TextMeshProUGUI>();
        _gameOver = transform.Find("GameOver").GetComponent<TextMeshProUGUI>();
        _gameOverPrompt = transform.Find("GameOverPrompt").GetComponent<TextMeshProUGUI>();
        _fill = transform.Find("Fill").GetComponent<RectTransform>();

        // fading in start screen so make everything invisible
        ClearCanvas();

        // initialise unused corroutine here to avoid null issues
        _idleCoroutine = SlowFlashPrompt();
    }


    public void ClearCanvas()
    {
        _title.alpha = 0f;
        _startPrompt.alpha = 0f;
        _gameOver.alpha = 0f;
        _gameOverPrompt.alpha = 0f;
        
        //_fill.anchorMin = Vector2.zero;
        //_fill.anchorMax = new Vector2(0, 1);
    }

    public void LoadStartMenu()
    {
        _title.alpha = 1f;
        _startPrompt.alpha = 1f;

        StopCoroutine(_idleCoroutine);
        _idleCoroutine = SlowFlashPrompt();
        StartCoroutine(_idleCoroutine);
    }





    // fade in space prompt after game over?

    public IEnumerator TransitionOut()
    {
        _fill.anchorMin = Vector2.zero;
        _fill.anchorMax = new Vector2(0, 1);

        // wipe the fill onto the canvas over time
        for (float transitionTime = 0f; transitionTime < TransitionDuration; transitionTime += Time.deltaTime)
        {

            _fill.anchorMax = new Vector2(Mathf.Lerp(0, 1, transitionTime / TransitionDuration), 1);
            yield return null;
        }

        _fill.anchorMax = Vector2.one;
    }

    public IEnumerator TransitionIn()
    {
        _fill.anchorMin = Vector2.zero;
        _fill.anchorMax = Vector2.one;

        // wipe the fill off the canvas over time
        for (float transitionTime = 0f; transitionTime < TransitionDuration; transitionTime += Time.deltaTime)
        {

            _fill.anchorMin = new Vector2(Mathf.Lerp(0, 1, transitionTime / TransitionDuration), 0);
            yield return null;
        }

        _fill.anchorMin = new Vector2(1, 0);
    }

    public IEnumerator FadeOutStartMenu()
    {
        StopCoroutine(_idleCoroutine);

        for (float fadeTime = 0f; fadeTime < FadeOutStartMenuDuration; fadeTime += Time.deltaTime)
        {
            _title.alpha = Mathf.Min(Mathf.Lerp(1, 0, fadeTime / FadeOutStartMenuDuration), _title.alpha);                  // if alpha is already kind of faded out, don't snap it back in again, just pick the min
            _startPrompt.alpha = Mathf.Min(Mathf.Lerp(1, 0, fadeTime / FadeOutStartMenuDuration), _startPrompt.alpha);
            yield return null;
        }

        _title.alpha = 0f;
        _startPrompt.alpha = 0f;
    }

    public IEnumerator FadeInStartMenu()
    {
        StopCoroutine(_idleCoroutine);

        for (float fadeTime = 0f; fadeTime < FadeInStartMenuDuration; fadeTime += Time.deltaTime)
        {
            _title.alpha = Mathf.Max(Mathf.Lerp(0, 1, fadeTime / FadeInStartMenuDuration), _title.alpha);
            _startPrompt.alpha = Mathf.Max(Mathf.Lerp(0, 1, fadeTime / FadeInStartMenuDuration), _startPrompt.alpha);
            yield return null;
        }

        _title.alpha = 1f;
        _startPrompt.alpha = 1f;

        // stop idle coroutine again just in case it activated whilst this coroutine was going on
        StopCoroutine(_idleCoroutine);
        _idleCoroutine = SlowFlashPrompt();
        StartCoroutine(_idleCoroutine);
    }


    public IEnumerator FadeOutGameOver()
    {
        StopCoroutine(_idleCoroutine);

        for (float fadeTime = 0f; fadeTime < FadeOutStartMenuDuration; fadeTime += Time.deltaTime)
        {
            _gameOver.alpha = Mathf.Min(Mathf.Lerp(1, 0, fadeTime / FadeOutStartMenuDuration), _gameOver.alpha);                  // if alpha is already kind of faded out, don't snap it back in again, just pick the min
            _gameOverPrompt.alpha = Mathf.Min(Mathf.Lerp(1, 0, fadeTime / FadeOutStartMenuDuration), _gameOverPrompt.alpha);
            yield return null;
        }

        _gameOver.alpha = 0f;
        _gameOverPrompt.alpha = 0f;
    }

    public IEnumerator FadeInGameOver()
    {
        StopCoroutine(_idleCoroutine);

        for (float fadeTime = 0f; fadeTime < FadeInStartMenuDuration; fadeTime += Time.deltaTime)
        {
            _gameOver.alpha = Mathf.Max(Mathf.Lerp(0, 1, fadeTime / FadeInStartMenuDuration), _gameOver.alpha);
            _gameOverPrompt.alpha = Mathf.Max(Mathf.Lerp(0, 1, fadeTime / FadeInStartMenuDuration), _gameOverPrompt.alpha);
            yield return null;
        }

        _gameOver.alpha = 1f;
        _gameOverPrompt.alpha = 1f;

        // stop idle coroutine again just in case it activated whilst this coroutine was going on
        StopCoroutine(_idleCoroutine);
        _idleCoroutine = SlowFlashGameOverPrompt();
        StartCoroutine(_idleCoroutine);
    }






    // The "Space to start" prompt should slowly flash
    private IEnumerator SlowFlashPrompt()
    {
        bool fadeIn = false;
        float fadeTime = 0f;

        while (true)
        {
            fadeTime += Time.deltaTime;

            if (fadeIn)
            {
                _startPrompt.alpha = Mathf.Lerp(0, 1, fadeTime / StartMenuFlashDuration);
            }
            else
            {
                _startPrompt.alpha = Mathf.Lerp(1, 0, fadeTime / StartMenuFlashDuration);
            }

            if (fadeTime >= StartMenuFlashDuration)
            {
                fadeTime -= StartMenuFlashDuration;
                fadeIn = !fadeIn;
            }

            yield return null;
        }
        
    }


    // The "Space to reset" prompt should slowly flash
    private IEnumerator SlowFlashGameOverPrompt()
    {
        bool fadeIn = false;
        float fadeTime = 0f;

        while (true)
        {
            fadeTime += Time.deltaTime;

            if (fadeIn)
            {
                _gameOverPrompt.alpha = Mathf.Lerp(0, 1, fadeTime / StartMenuFlashDuration);
            }
            else
            {
                _gameOverPrompt.alpha = Mathf.Lerp(1, 0, fadeTime / StartMenuFlashDuration);
            }

            if (fadeTime >= StartMenuFlashDuration)
            {
                fadeTime -= StartMenuFlashDuration;
                fadeIn = !fadeIn;
            }

            yield return null;
        }

    }
}
