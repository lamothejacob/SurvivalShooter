using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
struct cutscene {
    [Header("----- Camera Movement -----")]
    public float timeStay;
    public Vector3 position;
    public Quaternion rotation;
    public float timeFadeNext;

    [Header("----- Audio -----")]
    public AudioClip clipToPlay;
    [Range(0f, 1f)]
    public float volume;
}

public class CutsceneHandler : MonoBehaviour {
    [SerializeField] Camera cutsceneCamera;
    [SerializeField] List<cutscene> cutsceneList;
    Queue<cutscene> cutsceneQueue;

    void Start() {
        cutsceneQueue = new Queue<cutscene>(cutsceneList);

        if (cutsceneCamera == null) {
            cutsceneCamera = Camera.main;
        }

        StartCoroutine(MoveToPosition(0f));
    }

    IEnumerator MoveToPosition(float time) {
        float elapsedTime = 0f;
        Vector3 position = cutsceneCamera.transform.position;
        Quaternion rotation = cutsceneCamera.transform.rotation;
        cutscene cut = cutsceneQueue.Dequeue();

        while (elapsedTime < time) {
            cutsceneCamera.transform.SetPositionAndRotation(Vector3.Lerp(position, cut.position, elapsedTime / time), Quaternion.Lerp(rotation, cut.rotation, elapsedTime / time));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        cutsceneCamera.transform.SetPositionAndRotation(cut.position, cut.rotation);

        if (cut.clipToPlay != null) {
            AudioSource a = new AudioSource();
            a.volume = cut.volume;

            Instantiate(a);
            a.PlayOneShot(cut.clipToPlay);
            Destroy(a, cut.clipToPlay.length);
        }

        yield return new WaitForSeconds(cut.timeStay);

        if (cutsceneQueue.Count > 0) {
            StartCoroutine(MoveToPosition(cut.timeFadeNext));
        }
    }
}