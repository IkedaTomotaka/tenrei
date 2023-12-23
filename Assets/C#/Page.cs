using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Page : MonoBehaviour
{
   public string NextStage;
   private float step_time;
   public float ReturnTitleTime;

   void Start()
   {
       step_time = 0.0f;
   }

   void Update()
   {
       step_time += Time.deltaTime;

       if(step_time >= ReturnTitleTime)
       {
           SceneManager.LoadScene(NextStage);
       }
   }
}