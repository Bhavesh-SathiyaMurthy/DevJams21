using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class NPCController : MonoBehaviour, interactable
{
    [SerializeField] Dialog dialog;
    [SerializeField] BattleController battleController;

    public void Interact()
    {
      StartCoroutine(DialogManager.instance.ShowDialog(dialog));
    }

    public void Boss()
    {
        StartCoroutine(BattleController.instance.BossEncounter());
    }
}

