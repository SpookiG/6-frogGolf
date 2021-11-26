using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationTransition : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        SequenceManager.Current.Events.uITransition += RunTransition;
    }

    private void OnDestroy()
    {
        SequenceManager.Current.Events.uITransition -= RunTransition;
    }

    public IEnumerator RunTransition(bool transitionIn)
    {
        yield return null;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
