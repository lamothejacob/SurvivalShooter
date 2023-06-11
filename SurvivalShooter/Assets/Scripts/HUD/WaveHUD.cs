using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WaveHUD : HUD
{

    [Header("------ Wave HUD Components ------")]
    [SerializeField] TextMeshProUGUI waveInfo;
    [SerializeField] TextMeshProUGUI enemies;

    float enemiesLeft;

    // Start is called before the first frame update
    void Start()
    {
        gunType.preserveAspect = true;
        bHUDVisible = true;
        ammoColorTrans = new Color(1f, 1f, 1f, .4f);
        ammoColor = new Color(1f, 1f, 1f, 1f);
        ammoColorEmpty = new Color(1f, 1f, 1f, 0f);
        shieldBar.maxValue = gameManager.instance.playerScript.getShieldHPMax();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateHUD();
        DisplayKills();
        UpdateWave();
    }

    void DisplayKills()
    {
        // This will display how many enemies are left in the game.
        enemiesLeft = gameManager.instance.enemySpawnerScript.EnemiesLeft();
        enemies.SetText(enemiesLeft.ToString());

    }

    void UpdateWave()
    {
        int waveNumber = gameManager.instance.enemySpawnerScript.GetWaveNumber();
        waveInfo.SetText("Wave " + waveNumber.ToString());
    }
}
