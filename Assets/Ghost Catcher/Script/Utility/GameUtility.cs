using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Unicorn.GhostCather.Utility
{
    public static class GameUtility
    {
        public static int TryParseInt(this string s, int defaultResult = 0)
        {
            int result = defaultResult;
            bool success = int.TryParse(s, out result);
            if (!success) Debug.Log(s + "Không thể convert sang int!"); 
            return result;
        }

        private static readonly System.Random random = new System.Random();

        /// <summary>
        /// random từ (min,max)
        /// </summary>
        public static int RandomInt(int min,int max)
        {
            return random.Next(min,max + 1);
        }

        public static T LoadResources<T>(string path) where T : UnityEngine.Object
        {
            T t = Resources.Load<T>(path);
            if (t == null) Debug.Log($"Không thể tìm thấy tài nguyên tại đường dẫn: {path}");
            return t;
        }

        static Dictionary<string, Coroutine> coroutinesOn = new Dictionary<string, Coroutine>();
        /// <summary>
        /// Dùng coroutine để gọi 1 hàm sau 1 khoảng thời gian truyền vào
        /// </summary>
        public static void DelayCall(this MonoBehaviour monoBehaviour, string name, Action callBack, float timeDelay)
        {
            Coroutine currentCall = monoBehaviour.StartCoroutine(IEDelay(callBack, timeDelay, name));
            coroutinesOn[name] = currentCall;
        }
        /// <summary>
        /// hủy gọi hàm delay
        /// </summary>
        public static void StopDeLayCall(this MonoBehaviour monoBehaviour, string name)
        {
            if (coroutinesOn.ContainsKey(name))
            {
                Coroutine coroutine = coroutinesOn[name];
                monoBehaviour.StopCoroutine(coroutine);
                coroutinesOn.Remove(name);
            }

        }
        private static IEnumerator IEDelay(Action callBack, float timeDelay, string name)
        {
            yield return new WaitForSeconds(timeDelay);
            callBack?.Invoke();
            coroutinesOn.Remove(name);
        }

        #region dotween
        /// <summary>
        ///  dùng để chạy anim BackPressed
        ///  list animators truyền vào để tạm tắt animation tránh xung đột làm cho dotween không chạy
        /// </summary>
        public static void OnBackPressedWithAnim(this MonoBehaviour mono, List<DOTweenAnimation> DOTweenAnimations, Action callback = null, List<Animator> animators = null)
        {
            if (animators != null)
            {
                foreach (var animator in animators)
                    animator.enabled = false;
            }
            float longestInterval = 0;
            foreach (var anim in DOTweenAnimations)
            {
                if (anim.loops != -1)
                {
                    float animDuration = anim.delay + anim.loops * anim.duration;
                    longestInterval = longestInterval > animDuration ? longestInterval : animDuration;

                    anim.DOPlayBackwards();
                }
                else
                    Debug.LogWarning("The animation can't be played backward because it infinitely loops");
            }
            mono.DelayCall(nameof(OnBackPressedWithAnim), () =>
            {
                callback?.Invoke();
                if (animators != null)
                {
                    foreach (var animator in animators)
                        animator.enabled = true;
                }
            }, longestInterval);
        }

        #endregion
    }
}
