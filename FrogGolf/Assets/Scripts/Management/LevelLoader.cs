using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelLoader : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        SequenceManager.Current.Events.loadLevel += LoadLevel;
    }

    private void OnDestroy()
    {
        SequenceManager.Current.Events.loadLevel -= LoadLevel;
    }

    public void LoadLevel()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
