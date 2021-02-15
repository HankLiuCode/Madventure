using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart : Item
{
    public ParticleSystem heartParticle;
    private ParticleSystem particle;
    public override void Use()
    {
        Character character = GameObject.Find("Player").GetComponent<Character>();
        particle = Instantiate(heartParticle, Vector3.zero, heartParticle.transform.rotation);
        particle.transform.SetParent(character.gameObject.transform, false);
        particle.Play();
    }
}
