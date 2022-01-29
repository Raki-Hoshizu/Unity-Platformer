using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orb : MonoBehaviour
{
    CircleCollider2D OrbCollider;
    AudioSource Audio;

    private void Start()
    {
        OrbCollider = GetComponent<CircleCollider2D>();
        Audio = GetComponent<AudioSource>();
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (LayerMask.LayerToName(other.gameObject.layer) == "Player")
        {
            FindObjectOfType<PlayerMovement>().Jump();
            Audio.Play();
        }
    }
}
