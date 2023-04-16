using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTarget : MonoBehaviour
{
    [SerializeField] GameObject missilePrefab;
    [SerializeField] GameObject missileLauncherPrefab;
    [SerializeField] private AudioClip playerMissileLaunch;
    private float reloadTime;


    // Update is called once per frame
    void Update()
    {
        if (reloadTime >= 0.0f)
        {
           reloadTime -= Time.deltaTime;
        } 
        if (reloadTime <= 0.0f && Input.GetMouseButtonDown(0))
        {
            if (GameController.Instance.TowerState == PlayerTowerState.Damaged && GameController.Instance.PlayerPowerUps == PowerUps.Default)
            {
                AudioMannager.Instance.PlaySound(playerMissileLaunch, 0.2f);
                Instantiate(missilePrefab, missileLauncherPrefab.transform.position, Quaternion.identity);
                reloadTime = 2.0f;
            }
            else if (GameController.Instance.PlayerPowerUps == PowerUps.RappidFire && GameController.Instance.GameState == State.Play)
            {
                AudioMannager.Instance.PlaySound(playerMissileLaunch, 0.2f);
                Instantiate(missilePrefab, missileLauncherPrefab.transform.position, Quaternion.identity);
                reloadTime = 0;
            }
            else
            {
                AudioMannager.Instance.PlaySound(playerMissileLaunch, 0.2f);
                Instantiate(missilePrefab, missileLauncherPrefab.transform.position, Quaternion.identity);
                reloadTime = 0.5f;
            }
        }
    }
}
