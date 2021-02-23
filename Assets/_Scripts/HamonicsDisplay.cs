using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HamonicsDisplay : MonoBehaviour
{
    /*
     * ATTACH TO GAMEOBJECT WITH LINERENDERER AND ASSIGN LINERENDERER TO SCRIPT
     * TO ENABLE HIT MIDDLE MOUSE BUTTON
     * USE LEFT AND RIGHT MOUSE BUTTONS TO DISPLAY INPUT
     * 
    */

    public int numberOfSides;
    public float polygonRadius;
    public Vector2 polygonCenter;
    public LineRenderer lr;

    private float detectRadius = 1.01f;

    private Vector2 mosPos;

    private bool harmonicsMode = false;

    void Start() 
    {
        if (!lr) 
        {
            if (gameObject.GetComponent<LineRenderer>() == null) 
            {
                gameObject.AddComponent<LineRenderer>();                
            }
            lr = gameObject.GetComponent<LineRenderer>();
            lr.loop = true;
        }
    }

    void Update()
    {
        mosPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        lr.positionCount = numberOfSides;

        DrawPolygon(polygonCenter, polygonRadius, numberOfSides);

        if (Input.GetMouseButtonDown(2))
        {
            if (!harmonicsMode)
            {
                harmonicsMode = true;
            }
            else
            {
                harmonicsMode = false;
            }
        }

        if (harmonicsMode)
        {
            gameObject.GetComponent<LineRenderer>().enabled = true;
            if (Input.GetMouseButton(0) || Input.GetMouseButton(1))
            {
                DetectNoteInput();
            }
        }
        else 
        {
            gameObject.GetComponent<LineRenderer>().enabled = false;
        }
    }

    // Draw a polygon in the XY plane with a specfied position, number of sides
    // and radius.
    void DrawPolygon(Vector2 center, float radius, int numSides)
    {
        // The corner that is used to start the polygon (parallel to the X axis).
        //Vector2 startCorner = (new Vector2(radius, 0) + center);
        Vector2 startCorner = transform.localToWorldMatrix * (new Vector2(radius, 0) + center);

        var points = new Vector3[numSides];
        points[0] = startCorner;
        
        // For each corner after the starting corner...
        for (int i = 1; i < numSides; i++)
        {
            // Calculate the angle of the corner in radians.
            float cornerAngle = 2f * Mathf.PI / (float)numSides * i;

            // Get the X and Y coordinates of the corner point.
            //Vector2 currentCorner = (new Vector2(Mathf.Cos(cornerAngle) * radius, Mathf.Sin(cornerAngle) * radius) + center);
            Vector2 currentCorner = transform.localToWorldMatrix * (new Vector2(Mathf.Cos(cornerAngle) * radius, Mathf.Sin(cornerAngle) * radius) + center);

            points[i] = currentCorner;
        }

        lr.SetPositions(points);
    }

    /// <summary>
    /// Detect input for each harmonic node
    /// </summary>
    void DetectNoteInput() 
    {
        for (int i = 0; i < lr.positionCount - 1; i++)
        {
            if ((Vector2.Distance(lr.GetPosition(i), mosPos) + Vector2.Distance(lr.GetPosition(i + 1), mosPos))
                 <= Vector2.Distance(lr.GetPosition(i), lr.GetPosition(i + 1)) * detectRadius)
            {
                Debug.Log("We did hit Line: " + (i + 1));
            }
        }
        if ((Vector2.Distance(lr.GetPosition(0), mosPos) + Vector2.Distance(lr.GetPosition(lr.positionCount - 1), mosPos))
                <= Vector2.Distance(lr.GetPosition(0), lr.GetPosition(lr.positionCount - 1)) * detectRadius)
        {
            Debug.Log("We did Line: " + (lr.positionCount));
        }
    }
}
