using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeniusState : StateMachineBehaviour {

    public int statesQtd = 4;
    public List<AudioClip> audiosList = new List<AudioClip>(4);

    private int _currentState = 0;
    private List<int> _states;

    private bool _waiting = false;

	 // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        _states = new List<int>(statesQtd);
        for (int i = 0; i < statesQtd; i++)
        {
            _states[0] = Random.Range(1,4);
        }
	}

	// OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
	override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	    if(_currentState >= statesQtd)
        {
            animator.SetTrigger("nextState");
        }
        else
        {
            // Play this state audios
            if (!_waiting)
            {
                for (int i = 0; i < _currentState; i++)
                {
                    AudioManager.Instance.PlayAudio(audiosList[i]);
                }
                _waiting = true;
            }
            // Wait for player inputs for this given state
            else
            {
                   
            }
        }
	}




	// OnStateExit is called when a transition ends and the state machine finishes evaluating this state
	//override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}

	// OnStateMove is called right after Animator.OnAnimatorMove(). Code that processes and affects root motion should be implemented here
	//override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}

	// OnStateIK is called right after Animator.OnAnimatorIK(). Code that sets up animation IK (inverse kinematics) should be implemented here.
	//override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}
}
