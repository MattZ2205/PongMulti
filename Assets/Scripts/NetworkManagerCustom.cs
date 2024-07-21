using UnityEngine;
using Mirror;
using System.Collections;
using System.Collections.Generic;

[AddComponentMenu("")]
public class NetworkManagerCustom : NetworkManager
{
    [SerializeField] GameObject ballPref;

    public Transform leftRacketSpawn;
    public Transform rightRacketSpawn;

    GameObject ball;

    public override void OnServerAddPlayer(NetworkConnectionToClient conn)
    {
        Transform start = numPlayers == 0 ? leftRacketSpawn : rightRacketSpawn;
        GameObject player = Instantiate(playerPrefab, start.position, start.rotation);
        NetworkServer.AddPlayerForConnection(conn, player);

        if (numPlayers == 2)
        {
            ball = Instantiate(spawnPrefabs.Find(prefab => prefab.name == "Ball"));
            //ball = Instantiate(ballPref);
            NetworkServer.Spawn(ball);
        }
    }

    public override void OnServerDisconnect(NetworkConnectionToClient conn)
    {
        if (ball != null)
            NetworkServer.Destroy(ball);

        base.OnServerDisconnect(conn);
    }
}
