using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrampolineScript : MonoBehaviour
{
    [SerializeField]
    private float bounceHeight = 20;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Player")
        {
            CharacterControllerScript player = collision.gameObject.GetComponent<CharacterControllerScript>();
            player.MoveDirectionY = player.MoveDirectionY + bounceHeight;
        }
    }
}
