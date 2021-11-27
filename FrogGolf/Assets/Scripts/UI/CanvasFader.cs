using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasFader : MonoBehaviour
{
    public string MenuName = "StartMenu";
    public float Duration = .8f;

    private CanvasGroup _canvasGroup;

    // Start is called before the first frame update
    void Start()
    {
        SequenceManager.Current.Events.fadeMenuTo += FadeMenuTo;
        _canvasGroup = GetComponent<CanvasGroup>();
    }

    private void OnDestroy()
    {
        SequenceManager.Current.Events.fadeMenuTo -= FadeMenuTo;
    }

    public IEnumerator FadeMenuTo(string menuName, float targetAlpha)
    {
        if (menuName == MenuName)
        {
            float initialAlpha = _canvasGroup.alpha;

            for (float fadeTime = 0f; fadeTime < Duration; fadeTime += Time.deltaTime)
            {
                _canvasGroup.alpha = Mathf.Lerp(initialAlpha, targetAlpha, fadeTime / Duration);
                yield return null;
            }
            _canvasGroup.alpha = targetAlpha;
        }

        yield return null;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
