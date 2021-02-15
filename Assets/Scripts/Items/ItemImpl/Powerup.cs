using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : Item
{
    public ParticleSystem powerupParticle;
    public float jumpEnhance;
    private ParticleSystem particle;
    private bool isTakingEffect;
    

    public override void Use()
    {
        if (!isTakingEffect)
        {
            Character character = GameObject.Find("Player").GetComponent<Character>();
            character.AddJumpSpeed(jumpEnhance);
            particle = Instantiate(powerupParticle, Vector3.zero, powerupParticle.transform.rotation);
            particle.transform.SetParent(character.gameObject.transform, false);
            particle.Play();
            isTakingEffect = true;
        }
        else
        {
            Character character = GameObject.Find("Player").GetComponent<Character>();
            character.SubtractJumpSpeed(jumpEnhance);
            particle.Stop();
            particle = null;
            isTakingEffect = false;
        }
    }
}
