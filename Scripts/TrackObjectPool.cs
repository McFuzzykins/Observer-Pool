using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class TrackObjectPool : MonoBehaviour
{
    [SerializeField]
    private List<Track> tracks;

    private int maxPoolSize = 10;
    public int stackDefaultCapacity = 10;

    private IObjectPool<Track> _pool;

    public IObjectPool<Track> Pool
    {
        get
        {
            if (_pool == null)
            {
                _pool = new ObjectPool<Track>
                    (CreatedPoolItem, OnTakeFromPool, OnReturnedToPool, OnDestroyPoolObject, true, stackDefaultCapacity, maxPoolSize);
                
                return _pool;
            }
        }
    }

    private Track CreatedPoolItem()
    {
        var go;
        for (int i = 0; i < tracks.Count; i++)
        {
            go = GameObject.Instantiate(tracks[i]);
        }

        Track track = go.AddComponent<Track>();

        go.Name = "Track";
        track.Pool = Pool;

        return track;
    }

    private void OnReturnedToPool(Track track)
    {
        track.gameObject.SetActive(false);
    }

    private void OnTakeFromPool(Track track)
    {
        track.gameObject.SetActive(true);
    }

    private void OnDestroyPoolObject(Track track)
    {
        Destroy(track.gameObject);
    }

    public void Spawn()
    {
        var track = Pool.Get();

       // track.transform.position = 
    }
}
