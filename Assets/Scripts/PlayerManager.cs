﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    #region
        public static PlayerManager instance;
        void Awake()
        {
            instance = this;
        }
    #endregion

    public GameObject player;
}
