using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopLine : MonoBehaviour
{
 [SerializeField] private GameManager _gameManager;


 private void OnTriggerEnter(Collider other)
 {
  if (other.CompareTag("PlayerLine"))
  {
   _gameManager.ReachLine();
  }
 }
}
