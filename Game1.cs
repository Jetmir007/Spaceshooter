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
    private List<Enemy2> enemies2 = new List<Enemy2>();
    private Boss boss;
    private int bossHP = 10;
    private int point = 0;
    private int hp = 3;
    SpriteFont fontScore;

    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
        _graphics.PreferredBackBufferHeight = 1080;
        _graphics.PreferredBackBufferWidth = 1920; 
        _graphics.ApplyChanges();
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
        fontScore = Content.Load<SpriteFont>("fontScore");

        player = new Player(spaceShip, new Vector2(540, 1000), 50);
        boss = new Boss(spaceShip, new Vector2(900, -100));
        

        // TODO: use this.Content to load your game content here
    }

     protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        // TODO: Add your update logic here


        player.Update();
        if(point>100){
            boss.Update();
        }
        foreach(Enemy enemy in enemies){
            enemy.Update();
        }
        foreach(Enemy2 enemy2 in enemies2){
            enemy2.Update();
        }
        EnemyBulletCollision();
        EnemyPlayerCollision();
        EnemyEnemyCollison();
        SpawnEnemy();
        BossCollision();
        PlayerBulletCollision();
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
        foreach(Enemy2 enemy2 in enemies2){
            enemy2.Draw(_spriteBatch);
        } 
        if(point>100 && bossHP>0){
            boss.Draw(_spriteBatch);
            _spriteBatch.DrawString(fontScore, "Boss HP: " + Convert.ToString(bossHP), new Vector2(50, 150), Color.DarkBlue);
        }
        if(bossHP<=0){
            _spriteBatch.DrawString(fontScore, "Hurra Du Vann!!", new Vector2(900, 500), Color.Gold);
            
        }
        _spriteBatch.DrawString(fontScore, Convert.ToString(point), new Vector2(50, 50), Color.Black);
        _spriteBatch.DrawString(fontScore, "HP: " + Convert.ToString(hp), new Vector2(1760, 50), Color.Red);
        _spriteBatch.End();
        base.Draw(gameTime);
    }

    private void SpawnEnemy(){
        Random rand = new Random();
        int value = rand.Next(1, 101);
        int spawnChancePercent = 3;
        if(value<=spawnChancePercent){
            enemies.Add(new Enemy(spaceShip));
            enemies2.Add(new Enemy2(spaceShip));   
        }
        if(point>100){
            enemies.RemoveRange(0, enemies.Count);
            enemies2.RemoveRange(0, enemies2.Count);
        }
    }

    private void EnemyBulletCollision(){
        Random rng = new Random();
        for(int i = 0; i < enemies.Count; i++){
            for (int j = 0; j < player.Bullets.Count; j++)
            {
                if(enemies[i].Hitbox.Intersects(player.Bullets[j].Hitbox)){
                    enemies.RemoveAt(i);
                    player.Bullets.RemoveAt(j);
                    point += rng.Next(20, 50);
                }
            }
        }
        for(int i = 0; i < enemies2.Count; i++){
            for (int j = 0; j < player.Bullets.Count; j++)
            {
                if(enemies2[i].Hitbox.Intersects(player.Bullets[j].Hitbox)){
                    enemies2.RemoveAt(i);
                    player.Bullets.RemoveAt(j);
                    point += rng.Next(20, 50);
                }
            }
        }
    }

    private void EnemyPlayerCollision(){
        for (int i = 0; i < enemies.Count; i++)
        {
            if(hp>0){
                if(enemies[i].Hitbox.Intersects(player.Hitbox)){
                    hp--;
                    enemies.RemoveAt(i);
                }
            }
            else{
                Exit();
            }
        }
        for (int i = 0; i < enemies2.Count; i++)
        {
            if(hp>0){
                if(enemies2[i].Hitbox.Intersects(player.Hitbox)){
                    hp--;
                    enemies2.RemoveAt(i);
                }
            }
            else{
                Exit();
            }
        }
        if(hp>0){
            if(boss.Hitbox.Intersects(player.Hitbox)){
            hp--;
            }
        }
        else{
            Exit();
        }
    }

    private void EnemyEnemyCollison(){
        for (int i = 0; i < enemies.Count; i++)
        {
            for (int j = 0; j < enemies2.Count; j++)
            {
                if(enemies[i].Hitbox.Intersects(enemies2[j].Hitbox)){
                    enemies.RemoveAt(i);
                    enemies2.RemoveAt(j);
                }
            }
        }
    }
    private void BossCollision(){
        if(bossHP>0){
            for (int i = 0; i < player.Bullets.Count; i++)
            {
                if(player.Bullets[i].Hitbox.Intersects(boss.Hitbox)){
                    player.Bullets.RemoveAt(i);
                    bossHP--;
                    
                }
            }
        }
    }
    private void PlayerBulletCollision(){
        for (int i = 0; i < boss.Bullets.Count; i++)
        {
            if(boss.Bullets[i].Hitbox.Intersects(player.Hitbox)){
                hp--;
                boss.Bullets.RemoveAt(i);
            }
        }
        for (int i = 0; i < player.Bullets.Count; i++)
        {
            for (int j = 0; j < boss.Bullets.Count; j++)
            {
                if(boss.Bullets[j].Hitbox.Intersects(player.Bullets[i].Hitbox)){
                    boss.Bullets.RemoveAt(j);
                    player.Bullets.RemoveAt(i);
                }
            }
        }
    }
}