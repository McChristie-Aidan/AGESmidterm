using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrampolineScript : MonoBehaviour
{
    [SerializeField]
    private float bounceHeight = 20;

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("bounced on trampoline");
        if (other.gameObject.transform.tag == "Player")
        {
            CharacterControllerScript player = other.gameObject.GetComponent<CharacterControllerScript>();

            player.MoveDirectionY = bounceHeight;
            player.createParticles();
        }
    }
}
