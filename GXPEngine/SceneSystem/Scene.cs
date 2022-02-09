using System;
using GXPEngine;
using TiledMapParser;

    /// <summary>
    /// This is a scene withing the game.
    /// A massive parent object that gets activated and deactivated by the scenemanager.
    /// </summary>
    public class Scene : GameObject
    {
        public bool isActive = true;
        public bool softUnload = false;
        public bool displayHUD { get; private set; }
        public Scene(bool displayHUD = false)
        {
            this.displayHUD = displayHUD;
        }
        
        /// <summary>
        /// call the protected start method. 
        /// </summary>
        public virtual void LoadScene() 
        {
            //Console.WriteLine("LoadScene started");
            Start();
        }
        
        /// <summary>
        /// Remove all objects from this scene (softUnload means that objects will only be disabled)
        /// </summary>
        public void UnLoadScene()
        {
            if (softUnload)
            {
                foreach (GameObject gameObject in GetChildren())
                {
                    if(gameObject != SceneManager.instance)
                    gameObject.visible = false;
                }
            }
            else
            {
                foreach (GameObject gameObject in GetChildren())
                {
                    if(gameObject != SceneManager.instance)
                    gameObject.LateDestroy();
                }
            }
            isActive = false;
            visible = false;
        }
        
        protected virtual void Update()
        {
            if (!isActive) return;
        }
        
        protected virtual void Start()
        {
            visible = true;
            isActive = true;
            GetChildren().ForEach(x => x.visible = true);
        }
    }
