using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    public PlatformDirection Direction;

    [Header("Movement properties")]
    [Range(1.0f, 20.0f)]
    public float HorizontalDistance = 8.0f;
    [Range(1.0f, 20.0f)]
    public float HorizontalSpeed = 3.0f;
    [Range(1.0f, 20.0f)]
    public float VerticalDistance = 8.0f;
    [Range(1.0f, 20.0f)]
    public float VerticalSpeed = 3.0f;

    private Vector2 StartPoint;

    // Start is called before the first frame update
    void Start()
    {
        StartPoint = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }


    public void Move()
    {
        switch (Direction)
        {
            case PlatformDirection.HORIZONTAL:
                transform.position = new Vector2(Mathf.PingPong(HorizontalSpeed * Time.time, HorizontalDistance) + StartPoint.x, StartPoint.y);
                break;
            case PlatformDirection.VERTICAL:
                transform.position = new Vector2(StartPoint.x, Mathf.PingPong(VerticalSpeed * Time.time, VerticalDistance) + StartPoint.y);
                break;
            case PlatformDirection.DIAGONAL_UP:
                transform.position = new Vector2(Mathf.PingPong(HorizontalSpeed * Time.time, HorizontalDistance) + StartPoint.x,
                                                 Mathf.PingPong(VerticalSpeed * Time.time, VerticalDistance) + StartPoint.y);
                break;
            case PlatformDirection.DIAGONAL_DOWN:
                transform.position = new Vector2(Mathf.PingPong(HorizontalSpeed * Time.time, HorizontalDistance) + StartPoint.x,
                                                 StartPoint.y - Mathf.PingPong(VerticalSpeed * Time.time, VerticalDistance));
                break;
            default:
                break;
        }
    }
}
