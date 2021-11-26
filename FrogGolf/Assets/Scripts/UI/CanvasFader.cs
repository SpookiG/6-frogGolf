using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasFader : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        SequenceManager.Current.Events.fadeMenuTo += FadeMenuTo;
    }

    private void OnDestroy()
    {
        SequenceManager.Current.Events.fadeMenuTo -= FadeMenuTo;
    }

    public IEnumerator FadeMenuTo(string menuName, float targetAlpha)
    {
        yield return null;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
