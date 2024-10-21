using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AniOffAndDestroy : StateMachineBehaviour
{

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)    //애니메이션 종료시 해당 오브젝트 파괴
    {
        Destroy(animator.gameObject);
    }

   
}
