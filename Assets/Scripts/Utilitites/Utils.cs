using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utilities
{
    public static class Utils
    {
        public static IEnumerator LerpCanvasGroup(CanvasGroup canvasGroup, float toValue, float duration, Action onLerpEnd = null)
        {
            float time = 0;
            float initValue = canvasGroup.alpha;
            while (time < duration)
            {
                canvasGroup.alpha = Mathf.Lerp(initValue, toValue, time / duration);
                time += Time.deltaTime;
                yield return null;
            }
            canvasGroup.alpha = toValue;
            onLerpEnd?.Invoke();
        }

        public static string ToNormalTime(int time)
        {
            string minutes = Mathf.FloorToInt(time / 60).ToString();
            int secondsInt = Mathf.FloorToInt(time % 60);
            string seconds = secondsInt < 10 ? "0" + secondsInt.ToString() : secondsInt.ToString();
            return minutes + ":" + seconds;

        }
    }

}

