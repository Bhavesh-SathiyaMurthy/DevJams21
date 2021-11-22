using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum GameSate { Freeroam, Dialog, Battle}

public class GameController : MonoBehaviour
{
    [SerializeField] PlayerController playerController;
    GameSate State;
    [SerializeField] BattleController battleController;


    private void Start()
    {
        DialogManager.instance.OnShowDialog += () =>
         {
             State = GameSate.Dialog;
         };
        DialogManager.instance.OnCloseDialog += () =>
        {
            if (State == GameSate.Dialog)
                State = GameSate.Freeroam;
        };
        battleController.OnBossEncounter += () =>
        {
            State = GameSate.Battle;
        };
        BattleController.instance.OnEndBoss += () =>
        {
            if (State == GameSate.Battle)
                State = GameSate.Freeroam;
        };
        //BattleController.instance.OnBossEncounter += () =>
        //{
        // State == GameSate.Battle;
        //};
        //BattleController.instance.OnEndBoss += () =>
        //{
        // if (State == GameSate.Battle)
        //.gameobject.SetActive(true);
        //  State = GameSate.Freeroam;
        //};
    }

    private void Update()
    {
        if (State == GameSate.Freeroam)
        {
            playerController.HandleUpdate();
        }
        else if (State == GameSate.Dialog)
        {
            DialogManager.instance.HandleUpdate();
        }
        else if (State == GameSate.Battle)
        {

            Debug.Log("Battle initiated");
            BattleController.instance.HandleUpdate(); 
        }

    }
}
