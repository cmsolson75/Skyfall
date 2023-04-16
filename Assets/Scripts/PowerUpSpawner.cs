using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpSpawner : MonoBehaviour
{
    [SerializeField] private GameObject boxPrefab;
    [SerializeField] private GameObject slowMoPrefab;
    private float delayPowerUps;
    private float rSelect;
    private bool _inCoroutine;


    private void Start()
    {
        delayPowerUps = Random.Range(20, 60);
    }


    // Update is called once per frame
    void Update()
    {
        if (!_inCoroutine)
        {
            StartCoroutine(SpawnPowerUp());
            _inCoroutine = true;
        }
    }
    public IEnumerator SpawnPowerUp()
    {
        delayPowerUps = Random.Range(15, 30);
        yield return new WaitForSeconds(delayPowerUps);
        //Random Select Between 0, 1
        rSelect = Random.Range(0, 2);
        if (rSelect == 0)
        {
            Instantiate(boxPrefab);

        }
        else
        {
            Instantiate(slowMoPrefab);
        }


        _inCoroutine = false;

    }
}
