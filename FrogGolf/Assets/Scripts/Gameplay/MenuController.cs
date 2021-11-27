using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        SequenceManager.Current.Events.toggleControls += ToggleControls;
        enabled = false;
    }

    private void OnDestroy()
    {
        SequenceManager.Current.Events.toggleControls -= ToggleControls;
    }

    void ToggleControls(string controlMode)
    {
        if (controlMode == "menu")
        {
            enabled = true;
        }
        else
        {
            enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            SequenceManager.Current.CallSequence(SequenceManager.Current.Sequences.NextLevel);
        }
    }
}
