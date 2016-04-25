using UnityEngine;
using System.Collections;

public class Spawn : MonoBehaviour {

    public GameObject enemy0Prefab;
    public GameObject enemy1Prefab;
    public GameObject enemy2Prefab;
    public GameObject missilePrefab; // missile
    public GameObject awardType0Prefab; // gun
    public GameObject awardType1Prefab; // bomb
    public GameObject awardType2Prefab; // clock
    public GameObject Container;

    public float enemy0Rate = 0.5f; // spawn one per second
    public float enemy1Rate = 5f;
    public float enemy2Rate = 8f;
    public float enemy3Rate = 0.1f;
    public float awardType0Rate = 15.6525f;
    public float awardType1Rate = 20.7898f;
    public float awardType2Rate = 30.4673f;
    //public float awardType0First = Random.Range(10.0f, 20.0f);
    //public float awardType1First = Random.Range(10.0f, 20.0f);
    //public float awardType2First = Random.Range(10.0f, 20.0f);
    private int min = 0;
    private int max = 100000;

	// Use this for initialization
	void Start () {
        InvokeRepeating("createEnemy0", 1, enemy0Rate);
        InvokeRepeating("createEnemy1", 1, enemy1Rate);
        InvokeRepeating("createEnemy2", 1, enemy2Rate);
        InvokeRepeating("createEnemy3", 1, enemy3Rate);
        //InvokeRepeating("createawardType0", awardType0First, awardType0Rate);
        //InvokeRepeating("createawardType1", awardType1First, awardType1Rate);
        //InvokeRepeating("createawardType2", awardType2First, awardType2Rate);
	}

	// Update is called once per frame
	void Update () {
	    int p = Random.Range(min, max);
        if (p <= 10) {
            createawardType2();
        } else if (p > 10 && p <= 100) {
            createawardType0();
        } else if (p > 100 && p <= 120) {
            createawardType1();
        } else {

        }
	}

    public void createEnemy0() {
        float x = Random.Range(-2.15f, 2.15f);
        GameObject o = GameObject.Instantiate(enemy0Prefab, new Vector3(x,transform.position.y,transform.position.z), Quaternion.identity) as GameObject;
        o.transform.parent = Container.transform;
    }

    public void createEnemy1() {
        float x = Random.Range(-2.06f, 2.06f);
        GameObject o = GameObject.Instantiate(enemy1Prefab, new Vector3(x, transform.position.y, transform.position.z), Quaternion.identity) as GameObject;
        o.transform.parent = Container.transform;
    }

    public void createEnemy2() {
        float x = Random.Range(-1.59f, 1.59f);
        GameObject o = GameObject.Instantiate(enemy2Prefab, new Vector3(x, transform.position.y, transform.position.z), Quaternion.identity) as GameObject;
        o.transform.parent = Container.transform;
    }

    public void createEnemy3() {
        float x = Random.Range(-1.59f, 1.59f);
        float y = Random.Range(-5.5f, 5.5f);
        GameObject o = GameObject.Instantiate(missilePrefab, new Vector3(x, y, transform.position.z), Quaternion.identity) as GameObject;
        o.transform.parent = Container.transform;
    }

    public void createawardType0() {
        float x = Random.Range(-1.59f, 1.59f);
        GameObject o = GameObject.Instantiate(awardType0Prefab, new Vector3(x, transform.position.y, transform.position.z), Quaternion.identity) as GameObject;
        o.transform.parent = Container.transform;
        GetComponent<AudioSource>().Play();
    }

    public void createawardType1() {
        float x = Random.Range(-2.1f, 2.1f);
        GameObject o = GameObject.Instantiate(awardType1Prefab, new Vector3(x, transform.position.y, transform.position.z), Quaternion.identity) as GameObject;
        o.transform.parent = Container.transform;
        GetComponent<AudioSource>().Play();
    }
    public void createawardType2() {
        float x = Random.Range(-2.1f, 2.1f);
        GameObject o = GameObject.Instantiate(awardType2Prefab, new Vector3(x, transform.position.y, transform.position.z), Quaternion.identity) as GameObject;
        o.transform.parent = Container.transform;
        GetComponent<AudioSource>().Play();
    }
}
