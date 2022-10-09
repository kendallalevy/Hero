using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Spawner : MonoBehaviour
{
    public GameObject planeSpawn;
    public GameObject eggSpawn;
    public GameObject BigEggSpawn;
    public TextMeshProUGUI eggData;
    public TextMeshProUGUI enemyData;
    public Camera cam;
    public int maxPlanes;
    private int curPlanes;
    private int totDestroyed;
    private float fireRate;
    private float lastShot;
    private float bigFireRate;
    private float lastBigShot;
    private int eggOnScreen;

    void Start()
    {
        // spawn all initial planes
        for (int i = 0; i < maxPlanes; i++) {
            instantiatePlane();
            curPlanes++;
        }

        // fire rate for egg
        fireRate = 0.2f;
        bigFireRate = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        // spawn new plane if not 10
        if (curPlanes < maxPlanes)
        {
            instantiatePlane();
            curPlanes++;
        }

        // spawn egg
        if (Input.GetKey(KeyCode.Space) && Time.time - lastShot >= fireRate)
        {
            instantiateEgg(eggSpawn);
            lastShot = Time.time;
        }

        if (Input.GetKeyDown("f") && Time.time - lastBigShot >= bigFireRate)
        {
            instantiateEgg(BigEggSpawn);
            lastBigShot = Time.time;
        }

        // quit
        if (Input.GetKeyDown("q"))
        {
            Application.Quit();
        }

        // update text
        eggData.text = "EGG: OnScreen(" + eggOnScreen.ToString() + ")";
        enemyData.text = "ENEMY: Count(" + curPlanes.ToString() + ") Destroyed(" + totDestroyed.ToString() + ")";
    }

    private void instantiatePlane()
    {
        // generate coordinates
        float maxY = cam.orthographicSize;
        float maxX = cam.orthographicSize * cam.aspect;

        float limitX = maxX * 0.1f;
        float limitY = maxY * 0.1f;
        maxX = maxX - limitX;
        float minX = -1f * maxX;
        maxY = maxY - limitY;
        float minY = -1f * maxY;

        Vector3 spawnPos = new Vector3(Random.Range(minX, maxX), Random.Range(minY, maxY), 0f);

        // spawn
        Instantiate(planeSpawn, spawnPos, Quaternion.identity);
    }

    private void instantiateEgg(GameObject spawnType)
    {
        GameObject arrow = GameObject.FindGameObjectWithTag("Player");
        Vector3 arrowPos = arrow.transform.position;
        Quaternion arrowRotate = arrow.transform.rotation;
        Instantiate(spawnType, arrowPos, arrowRotate);
        eggOnScreen++;
    }

    public void decrementPlane() {
        curPlanes--;
        totDestroyed++;
    }
    
    public void decrementEgg() { eggOnScreen--; }
}
