using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ContexualMessageTrigger : MonoBehaviour
{
    [SerializeField]
    [TextArea(3,5)]
    private string message = "your message here";
    [SerializeField]
    private float messageDuration = 1.0f;

    public static Action<string, float> ContextualMessageTriggered;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (ContextualMessageTriggered != null)
            {
                ContextualMessageTriggered.Invoke(message, messageDuration);
            }
        }
    }
}
