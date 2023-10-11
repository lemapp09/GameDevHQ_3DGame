using UnityEngine;

namespace LemApperson_3DGame.Animations
{
    public class AnimationStateAlert : StateMachineBehaviour
    {
        // Reference to the script you want to alert
        private Player.Player someOtherScript;

        // Called when the animation state is entered
        override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            // Initialize the reference to the script you want to alert
            someOtherScript = animator.GetComponentInParent<Player.Player>();

            // Call a method in that script to alert it that the state has been entered
            if (someOtherScript != null)
            {
                someOtherScript.OnAnimationStateEntered(stateInfo.length);
            }
        }

        // Called when the animation state is exited
        override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            // Call a method in that script to alert it that the state has been exited
            if (someOtherScript != null)
            {
                someOtherScript.OnAnimationStateExited(stateInfo.shortNameHash);
            }
        }
    }
}