using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Monogame._2;

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    private Player player;
    private Texture2D spaceShip;
    private List<Enemy> enemies = new List<Enemy>();

    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
        _graphics.PreferredBackBufferHeight = 1080;
        _graphics.PreferredBackBufferWidth = 1920; 
        _graphics.IsFullScreen = true;
    }

    protected override void Initialize()
    {
        // TODO: Add your initialization logic here

        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        spaceShip = Content.Load<Texture2D>("spaceship");

        player = new Player(spaceShip, new Vector2(540, 1000), 50);
        

        // TODO: use this.Content to load your game content here
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        // TODO: Add your update logic here


        player.Update();
        foreach(Enemy enemy in enemies){
            enemy.Update();
        }
        EnemyBulletCollision();
        EnemyPlayerCollision();
        SpawnEnemy();
        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        // TODO: Add your drawing code here
        _spriteBatch.Begin();
        player.Draw(_spriteBatch);
        foreach(Enemy enemy in enemies){
            enemy.Draw(_spriteBatch);
        }
        _spriteBatch.End();
        base.Draw(gameTime);
    }

    private void SpawnEnemy(){
        Random rand = new Random();
        int value = rand.Next(1, 101);
        int spawnChancePercent = 3;
        if(value<=spawnChancePercent){
            enemies.Add(new Enemy(spaceShip));   
        }
    }

    private void EnemyBulletCollision(){
        for(int i = 0; i < enemies.Count; i++){
            for (int j = 0; j < player.Bullets.Count; j++)
            {
                if(enemies[i].Hitbox.Intersects(player.Bullets[j].Hitbox)){
                    enemies.RemoveAt(i);
                    player.Bullets.RemoveAt(j);
                    i--;
                    j--;
                }
            }
        }
    }

    private void EnemyPlayerCollision(){
        int hp = 3;
        for (int i = 0; i < enemies.Count; i++)
        {
            
        }
    }
}