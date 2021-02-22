using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _coin;

    [SerializeField]
    private Text _ammoText;

    public void updateAmmo(int count)
    {
        _ammoText.text = "Ammo: " + count;
    }

    public void coinPickedUp()
    {
        _coin.SetActive(true);
    }

    public void coinGive()
    {
        _coin.SetActive(false);
        _ammoText.text = "Ammo: 50";
        
    }
}
