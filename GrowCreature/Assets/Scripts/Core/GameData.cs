using System.Collections.Generic;

namespace Core
{
    public class GameData
    {
        public static GameData CreateInstance()
        {
            return new GameData();
        }

        public List<PlayerInfo> playerInfoList = new List<PlayerInfo>();
    }
}