using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class EnemyMissile : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private GameObject explosionPrefab;
    GameObject[] tagPlayerObjects;
    GameObject[] tagMissileTower;
    GameObject[] playerObjects;
    [SerializeField] AudioClip cityExplosion;
    [SerializeField] AudioClip enemyMissileExplosion;
    [SerializeField] AudioClip dammagedTowerSound;
    Vector3 target;

    // Start is called before the first frame update
    void Start()
    {
        tagPlayerObjects = GameObject.FindGameObjectsWithTag("PlayerObjects");
        tagMissileTower = GameObject.FindGameObjectsWithTag("MissileTower");
        playerObjects = tagPlayerObjects.Concat(tagMissileTower).ToArray();
        

        target = playerObjects[Random.Range(0, playerObjects.Length)].transform.position + new Vector3(0, -0.5f, 0);

    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Make into switch <--- dont listen to that guy it works dont touch
        if (collision.tag == "PlayerObjects")
        {
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            Destroy(gameObject);
            AudioMannager.Instance.PlaySound(cityExplosion, 0.5f);
            Destroy(collision.gameObject);
            FindObjectOfType<GameController>().cityUpdate();

        }
        else if (collision.tag == "Explosions")
        {
            AudioMannager.Instance.PlaySound(enemyMissileExplosion, 0.5f);
            FindObjectOfType<GameController>().AddMissileDestroyedScore();
            MissileExplode();
            Destroy(collision.gameObject);
        }
        else if (collision.tag == "PlayerMissiles")
        {
            AudioMannager.Instance.PlaySound(enemyMissileExplosion, 0.5f);
            FindObjectOfType<GameController>().AddMissileDestroyedScore();
            MissileExplode();
            Destroy(collision.gameObject);
        }
        else if (collision.tag == "Ground")
        {
            AudioMannager.Instance.PlaySound(enemyMissileExplosion, 0.5f);
            MissileExplode();
        }
        else if (collision.tag == "MissileTower")
        {
            AudioMannager.Instance.PlaySound(dammagedTowerSound, 0.3f);
            MissileExplode();
            GameController.Instance.TowerState = PlayerTowerState.Damaged;
            Debug.Log("TowerHit");
        }
    }
    private void MissileExplode()
    {
        Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
