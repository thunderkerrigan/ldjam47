using Doozy.Engine.Extensions;
using Nectunia.Utility;
using PathCreation;
using UnityEngine;

public class SpawnerPathFollower : MonoBehaviour {

    public GameObject   _defaultTrainPrefab;
	[Range(0.1f, 5f)]
	public float        _defaultTrainSpeed = 0.5f;
    public PathCreator  _pathCreator;
	private Vector3		_startPosition;

	/*[Range(1f, 300f)]
    public int          _spawnTimeSeconde = 20;
	[Range(1f, 300f)]
    public int          _firstSpawnTimeSeconde = 5;

	private CountDown	_countDownSpawn;
	private CountDown	_countDownFirstSpawn;
	private bool		_isFirstSpaned = false;*/

	private void OnEnable () {	
		// Check if the train prefab have a PathFollowerComponent
		PathFollower_Tilled followerComponent = this._defaultTrainPrefab.GetComponent<PathFollower_Tilled>();
		if(followerComponent == null) { _defaultTrainPrefab = null; }
		/*
		this._countDownSpawn = new CountDown(this._spawnTimeSeconde);
		this._countDownFirstSpawn = new CountDown(this._firstSpawnTimeSeconde);
		this._countDownFirstSpawn.start();*/

		if (this._pathCreator != null) { this._startPosition = this._pathCreator.path.GetPointAtDistance(0f, EndOfPathInstruction.Stop); }
	}

	// Endless spawn
	/*void Update() {
        if(this._defaultTrainPrefab != null && this._pathCreator && this._countDownFirstSpawn.IsUp) {
			// First spawn
			if (!this._isFirstSpaned) {
				this._isFirstSpaned = true;
				this._countDownSpawn.start();
				this.spawn();
			}

			// Endless Spawn
			if (this._countDownSpawn.IsUp) {
				this.spawn();
				this._countDownSpawn.start();
			}
		}
    }*/

	public void spawn () {
		this.spawn(this._defaultTrainPrefab, this._defaultTrainSpeed);
	}
	
	public void spawn (GameObject trainPrefab) {
		this.spawn(trainPrefab, this._defaultTrainSpeed);
	}

	public void spawn (GameObject trainPrefab, float trainSpeed) {
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
		}
	}
}
