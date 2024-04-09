using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Tadi.Utils
{
    public class OnTrigger2DUtil : MonoBehaviour
    {
        public string targetTag = "Player";
        public UnityEvent OnTriggerEnterEvent, OnTriggerExitEvent;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag(targetTag))
            {
                OnTriggerEnterEvent?.Invoke();
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.CompareTag(targetTag))
            {
                OnTriggerExitEvent?.Invoke();
            }
        }
    }
}