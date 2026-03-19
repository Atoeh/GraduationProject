using Unity.Mathematics;
using Unity.VisualScripting;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UIElements;

public class PlayerMovement : MonoBehaviour
{
    //MOVEMENT STATS --------------------
    [SerializeField]
    private int moveDistance = 2;
    [SerializeField]
    private float moveDuration = .2f;

    //ROTATION STATS --------------------
    [SerializeField]
    private float rotationDuration = .2f;
    private int rotationDegrees = 90;

    public void MoveForward()
    {
        StartCoroutine(MovePlayer(transform.forward));
    }

    public void MoveBack()
    {
        StartCoroutine(MovePlayer(-transform.forward));
    }

    public void MoveLeft()
    {
        StartCoroutine(MovePlayer(-transform.right));
    }

    public void MoveRight() 
    {
        StartCoroutine(MovePlayer(transform.right));
    }

    public void TurnLeft()
    {
        StartCoroutine(RotatePlayer(-rotationDegrees));
    }

    public void TurnRight() 
    {
        StartCoroutine(RotatePlayer(rotationDegrees));
    }

    IEnumerator RotatePlayer(float degrees)
    {
        GameEvents.ToggleMoveUI();
        Quaternion startRotation = transform.rotation;
        Quaternion targetRotation = startRotation * Quaternion.Euler(0,degrees,0);

        float elapsed = 0f;

        while (Quaternion.Angle(transform.rotation, targetRotation) > 0.1f)
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, 
                targetRotation, elapsed / rotationDuration);
            elapsed += Time.deltaTime;
            yield return null;
        }
        transform.rotation = targetRotation;
        GameEvents.ToggleMoveUI();
    }

    IEnumerator MovePlayer(Vector3 Direction)
    {
        GameEvents.ToggleMoveUI();

        Vector3 startPosition = transform.position;
        Vector3 targetPosition = startPosition + Direction * moveDistance;

        float elapsed = 0f;

        while (transform.position != targetPosition)
        {
            transform.position = Vector3.Lerp(startPosition, targetPosition, elapsed /
                moveDuration);
            elapsed += Time.deltaTime;
            yield return null;
        }
        transform.position = targetPosition;

        GameEvents.ToggleMoveUI();
    }
}
