using Cyberultimate.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Cyberultimate
{
    public static class Async
    {
        public static WaitForUpdate NextFrame { get; } = new WaitForUpdate();
        public static WaitForFixedUpdate FixedUpdate { get; } = new WaitForFixedUpdate();
        public static WaitForEndOfFrame EndOfFrame { get; } = new WaitForEndOfFrame();
        public static WaitForAnimator WaitForAnimator(Animator animator, string animName, int layer = 0)
            => new WaitForAnimator(animator, animName, layer);
        public static WaitForAnim WaitForAnim(Animation anim)
            => new WaitForAnim(anim);
        public static WaitForSeconds Wait(TimeSpan time)
            => new WaitForSeconds((float)time.TotalSeconds);
        public static WaitUntil Wait(Task task)
        {
            return new WaitUntil(()=>task.IsCompleted);
        }
        public static object Wait(TimeSpan time, bool realTime )
            => Wait((float)time.TotalSeconds, realTime);
        public static WaitForSeconds Wait(float seconds) => new WaitForSeconds(seconds);
        public static WaitForSeconds Wait(SerializedTimeSpan time) => Wait(time.TotalSeconds);
        public static object Wait(float seconds, bool realTime)
            => (realTime) ? (object)WaitUnscaled(seconds) : (object)Wait(seconds);

        public static WaitForSecondsRealtime WaitUnscaled(float seconds)
            => new WaitForSecondsRealtime(seconds);
        public static WaitForSecondsRealtime WaitUnscaled(TimeSpan time)
            => new WaitForSecondsRealtime((float)time.TotalSeconds);
        public static WaitUntil Until(Func<bool> predicate)
            => new WaitUntil(predicate);
        public static WaitWhile While(Func<bool> predicate) 
            => new WaitWhile(predicate);
    }
}

