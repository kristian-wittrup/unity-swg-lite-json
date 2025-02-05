public enum MitigationLevel
{
    None, // 0%
    Level1, // 20%
    Level2, // 40%
    Level3 // 60%
}

public static class Mitigation
{
    public static float GetMitigationValue(MitigationLevel level)
    {
        switch (level)
        {
            case MitigationLevel.Level1:
                return 0.20f;
            case MitigationLevel.Level2:
                return 0.40f;
            case MitigationLevel.Level3:
                return 0.60f;
            default:
                return 0.0f;
        }
    }
}