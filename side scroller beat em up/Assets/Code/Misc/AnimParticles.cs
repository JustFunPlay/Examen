using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimParticles : MonoBehaviour
{
    public ParticleSystem Attack;
    public ParticleSystem Footstep;


    private void Start()
    {
        //monsterAttack = GetComponent<ParticleSystem>();
        // monsterAttack2 = GetComponent<ParticleSystem>();
    }

    void PlayParticleAttack()
    {
        Attack.Play();
    }

    void PlayParticleWalking()
    {
        Footstep.Play();
    }

}
