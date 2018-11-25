using UnityEngine;

public class NormalSceneState : StateMachineBehaviour {

    public AudioClip enterAudio;

    private bool enterAudioEnded = false;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        AudioManager.Instance.PlayAudio(enterAudio, () => { enterAudioEnded = true; });
    }

	// OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
	override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        if (enterAudioEnded)
        {
            animator.SetTrigger("nextState");
        }
	}
}
