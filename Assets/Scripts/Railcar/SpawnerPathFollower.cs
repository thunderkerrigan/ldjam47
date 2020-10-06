using Doozy.Engine.Extensions;
using PathCreation;
using UnityEngine;

public class SpawnerPathFollower : MonoBehaviour {

    public GameObject   _defaultTrainPrefab;
	[Range(0.1f, 5f)]
	public float        _defaultTrainSpeed = 0.5f;
    public PathCreator  _pathCreator;
	private Vector3		_startPosition;


	private void OnEnable () {	
		// Check if the train prefab have a PathFollowerComponent
		PathFollower_Tilled followerComponent = this._defaultTrainPrefab.GetComponent<PathFollower_Tilled>();
		if(followerComponent == null) { _defaultTrainPrefab = null; }		

		if (this._pathCreator != null) { this._startPosition = this._pathCreator.path.GetPointAtDistance(0f, EndOfPathInstruction.Stop); }
	}

	public void Spawn () {
		this.Spawn(this._defaultTrainPrefab, this._defaultTrainSpeed);
	}
	
	public GameObject Spawn (GameObject trainPrefab) {
		return this.Spawn(trainPrefab, this._defaultTrainSpeed);
	}

	public GameObject Spawn (GameObject trainPrefab, float trainSpeed) {
		// Check the if the Prefab
		PathFollower_Tilled followerComponent = trainPrefab?.GetComponent<PathFollower_Tilled>();
		if(followerComponent == null) { trainPrefab = _defaultTrainPrefab; }

		if(trainPrefab != null) {
			GameObject newTrain = Instantiate(trainPrefab, this._startPosition, Quaternion.identity);
			followerComponent = newTrain.GetComponent<PathFollower_Tilled>();
			followerComponent._currentPath._pathCreator = this._pathCreator;
			followerComponent._currentPath._pathWay = PathTile.PathWay.Start;
			followerComponent._speed = trainSpeed;
			newTrain.SetActive(true);

			return newTrain;
		}

		return null;
	}
}
