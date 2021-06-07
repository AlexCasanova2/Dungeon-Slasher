using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHitbox : MonoBehaviour
{
    public ParticleSystem hitParticle;
    public PlayerController playerController;
    
    private void Update()
    {
        if (playerController.isFacingRight == true)
        {
            var sh = hitParticle.shape;
            sh.scale = new Vector3(1f, 0f, 0f);
        }
        else
        {
            var sh = hitParticle.shape;
            sh.scale = new Vector3(-1f, 0f, 0f); ;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Ey");
        hitParticle.Play();
    }
}
