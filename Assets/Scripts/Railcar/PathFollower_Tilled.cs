using UnityEngine;
using PathCreation;

[RequireComponent(typeof(Collider))]
public class PathFollower_Tilled : MonoBehaviour{

	public PathCreator  _currentPath;
    public PathEndTrigger.PathWay _currentPathWay = PathEndTrigger.PathWay.Start;
    public float        _speed = 5;

    private EndOfPathInstruction _endOfPathInstruction = EndOfPathInstruction.Stop;
	private PathCreator _nextPath;
    private float _distanceTravelled = 0f;
    private float _absolutDistanceTravelled = 0f;
    private PathEndTrigger.PathWay _nextPathWay = PathEndTrigger.PathWay.Start;

    void Start() {
        // Subscribed to the pathUpdated event so that we're notified if the path changes during the game
        if (_currentPath != null) { _currentPath.pathUpdated += OnPathChanged; }
    }

    void Update() { 
        if (_currentPath != null){
            _absolutDistanceTravelled += _speed * Time.deltaTime;

            if (_absolutDistanceTravelled > _currentPath.path.length && _nextPath != null) {
                _absolutDistanceTravelled = _speed * Time.deltaTime;
                this._currentPath = this._nextPath;
                this._nextPath = null;
                this._currentPathWay = this._nextPathWay;
            }

            if(_absolutDistanceTravelled <= _currentPath.path.length) {
                // Get the distance travelled depend of the the way
                if(_currentPathWay == PathEndTrigger.PathWay.Start) {  _distanceTravelled = _absolutDistanceTravelled; } 
                else{ _distanceTravelled = _currentPath.path.length - _absolutDistanceTravelled; }

                // Move the follower on the path
                transform.position = _currentPath.path.GetPointAtDistance(_distanceTravelled, _endOfPathInstruction);
                transform.rotation = _currentPath.path.GetRotationAtDistance(_distanceTravelled, _endOfPathInstruction);
                
                // Rotate the follower depend of the way
                if(_currentPathWay != PathEndTrigger.PathWay.Start) {  transform.forward = -transform.forward; } 
            }
        }
    }

    ///<summary>
    /// If the path changes during the game, update the distance travelled so that the follower's position on the new path
    /// is as close as possible to its position on the old path
    /// </summary> 
    void OnPathChanged() {
        _distanceTravelled = _currentPath.path.GetClosestDistanceAlongPath(transform.position);
    }

	private void OnTriggerEnter (Collider other) {
		PathEndTrigger pathEndTrigger = other.gameObject.GetComponent<PathEndTrigger>();
        Debug.Log("Trigger " + other.gameObject.name);
		if (pathEndTrigger != null && pathEndTrigger._path != this._currentPath) {
			this._nextPath = pathEndTrigger._path;
            this._nextPathWay = pathEndTrigger._pathWay;
		}
	}
}
