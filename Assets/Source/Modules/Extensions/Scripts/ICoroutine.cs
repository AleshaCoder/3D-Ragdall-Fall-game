using UnityEngine;
using System.Collections;

namespace Assets.Source.Extensions.Scripts
{
    public interface ICoroutine
    {
        public Coroutine StartCoroutine(IEnumerator coroutine);
        public void StopCoroutine(Coroutine coroutine);
        public void StopCoroutine(IEnumerator waitCoroutine);
    }
}
