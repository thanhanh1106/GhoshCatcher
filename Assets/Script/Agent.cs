using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Unicorn
{
    public class Agent : MonoBehaviour
    {
        [SerializeField] private NavMeshAgent agent;
        private Camera mainCam;
        public void Init()
        {
            //agent = GetComponent<NavMeshAgent>();
            mainCam = GameManager.Instance.MainCamera.Camera;
        }
        public void UpdateInGame()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = mainCam.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if(Physics.Raycast(ray, out hit))
                {
                    agent.SetDestination(hit.point);    
                }
            }
        }

    }
}
