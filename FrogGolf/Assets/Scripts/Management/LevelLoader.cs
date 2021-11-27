using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public string LevelName = "Round1";

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
        SceneManager.LoadScene(LevelName);
    }
}
