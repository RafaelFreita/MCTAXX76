using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadioTryState : StateMachineBehaviour
{

    public int pot1Goal = 64;
    public int pot2Goal = 220;

    public int goalThreshold = 40;
    public int minStartDifference = 80;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        do
        {
            pot1Goal = Random.Range(0, 255);
        } while (Mathf.Abs(pot1Goal - animator.GetInteger("pot1")) < minStartDifference);
        do
        {
            pot2Goal = Random.Range(0, 255);
        } while (Mathf.Abs(pot2Goal - animator.GetInteger("pot2")) < minStartDifference);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        int pot1CurVal = pot1Goal - animator.GetInteger("pot1");
        int pot2CurVal = pot2Goal - animator.GetInteger("pot2");

        if ((Mathf.Abs(pot1CurVal) + Mathf.Abs(pot2CurVal)) < goalThreshold)
        {
            animator.SetTrigger("nextState");
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
