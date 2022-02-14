using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine;
using TiledMapParser;

public class PlatformMap
{
    public TiledLoader loader;
    public bool loaded = false;

    public PlatformMap(string fileName, GameObject rootObject)
    {
        loader = new TiledLoader(fileName, rootObject);
    }
}
class Level : Scene
{
    private Player player;
    private Pivot objectOwner;
    private float gravity = .3f;
    private float gravityIncrements;
    public int score = 0;
    public bool levelIsLost = false;
    private float timer = 1.5f;
    EasyDraw scoreUI;
    private SFXHandler sfxHandler;
    public Level(float startingGravity, float gravityIncrements, Dictionary<string,Sound> soundLibrary): base()
    {
        this.gravity = startingGravity;
        this.gravityIncrements = gravityIncrements;
        sfxHandler = new SFXHandler(soundLibrary, .4f);
    }

    /// <summary>
    ///Set up the level.
    /// </summary>
    /// 

    protected override void Start()
    {
        isActive = true;
        objectOwner = new Pivot();
        player = new Player("charSpriteSheet.png", 4,2,objectOwner,4,4,null);
        player.SetOrigin(player.width / 2, player.height / 2);
        player.SetScaleXY(.5f,.5f);
        player.x = game.width / 2;
        player.y = game.height - 300;
        Platform starterPlatform = new Platform("Square.png");
        starterPlatform.SetXY(game.width / 2, game.height - 300);
        starterPlatform.SetScaleXY(2,2);
        Platform p1 = new Platform("Square.png"), p2 = new Platform("Square.png"), p3 = new Platform("Square.png");
        p1.SetXY(200, 100);
        p1.SetScaleXY(2);
        p2.SetXY(700, 200);
        p2.SetScaleXY(2);
        p3.SetXY(1000, 100);
        p3.SetScaleXY(2);
        objectOwner.AddChild(starterPlatform);
        objectOwner.AddChild(p1);
        objectOwner.AddChild(p2);
        objectOwner.AddChild(p3);
        
        AddChild(objectOwner);
        objectOwner.AddChild(player);
        player.OnJump += PlaySoundEffect;
        PlatformSpawner platformSpawner = new PlatformSpawner(1.3f, objectOwner, 10, 3, 10);
        AddChild(platformSpawner);
        // Score UI 
        scoreUI = new EasyDraw(200, 30, false);
        scoreUI.SetXY(game.width - (scoreUI.width), 30);
        AddChild(scoreUI);
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
        scoreUI.Text("SCORE: " + score, true);
        //Console.WriteLine(gravity);
        if (!levelIsLost)
        {
            objectOwner.Move(0, gravity);
                  gravity += gravityIncrements * (1 + Mathf.Pow((float)Time.deltaTime/ 1000, gravity));  
        }
        else
        {
            timer -= (float)Time.deltaTime / 1000;
            player.scale = MathFunctions.Lerp(0, .5f, timer / 1.5f);
        }

        if (timer <= 0)
        {
            SceneManager.instance.TryLoadNextScene();
        }
        
    }

    public void LostLevel()
    {
        levelIsLost = true;
        sfxHandler.PlaySound("GameOver");
    }

    private void PlaySoundEffect(string soundEffectName)
    {
        sfxHandler.PlaySound(soundEffectName);
    }
    
    public override void UnLoadScene()
    {
        MyGame mGame = (MyGame)game;
        mGame.currentScore = score;
        base.UnLoadScene();
        
    }
}

