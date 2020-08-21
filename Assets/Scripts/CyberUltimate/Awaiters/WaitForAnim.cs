using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Cyberultimate
{
    public class WaitForAnim : CustomYieldInstruction
    {
        public override bool keepWaiting
        {
            get
            {
                return Animation.isPlaying;
            }
        }
        public Animation Animation { get; }
        public WaitForAnim(Animation animation)
        {
            Animation = animation;
        }
    }
}
