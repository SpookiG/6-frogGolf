using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Sequences can be called from anywhere
// Anything can subscribe to events used by sequences via SequenceEventAccessor
// Only Sequences can call events used by sequences
// added to special assembly to allow use of internal modifier
public class SequenceManager : MonoBehaviour
{
    public static SequenceManager Current;

    public SequenceEventAccessor Events;
    public SequenceAccessor Sequences;

    private void Awake()
    {
        // SequenceManager is carried between scenes so remove any other instances of SequenceManagers (used for testing individual scenes)
        if (Current == null)
        {
            Current = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }

        
        Events = new SequenceEventAccessor();
        Sequences = new SequenceAccessor();
        
    }

    // sequence call function, pass in sequence via the SequenceAccessor
    public void CallSequence(Sequence sequence)
    {
        StartCoroutine(sequence.RunSequence(this, Events));
    }


    // Start is called before the first frame update
    /*void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }*/
}



// --------------------------------------------------------------------------------------
// Sequence Event related objects



public class SequenceEventAccessor
{
    // coroutine events (yield return these in a sequence)
    public event Func<string, float, IEnumerator> fadeMenuTo;
    internal Coroutine FadeMenuTo(SequenceManager sequenceManager, string menuName, float targetAlpha)
    {
        if (fadeMenuTo != null)
        {
            return sequenceManager.StartCoroutine(fadeMenuTo(menuName, targetAlpha));
        }

        return null;
    }

    public event Func<string, bool, IEnumerator> slideMenu;
    internal Coroutine SlideMenu(SequenceManager sequenceManager, string menuName, bool slideIn)
    {
        if (slideMenu != null)
        {
            return sequenceManager.StartCoroutine(slideMenu(menuName, slideIn));
        }

        return null;
    }

    public event Func<bool, IEnumerator> uITransition;
    internal Coroutine UITransition(SequenceManager sequenceManager, bool transitionIn)
    {
        if (uITransition != null)
        {
            return sequenceManager.StartCoroutine(uITransition(transitionIn));
        }

        return null;
    }

    // instant events (no need for coroutines)
    public event Action<string> toggleControls;                 // TODO: distinguish between player and menu controls
    internal void ToggleControls(string controlMode)
    {
        toggleControls?.Invoke(controlMode);
    }

    public event Action loadLevel;                            // subscribe & remove next level based on where u are
    internal void LoadLevel()
    {
        loadLevel?.Invoke();
    }

    public event Action resetLevel;
    internal void ResetLevel()
    {
        resetLevel?.Invoke();
    }


    // TODO add a clear UI event
}



// --------------------------------------------------------------------------------------
// Sequence related objects



public abstract class Sequence
{
    internal abstract IEnumerator RunSequence(SequenceManager sequenceManager, SequenceEventAccessor sequenceEventManager);
    //public void InterruptSequence(SequenceManager sequenceManager) {}
}


public class SequenceAccessor
{
    public Sequence Opening = new OpeningSequence();
    public Sequence WinLevel = new WinLevelSequence();
    public Sequence NextLevel = new NextLevelSequence();
    public Sequence Die = new DieSequence();
    public Sequence Restart = new RestartSequence();
    public Sequence FinishGame = new FinishGameSequence();
}



public class OpeningSequence : Sequence
{
    internal override IEnumerator RunSequence(SequenceManager sequenceManager, SequenceEventAccessor sequenceEventManager)
    {
        // IMPORTANT
        // Sequencer can't do simultaneous sequences atm so switch controlls off during every sequence so player can't trigger more than one at a time
        sequenceEventManager.ToggleControls("off");

        // maybe play a sound?

        yield return sequenceEventManager.SlideMenu(sequenceManager, "StartMenu", true);
        sequenceEventManager.ToggleControls("menu");
    }
}

public class WinLevelSequence : Sequence
{
    internal override IEnumerator RunSequence(SequenceManager sequenceManager, SequenceEventAccessor sequenceEventManager)
    {
        sequenceEventManager.ToggleControls("off");
        // toggle menu controls true
        sequenceEventManager.FadeMenuTo(sequenceManager, "GameUI", 0);
        yield return sequenceEventManager.SlideMenu(sequenceManager, "WinScreen", true);
        sequenceEventManager.ToggleControls("menu");
    }
}

public class NextLevelSequence : Sequence
{
    internal override IEnumerator RunSequence(SequenceManager sequenceManager, SequenceEventAccessor sequenceEventManager)
    {
        sequenceEventManager.ToggleControls("off");
        yield return sequenceEventManager.UITransition(sequenceManager, false);

        // stop all coroutines to prevent sequences interfering, but this also stops NextLevelSequence1 so we need to continue by calling NextLevelSequence2
        sequenceManager.StopAllCoroutines();
        sequenceEventManager.LoadLevel();
        yield return sequenceEventManager.UITransition(sequenceManager, true);
        sequenceEventManager.ToggleControls("player");
    }
}

public class DieSequence : Sequence
{
    internal override IEnumerator RunSequence(SequenceManager sequenceManager, SequenceEventAccessor sequenceEventManager)
    {
        sequenceEventManager.ToggleControls("off");
        // toggle menu controls true
        // update death counter?

        yield return sequenceEventManager.SlideMenu(sequenceManager, "DieScreen", true);
        sequenceEventManager.ToggleControls("menu");
    }
}

public class RestartSequence : Sequence
{
    internal override IEnumerator RunSequence(SequenceManager sequenceManager, SequenceEventAccessor sequenceEventManager)
    {
        sequenceEventManager.ToggleControls("off");
        yield return sequenceEventManager.UITransition(sequenceManager, false);
        sequenceEventManager.ResetLevel();
        yield return sequenceEventManager.UITransition(sequenceManager, true);
        sequenceEventManager.ToggleControls("player");
    }
}

public class FinishGameSequence : Sequence
{
    internal override IEnumerator RunSequence(SequenceManager sequenceManager, SequenceEventAccessor sequenceEventManager)
    {
        sequenceEventManager.ToggleControls("off");
        // play sound?
        yield return sequenceEventManager.SlideMenu(sequenceManager, "FinalScreen", true);
        // toggle menu controls true
        sequenceEventManager.ToggleControls("menu");
    }
}