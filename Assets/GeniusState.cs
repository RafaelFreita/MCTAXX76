using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeniusState : StateMachineBehaviour
{

    public int statesQtd = 4;
    public List<float> freqsList = new List<float>(4);

    private Oscilator oscilator;
    private int _currentState = 1;
    private List<int> _states;

    private bool finishedAudios = false;

    private bool waitingAudio = false;
    private bool audioEnded = false;
    private int playingState = 0;

    private int currentTry = 0;
    private bool reseting = false;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _states = new List<int>(4);
        List<int> myArr = new List<int>() { 1, 2, 3, 4 };
        for (int i = 0; i < statesQtd; i++)
        {
            int index = Random.Range(0, myArr.Count - 1);
            _states.Add(myArr[index]);
            myArr.RemoveAt(index);
            Debug.Log(_states[i]);
        }
        oscilator = AudioManager.Instance.oscilator1;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (_currentState > statesQtd)
        {
            animator.SetTrigger("nextState");
        }
        else if (!reseting)
        {
            // Play this state audios
            if (!finishedAudios)
            {
                PlayStateAudios();
            }
            // Wait for player inputs for this given state
            else
            {
                if (animator.GetBool("btn1"))
                {
                    if (_states[currentTry] == 1)
                    {
                        AdvanceTry();
                    }
                    else
                    {
                        Reset();
                    }
                }

                if (animator.GetBool("btn2"))
                {
                    if (_states[currentTry] == 2)
                    {
                        AdvanceTry();
                    }
                    else
                    {
                        Reset();
                    }
                }

                if (animator.GetBool("btn3"))
                {
                    if (_states[currentTry] == 3)
                    {
                        AdvanceTry();
                    }
                    else
                    {
                        Reset();
                    }
                }

                if (animator.GetBool("btn4"))
                {
                    if (_states[currentTry] == 4)
                    {
                        AdvanceTry();
                    }
                    else
                    {
                        Reset();
                    }
                }
            }

            if (currentTry >= _currentState)
            {
                currentTry = 0;
                _currentState++;
                playingState = 0;
                waitingAudio = false;
                audioEnded = false;
                finishedAudios = false;
            }

            if (_currentState > statesQtd)
            {
                animator.SetTrigger("nextState");
            }
        }
    }



    private void AdvanceTry()
    {
        reseting = true;
        oscilator.PlayFrequency(freqsList[_states[currentTry]-1], 0.5f, () => { reseting = false; });
        currentTry++;
    }


    private void Reset()
    {
        reseting = true;
        oscilator.PlayFrequency(120.0f, 1.5f, () =>
        {
            currentTry = 0;
            finishedAudios = false;
            reseting = false;
            playingState = 0;
            waitingAudio = false;
            audioEnded = false;
        });
    }

    private void PlayStateAudios()
    {
        if (!waitingAudio)
        {
            waitingAudio = true;
            oscilator.PlayFrequency(freqsList[_states[playingState]-1], 0.5f, () => { audioEnded = true; });
        }
        else if (audioEnded)
        {
            playingState += 1;
            audioEnded = false;
            waitingAudio = false;
        }

        if (playingState >= _currentState)
        {
            finishedAudios = true;
        }
    }

}
