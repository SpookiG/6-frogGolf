using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationTransition : MonoBehaviour
{
    public float Duration = .8f;

    private RectTransform _rectTransform;

    // Start is called before the first frame update
    void Start()
    {
        SequenceManager.Current.Events.uITransition += RunTransition;
        _rectTransform = GetComponent<RectTransform>();
    }

    private void OnDestroy()
    {
        SequenceManager.Current.Events.uITransition -= RunTransition;
    }

    public IEnumerator RunTransition(bool transitionIn)
    {
        float initialRotation;
        float targetRotation;

        if (transitionIn)
        {
            initialRotation = 180;
            targetRotation = 0;
        }
        else
        {
            initialRotation = 360;          // 360 because we're taking away rotation
            targetRotation = 180;
        }

        for (float transitionTime = 0f; transitionTime < Duration; transitionTime += Time.deltaTime)
        {
            _rectTransform.rotation = Quaternion.Euler(0, 0, Mathf.Lerp(initialRotation, targetRotation, transitionTime / Duration));
            yield return null;
        }

        _rectTransform.rotation = Quaternion.Euler(0, 0, targetRotation);
    }

    // Update is called once per frame
    void Update()
    {
        //Quaternion rot = _rectTransform.rotation;
        //rot.eulerAngles -= new Vector3(0, 0, Time.deltaTime * 100);
        //_rectTransform.rotation = rot;
    }
}
