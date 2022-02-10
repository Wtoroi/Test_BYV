using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowLine : MonoBehaviour
{
    [SerializeField] private LineRenderer line;

    private Vector2 startPosition;
    private Vector2 newPosition;
    private bool onTouch = false;

    public delegate void ThrowAxe(Vector2 direction);
    public static event ThrowAxe throwAxe;

    void Start()
    {
        line.startWidth = 0.2f;
        line.endWidth = 0.02f;
        line.positionCount = 0;
        line.material = new Material(Shader.Find("Sprites/Default"));
        line.SetColors(Color.red, Color.blue);
        line.numCapVertices = 5;
    }

    void Update()
    {

        if (Input.touchCount <= 0)
        {
            if (onTouch)
            {
                Vector2 direction = new Vector2(newPosition.x - startPosition.x, newPosition.y - startPosition.y);
                float d = Mathf.Sqrt(direction.x * direction.x + direction.y * direction.y);
                direction = direction / d;
                throwAxe(direction);
            }

            onTouch = false;
            line.positionCount = 0;
            return;
        }

        if (GetWorldCoordinate(Input.GetTouch(0).position).x > 6.5f)
            return;

        if (!onTouch)
        {
            onTouch = true;
            startPosition = GetWorldCoordinate(Input.GetTouch(0).position);
        }
        line.positionCount = 0;

        line.positionCount++;
        line.SetPosition(line.positionCount - 1, startPosition);

        newPosition = GetWorldCoordinate(Input.GetTouch(0).position);
        line.positionCount++;
        line.SetPosition(line.positionCount - 1, newPosition);

    }

    private Vector2 GetWorldCoordinate(Vector3 position)
    {
        Vector2 point = new Vector3(position.x, position.y, 1);
        return Camera.main.ScreenToWorldPoint(point);
    }
}