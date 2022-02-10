using System;
using System.Collections.Generic; // System contains a lot of default C# libraries 
using GXPEngine;                                // GXPEngine contains the engine
using System.Drawing;							// System.Drawing contains drawing tools such as Color definitions

public class MyGame : Game
{
	private Player player;
	public float gravity = .3f;
	public int currentScore;

	public MyGame() : base(1366, 768, false)		// Create a window that's 800x600 and NOT fullscreen
	{
		Sprite backGround = new Sprite("background-01.png", false, false);
		AddChild(backGround);
		SetUpScenes();
	}

	public void SetUpScenes()
	{
		MainMenuScene menuScene = new MainMenuScene();
		SceneManager.instance.AddScene(menuScene);
		Level level1 = new Level();
		SceneManager.instance.AddScene(level1);
		GameOverScene gameOverScene = new GameOverScene();
		SceneManager.instance.AddScene(gameOverScene);
		SceneManager.instance.LoadScene(0);
	}
	// For every game object, Update is called every frame, by the engine:
	void Update()
	{
		//Console.WriteLine(gravity);
	}

	static void Main()							// Main() is the first method that's called when the program is run
	{ 
		new MyGame().Start();					// Create a "MyGame" and start it
	}
}