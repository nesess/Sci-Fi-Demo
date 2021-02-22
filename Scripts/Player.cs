using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private CharacterController _controller;
    private Vector3 _direction;
    [SerializeField]
    private float _speed = 3.5f;
    [SerializeField]
    private float _gravity = 9.81f;
    private ParticleSystem[] muzzleFlash;
    [SerializeField]
    private GameObject _hitMarketPrefab;
    [SerializeField]
    private AudioSource _weaponAudio;
    [SerializeField]
    private int currentAmmo = 0;
    private int maxAmmo = 50;
    private bool reloadCon = false;
    [SerializeField]
    private AudioSource _reloadAudio;
    [SerializeField]
    public bool hasCoin = false;
    [SerializeField]
    public bool hasWeapon = false;
    private UIManager _uiManager;
    [SerializeField]
    private GameObject _weapon;

    void Start()
    {
        
        _controller = GetComponent<CharacterController>();
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        muzzleFlash = GetComponentsInChildren<ParticleSystem>();

        currentAmmo = maxAmmo;
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();

    }

   
    void Update()
    {
        if(Input.GetMouseButton(0) && currentAmmo > 0 && reloadCon == false && hasWeapon == true)
        {
            shoot();
        }
        else
        {
            foreach (ParticleSystem element in muzzleFlash)
            {
                element.Stop();
            }
            _weaponAudio.Stop();
        }
        
        if(Input.GetKeyDown(KeyCode.R) && reloadCon == false && currentAmmo < maxAmmo)
        {
            reloadCon = true;
            _reloadAudio.Play();
            Invoke("Reload", 1.5f);
        }

    }

    void FixedUpdate()
    {
        calculateMovement();
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }

    void shoot()
    {
        currentAmmo--;
        _uiManager.updateAmmo(currentAmmo);
        Ray rayOrigin = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hitInfo;
        if (Physics.Raycast(rayOrigin, out hitInfo))
        {
            Debug.Log("Hit: " + hitInfo.transform.name);
            GameObject hitmarker = Instantiate(_hitMarketPrefab, hitInfo.point, Quaternion.LookRotation(hitInfo.normal)) as GameObject;
            Destroy(hitmarker, 0.5f);

            Destructable crate = hitInfo.transform.GetComponent<Destructable>();
            if(crate != null)
            {
                crate.DestroyCrate();
            }
        }
        if (_weaponAudio.isPlaying == false)
        {
            _weaponAudio.Play();
        }
        foreach (ParticleSystem element in muzzleFlash)
        {
            element.Emit(1);
        }

    }

    void calculateMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        _direction = new Vector3(horizontalInput, 0, verticalInput);
        Vector3 velocity = _direction * _speed;
        velocity.y -= _gravity;

        velocity = transform.transform.TransformDirection(velocity);
        _controller.Move(velocity * Time.deltaTime);
    }

    void Reload()
    {
        currentAmmo = maxAmmo;
        _uiManager.updateAmmo(currentAmmo);
        reloadCon = false;
    }

    public void weaponEarned()
    {
        hasWeapon = true;
        _weapon.SetActive(true);
        
    }



   

    
}
