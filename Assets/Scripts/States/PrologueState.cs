using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrologueState : StateMachineBehaviour {

    public AudioClip enterAudio;
    public AudioClip outroAudio;

    private bool enterAudioEnded = false;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        AudioManager.Instance.PlayAudio(enterAudio, () => { enterAudioEnded = true; });
    }

	// OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
	override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        if (enterAudioEnded)
        {
            Debug.Log("UPDATE!");
            AudioManager.Instance.PlayAudio(outroAudio);
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	
	}

}