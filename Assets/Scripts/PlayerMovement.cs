using Unity.Mathematics;
using Unity.VisualScripting;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UIElements;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private int moveDistance = 2;
    private int rotationDegrees = 90;

    [SerializeField]
    private float moveSpeed = 0.5f;
    [SerializeField]
    private float rotaionSpeed;

    [SerializeField] // gotta change the names for these two rotate things, not intuitive at all
    private GameObject rotationGoal;
    [SerializeField]
    private GameObject rotationHolder;

    private Vector3 startPosition;
    private Vector3 endPosition;

    private Transform startAngle;
    private Transform endAngle;

    private float startTime;
    private float endTime;

    AnimationCurve animCurve;

    public void MoveForward()
    {
        SetStartValues();
        endPosition = startPosition + transform.forward * moveDistance;
    }

    public void MoveBack()
    {
        SetStartValues();
        endPosition += -transform.forward * moveDistance;
    }

    public void MoveLeft()
    {
        SetStartValues();
        endPosition += -transform.right * moveDistance;
    }

    public void MoveRight() 
    {
        SetStartValues();
        endPosition += transform.right * moveDistance;
    }

    public void TurnLeft()
    {
        SetStartValues();
        rotationHolder.transform.Rotate(Vector3.up * -rotationDegrees);
    }

    public void TurnRight() 
    {
        SetStartValues();
        rotationHolder.transform.Rotate(Vector3.up * rotationDegrees);
        //transform.Rotate(Vector3.up * rotationDegrees);
    }

    private void SetStartValues()
    {
        startPosition = transform.position;
        startTime = Time.time;
    }

    //perhaps this should become a coroutine idk
    public void Update()
    {
        if (startPosition != endPosition)
        {
            transform.position = Vector3.Lerp(startPosition, endPosition, (Time.time - startTime) / moveSpeed);
        }

        if (rotationHolder.transform.rotation != transform.rotation)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, rotationGoal.transform.rotation, (Time.time - startTime / rotaionSpeed));
        }
    }
}
