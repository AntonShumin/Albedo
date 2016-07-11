using UnityEngine;
using System.Collections;

public class script_Light_Look : MonoBehaviour {

    public GameObject Player;
    private Transform player_transform;

	// Use this for initialization
	void Start ()
    {
        player_transform = Player.transform;
	}
	
	// Update is called once per frame
	void Update ()
    {
        transform.LookAt(player_transform);
	}
}
