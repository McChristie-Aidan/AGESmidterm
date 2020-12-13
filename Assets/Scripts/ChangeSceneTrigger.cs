using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using System;

public class ChangeSceneTrigger : MonoBehaviour
{
    [SerializeField]
    private Scene TargetScene;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            string targetSceneName = TargetScene.name;
            SceneManager.LoadScene(targetSceneName);
        }
    }
}
