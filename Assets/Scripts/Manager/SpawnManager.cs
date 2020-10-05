﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Nectunia.Utility;
using Doozy.Engine;


public delegate void SpawnUpdateHandler(int spawn);
public delegate void DeathUpdateHandler(GameObject train);

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
	public event DeathUpdateHandler OnDeathUpdate;

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
				if (_countDownSpawn != null && this._countDownSpawn.IsUp) { this.Spawn(); }
			}
		}
    }

	private void SetNextSpawn () {		 
		Rail nextSpawnRail = this._spawnList[Random.Range(0, this._spawnList.Count)];
		if(nextSpawnRail != null) {
			this._nextSpawn = nextSpawnRail.gameObject.GetComponent<SpawnerPathFollower>();
		// Send the countdown to the next Spawner
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
		StartSpawn();
	}

	public void StartSpawn()
	{
		if (_countDownSpawn == null)
		{
			_countDownSpawn = new CountDown(_spawnTimeSeconde);
			_countDownSpawn.start();

		}
	}

	public void StopSpawnList()
	{
		foreach (var _train in spawnedTrains)
		{
			var _pathFollowerTilled = _train.GetComponent<PathFollower_Tilled>();
			_pathFollowerTilled.KillMePleaseHandler -= RemoveTrainToList;
			_train.GetComponent<TrainController>().Unsub();
			Destroy(_train);
		}
		spawnedTrains = new List<GameObject>();

		_spawnList = new List<Rail>();

		_nextSpawn = null;
		_countDownSpawn = null;
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
		StartCoroutine(ShowGameOverMenu());
		if (OnDeathUpdate != null)
		{
			OnDeathUpdate(train);
		}
	}

	private IEnumerator ShowGameOverMenu()
	{
		yield return new WaitForSeconds(3);
		GameEventMessage.SendEvent("Dead");
	}
	 
	private void NotifySpawn()
	{
		if (OnSpawnUpdate != null)
        {
            
			OnSpawnUpdate(spawnedTrains.Count);
			
        } 
	}
}