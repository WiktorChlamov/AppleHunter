using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Localization
{   public static Lang lang;
    public enum Lang
    {
        Eng,Rus
    }
    public static Dictionary<int, (string, string)> dict = new Dictionary<int, (string, string)>
    {
        [0] =("Add extra points for apple: ", "Дополнительные очки за яблоко: ") ,
        [1] =("Add extra lifes: ","Дополнительные жизни: ") ,
        [2] =("Destroy knifes: ","Уничтожить ножи: "),
        [3] =("Slow target's rotating. ","Замедление вращения цели. "),
        [4] =("Faster life recovering. ", "Ускорение восстановления жизни. "),
        [5] = ("Spawning extra apples: ","Появление дополнительных яблок: "),
        [6] = ("For life changing ", "В обмен на жизнь "),
        [7] = ("Game starts from 51 stage", "Игра начинается с 51 уровня"),

        [100]=("level","уровень"),
        [101]=("Stage: ","Уровень: "),
        [102]=("Score: ","Очки: "),
        [103]=("Hits: ","Попаданий: "),
        [104] = ("Lifes: ", "Жизни: "),
        [105] = ("Oppening: ", "Открытие: "),

        [200]=("points","очков"),
        [201]=("hitting by knife in raw ","попаданий ножом вподряд"),
        [202]=("Reach level: ","Достигнуть уровень: "),
        [203]=("Watch video: ", "Посмотреть видео: "),

        [300]=("Hit Record: ","Рекорд попаданий: "),
        [301]=("Stage record: ","Рекордный уровень: "),

        [500] = ("Casual", "Обычный"),
        [501] = ("Sunny", "Солнечный"),
        [502] = ("Tomato", "Томатный"),
        [503] = ("Greeny", "Зелёненький"),
        [504] = ("Dangerous", "Опасный"),
        [505] = ("Molten", "Раскалённый"),
        [506] = ("Slim", "Тонкий"),
        [507] = ("Apple Eater","Пожиратель яблок"),
        [508] = ("Rainbow", "Радужный"),
        [509] = ("Pumpkin", "Тыквенный"),
        [510] = ("Timeless", "Вневременный"),
        [511] = ("Dragon", "Дракон")
    };
    public enum KnifeNames
    {
        Casual=500,
        Sun,
        Tomato,
        Greeny,
        Dangerous,
        Molten,
        Slim,
        AppleEater,
        Rainbow,
        Pumpkin,
        Timeless,
        Dragon
    }
    public enum PerkLoc
    {
        Points =0,
        Life,
        KnifesRemoving,
        SlowingRotate,
        MoreLifeCharging,
        AppleSpawning,
        TakeLife,
        SetStage
    }
    public enum Record
    {
        HitRecord =300,
        StageRecord,
    }
    public enum PerkCost
    {
        Score =200,
        Hitting,
        Level,
        Ads
    }
    public enum Other
    {
        Level =100,
        Stage,
        Score,
        Hitting,
        Lifes,
        Oppening
    }
    public static string Translate(int text)
    {
        switch (lang)
        {
            case Lang.Eng:
                return dict[text].Item1;
            case Lang.Rus:
                return dict[text].Item2;
            default: return "";
        }
    }
    public static void ChangeLang(Lang language)
    {
        lang = language;
    }
}