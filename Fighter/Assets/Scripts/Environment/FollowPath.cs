using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPath : MonoBehaviour
{
    public enum FollowType
    {
        MoveTowards,
        Lerp
    }

    public FollowType Type = FollowType.MoveTowards;
    public PathFinder pathFinder;

    public float speed = 1;
    public float maxDistanceToPoint = .1f;

    private IEnumerator<Transform> CurrentPointEnumerator;

    public void Start()
    {
        if (pathFinder == null)
        {
            Debug.LogError("Platform is null ", gameObject);
            return;
        }

        CurrentPointEnumerator = pathFinder.GetPathEnumerator();
        CurrentPointEnumerator.MoveNext();

        if (CurrentPointEnumerator.Current == null)
            return;

        transform.position = CurrentPointEnumerator.Current.position;
    }

    public void Update()
    {
        if (CurrentPointEnumerator == null || CurrentPointEnumerator.Current == null)
            return;

        if (Type == FollowType.MoveTowards)
        {
            transform.position = Vector3.MoveTowards(transform.position, CurrentPointEnumerator.Current.position, speed * Time.deltaTime);
        }
        else if (Type == FollowType.Lerp)
        {
            transform.position = Vector3.Lerp(transform.position, CurrentPointEnumerator.Current.position, speed * Time.deltaTime);
        }

        var distanceSquared = (transform.position - CurrentPointEnumerator.Current.position).sqrMagnitude;

        if(distanceSquared < Mathf.Pow(maxDistanceToPoint, 2))
        {
            CurrentPointEnumerator.MoveNext();
        }
    }
}
