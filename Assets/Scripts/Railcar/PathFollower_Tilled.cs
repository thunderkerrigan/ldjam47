using UnityEngine;
using PathCreation;
public delegate void KillMePlease(GameObject train);

[RequireComponent(typeof(Collider))]
public class PathFollower_Tilled : MonoBehaviour{

	public PathTile  _currentPath;
    private Rail _currentRail;
    private Rail _nextRail;
    public float _speed = 5;

    private EndOfPathInstruction _endOfPathInstruction = EndOfPathInstruction.Stop;
    private float _distanceTravelled = 0f;
    private float _absolutDistanceTravelled = 0f;
	private PathTile _nextPath;
    private bool stopMoving = false;
    public event KillMePlease KillMePleaseHandler;

    void Update() {
        if (stopMoving)
        {
            return;
        }
        if (_currentPath != null){
            _absolutDistanceTravelled += _speed * Time.deltaTime;

            if (_absolutDistanceTravelled > _currentPath._pathCreator.path.length ) {
                if (_nextPath != null)
                {
                    if (_currentRail)
                    {
                        _currentRail.RemoveTrain(gameObject);
                    }
                    _nextRail.AddTrain(gameObject);
                    _currentRail = _nextRail;
                    _nextRail = null;

                    _absolutDistanceTravelled = _speed * Time.deltaTime;
                    this._currentPath = this._nextPath;
                    this._nextPath = null;
                }
                else
                {
                    if (_currentRail)
                    {
                        _currentRail.RemoveTrain(gameObject);
                    }
                    if (KillMePleaseHandler != null)
                    {
                        stopMoving = true;
                        KillMePleaseHandler(this.gameObject);
                    }
                }
            }
 

            if(_absolutDistanceTravelled <= _currentPath._pathCreator.path.length) {
                // Get the distance travelled depend of the the way
                if(_currentPath._pathWay == PathTile.PathWay.Start) {  _distanceTravelled = _absolutDistanceTravelled; } 
                else{ _distanceTravelled = _currentPath._pathCreator.path.length - _absolutDistanceTravelled; }

                // Move the follower on the path
                transform.position = _currentPath._pathCreator.path.GetPointAtDistance(_distanceTravelled, _endOfPathInstruction);
                Quaternion rotation = _currentPath._pathCreator.path.GetRotationAtDistance(_distanceTravelled, _endOfPathInstruction);
                // Force the angle on X and Z axis to 0
                rotation.eulerAngles = new Vector3(0f, rotation.eulerAngles.y, 0f);
                transform.rotation = rotation;
                
                // Rotate the follower depend of the way
                if(_currentPath._pathWay != PathTile.PathWay.Start) {  transform.forward = -transform.forward; } 
            }
        }
    }

	private void OnTriggerEnter (Collider other) {
        PathEndTrigger pathEndTrigger = other.gameObject.GetComponent<PathEndTrigger>();
        //Debug.Log("Trigger " + other.name + " : " + pathEndTrigger.paths[0]._pathWay.ToString() );
        var otherTrain = other.gameObject.GetComponent<TrainController>();

        if (pathEndTrigger != null && pathEndTrigger.paths.Count > 0)
        {
            
            bool currentPathExistsInTrigger = false;
            foreach (PathTile currentPath in pathEndTrigger.paths) {
                if (currentPath._pathCreator == this._currentPath._pathCreator) { 
                    currentPathExistsInTrigger = true; 
                    break; 
                }
            }
            if (!currentPathExistsInTrigger) {
                int randomPath = Random.Range(0, pathEndTrigger.paths.Count);
                this._nextPath = pathEndTrigger.paths[randomPath];
                _nextRail = pathEndTrigger.GetComponentInParent<Rail>();
            }
            return;
        }

        if (otherTrain != null)
        {
            if (KillMePleaseHandler != null)
            {
             KillMePleaseHandler(this.gameObject);
             return;
            }          
        }
	}
}
