using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hero_Adventure
{
    public class GameSaveData
    {
        public void SaveGame(int numberOfLevels, int levelnumber, string level, Tile[,] tiles, HeroTile hero, EnemyTile[] enemyTiles)
        {
            const char DELIMITER = ',';
            const string FILE_NAME = "SaveFile.txt";

            // Opening Bridge
            FileStream fStream = new FileStream(FILE_NAME, FileMode.Create, FileAccess.Write);

            // Allow traffic across the bridge
            StreamWriter sWriter = new StreamWriter(fStream);

            // Storing number of level,level number,level size,
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
    }
}
