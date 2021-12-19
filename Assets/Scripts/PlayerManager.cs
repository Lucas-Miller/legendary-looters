using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerManager : MonoBehaviour
{
    #region
        public static PlayerManager instance;
        void Awake()
        {
            instance = this;
            GameObject.FindGameObjectWithTag("Player");
        }
    #endregion

    public GameObject player;

}
