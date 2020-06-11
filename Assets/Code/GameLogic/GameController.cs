using System;
using Code.Factories;
using Code.UI;
using Code.Utility;
using Code.World;
using UnityEngine;
using UnityEngine.UI;

namespace Code.GameLogic
{
    public class GameController : MonoBehaviour
    {
        public static event EventHandler<bool> GameRestartedEvent;
        public static event EventHandler GameEndedEvent;
        
        [SerializeField]
        private Material[] materials;
        
        private Button _startStandardGameBtn;
        private Button _startBuffedGameBtn;

        private void Awake()
        {
            IResourcesRepository resourcesRepository = new ResourcesRepository();
            
            IUiGeneratorFactory uiGeneratorFactory = new UiGeneratorFactory(gameObject, resourcesRepository);
            var uiElementsGenerator = uiGeneratorFactory.UiElementsGenerator;
            uiElementsGenerator.InitializeGenerator(resourcesRepository);
            InitializeRestartButtons(uiElementsGenerator);
            
            IBuffRepositoryFactory buffRepoFactory = new BuffRepositoryFactory(resourcesRepository);
            var buffsRepository = buffRepoFactory.BuffsRepository;

            new PlayerUiControllerFactory(resourcesRepository, uiElementsGenerator);
            new PlayerControllerFactory(buffsRepository, resourcesRepository);
         
            IWorldObjectsGenerator worldGenerator = GetComponent<WorldObjectsGenerator>();
            worldGenerator.PlayerModelPrefab = resourcesRepository.GetPlayerModelPrefab();
            new WorldObjectControllerFactory(worldGenerator, resourcesRepository, materials);
            StartDefaultGame();
        }

        private void InitializeRestartButtons(IUiElementsGenerator uiElementsGenerator)
        {
           var buttons = uiElementsGenerator.CreateAndGetGameRestartButtons();
            _startStandardGameBtn = buttons[0];
            _startBuffedGameBtn = buttons[1];
            _startStandardGameBtn.onClick.AddListener(StartDefaultGame);
            _startBuffedGameBtn.onClick.AddListener(StartBuffedGame);
        }
        
        private void StartDefaultGame()
        {
            GameRestartedEvent?.Invoke(this, false);
        }
    
        private void StartBuffedGame()
        {
            GameRestartedEvent?.Invoke(this, true);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                GameEndedEvent?.Invoke(this, EventArgs.Empty);
                Application.Quit();
            }
        }
    }
}
