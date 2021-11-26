using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasSlider : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        SequenceManager.Current.Events.slideMenu += SlideMenu;
    }

    private void OnDestroy()
    {
        SequenceManager.Current.Events.slideMenu -= SlideMenu;
    }

    public IEnumerator SlideMenu(string menuName, bool slideIn)
    {
        yield return null;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
