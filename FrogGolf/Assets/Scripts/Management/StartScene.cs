using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartScene : MonoBehaviour
{
    public bool FirstScene = false;

    // Update is called once per frame
    void Update()
    {
        if (FirstScene)
        {
            SequenceManager.Current.CallSequence(SequenceManager.Current.Sequences.Opening);
            
        }
        else
        {

            SequenceManager.Current.CallSequence(SequenceManager.Current.Sequences.DebugStartLevel);

        }

        enabled = false;
    }
}
