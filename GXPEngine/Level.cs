using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine;
using TiledMapParser;

class Level : Scene
{

    TiledLoader levelMap;
    private string enemyMapFileName;
    public Level(string fileName = "")
    {
        enemyMapFileName = fileName;
        Console.WriteLine(enemyMapFileName);
    }

    /// <summary>
    ///Set up the level.
    /// </summary>
    protected override void Start()
    {
        isActive = true;
        if (enemyMapFileName != "")
        {
            levelMap = new TiledLoader(enemyMapFileName, this);
            levelMap.addColliders = false;
            levelMap.rootObject = this;
            levelMap.LoadImageLayers();

            levelMap.rootObject = this;
            levelMap.autoInstance = true;
            levelMap.LoadObjectGroups();
        }
    }

    /// <summary>
    /// Gets called every frame
    /// When all enemies are out of the game. Complete the level
    /// When hte level is considered complete. Start a timer. When this timer is complete go to the next level
    /// When you are out of health. Tell the game to end it.
    /// </summary>
    protected override void Update()
    {
        if (!base.isActive)
        {
            return;
        }
        // Console.WriteLine(enemiesLeft);
        // SceneManager.instance.TryLoadNextScene();

    }
    /// <summary>
    /// Remove an enemy and possibly lives
    /// </summary>
    /// <param name="enemyToRemove"></param>
}

