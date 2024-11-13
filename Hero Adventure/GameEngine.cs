using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Hero_Adventure
{
    public class GameEngine
    {
        private Level level;
        private int noOfLevels;
        private Random random;

        private int currentLevel;
        bool callEnemies = false;
        private int currentLevelNumber = 1;

        const int MIN_SIZE = 10;
        const int MAX_SIZE = 20;

        GameState gameState = GameState.InProgress;

        // Temporary
        public string coords;

        public GameEngine(int aNoOfLevels)
        {
            int tempW;
            int tempH;

            noOfLevels = aNoOfLevels;
            random = new Random();
            tempW = random.Next(MIN_SIZE, MAX_SIZE);
            tempH = random.Next(MIN_SIZE, MAX_SIZE);
            level = new Level(tempH, tempW, 1, currentLevelNumber);
        }

        public override string ToString()
        {
            switch(gameState)
            {
                case GameState.Complete:
                    {
                        return "Congratulations, You Have Completed The Game!";
                    }
                case GameState.InProgress:
                    {

                        return level.ToString();
                    }
                case GameState.GameOver:
                    {
                        return "Game Over";
                    }
                default:
                    {
                        return "";
                    }
            }
        }

        private bool MoveHero(Direction aDirection)
        {
            Tile targetTile = level.Hero.vision[(int)aDirection];
            
            //targetTile.X = targetTile.X;
            //targetTile.Y = targetTile.Y;

            if (targetTile is ExitTile && currentLevel == noOfLevels)
            {
                gameState = GameState.Complete;
                return false;
            }
            else if(targetTile is ExitTile && level.exit.DoorLock == false) // Door as to be unlocked now
            {
                NextLevel();
                return true;
            }
            else if (targetTile is PickupTile)
            {
                foreach (PickupTile pickup in level.PickupTiles)
                {
                    if (targetTile.Position == pickup.Position)
                    {
                        //Tile temp;


                        //level.hero.Position = targetTile.Position;

                        //temp = level.hero;
                        //temp.Position = level.hero.Position;
                        //targetTile = new EmptyTile(temp.Position);

                        //level.tiles[level.hero.Position.X, level.hero.Position.Y] = level.hero;
                        //level.tiles[targetTile.Position.X, targetTile.Position.Y] = targetTile;

                        pickup.ApplyEffect(level.hero);
                        targetTile = new EmptyTile(pickup.Position);
                        level.SwopTiles(level.hero, targetTile);


                        Level.UpdateVision();

                        if (callEnemies == false)
                        {
                            callEnemies = true;
                        }
                        else
                        {
                            callEnemies = false;
                        }

                    }
                }
                        return true;
            }
            else if (targetTile is EmptyTile)
            {
                Level.UpdateVision();
                level.SwopTiles(level.hero, targetTile);
                Level.UpdateVision();

                if (callEnemies == false)
                {
                    callEnemies = true;
                }
                else
                {
                    callEnemies = false;
                }

                return true;
            }
            else
            {
                return false;
            }
        }

        public void TriggerMovement(Direction aDirection)
        {
            if (gameState == GameState.GameOver)
            {
                return;
            }

            level.hero.UpdateVision(level);
            MoveHero(aDirection);

            // every second call:
            if (callEnemies == true)
            {
                MoveEnemies(Level.Enemies);
            }

            coords = $"X:{level.hero.X}\nY:{level.hero.Y}";
        }

        public Level Level
        {
            get { return level; }
            set { level = value; }
        }

        public int NoOfLevels
        {
            get { return noOfLevels; }
            set { noOfLevels = value; }
        }

        public int CurrentLevel
        {
            get { return currentLevel; }
            set { currentLevel = value; }
        }

        public enum GameState
        {
            InProgress,
            Complete,
            GameOver
        }

        public void NextLevel()
        {
            currentLevel++;

            int tempW;
            int tempH;
            HeroTile tempHero;
            tempHero = level.hero;

            tempW = random.Next(MAX_SIZE, MAX_SIZE);
            tempH = random.Next(MAX_SIZE, MAX_SIZE);
            level = new Level(tempH, tempW, currentLevelNumber, 1, tempHero);

            currentLevelNumber++;
        }

        private void MoveEnemies(EnemyTile[] enemys)
        {
            Tile targetTile = null;

            for (int y = 0; y < enemys.Length; y++)
            {
                if (enemys[y].isDead == false && enemys[y].GetMove(out targetTile) == true)
                {
                    level.UpdateVision();
                    Level.SwopTiles(enemys[y], targetTile);
                    Level.UpdateVision();
                }
                else
                {
                    return;
                }
            }
        }

        private bool HeroAttack(Direction direction)
        {
            Tile attackTarget = level.hero.vision[(int)direction];

            if (attackTarget is CharacterTile)
            {
                level.hero.Attack((CharacterTile)attackTarget);
                return true;
            }
            else
            {
                return false;
            }

        }

        public void TriggerAttack(Direction direction)
        {
            if (gameState == GameState.GameOver)
            {
                return;
            }

            bool successful;

            successful = HeroAttack(direction);

            if (successful)
            {
                EnemiesAttack();

                if (level.hero.IsDead == true)
                {
                    gameState = GameState.GameOver;
                }
            }

            level.UpdateExit();
        }

        private void EnemiesAttack()
        {
            CharacterTile[] targets;

            for (int i = 0; i < level.Enemies.Length; i++)
            {
                if (level.Enemies[i].IsDead == false)
                {
                    targets = level.Enemies[i].GetTargets();

                    for (int j = 0; j < targets.Length; j++)
                    {
                        level.Enemies[i].Attack(targets[j]);
                    }
                }
            }
        }

        public void SaveGame(int numberOfLevels, int levelnumber, string level, Tile[,] tiles, HeroTile hero, EnemyTile[] enemyTiles)
        {
            const char DELIMITER = ',';
            const string FILE_NAME = "SaveFile.txt";

            // Opening Bridge
            FileStream fStream = new FileStream(FILE_NAME, FileMode.Create, FileAccess.Write);

            // Allow traffic across the bridge
            StreamWriter sWriter = new StreamWriter(fStream);

            // Storing info
            sWriter.WriteLine(
                numberOfLevels.ToString() + DELIMITER +
                levelnumber.ToString() + DELIMITER +
                tiles.GetLength(0).ToString() + DELIMITER +
                tiles.GetLength(1).ToString() + DELIMITER +
                hero.hitPoints + DELIMITER +
                hero.DoubleDamageCount
                );

            foreach (EnemyTile enemyTile in enemyTiles)
            {
                if (enemyTile is GruntTile)
                {
                    sWriter.Write($"Grunt:");
                }
                if (enemyTile is WarlockTile)
                {
                    sWriter.Write($"Warlock:");
                }
                if (enemyTile is TyrantTile)
                {
                    sWriter.Write($"Tyrant:");
                }
                sWriter.Write(DELIMITER + enemyTile.hitPoints.ToString() + DELIMITER +
                        enemyTile.X.ToString() + DELIMITER +
                        enemyTile.Y.ToString() + DELIMITER);
            }

            sWriter.Write("\n" + level);

            sWriter.Close(); // Close the StreamWriter
            fStream.Close(); // Close the FileWriter
        }

        public void LoadGame()
        {
            // Opening bridge
            FileStream fStream = new FileStream("SaveFile.txt", FileMode.Open, FileAccess.Read);

            // Allow traffic across the bridge
            StreamReader sReader = new StreamReader(fStream);

            string recordIn;
            string[] fields;

            // Level requirements
            HeroTile loadedHero = null;
            ExitTile loadedExitTile = null;
            List<EnemyTile> enemyList = new List<EnemyTile>();
            List<PickupTile> pickupList = new List<PickupTile>();

            recordIn = sReader.ReadLine();
            fields = recordIn.Split(',');

            // Level size
            noOfLevels = Int32.Parse(fields[0]);
            currentLevel = Int32.Parse(fields[1]);
            Tile[,] loadedTiles = new Tile[Int32.Parse(fields[2]), Int32.Parse(fields[3])];

            // Hero Stats
            int hitpoints = Int32.Parse(fields[4]);
            int doubleDamageCount = Int32.Parse(fields[5]);

            // Enemy types, hitpoints and positions
            recordIn = sReader.ReadLine();
            string[] enemyStats = recordIn.Split(',');

            int y = 0;
            int x = 0;
            recordIn = sReader.ReadLine();
            while (recordIn != null)
            {
                //fields = recordIn.Split();
                foreach (char c in recordIn) // Separates string into chars
                {
                    Tile tile = null;
                    if (c == '█')
                    {
                        tile = new WallTile(new Position(x, y));
                    }
                    else if (c == '.')
                    {
                        tile = new EmptyTile(new Position(x, y));
                    }
                    else if (c == 'Ϫ')
                    {
                        tile = new GruntTile(new Position(x, y), this.level);
                        enemyList.Add((EnemyTile)tile);
                    }
                    else if (c == '§')
                    {
                        tile = new TyrantTile(new Position(x, y), this.level);
                        enemyList.Add((EnemyTile)tile);
                    }
                    else if (c == 'ᐂ')
                    {
                        tile = new WarlockTile(new Position(x, y), this.level);
                        enemyList.Add((EnemyTile)tile);
                    }
                    else if (c == 'x')
                    {
                        GruntTile grunt = new GruntTile(new Position(x, y), this.level);
                        grunt.hitPoints = 0;
                        tile = grunt;
                    }
                    else if (c == '▼')
                    {
                        tile = new HeroTile(new Position(x, y));
                        loadedHero = (HeroTile)tile;
                    }
                    else if (c == '▓')
                    {
                        tile = new ExitTile(new Position(x, y));
                        loadedExitTile = (ExitTile)tile;
                    }
                    else if (c == '▒')
                    {
                        ExitTile exit = new ExitTile(new Position(x, y));
                        exit.DoorLock = false;
                        tile = exit;
                        loadedExitTile = exit;
                    }
                    else if (c == '+')
                    {
                        tile = new HealthPickup(new Position(x, y));
                        pickupList.Add((PickupTile)tile);
                    }
                    else if (c == '*')
                    {
                        tile = new AttackBuffPickupTile(new Position(x, y));
                        pickupList.Add((PickupTile)tile);
                    }
                    loadedTiles[x, y] = tile;
                    x++;
                }
                recordIn = sReader.ReadLine();
                x = 0;
                y++;
            }

            // Closing bridge
            sReader.Close();
            fStream.Close();

            // plugging in the new level info
            Level loadedingLevel = new Level(loadedTiles.GetLength(0), loadedTiles.GetLength(1), currentLevel, 1);
            loadedingLevel.tiles = loadedTiles;
            loadedingLevel.Enemies = enemyList.ToArray();
            loadedingLevel.PickupTiles = pickupList.ToArray();
            loadedingLevel.exit = loadedExitTile;
            loadedHero.hitPoints = hitpoints;
            loadedHero.DoubleDamageCount = doubleDamageCount;
            loadedingLevel.hero = loadedHero;

            for (int i = 0; i < loadedingLevel.Enemies.GetLength(0); i++)
            {
                if (loadedingLevel.Enemies[i] is GruntTile && enemyStats[i] == "Grunt:" &&
                    loadedingLevel.Enemies[i].X == int.Parse(enemyStats[i + 2]) &&
                    loadedingLevel.Enemies[i].Y == int.Parse(enemyStats[i + 3]))
                {
                    loadedingLevel.Enemies[i].hitPoints = int.Parse(enemyStats[i + 1]);
                }
                else if (loadedingLevel.Enemies[i] is WarlockTile && enemyStats[i] == "Warlock:" &&
                         loadedingLevel.Enemies[i].X == int.Parse(enemyStats[i + 2]) &&
                         loadedingLevel.Enemies[i].Y == int.Parse(enemyStats[i + 3]))
                {
                    loadedingLevel.Enemies[i].hitPoints = int.Parse(enemyStats[i + 1]);
                }
                else if (loadedingLevel.Enemies[i] is TyrantTile && enemyStats[i] == "Tyrant:" &&
                         loadedingLevel.Enemies[i].X == int.Parse(enemyStats[i + 2]) &&
                         loadedingLevel.Enemies[i].Y == int.Parse(enemyStats[i + 3]))
                {
                    loadedingLevel.Enemies[i].hitPoints = int.Parse(enemyStats[i + 1]);
                }
            }

            // Getting the game reddy by assigning the current level to be the loaded one
            this.level = loadedingLevel;
        }



        public string HeroStats
        {
            get { return $"{Level.hero.hitPoints} / {Level.hero.maximumHitPoints}"; }
        }

    }
        public enum Direction
        {
            Up = 0,
            Right = 1,
            Down = 2,
            Left = 3,
            None = 4
        }

    
}
