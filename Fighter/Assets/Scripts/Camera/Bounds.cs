using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bounds : MonoBehaviour
{
    Transform player;

    public Transform rightBound;
    public Transform leftBound;

    public bool border;
    public bool noBorder;
    //public bool locked;

    public float cameraMovementSpeed;
    //public float waitTime;

    //[Space(10)]
    //public float minX;
    //public float maxX;

    [Space(10)]
    public Vector3 minCameraBounds;
    public Vector3 maxCameraBounds;
    
    Vector3 currentCameraBounds;
    
    //public float minY;
    //public float maxY;
    private void Start()
    {
        player = GameObject.Find("Player").transform;

        currentCameraBounds = minCameraBounds;
    }

    [SerializeField] bool cameraShift = false;

    void LateUpdate()
    {
        if (cameraShift)
        {
            Transform playerPos = GameObject.Find("Player").transform;
            Vector3 nextCameraBounds = currentCameraBounds;

            float _rightBoundPosX = rightBound.position.x;
            float _leftBoundPosX = leftBound.position.x;

            if (playerPos.position.x > _rightBoundPosX)
            {
                float _nextCameraBoundX = currentCameraBounds.x + 35f; //if it works, dont touch it!
                float _nextCameraBoundY = minCameraBounds.y;

                currentCameraBounds = new Vector3(_nextCameraBoundX, _nextCameraBoundY, -10f);

                nextCameraBounds = new Vector3(
                    Mathf.Clamp(currentCameraBounds.x, minCameraBounds.x, maxCameraBounds.x)
                    , Mathf.Clamp(currentCameraBounds.y, minCameraBounds.y, maxCameraBounds.y)
                    , Mathf.Clamp(currentCameraBounds.z, -10f, -10f));
            }
            if (playerPos.position.x < _leftBoundPosX)
            {
                float _prevCameraBoundX = currentCameraBounds.x - 35f; //if it works, dont touch it!
                float _prevCameraBoundY = minCameraBounds.y;

                currentCameraBounds = new Vector3(_prevCameraBoundX, _prevCameraBoundY, -10f);

                nextCameraBounds = new Vector3(
                    Mathf.Clamp(currentCameraBounds.x, minCameraBounds.x, maxCameraBounds.x)
                    , Mathf.Clamp(currentCameraBounds.y, minCameraBounds.y, maxCameraBounds.y)
                    , Mathf.Clamp(currentCameraBounds.z, -10f, -10f));
            }

            transform.position = Vector3.Lerp(transform.position, nextCameraBounds, cameraMovementSpeed * Time.deltaTime);
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
                transform.position = new Vector3(player.transform.position.x, -0.3f, -10f);
            }
        }
    }
}
