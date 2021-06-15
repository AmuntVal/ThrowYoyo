using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    // Start is called before the first frame update
    private PlayerInput playerInput;
    private PlayerAction player;

    void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        int index = playerInput.playerIndex;
        PlayerAction[] players = FindObjectsOfType<PlayerAction>();
        for(int i = 0; i < players.Length; i++)
        {
            if(players[i].playerID == index)
            {
                player = players[i];
                break;
            }
        }
        if(player == null) Debug.LogError("玩家输入器数量高于场景中可控制的avatar的数量");
    }
    private void OnMove(InputValue inputValue)
    {
        //Debug.Log("Input Value:" + inputValue.Get<Vector2>());
        player.SetMoveVector(inputValue.Get<Vector2>());
    }

    private void OnRotate(InputValue inputValue)
    {
        //Debug.Log("Rotate Value:  " + inputValue.Get<Vector2>());
        player.SetRotateVector(inputValue.Get<Vector2>());
        // 另外一种回调写法
        //PlayerControls controls;
        //controls = new PlayerControls();
        //controls.Gameplay.Rotate.performed += ctx => m_Rotate = ctx.ReadValue<Vector2>();
        //controls.Gameplay.Rotate.canceled += ctx => m_Rotate = Vector2.zero; 

        /* void OnEnable()
        {
            controls.Gameplay.Enable();        
        }

        void OnDisable()
        {
            controls.Gameplay.Disable();
        }
         */
    }

    private void OnShield(InputValue inputValue)
    {
        Debug.Log("Shield:  " + inputValue.Get<float>());
        player.StartShield();
    }

    private void OnRotateN(InputValue inputValue)
    {
        //Debug.Log("Rotate N:  " + inputValue.Get<float>());
        player.SetRotateN(inputValue.Get<float>());
    }

    private void OnRotateM(InputValue inputValue)
    {
        //Debug.Log("Rotate M:  " + inputValue.Get<float>());
        player.SetRotateM(inputValue.Get<float>());
    }

}
