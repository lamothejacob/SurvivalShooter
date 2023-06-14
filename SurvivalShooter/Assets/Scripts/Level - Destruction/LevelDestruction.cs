using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Recorder.OutputPath;

public class LevelDestruction : MonoBehaviour
{

    [Header("----- Encounter 1 -----")]
    [SerializeField] GameObject door;
    [SerializeField] List<GameObject> fuel;


    [Header("----- Encounter 2 -----")]
    [SerializeField] List<GameObject> generators;



    public int encountersCompleted;

    bool doorOpen;
    bool encounter1Compl;
    bool encounter2Compl;
    bool encounter3Compl;
    // Start is called before the first frame update
    void Start()
    {
        gameManager.instance.levelGoalText.SetText("Find and destroy Fuel Deposit");

    }

    // Update is called once per frame
    void Update()
    {

        if (gameManager.instance.GetEnemiesKilled() == 25)
        {
            if (!doorOpen)
            {
                OpenDoor();
            }
        }

        if (encounter1Compl == false && fuel[0] == null)
        {
            encountersCompleted++;
            encounter1Compl = true;
        }

        if (encounter2Compl == false && generators[2] == null)
        {
            encountersCompleted++;
            encounter2Compl = true;
        }

        if (encountersCompleted == 2)
        {
            gameManager.instance.levelGoalText.SetText("");
            gameManager.instance.WinState(2);
        }
        else if (encountersCompleted == 1)
        {
            gameManager.instance.levelGoalText.SetText("Find and destroy Generator");
        }
    }

    void OpenDoor()
    {
        //door.transform.Rotate(door.transform.rotation.x, door.transform.rotation.y, -90);
        Quaternion rot = Quaternion.Euler(door.transform.rotation.x, 41, -90f);
        door.transform.rotation = Quaternion.Slerp(door.transform.rotation, rot, Time.deltaTime * 1.5f);
        if (door.transform.rotation == rot)
            doorOpen = true;
    }
}
