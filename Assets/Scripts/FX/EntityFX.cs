using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class EntityFX : MonoBehaviour
{

    [HideInInspector] public Player player;

    private void Awake()
    {

    }

    private void Start()
    {
        player = PlayerManager.instance.player;
    }

    private void Update()
    {
        
    }


}
