using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;


[Serializable]
public class BallArea
{
    public Animator elevator;
    public TextMeshProUGUI countText;
    public int targetBallCount;
    public GameObject[] balls;
}
public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject ballControlObject;
    [SerializeField] public bool playerSituation;
    private int ballCount;
    private int totalStageIndex;
    private int availableStageIndex;
    [SerializeField]public List<BallArea> ballArea = new List<BallArea>();




    // Start is called before the first frame update
    void Start()
    {
        playerSituation = true;
        for (int i = 0; i < ballArea.Count; i++)
        {
            ballArea[i].countText.text = ballCount + "/" + ballArea[i].targetBallCount;
        }
        totalStageIndex = ballArea.Count-1;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerSituation)
        {
            player.transform.position += 5f * Time.deltaTime * player.transform.forward;

            if (Time.timeScale!=0)//Oyun durmadıysa.
            {
                if (Input.GetKey(KeyCode.A))
                {
                    player.transform.position = Vector3.Lerp(player.transform.position,
                        new Vector3(player.transform.position.x - 0.1f, player.transform.position.y,
                            player.transform.position.z), 0.5f);
                } if (Input.GetKey(KeyCode.D))
                {
                    player.transform.position = Vector3.Lerp(player.transform.position,
                        new Vector3(player.transform.position.x + 0.1f, player.transform.position.y,
                            player.transform.position.z), 0.5f);
                }

            }
        }
        
    }

    public void ReachLine()
    {
        playerSituation = false;
        Invoke("StageControl",2f);//Alan ulaştıktan 2 saniye sonra kontrol et anlamına gelir.
        Collider[] HitColl = Physics.OverlapBox(ballControlObject.transform.position,
            ballControlObject.transform.localScale / 2, Quaternion.identity);

        int i = 0;
        while (i<HitColl.Length)//Player içerisine giren topları teker teker sayar.
        {
           
            HitColl[i].GetComponent<Rigidbody>().AddForce(new Vector3(0,0,0.8f),ForceMode.Impulse);//Toplara fizik gücü uygular.
            i++;
        }
    }

    public void CountToBall()
    {
        ballCount++;
        ballArea[availableStageIndex].countText.text = ballCount + "/" + ballArea[availableStageIndex].targetBallCount;
       
    }

    void StageControl()
    {
        if (ballCount>=ballArea[availableStageIndex].targetBallCount)
        {
            ballArea[availableStageIndex].elevator.Play("Elevator");
            foreach (var item in ballArea[availableStageIndex].balls)
            {
                item.SetActive(false);
            }

            if (availableStageIndex==totalStageIndex)
            {
                Debug.Log("You Win !");
                Time.timeScale = 0;
            }
            else
            {
                availableStageIndex++;
                ballCount = 0;
            }

            
        }
        else
        {
            Debug.Log("You Lost!");
 
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color=Color.red;
        Gizmos.DrawCube(ballControlObject.transform.position,ballControlObject.transform.localScale);
    }
    
  
}
