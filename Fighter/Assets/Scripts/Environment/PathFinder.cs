using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinder : MonoBehaviour
{
    public Transform[] points;

    public IEnumerator<Transform> GetPathEnumerator()
    {
        //Checking if there are points or atleast a point to follow a path
        if (points == null || points.Length < 1)
            yield break;

        var direction = 1;
        var index = 0;

        while (true)
        {
            yield return points[index];

            if (points.Length == 1)
                continue;

            if (index <= 0)
                direction = 1;
            else if (index >= points.Length - 1)
                direction = -1;

            index += direction;
        }
    }

    public void OnDrawGizmos()
    {
        //checking if there are more that to points to draw a line
        if (points == null || points.Length < 2)
            return;

        for (int i = 1; i < points.Length; i++)
        {
            Gizmos.DrawLine(points[i - 1].position, points[i].position);
        }
    }
}
