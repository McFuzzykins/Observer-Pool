using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Track : MonoBehaviour
{
    public IObjectPool<Track> Pool { get; set; }

    public Transform _curPos;
    public Transform playerPos;

    public PlayerController player;

    [SerializeField]
    private float resetDist = 10.0f;

    // Start is called before the first frame update
    void Start()
    {
        _curPos = new Vector3(0, 0, 0);
        playerPos = player.transform.position;
    }

    void OnEnable()
    {
        StartCoroutine(NextTrack());
    }

    void OnDisable()
    {
        ResetPosition();
    }

    IEnumerator NextTrack()
    {
        yield return new WaitUntil(playerPos.x > (_curPos.x + 10));
    }

    private void ReturnToPool()
    {
        Pool.Release(this);
    }

    private void ResetPosition()
    {
        _curPos = playerPos.x + 10;
    }
}
