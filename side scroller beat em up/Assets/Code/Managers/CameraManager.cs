using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public List<Character> Characters = new List<Character>();
    private Vector3 cameraPosition;
    bool camIsLocked;
    [SerializeField] private float camLimitLeft, camLimitRight;

    [Header("editor testing")]
    [SerializeField] bool togleLock;

    private void Start()
    {
        cameraPosition = transform.position;
    }
    private void Update()
    {
        if (togleLock)
        {
            if (!camIsLocked)
                LockAtPosition(0);
            else
                ReleaseFromPosition();
            togleLock = false;
        }
        List<float> characterPositions = new List<float>();
        if (Characters.Count == 0) return;
        for (int i = 0; i < Characters.Count; i++)
        {
            characterPositions.Add(Characters[i].transform.position.x);
            Characters[i].SetMoveEdges(transform.position.x - 5, transform.position.x + 5);
        }
        float avaragePosition = 0;
        for (int i = 0;i < characterPositions.Count; i++)
        {
            avaragePosition += characterPositions[i];
        }
        avaragePosition /= characterPositions.Count;
        if (!camIsLocked) cameraPosition.x = Mathf.Clamp(avaragePosition, camLimitLeft, camLimitRight);

        transform.position = Vector3.Lerp(transform.position, cameraPosition, 1.2f * Time.deltaTime);
    }

    public void LockAtPosition(float position)
    {
        camIsLocked = true;
        cameraPosition.x = position;
        Debug.Log("Camera has been locked");
    }
    public void ReleaseFromPosition()
    {
        camIsLocked = false;
        Debug.Log("Camera has been released");
    }
}
