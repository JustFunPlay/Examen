using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    public Animator Transition;

    public float TransitionTime = 1f;

    public void LoadScene(int sceneToLoad = -1)
    {
        if (sceneToLoad == -1) sceneToLoad = SceneManager.GetActiveScene().buildIndex + 1;
        StartCoroutine(LoadSceneAnim(sceneToLoad));
    }



    IEnumerator LoadSceneAnim(int sceneIndex)
    {
        Transition.SetTrigger("Start");

        yield return new WaitForSeconds(TransitionTime);

        SceneManager.LoadScene(sceneIndex);
    }
}
