using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaneManager : MonoBehaviour
{
    public GameObject[] lanePrefabs;
    private Transform playerTransform;
    private float spawnZ = -4.0f;
    private float laneLength = 13.0f;
    private float safeZone = 18.0f;
    private int amnLanesOnScreen = 7;

    private int lastPrefabIndex = 0;

    private bool laneSpawned = false;

    private static List<GameObject> activeLanes;
    // Start is called before the first frame update
    void Start()
    {
        activeLanes = new List<GameObject>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        
        for(int i = 0; i < amnLanesOnScreen; i++)
        {
            if(i < 2)
            {
                SpawnLane(0);
            }
            else
            {
                SpawnLane();
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(playerTransform.position.z - safeZone> (spawnZ - amnLanesOnScreen * laneLength))
        {
            SpawnLane();
            DeleteLane();
        }
        BluetoothTest.SendValue(activeLanes[1].ToString().Substring(0, 1));
        //ArduinoConnect.SendValue(activeLanes[1].ToString().Substring(0, 1));
        Debug.Log(activeLanes[1].ToString().Substring(0, 1));
    }

    public void SpawnLane(int prefabIndex = -1)
    {
        GameObject go;
        if(prefabIndex == -1)
        {
            int randomLanes = RandomPrefabIndex();
            go = Instantiate(lanePrefabs[randomLanes]) as GameObject;
            //ArduinoConnect.SendValue(randomLanes.ToString());
        }
        else
        {
            go = Instantiate(lanePrefabs[prefabIndex]) as GameObject;
        }
        
        go.transform.SetParent(transform);
        go.transform.position = Vector3.forward * spawnZ;
        spawnZ += laneLength;
        activeLanes.Add(go);
        laneSpawned = true;
    }

    public bool laneSpawnedTrue()
    {
        return laneSpawned;
    }

    private void DeleteLane()
    {
        Destroy(activeLanes[0]);
        activeLanes.RemoveAt(0);
    }

    public int RandomPrefabIndex()
    {
        if(lanePrefabs.Length <= 1)
        {
            return 0;
        }

        int randomIndex = lastPrefabIndex;
        while(randomIndex == lastPrefabIndex)
        {
            randomIndex = Random.Range(0, lanePrefabs.Length);
        }

        lastPrefabIndex = randomIndex;
        return randomIndex;
    }
}
