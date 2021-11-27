using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReplayGame : MonoBehaviour
{
    

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            SequenceManager.Current.CallSequence(SequenceManager.Current.Sequences.NextLevel);
        }
    }
}
