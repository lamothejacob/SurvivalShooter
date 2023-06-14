using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneExplosion : MonoBehaviour
{
    [SerializeField] List<GameObject> ExplosionEffects;
    [SerializeField] float timeToExplosion;

    // Start is called before the first frame update
    void Start()
    {
        foreach(GameObject go in ExplosionEffects) {
            go.SetActive(false);
        }

        StartCoroutine(Activate());
    }

    IEnumerator Activate() {
        yield return new WaitForSeconds(timeToExplosion);

        foreach (GameObject go in ExplosionEffects) {
            go.SetActive(true);
        }
    }
}
