using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySounds : MonoBehaviour
{
    public AudioSource[] EnemyIdle;
    public AudioSource[] EnemyAlert;
    public AudioSource[] EnemyAttack;
    public AudioSource[] EnemyAttack2;
    public AudioSource[] walking;
    public AudioSource[] normalsound;


    void SoundEnemyIdle()
    {
        if (EnemyIdle.Length == 0)
        {
            return;
        }
        int index = Random.Range(0, EnemyIdle.Length - 1);
        EnemyIdle[index].Play();
        EnemyIdle[index].pitch = Random.Range(0.7f, 1.3f);
    }

    void SoundEnemyAlert()
    {
        if (EnemyAlert.Length == 0)
        {
            return;
        }
        int index = Random.Range(0, EnemyAlert.Length - 1);
        EnemyAlert[index].Play();
        EnemyAlert[index].pitch = Random.Range(0.7f, 1.3f);
    }

    void SoundAttack()
    {
        if (EnemyAttack.Length == 0)
        {
            return;
        }
        int index = Random.Range(0, EnemyAttack.Length - 1);
        EnemyAttack[index].Play();
        EnemyAttack[index].pitch = Random.Range(0.7f, 1.3f);
    }

    void SoundAttack2()
    {
        if (EnemyAttack2.Length == 0)
        {
            return;
        }
        int index = Random.Range(0, EnemyAttack2.Length - 1);
        EnemyAttack2[index].Play();
        EnemyAttack2[index].pitch = Random.Range(0.7f, 1.3f);
    }

    void SoundWalking()
    {
        if (walking.Length == 0)
        {
            return;
        }
        int index = Random.Range(0, walking.Length - 1);
        walking[index].Play();
        walking[index].pitch = Random.Range(0.5f, 0.8f);
    }

    public void NormalPitchSound()
    {
        if (normalsound.Length == 0)
        {
            return;
        }
        int index = Random.Range(0, normalsound.Length - 1);
        normalsound[index].Play();
    }
}
