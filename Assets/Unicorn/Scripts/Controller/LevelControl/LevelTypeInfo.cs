using System;
using Newtonsoft.Json;
using UnityEngine;

namespace Unicorn
{
    /// <summary>
    /// Hỗ trợ cho <see cref="UnicornDataLevel"/>, viết lại nếu cần.
    /// </summary>
    [Serializable]
    public class LevelTypeInfo
    {
        [JsonProperty] private LevelType levelType;
        [JsonProperty] private int currentLevel = 1;
        [JsonProperty] private int maxLevel = int.MinValue;

        [JsonIgnore]
        public LevelType LevelType
        {
            get => levelType;
            set => levelType = value;
        }
        
        [JsonIgnore]
        public int CurrentLevel
        {
            get => currentLevel;
            set => currentLevel = value;
        }

        [JsonIgnore]
        public int MaxLevel => maxLevel;

        public LevelTypeInfo(LevelType levelType)
        {
            this.levelType = levelType;
        }
        

        // dùng để tự động tăng level
        // nếu mà đã đi qua hết level thì nó quay lại level 1
        public int IncreaseLevel(LevelConstraint levelConstraint)
        {
            var levelCount = levelConstraint.GetLevelCount(levelType);
            currentLevel++;
            maxLevel = Mathf.Max(maxLevel, currentLevel);
            
            if (maxLevel <= levelCount) return currentLevel;
            
            currentLevel = 1;
            maxLevel = 1;
            return currentLevel;
        }


        // để lựa chọn level dựa trên level truyền vào
        // clamp tự động 
        public void SetLevel(int level, LevelConstraint levelConstraint)
        {
            var levelCount = levelConstraint.GetLevelCount(levelType);
            currentLevel = Mathf.Clamp(level, 1, levelCount);
        }
    }
}