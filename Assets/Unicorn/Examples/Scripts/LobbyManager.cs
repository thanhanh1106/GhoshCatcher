using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;

namespace Unicorn.Examples
{
    public class LobbyManager : LevelManager
    {
        [SerializeField] private new Camera camera;
        public Agent Player;
        public PlayerBrain PlayerBrain;
        public new static LobbyManager Instance => LevelManager.Instance as LobbyManager;


        protected override void Awake()
        {
            base.Awake();
            SetUpCamera();
        }

        private void SetUpCamera()
        {
            CameraController mainCamera = GameManager.Instance.MainCamera;
            var mainCameraTransform = mainCamera.transform;
            mainCameraTransform.position = camera.transform.position;
            mainCameraTransform.rotation = camera.transform.rotation;
            mainCamera.Camera.fieldOfView = camera.fieldOfView;
        }

        public override void StartLevel()
        {
            
        }
        public void EndGameCall()
        {
            EndGame(LevelResult.Win);
        }

        public override void ExitLevel()
        {
            
        }
    }

}