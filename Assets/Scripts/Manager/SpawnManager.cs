using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Nectunia.Utility;


public delegate void SpawnUpdateHandler(int spawn);

public class SpawnManager : MonoBehaviour{

	[Range(1f, 300f)]
	public int          _spawnTimeSeconde = 5;
	[Range(1f, 300f)]
	public int          _firstSpawnTimeSeconde = 5;
	public List<GameObject> _trainPrefabs;

	private List<Rail>  _spawnList;
	private CountDown   _countDownSpawn;
	private CountDown   _countDownFirstSpawn;
	private bool        _isFirstSpawned = false;
	private bool        _haveToStartFirstSpawn = false;
	private SpawnerPathFollower  _nextSpawn;
	private List<GameObject> spawnedTrains = new List<GameObject>();

	public event SpawnUpdateHandler OnSpawnUpdate;

	public int spawn = 0;



	// Init CountDown
	void OnEnable () {
		this._countDownSpawn = new CountDown(this._spawnTimeSeconde);
		this._countDownFirstSpawn = new CountDown(this._firstSpawnTimeSeconde);		
	}

	private void Start() {

		var gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
		if (gameManager !=null)
		{
			gameManager.hookspawnmanager (this);
		}

	}
	// Endless spawn
	void Update() {
		// Start the first spawn countdown if the spawnlist have been set
		if (this._haveToStartFirstSpawn) { 
			this._countDownFirstSpawn.start();
			this._haveToStartFirstSpawn = false;
		}

		// Manage Spawn
        if(this._trainPrefabs.Count > 0 && this._spawnList.Count > 0) {
			if (this._nextSpawn == null) { this.SetNextSpawn(); }

			if (this._countDownFirstSpawn.IsUp) {
				// First spawn
				if (!this._isFirstSpawned) {
					this._isFirstSpawned = true;
					this.Spawn();
				}

				// Endless Spawn
				if (this._countDownSpawn.IsUp) { this.Spawn(); }
			}
		}
    }

	private void SetNextSpawn () {		 
		Rail nextSpawnRail = this._spawnList[Random.Range(0, this._spawnList.Count)];
		this._nextSpawn = nextSpawnRail.gameObject.GetComponent<SpawnerPathFollower>();
		// Send the countdown to the next Spawner
		if(this._nextSpawn != null) {
			if (this._isFirstSpawned) { this._nextSpawn.NextSpawnTime = this._countDownSpawn; }
			else{ this._nextSpawn.NextSpawnTime = this._countDownFirstSpawn; }
		} else { Debug.LogWarning("SpawnRail have no SpawnerPathFollower component" + nextSpawnRail.name); }		
	}

	private void Spawn () {
		GameObject currentPrefab = null;
		if(this._trainPrefabs.Count > 0) { currentPrefab = this._trainPrefabs[Random.Range(0, this._trainPrefabs.Count)];}
		var newTrain = this._nextSpawn.Spawn(currentPrefab);
		this._countDownSpawn.start();
		this.SetNextSpawn();
		AddTrainToList(newTrain);

	}

	public void InitSpawnList (List<Rail> spawnList) {
		this._spawnList = spawnList;
		this._haveToStartFirstSpawn = true;
	}

	private void AddTrainToList(GameObject train)
	{
		var pathFollowerTilled = train.GetComponent<PathFollower_Tilled>();
		pathFollowerTilled.KillMePleaseHandler += RemoveTrainToList;
		spawnedTrains.Add(pathFollowerTilled.gameObject);
		NotifySpawn();
	}
	
	private void RemoveTrainToList(GameObject train)
	{
		var pathFollowerTilled = train.GetComponent<PathFollower_Tilled>();
		pathFollowerTilled.KillMePleaseHandler -= RemoveTrainToList;
		var controller = train.GetComponent<TrainController>();
		spawnedTrains.Remove(train);
		NotifySpawn();
	}
	 
	private void NotifySpawn()
	{
		if (OnSpawnUpdate != null)
        {
            
			OnSpawnUpdate(spawnedTrains.Count);
            
        } 
	}
}