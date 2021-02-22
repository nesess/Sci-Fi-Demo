using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SharkShop : MonoBehaviour
{

    [SerializeField]
    private AudioSource _weaponBuy;
    private UIManager _uiManager;

    // Start is called before the first frame update
    void Start()
    {
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
    }


    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                Player player = other.GetComponent<Player>();
                if (player != null)
                {
                    if(player.hasCoin == true)
                    {
                        _uiManager.coinGive();
                        player.hasCoin = false;
                        player.weaponEarned();
                        _weaponBuy.Play();
                    }
                }
            }
        }
    }
}
