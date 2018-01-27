[System.Flags]
public enum UnitType
{
    None = 0,
    Archers = 1,
    Horsemen = 2,
    Spearmen = 4,


    ArchersHorsemen = Archers | Horsemen,
    ArchersSpearmen = Archers | Spearmen,
    HorsemenSpearmen = Horsemen | Spearmen,

    ArchersHorsemenSpearmen = Archers | Horsemen | Spearmen
}

