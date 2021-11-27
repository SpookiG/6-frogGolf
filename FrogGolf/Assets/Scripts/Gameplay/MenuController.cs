using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    private bool _dead;

    // Start is called before the first frame update
    void Start()
    {
        SequenceManager.Current.Events.toggleControls += ToggleControls;
        enabled = false;
        _dead = false;
    }

    private void OnDestroy()
    {
        SequenceManager.Current.Events.toggleControls -= ToggleControls;
    }

    void ToggleControls(string controlMode)
    {
        if (controlMode == "menu")
        {
            _dead = false;
            enabled = true;
        }
        else if (controlMode == "deadMenu")
        {
            _dead = true;
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
            if (_dead)
            {
                SequenceManager.Current.CallSequence(SequenceManager.Current.Sequences.Restart);
            }
            else
            {
                SequenceManager.Current.CallSequence(SequenceManager.Current.Sequences.NextLevel);
            }
        }
    }
}
