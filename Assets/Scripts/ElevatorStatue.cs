using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorStatue : MonoBehaviour
{
    [SerializeField] private GameManager _gameManager;
    [SerializeField] private Animator barrierArea;


    public void BarrierUp()
    {
        barrierArea.Play("RaiseTheBarrier");
    }

    public void Go()
    {
        _gameManager.playerSituation = true;
    }

}
