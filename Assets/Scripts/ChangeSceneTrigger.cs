using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using System;

public class ChangeSceneTrigger : MonoBehaviour
{
    [SerializeField]
    private int TargetSceneIndex;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            SceneManager.LoadScene(TargetSceneIndex);
        }
    }
}
