using TMPro;
using System;
using System.IO;
using DG.Tweening;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;

namespace Assets.Source.Extensions.Scripts
{
    public static class ExtentionsCanvases
    {
        private static string[] _formatName = new[]
        {
            "", "K", "M", "T", "B", "S", "Q", "R", "X"
        };

        public const float Duration = 0.3f;
        public const float Delay = 0.5f;

        public static void EnableGroup(this CanvasGroup canvasGroup, float duration = Duration)
        {
            canvasGroup.interactable = true;
            canvasGroup.blocksRaycasts = true;
            canvasGroup.DOComplete(true);
            canvasGroup.DOFade(1f, duration).SetUpdate(true);
        }

        public static void DelayedEnableGroup(this CanvasGroup canvasGroup, float duration = Duration, float delay = Delay)
        {
            canvasGroup.interactable = true;
            canvasGroup.blocksRaycasts = true;
            canvasGroup.DOComplete(true);
            canvasGroup.DOFade(1f, duration).SetDelay(delay);
        }

        public static void EnableGroupInteracting(this CanvasGroup canvasGroup)
        {
            canvasGroup.interactable = true;
            canvasGroup.blocksRaycasts = true;
        }

        public static void DelayedDisableGroup(this CanvasGroup canvasGroup, float duration = Duration, float delay = Delay)
        {
            canvasGroup.interactable = false;
            canvasGroup.blocksRaycasts = false;
            canvasGroup.DOComplete(true);
            canvasGroup.DOFade(0f, duration).SetDelay(delay);
        }

        public static void DelayedDisableGroupInteractPast(this CanvasGroup canvasGroup, float duration = Duration, float delay = Delay)
        {
            canvasGroup.DOComplete(true);
            canvasGroup.DOFade(0f, duration).SetDelay(delay).OnKill(() =>
            {
                canvasGroup.interactable = false;
                canvasGroup.blocksRaycasts = false;
            });
        }

        public static void DisableGroup(this CanvasGroup canvasGroup, float duration = Duration)
        {
            canvasGroup.interactable = false;
            canvasGroup.blocksRaycasts = false;
            canvasGroup.DOComplete(true);
            canvasGroup.DOFade(0f, duration).SetUpdate(true);
        }

        public static void DisableGroupInteracting(this CanvasGroup canvasGroup)
        {
            canvasGroup.interactable = false;
            canvasGroup.blocksRaycasts = false;
        }

        public static void DisableGroups(this CanvasGroup[] canvasGroups, float duration = Duration)
        {
            foreach (CanvasGroup canvas in canvasGroups)
                canvas.DisableGroup();
        }

        public static void EnableFade(this CanvasGroup canvasGroup, float duration = Duration)
        {
            canvasGroup.DOComplete(true);
            canvasGroup.DOFade(1f, duration);
        }

        public static void DisableFade(this CanvasGroup canvasGroup, float duration = Duration)
        {
            canvasGroup.DOComplete(true);
            canvasGroup.DOFade(0f, duration);
        }

        public static Tween GetDisableFade(this CanvasGroup canvasGroup, float duration = Duration)
        {
            return canvasGroup.DOFade(0f, duration);
        }

        public static void EnableFadeText(this TMP_Text text, float duration = Duration)
        {
            text.DOComplete(true);
            text.DOFade(1f, duration);
        }

        public static void CompleteImmediately(this CanvasGroup canvasGroup)
        {
            canvasGroup.DOComplete(true);
            canvasGroup.alpha = 0;
        }

        public static void DisableFadeText(this TMP_Text text, float duration = Duration)
        {
            text.DOComplete(true);
            text.DOFade(0f, duration);
        }

        public static string FormatNumbers(float value)
        {
            if (value <= 0)
                return "0";

            int index;
            int devide = 1000;

            if (value < devide)
                return value.ToString("0");

            for (index = 0; index < _formatName.Length; index++)
            {
                if (value >= devide)
                    value /= devide;
                else
                    break;
            }

            return value.ToString("0.0") + _formatName[index];
        }

        public static void DiscardAfterPosition(this TMP_Text text, string label, int discardPosition)
        {
            if (label.Length > discardPosition)
            {
                text.text = label.Substring(0, discardPosition);
                return;
            }

            text.text = label;
        }

        public static void SetShortTimeFormat(this TMP_Text text, TimeSpan time)
        {
            string minutes = time.Minutes < 10 ? $"0{time.Minutes}" : time.Minutes.ToString();
            string seconds = time.Seconds < 10 ? $"0{time.Seconds}" : time.Seconds.ToString();
            text.text = $"{minutes}:{seconds}";
        }

        public static void SetFullTimeFormat(this TMP_Text text, TimeSpan time)
        {
            string hours = time.Hours < 10 ? $"0{time.Hours}" : time.Hours.ToString();
            string minutes = time.Minutes < 10 ? $"0{time.Minutes}" : time.Minutes.ToString();
            string seconds = time.Seconds < 10 ? $"0{time.Seconds}" : time.Seconds.ToString();
            text.text = $"{hours}:{minutes}:{seconds}";
        }

        public static void TrimLine(this TMP_Text text, string label, int maxLabelLenght)
        {
            if (label.Length == 0 || label == "Enter name...")
            {
                text.text = "Empty";
                return;
            }
            else if (label.Length < maxLabelLenght)
            {
                text.text = label;
                return;
            }

            text.text = label.Substring(0, label.Length - (label.Length - maxLabelLenght)) + "..";
        }

        public static byte[] ObjectToByteArray(this object obj)
        {
            if (obj == null)
                return null;

            BinaryFormatter bf = new BinaryFormatter();

            using (MemoryStream ms = new MemoryStream())
            {
                bf.Serialize(ms, obj);
                return ms.ToArray();
            }
        }

        public static object ByteArrayToObject(this byte[] array)
        {
            if (array == null)
                return null;

            BinaryFormatter bf = new BinaryFormatter();

            using (MemoryStream ms = new MemoryStream(array))
                return bf.Deserialize(ms);
        }

        private static Tween DOFade(this CanvasGroup canvasGroup, float value, float time) =>
            DOTween.To(() => canvasGroup.alpha, x => canvasGroup.alpha = x, value, time);


        private static Tween DOFade(this TMP_Text text, float value, float time) =>
            DOTween.To(() => text.alpha, x => text.alpha = x, value, time);
    }
}
