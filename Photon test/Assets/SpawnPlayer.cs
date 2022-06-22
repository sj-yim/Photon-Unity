using System.Collections;
using System.Collections.Generic;
using System.IO;
using Photon.Pun;
using UnityEngine;

public class SpawnPlayer : MonoBehaviour
{
    public List<GameObject> prefabs;

    // Start is called before the first frame update
    void Start()
    {
        //RPC_CreatePlayer();
        CustomPrefabPool();
    }

    [PunRPC]
    private void RPC_CreatePlayer()
    {
        Vector3 randomPos = new Vector3(Random.Range(-5, 5), 0, Random.Range(-5, 5));
        PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "PhotonNetworkPlayer"), randomPos, Quaternion.identity, 0);
    }

    void CustomPrefabPool()
    {
        DefaultPool pool = PhotonNetwork.PrefabPool as DefaultPool;

        if (pool != null && this.prefabs != null)
        {
            foreach(GameObject prefab in this.prefabs)
            {
                Vector3 randomPos = new Vector3(Random.Range(-5, 5), 0, Random.Range(-5, 5));

                pool.ResourceCache.Add(prefab.name, prefab);

                PhotonNetwork.Instantiate(prefab.name, randomPos, Quaternion.identity, 0);
            }
        }
    }
}
