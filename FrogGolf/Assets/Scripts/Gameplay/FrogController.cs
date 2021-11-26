using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        SequenceManager.Current.Events.toggleControls += ToggleControls;
        SequenceManager.Current.Events.resetLevel += ResetFrog;
    }

    private void OnDestroy()
    {
        SequenceManager.Current.Events.toggleControls -= ToggleControls;
        SequenceManager.Current.Events.resetLevel -= ResetFrog;
    }

    void ToggleControls(string controlMode)
    {

    }

    void ResetFrog()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
