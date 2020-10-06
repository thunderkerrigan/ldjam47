using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Camera currentCamera;
    private Vector3 initialPosition;
    private Quaternion initialRotation;
    public int cameraSpeed = 8; 

    private SpawnManager _manager;
    // Start is called before the first frame update
    void Start()
    {
        _manager = GameObject.FindGameObjectWithTag("GridManager").GetComponent<SpawnManager>();
        _manager.OnDeathUpdate += FollowDeathCam;
        initialPosition = gameObject.transform.position;
        initialRotation = gameObject.transform.rotation;
    }

    private void FollowDeathCam(GameObject target)
    {
        var position = target.transform.position;
        StartCoroutine(GoToNextPosition(position));
        
    }

    private IEnumerator GoToNextPosition(Vector3 finalPosition)
    {
        var reach = finalPosition != initialPosition ? 3f : 0f;
        while (Vector3.Distance(transform.position, finalPosition) > reach)
        {
            
            float step =  cameraSpeed * Time.deltaTime; // calculate distance to move
            transform.position = Vector3.MoveTowards(transform.position, finalPosition, step);
            yield return new WaitForFixedUpdate();
        }
        
        yield return new WaitForSeconds(2);
        if (finalPosition != initialPosition)
        {
            StartCoroutine(GoToNextPosition(initialPosition));
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
