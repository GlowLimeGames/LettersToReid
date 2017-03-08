/*
 * Author(s): Jeffrey Abt
 * Description: Handles Save Data Creation, Saving, and Loading
 * Usage: methods saveGame and loadGame return a boolean value as a measure of whether the save/load operation was a success
 *          GameSave save holds the game data to be saved and loaded
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Data.Core
{
    [Serializable]
    class GameSaveController
    {
        private GameSave save;
        private bool saveExists = false;

        public bool saveGame(GameSave game)
        {
            bool gameSaved = false;
            save = game;
            /*
             * if saveExists == false, create savegame
             * else ask if overwrite is ok, replace save file with current save data
             */

            return gameSaved;
        }

        private void createSaveFile()
        {
            //creates a file on the hard disk containing savedata
            //used as part of saveGame method
        }

        public bool loadGame(int saveSlot )
        {
            //loads the save data from a file into the GameSave save variable
            bool gameLoaded = false;
            

            return gameLoaded;
        }

    }
}
