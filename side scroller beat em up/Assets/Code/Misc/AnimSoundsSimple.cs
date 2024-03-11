using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimSoundsSimple : MonoBehaviour
{


    void SoundIdle()
    {
        //FindObjectOfType<AudioManagerScript>().Play("naamvangeluidhier");
    }


    void SoundAttack1()
    {
        FindObjectOfType<AudioManagerScript>().Play("Attack1");
    }

    void SoundAttack2()
    {
        FindObjectOfType<AudioManagerScript>().Play("Attack2");
    }

    void SoundAttack3()
    {
        FindObjectOfType<AudioManagerScript>().Play("Attack3");
    }

    void SoundAttack4()
    {
        FindObjectOfType<AudioManagerScript>().Play("Attack4");
    }

    void SoundAttack5()
    {
        FindObjectOfType<AudioManagerScript>().Play("Attack5");
    }

    void SoundAttack6()
    {
        FindObjectOfType<AudioManagerScript>().Play("Attack6");
    }

    void SoundWalk1()
    {
        FindObjectOfType<AudioManagerScript>().Play("Walk1");
    }

    void SoundWalk2()
    {
        FindObjectOfType<AudioManagerScript>().Play("Walk2");
    }

    void SoundWalk3()
    {
        FindObjectOfType<AudioManagerScript>().Play("Walk3");
    }
}
