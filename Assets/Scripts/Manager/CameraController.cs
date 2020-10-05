using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Camera currentCamera;
    private Vector3 initialPosition;
    public int cameraSpeed = 4; 

    private GameManager _manager;
    // Start is called before the first frame update
    void Start()
    {
        _manager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
        _manager.OnDeathUpdate += FollowDeathCam;
    }

    private void FollowDeathCam(GameObject target)
    {
        var position = target.transform.position;
        var truncatePosition = Vector3.Distance(transform.position, position) * 0.7;
        StartCoroutine(GoToNextPosition(position));
        
    }

    private IEnumerator GoToNextPosition(Vector3 finalPosition)
    {
        
        while (Vector3.Distance(transform.position, finalPosition) < 0.001f)
        {
            
            float step =  cameraSpeed * Time.deltaTime; // calculate distance to move
            transform.position = Vector3.MoveTowards(transform.position, finalPosition, step);
            yield return new WaitForFixedUpdate();
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
