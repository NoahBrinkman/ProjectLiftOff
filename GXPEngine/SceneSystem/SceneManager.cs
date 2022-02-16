using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace GXPEngine
{
    /// <summary>
    /// A singleton class made to handle the loading unloading and switching of scenes.
    /// </summary>
    public class SceneManager : GameObject
    {
        private List<Scene> scenes = new List<Scene>();
        private Scene activeScene;
        
        private static SceneManager _instance = null;
        public static SceneManager instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new SceneManager();
                    Console.WriteLine("making new Scene manager");
                }

                return _instance;
            }
        }
        /// <summary>
        /// No variabless should instantly be set. However whenever a new scenemanager is created it
        /// it should always be added as a child of game.
        /// </summary>
        public SceneManager()
        {
            if (!game.GetChildren().Contains(this))
            {
                game.AddChild(this);
            }
        }
        /// <summary>
        /// Add and insantly disable the scene added.
        /// </summary>
        /// <param name="sceneToAdd"></param>
        public void AddScene(Scene sceneToAdd)
        {
            scenes.Add(sceneToAdd);
            AddChild(sceneToAdd);
            sceneToAdd.visible = false;
            sceneToAdd.isActive = false;
        }
        
        /// <summary>
        /// Return the currently active scene
        /// </summary>
        /// <returns></returns>
        public Scene GetActiveScene()
        {
            return activeScene;
        }
        /// <summary>
        /// Attempt to load the next scene if possible.
        /// </summary>
        public void TryLoadNextScene()
        {
            int index = scenes.IndexOf(activeScene);
            if (index + 1 >= scenes.Count) return;
            index++;
            Console.WriteLine((index));
            LoadScene((index));
        }
        /// <summary>
        /// Load the last scene in the buildindex. (good for game over screens)
        /// </summary>
        public void LoadLastScene()
        {
            LoadScene(scenes.Count - 1);
        }
        
        /// <summary>
        /// LoadScene depending on the index you provide
        /// </summary>
        /// <param name="buildIndex"></param>
        public void LoadScene(int buildIndex)
        {
            Console.WriteLine("Loading scene: " + buildIndex);
            if (activeScene != null)
            {
                activeScene.UnLoadScene();
            }
            activeScene = scenes[buildIndex];
            foreach (Scene scene in scenes)
            {
                if (scene != activeScene)
                {
                    activeScene.visible = false;
                    activeScene.isActive = false;
                }
            }
            activeScene.LoadScene();
            activeScene.visible = true;
            activeScene.isActive = true;
        }
        /// <summary>
        /// Load a scene depending on a scene you provide
        /// </summary>
        /// <param name="scene"></param>
        public void LoadScene(Scene scene)
        {
            if (scenes.Contains(scene))
            {
                activeScene.UnLoadScene();
                activeScene = scene;
                activeScene.LoadScene();
            }
        }

        /// <summary>
        /// For debugging press P to show the currently loaded scene and how many scenes there are in the list
        /// </summary>
        void Update()
        {
            if (Input.GetKeyDown(Key.L))
            {
                Console.WriteLine("Total Scenes: " + scenes.Count);
                Console.WriteLine("Current Scene: "+ scenes.IndexOf(activeScene));
            }
        }
        /// <summary>
        /// Destroy all scenes from the list and create a new list.
        /// </summary>
        private void WipeScenes()
        {
            scenes.ForEach(x => x.LateDestroy());
            scenes = new List<Scene>();
            MyGame mg = (MyGame)game;
            mg.SetUpScenes();
        }
        /// <summary>
        ///Reloads the entire game.
        /// </summary>
        public void ReloadGame()
        {
            WipeScenes();
        }
    }
}