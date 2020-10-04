using UnityEngine;
using PathCreation;

[RequireComponent(typeof(Collider))]
public class PathFollower_Tilled : MonoBehaviour{

	public PathTile  _currentPath;
    public float _speed = 5;

    private EndOfPathInstruction _endOfPathInstruction = EndOfPathInstruction.Stop;
    private float _distanceTravelled = 0f;
    private float _absolutDistanceTravelled = 0f;
	private PathTile _nextPath;

    void Update() { 
        if (_currentPath != null){
            _absolutDistanceTravelled += _speed * Time.deltaTime;

            if (_absolutDistanceTravelled > _currentPath._pathCreator.path.length && _nextPath != null) {
                _absolutDistanceTravelled = _speed * Time.deltaTime;
                this._currentPath = this._nextPath;
                this._nextPath = null;
            }

            if(_absolutDistanceTravelled <= _currentPath._pathCreator.path.length) {
                // Get the distance travelled depend of the the way
                if(_currentPath._pathWay == PathTile.PathWay.Start) {  _distanceTravelled = _absolutDistanceTravelled; } 
                else{ _distanceTravelled = _currentPath._pathCreator.path.length - _absolutDistanceTravelled; }

                // Move the follower on the path
                transform.position = _currentPath._pathCreator.path.GetPointAtDistance(_distanceTravelled, _endOfPathInstruction);
                Quaternion rotation = _currentPath._pathCreator.path.GetRotationAtDistance(_distanceTravelled, _endOfPathInstruction);
                transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime);
                
                // Rotate the follower depend of the way
                if(_currentPath._pathWay != PathTile.PathWay.Start) {  transform.forward = -transform.forward; } 
            }
        }
    }

	private void OnTriggerEnter (Collider other) {
		PathEndTrigger pathEndTrigger = other.gameObject.GetComponent<PathEndTrigger>();
        Debug.Log("Trigger " + other.name + " : " + pathEndTrigger.paths[0]._pathWay.ToString() );
		
        if (pathEndTrigger != null && pathEndTrigger.paths.Count > 0) {
            bool currentPathExistsInTrigger = false;
            foreach (PathTile currentPath in pathEndTrigger.paths) {
                if (currentPath._pathCreator == this._currentPath._pathCreator) { currentPathExistsInTrigger = true; }
            }
            if (!currentPathExistsInTrigger) {
                int randomPath = Random.Range(0, pathEndTrigger.paths.Count);
                this._nextPath = pathEndTrigger.paths[randomPath];
            }
		}
	}
}
