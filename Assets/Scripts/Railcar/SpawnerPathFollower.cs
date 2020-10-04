using Doozy.Engine.Extensions;
using Nectunia.Utility;
using PathCreation;
using UnityEngine;

public class SpawnerPathFollower : MonoBehaviour {

    public GameObject   _trainPrefab;
	[Range(0.1f, 5f)]
	public float        _trainSpeed = 0.5f;
    public PathCreator  _pathCreator;
	[Range(1f, 300f)]
    public int          _spawnTimeSeconde = 20;
	[Range(1f, 300f)]
    public int          _firstSpawnTimeSeconde = 5;

	private CountDown	_countDownSpawn;
	private CountDown	_countDownFirstSpawn;
	private bool		_isFirstSpaned = false;
	private Vector3		_startPosition;

	private void OnEnable () {	
		// Check if the train prefab have a PathFollowerComponent
		PathFollower_Tilled followerComponent = this._trainPrefab.GetComponent<PathFollower_Tilled>();
		if(followerComponent == null) { _trainPrefab = null; }

		this._countDownSpawn = new CountDown(this._spawnTimeSeconde);
		this._countDownFirstSpawn = new CountDown(this._firstSpawnTimeSeconde);
		this._countDownFirstSpawn.start();

		if (this._pathCreator != null) { this._startPosition = this._pathCreator.path.GetPointAtDistance(0f, EndOfPathInstruction.Stop); }
	}

	// Update is called once per frame
	void Update() {
        if(this._trainPrefab != null && this._pathCreator && this._countDownFirstSpawn.IsUp) {
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
    }

	private void spawn () {
		GameObject newTrain = Instantiate(this._trainPrefab, this._startPosition, Quaternion.identity);
		PathFollower_Tilled followerComponent = newTrain.GetComponent<PathFollower_Tilled>();
		followerComponent._currentPath._pathCreator = this._pathCreator;
		followerComponent._currentPath._pathWay = PathTile.PathWay.Start;
		followerComponent._speed = this._trainSpeed;
		newTrain.SetActive(true);
	}
}
