using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Alex Pantuck
 * Time Manager
 * 
 * Rather than using Time.timeScale, this interface implements a custom variable, TimeScale,
 * which can be used and changed to slow down time manually.
 * Effects which require time-effects should be multiplied by TimeManager.TimeScale
 * Automatically adjusts physics udate interval as time slows and accelerates.
 * 
 */

namespace TimeManagement
{
    public class TimeManager : MonoBehaviour
    {
        [Range(0, 1)]
        public float slowestTimeScale = 0.1f;
        [Range(0, 1)]
        public float slowestPlayerTimeScale = 0.5f;

        public float slowDuration = 1.5f;
        public float fadeInTime = 0.2f;
        public float fadeOutTime = 0.7f;

        public static float TimeScale = 1;
        public static float PlayerTimeScale = 1;

        private float defaultFixedScale;

        private void Awake()
        {
            defaultFixedScale = Time.fixedDeltaTime;
        }

        public void SlowTime()
        {
            StartCoroutine(Slow_Time());
        }

        // Decelerate time, pause, accelerate time
        private IEnumerator Slow_Time()
        {
            StartCoroutine(Fade(fadeInTime, false));
            yield return new WaitForSecondsRealtime(slowDuration);
            StartCoroutine(Fade(fadeOutTime, true));
        }

        private IEnumerator Fade(float duration, bool isIncreasing)
        {
            float mult = (isIncreasing) ? 1 : -1;

            float startTime = Time.unscaledTime;
            while (startTime < duration)
            {
                TimeScale += (1 / duration) * Time.deltaTime * mult;
                TimeScale = Mathf.Clamp(TimeScale, slowestTimeScale, 1);

                PlayerTimeScale += (1 / duration) * Time.deltaTime * mult;
                PlayerTimeScale = Mathf.Clamp(PlayerTimeScale, slowestPlayerTimeScale, 1);

                Time.fixedDeltaTime *= TimeScale;
                Time.fixedDeltaTime = Mathf.Clamp(Time.fixedDeltaTime, 0, defaultFixedScale);

                yield return null;
            }
        }
        
    }
}