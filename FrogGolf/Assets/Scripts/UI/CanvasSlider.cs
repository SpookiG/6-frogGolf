using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasSlider : MonoBehaviour
{
    public string MenuName = "StartMenu";
    public float Duration = .8f;

    private RectTransform _rectTransform;

    //private bool slideIn;

    // Start is called before the first frame update
    void Start()
    {
        SequenceManager.Current.Events.slideMenu += SlideMenu;
        SequenceManager.Current.Events.resetLevel += ResetUI;
        _rectTransform = GetComponent<RectTransform>();
        _rectTransform.anchorMin = new Vector2(0, 1);
        _rectTransform.anchorMax = new Vector2(1, 2);

        Debug.Log(MenuName);
        //slideIn = true;
    }

    private void OnDestroy()
    {
        SequenceManager.Current.Events.slideMenu -= SlideMenu;
        SequenceManager.Current.Events.resetLevel -= ResetUI;
    }


    // NOTE: this assumes the anchors on the canvas object are set to min(0, 0) and max(1, 1). Making it account for other anchor values would be complicated
    public IEnumerator SlideMenu(string menuName, bool slideIn)
    {
        Debug.Log("hello?");
        Debug.Log(MenuName);

        if (menuName == MenuName)
        {
            Debug.Log("working??");

            Vector2 initialMin;
            Vector2 initialMax;
            Vector2 targetMin;
            Vector2 targetMax;

            if (slideIn)
            {
                initialMin = new Vector2(0, 1);
                initialMax = new Vector2(1, 2);
                targetMin = new Vector2(0, 0);
                targetMax = new Vector2(1, 1);
            }
            else
            {
                initialMin = new Vector2(0, 0);
                initialMax = new Vector2(1, 1);
                targetMin = new Vector2(0, 1);
                targetMax = new Vector2(1, 2);
            }

            for (float slideTime = 0f; slideTime < Duration; slideTime += Time.deltaTime)
            {
                _rectTransform.anchorMin = Vector2.Lerp(initialMin, targetMin, slideTime / Duration);
                _rectTransform.anchorMax = Vector2.Lerp(initialMax, targetMax, slideTime / Duration);
                yield return null;
            }

            _rectTransform.anchorMin = targetMin;
            _rectTransform.anchorMax = targetMax;

        }
        yield return null;
    }

    public void ResetUI()
    {
        _rectTransform.anchorMin = new Vector2(0, 1);
        _rectTransform.anchorMax = new Vector2(1, 2);
    }

    // Update is called once per frame
    void Update()
    {
        /*if (slideIn)
        {
            _rectTransform.anchorMin -= new Vector2(0, Time.deltaTime);
            _rectTransform.anchorMax -= new Vector2(0, Time.deltaTime);

            if (_rectTransform.anchorMin.y <= 0)
            {
                _rectTransform.anchorMin = new Vector2(0, 0);
                _rectTransform.anchorMax = new Vector2(1, 1);

                slideIn = false;
            }

            //Debug.Log(_canvasGroup.transform.relativePosition);
        }
        else
        {
            _rectTransform.anchorMin += new Vector2(0, Time.deltaTime);
            _rectTransform.anchorMax += new Vector2(0, Time.deltaTime);

            if (_rectTransform.anchorMin.y >= 1)
            {
                _rectTransform.anchorMin = new Vector2(0, 1);
                _rectTransform.anchorMax = new Vector2(1, 2);

                slideIn = true;
            }


        }*/

        
    }
}
