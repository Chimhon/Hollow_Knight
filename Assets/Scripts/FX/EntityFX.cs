using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class EntityFX : MonoBehaviour
{

    [HideInInspector] public Player player;
    public SwordSlashFX swordSlashFX { get; private set;}

    private void Awake()
    {

    }

    private void Start()
    {
        swordSlashFX = GetComponent<SwordSlashFX>();
        player = PlayerManager.instance.player;
    }

    private void Update()
    {
        
    }


}
