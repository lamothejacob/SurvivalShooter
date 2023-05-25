using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialEnemy : MonoBehaviour
{
    [SerializeField] GameObject impactGren;
    [SerializeField] Transform endPos;
    [SerializeField] int speed;
    [SerializeField] float throwRate;

    GameObject fire = null;

    bool isThrowing;
    // Update is called once per frame

    private void Start()
    {
        //endPos = gameManager.instance.player.transform;
    }
    void Update()
    {
        if (fire == null)
        {
            if (isThrowing == false)
                StartCoroutine(FireRate());
            //fire = Instantiate(impactGren, transform.position, transform.rotation);
        }

        float distCur = Vector3.Distance(fire.transform.position, endPos.position);

        Vector3 endPosition = new Vector3(endPos.position.x, endPos.position.y + (distCur / 2f), endPos.position.z);
        fire.transform.position = Vector3.MoveTowards(fire.transform.position, endPosition, Time.deltaTime * speed);

    }

    IEnumerator FireRate()
    {
        isThrowing = true;
        yield return new WaitForSeconds(throwRate);
        Transform temp = gameManager.instance.player.transform;
        endPos = temp;
        fire = Instantiate(impactGren, transform.position, transform.rotation);
        isThrowing = false;
    }
}
