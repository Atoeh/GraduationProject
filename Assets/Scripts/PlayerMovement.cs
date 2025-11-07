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
    private int rotaionDegree = 90;

    void Start()
    {

    }

    public void MoveForward()
    {
        transform.position += transform.forward * moveDistance;
    }

    public void MoveBack()
    {
        transform.position += -transform.forward * moveDistance;
    }

    public void MoveLeft()
    {
        transform.position += -transform.right * moveDistance;
    }

    public void MoveRight() 
    {
        transform.position += transform.right * moveDistance;
    }

    public void TurnLeft()
    {
        transform.Rotate(Vector3.down * rotaionDegree);
    }

    public void TurnRight() 
    {
        transform.Rotate(Vector3.up * rotaionDegree);
    }
}
