using UnityEngine;
using System.Collections;

namespace Assets.Code
{
    //Class describing !basic! enemy properties
    public class Enemy : MonoBehaviour
    {
        [SerializeField]
        private string enemyName = "Enemy!";
        public string EnemyName
        {
            get { return enemyName; }
        }
    }
}