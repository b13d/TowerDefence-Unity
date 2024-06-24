using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TraceFinishEvent : StateMachineBehaviour
{
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Removing gunshot marks
        Destroy(animator.gameObject, 1.2f);
    }
}
