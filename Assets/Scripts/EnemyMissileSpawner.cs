using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMissileSpawner : MonoBehaviour
{
    [SerializeField] private GameObject missilePrefab;
    [SerializeField] private float Ypadding = 0.5f;
    private float minX, maxX;
    private float _timer;
    public float delayBetweenMissiles;
    private bool _inCoroutine;
    [SerializeField] AudioClip enemyMissileLaunch;

    private void Awake()
    {
        delayBetweenMissiles = 2f;
    }

    // Start is called before the first frame update
    void Start()
    {
        minX = Camera.main.ViewportToWorldPoint(new Vector3(0, 1, 0)).x;
        maxX = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, 0)).x;

    }

    // Update is called once per frame
    void Update()
    {
        SpawnTimer();
    }
    
    public IEnumerator SpawnMissiles()
    {

        float randomX = Random.Range(minX, maxX);
        float yValue = Camera.main.ViewportToWorldPoint(new Vector3(0, 1, 0)).y;
        AudioMannager.Instance.PlaySound(enemyMissileLaunch, 0.2f);
        Instantiate(missilePrefab, new Vector3(randomX, yValue + Ypadding, 0), Quaternion.identity);
        yield return new WaitForSeconds(delayBetweenMissiles);

        
        _inCoroutine = false;

    }
    void SpawnTimer()
    {
        if (!_inCoroutine)
        {
            StartCoroutine(SpawnMissiles());
            _inCoroutine = true;
        }
        //CHANGE TO ELSE IF
        else if (_timer <= 20)
        {
            _timer += Time.deltaTime;
        }
        else
        {
            if (delayBetweenMissiles != 0.75f)
            {
                _timer = 0;
                delayBetweenMissiles -= 0.25f;
            }
        }
    }

}
