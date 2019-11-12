using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bounds : MonoBehaviour
{
    Transform player;

    public bool border;
    public bool noBorder;
    public bool cameraShift;
    public bool locked;

    public float cameraMovementSpeed;
    public float waitTime;

    //[Space(10)]
    //public float minX;
    //public float maxX;

    [Space(10)]
    public Vector3 minCameraBounds;
    public Vector3 maxCameraBounds;

    Vector3 currentCameraBounds;
    Vector3 prevCameraBounds;

    //public float minY;
    //public float maxY;
    private void Start()
    {
        player = GameObject.Find("Player").transform;

        currentCameraBounds = minCameraBounds;
        prevCameraBounds = minCameraBounds;
    }

    void FixedUpdate()
    {
        if (cameraShift)
        {
            StartCoroutine(CameraShifting(waitTime));
        }

        if (border)
        {
            transform.position = player.transform.position;

            transform.position = new Vector3(
                Mathf.Clamp(transform.position.x, minCameraBounds.x, maxCameraBounds.x)
                , Mathf.Clamp(transform.position.y, minCameraBounds.y, maxCameraBounds.y)
                , Mathf.Clamp(transform.position.z, -10f, -10f));
        }
        if (noBorder)
        {
            if (player.transform.position.x > minCameraBounds.x && player.transform.position.x < maxCameraBounds.x)
            {
                transform.position = new Vector3(player.transform.position.x, -1f, -10f);
            }
        }
    }

    IEnumerator CameraShifting(float waitTime)
    {
        Vector3 playerPos = player.transform.position;
        var screenSize = (Screen.width / Screen.height) * 2;

        prevCameraBounds.x = currentCameraBounds.x;

        if (playerPos.x > screenSize)
        {
            currentCameraBounds.x = currentCameraBounds.x + Mathf.Abs(minCameraBounds.x);

            transform.position = Vector3.Lerp(transform.position, currentCameraBounds, Time.deltaTime * cameraMovementSpeed);
            Mathf.Clamp(transform.position.x, minCameraBounds.x, maxCameraBounds.x);

            yield return new WaitForSeconds(waitTime);
        }
        if (playerPos.x < screenSize - 2)
        {
            transform.position = Vector3.Lerp(transform.position, prevCameraBounds, Time.deltaTime * cameraMovementSpeed);
            Mathf.Clamp(transform.position.x, minCameraBounds.x, maxCameraBounds.x);

            yield return new WaitForSeconds(waitTime);
        }
    }
}
