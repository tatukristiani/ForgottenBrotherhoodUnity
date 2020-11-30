using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;


//Used for destroying the explosion object. Had issues with animating it and this fixed it.
public class DestroyOnExit : StateMachineBehaviour
{

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Destroy(animator.gameObject, stateInfo.length);
    }
}
