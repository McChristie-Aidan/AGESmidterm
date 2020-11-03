using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneReset : MonoBehaviour
{
    [SerializeField]
    private float deathPlaneLevel = -50f;


    void Update()
    {
        GetButtonReset();
        FellOutOfWorldReset();
    }

    //resets the level if the player falls below a certain depth.
    private void FellOutOfWorldReset()
    {
        //Debug.Log("object is " + (-1*(deathPlaneLevel - transform.position.y)) + " from the death plane.");
        if (transform.position.y < deathPlaneLevel)
        {
            ReloadCurrentScene();
        }
    }

    //resets the level when a certain keyboard key is pressed.
    private void GetButtonReset()
    {
        if (Input.GetButtonDown("Reset"))
        {
            ReloadCurrentScene();
        }
    }

    //finds what the current scene is and then loads it.
    private void ReloadCurrentScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
