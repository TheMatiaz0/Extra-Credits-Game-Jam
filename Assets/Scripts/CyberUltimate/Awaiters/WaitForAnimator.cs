using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Cyberultimate
{
    public class WaitForAnimator : CustomYieldInstruction
    {
        public override bool keepWaiting
        {
            get
            {
                return Animator.GetCurrentAnimatorStateInfo(Layer).IsName(AnimName);
            }
        }
        public Animator Animator { get; }
        public string AnimName { get; }
        public int Layer { get; }
        public WaitForAnimator(Animator animator,string animName,int layer=0 )
        {

            Animator = animator;
            Layer = layer;
            AnimName = animName;

        }
    }

}