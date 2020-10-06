using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
	private Coroutine   _spawnCoroutine;
	private bool        _isFirstSpawned = false;
	private bool        _haveToStartFirstSpawn = false;
	private List<GameObject> spawnedTrains = new List<GameObject>();

	public event SpawnUpdateHandler OnSpawnUpdate;
	public event DeathUpdateHandler OnDeathUpdate;

	public int spawn = 0;
	

	private void Start() {

		var gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
		if (gameManager !=null)
		{
			gameManager.hookspawnmanager (this);
		}

	}

	private IEnumerator SpawnCoroutine()
	{
		while (true)
		{
			yield return new WaitForSeconds(_spawnTimeSeconde);
			if (_trainPrefabs.Count > 0 && _spawnList.Count >0)
			{
				var randomTrain = _trainPrefabs[Random.Range(0, _trainPrefabs.Count - 1)];
				var randomSpawner = _spawnList[Random.Range(0, _spawnList.Count - 1)];
				var newTrain = randomSpawner.GetComponent<SpawnerPathFollower>().Spawn(randomTrain); 
				AddTrainToList(newTrain);
			}			
		}
		
	}
	
	public void InitSpawnList (List<Rail> spawnList) {
		this._spawnList = spawnList;
		this._haveToStartFirstSpawn = true;
		StartSpawn();
	}

	public void StartSpawn()
	{
		if (_spawnCoroutine == null)
		{
			_spawnCoroutine = StartCoroutine(SpawnCoroutine());
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

		if (_spawnCoroutine != null)
		{
			StopCoroutine(_spawnCoroutine);
			_spawnCoroutine = null;
		}
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