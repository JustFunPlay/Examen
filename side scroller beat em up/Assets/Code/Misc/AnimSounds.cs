using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimSounds : MonoBehaviour
{
    public AudioSource[] Idle;
    public AudioSource[] Alert;
    public AudioSource[] Attack;
    public AudioSource[] Attack2;
    public AudioSource[] Attack3;
    public AudioSource[] Attack4;
    public AudioSource[] Walking;
    public AudioSource[] NormalSound;


    void SoundIdle()
    {
        if (Idle.Length == 0)
        {
            return;
        }
        int index = Random.Range(0, Idle.Length - 1);
        Idle[index].Play();
        Idle[index].pitch = Random.Range(0.7f, 1.3f);
    }

    void SoundaAlert()
    {
        if (Alert.Length == 0)
        {
            return;
        }
        int index = Random.Range(0, Alert.Length - 1);
        Alert[index].Play();
        Alert[index].pitch = Random.Range(0.7f, 1.3f);
    }

    void SoundAttack()
    {
        if (Attack.Length == 0)
        {
            return;
        }
        int index = Random.Range(0, Attack.Length - 1);
        Attack[index].Play();
        Attack[index].pitch = Random.Range(0.7f, 1.3f);
    }

    void SoundAttack2()
    {
        if (Attack2.Length == 0)
        {
            return;
        }
        int index = Random.Range(0, Attack2.Length - 1);
        Attack2[index].Play();
        Attack2[index].pitch = Random.Range(0.7f, 1.3f);
    }

    void SoundWalking()
    {
        if (Walking.Length == 0)
        {
            return;
        }
        int index = Random.Range(0, Walking.Length - 1);
        Walking[index].Play();
        Walking[index].pitch = Random.Range(0.5f, 0.8f);
    }

    public void NormalPitchSound()
    {
        if (NormalSound.Length == 0)
        {
            return;
        }
        int index = Random.Range(0, NormalSound.Length - 1);
        NormalSound[index].Play();
    }
}
