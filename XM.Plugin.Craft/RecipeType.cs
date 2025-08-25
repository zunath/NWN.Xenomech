namespace XM.Plugin.Craft
{
    public enum RecipeType
    {
        Invalid = 0,

        Test = 1,

        #region Weaponcraft: 1-3000

        // Daggers (2-25)
        AurionAlloyDagger = 2,
        AurionAlloyKnife = 3,
        Shadowpiercer = 4,
        PhantomKnife = 5,
        ArcDagger = 6,
        BrassfangDagger = 7,
        WhisperfangDagger = 8,
        ArcKnife = 9,
        SparkfangDagger = 10,
        VenomfangBaselard = 11,
        EtherfangKukri = 12,
        ToxinFang = 13,
        ToxinKnife = 14,
        MythriteDagger = 15,
        RangingFang = 16,
        ToxinKukri = 17,
        EtherfangKris = 18,
        StrikerKnife = 19,
        RoguesJambiya = 20,
        MythriteKnife = 21,
        MythriteKukri = 22,
        SparkfangBaselard = 23,
        WarcasterDagger = 24,
        BonefangKnife = 25,
        CorsairsFang = 26,

        // Longswords (27-49)
        AurionAlloyBlade = 27,
        ResonanceBlade = 28,
        VanguardCutter = 29,
        ZenithianArcblade = 30,
        SpathaTekEdge = 31,
        HivefangSpatha = 32,
        BrassArcblade = 33,
        BilbronEdge = 34,
        SparkfangEdge = 35,
        EtherScimitar = 36,
        FerriteSword = 37,
        ArcbladeLongsword = 38,
        DegenTek = 39,
        PulseTuck = 40,
        TitanBroadsword = 41,
        EtherFleuret = 42,
        SteelKiljBlade = 43,
        MythriteDegen = 44,
        SanctifiedEdge = 45,
        MythriteBlade = 46,
        SparkfangDegen = 47,
        RiftTulwar = 48,
        AscendantBlade = 49,
        InfernalDegen = 50,
        CrescentShotel = 51,
        SanctifiedDegen = 52,
        NoviceDuelistsTuck = 53,
        CombatCastersTalon = 54,
        ArcFalchion = 55,
        InfernalEdge = 56,
        VanguardKnightsSword = 57,
        MusketeersEdge = 58,

        // Claws (59-80) - 22 recipes
        PantherClaws = 59,
        EtherCesti = 60,
        AurionAlloyKnuckles = 61,
        BrassstrikeKnuckles = 62,
        TropicStrikers = 63,
        BrassfangClaws = 64,
        HydrofangClaws = 65,
        PrecisionClaws = 66,
        ZephyrClaws = 67,
        ShadowfangClaws = 68,
        AlloyKnuckles = 69,
        PredatorClaws = 70,
        RavagerClaws = 71,
        HydrofangTalons = 72,
        TyroWarKatars = 73,
        WarKatars = 74,
        ToxinfangClaws = 75,
        ToxinfangTalons = 76,
        MythriteKnuckles = 77,
        EtherwovenStraps = 78,
        MythriteClaws = 79,
        ArcstrikeAdargas = 80,
        PredatorPatas = 81,
        TacticianMagiciansTalons = 82,

        // Clubs (83-102) - 20 recipes
        AshStriker = 83,
        MapleConduit = 84,
        AurionAlloyMace = 85,
        AurionAlloyHammer = 86,
        AurionAlloyRod = 87,
        WillowConduit = 88,
        BrassforceRod = 89,
        BrassstrikeHammer = 90,
        YewConduit = 91,
        TitanMace = 92,
        WarbringerHammer = 93,
        ArcaneRod = 94,
        EremitesEtherWand = 95,
        BonecrusherCudgel = 96,
        TitanMaul = 97,
        MythriteRod = 98,
        MythriteMace = 99,
        OakfangCudgel = 100,
        HallowedMaul = 101,
        TitanClub = 102,
        BoneforgedRod = 103,
        HallowedMace = 104,
        RosethornConduit = 105,
        TacticianMagiciansConduit = 106,

        // Great Axes (107-123) - 17 recipes
        TraineeWaraxe = 107,
        RazorwingAxe = 108,
        InfernalCleaver = 109,
        TitanGreataxe = 110,
        HydroCleaver = 111,
        MothfangAxe = 112,
        VanguardSplitter = 113,
        VerdantSlayer = 114,
        CenturionsCleaver = 115,
        TwinfangAxe = 116,
        ReapersVoulge = 117,
        TitanWaraxe = 118,
        KhetenWarblade = 119,
        JuggernautMothAxe = 120,
        TewhaReaver = 121,
        LeucosReapersVoulge = 122,

        // Greatswords (124-139) - 16 recipes
        CorrodedGreatblade = 124,
        TitanClaymore = 125,
        GaleforgedClaymore = 126,
        VulcanWarblade = 127,
        MercenarysFang = 128,
        VanguardBattalionSword = 129,
        UnmarkedGreatsword = 130,
        WarbornBlade = 131,
        ZephyrCutter = 132,
        AbyssalBlade = 133,
        ReapersFalx = 134,
        MythriteClaymore = 135,
        RegalVanguardBlade = 136,
        TitanGreatsword = 137,
        ZephyrWarblade = 138,
        ExecutionersFaussar = 139,
        CobraWarblade = 140,

        // Staff (141-155) - 15 recipes
        AshenWarstaff = 141,
        AshenPolearm = 142,
        VerdantHollyStaff = 143,
        VerdantHollyPolearm = 144,
        ElmwoodWarstaff = 145,
        SpikedMaul = 146,
        ElmwoodPolearm = 147,
        LeviathanStaff = 148,
        OakwoodWarstaff = 149,
        PassaddhiMysticStaff = 150,
        OakwoodPolearm = 151,
        QiResonanceStaff = 152,
        MusketeersPolearm = 153,
        TundraWalrusStaff = 154,

        // Short Swords (156-168) - 13 recipes
        PhantomFang = 156,
        Stormpiercer = 157,
        GaleFang = 158,
        ShadowfangBlade = 159,
        ObsidianFang = 160,
        VanguardFang = 161,
        SkylarkEdge = 162,
        SwiftclawSword = 163,
        TempestFang = 164,
        FractureFang = 165,
        TrainingBlade = 166,
        Cherryblade = 167,
        TwilightFang = 168,
        CrimsonWing = 169,
        TwinstrikeBlade = 170,
        Nyxblade = 171,

        // Axes (172-183) - 12 recipes
        AurionAlloyAxe = 172,
        BrassstrikeAxe = 173,
        BonecleaverAxe = 174,
        BoneforgedPick = 175,
        TitanBattleaxe = 176,
        RaidersTomahawk = 177,
        WarbornPick = 178,
        MythriteAxe = 179,
        WarcasterAxe = 180,
        VeldtCleaver = 181,
        ReaversTabar = 182,
        MythritePick = 183,

        // Polearms (184-195) - 12 recipes
        StormHarpoon = 184,
        AurionAlloySpear = 185,
        BrassstrikeSpear = 186,
        SparkfangSpear = 187,
        VanguardLance = 188,
        TitanSpear = 189,
        SparkfangLance = 190,
        TitanLance = 191,
        WarbornHalberd = 192,
        ObeliskWarLance = 193,
        MythriteLance = 194,
        RoyalKnightArmyWarLance = 195,

        // Throwing (196-204) - 9 recipes
        CoarseShuriken = 196,
        ArcShuriken = 197,
        VanguardShuriken = 198,
        WingShuriken = 199,
        EtherShuriken = 200,
        LongstrikeShuriken = 201,
        WarcastersShuriken = 202,
        NoviceDuelistsShuriken = 203,
        YagudoCryoShuriken = 204,
        CometFangShuriken = 205,

        #endregion


        #region Armorcraft: 3001-6000
        
        // Head Armor (3001-3060) - 60 recipes
        AurionAlloyCap = 3001,                      // Level 1
        LeatherTacticalBandana = 3002,              // Level 23
        CombatHachimaki = 3003,                     // Level 24
        WayfarersHat = 3004,                        // Level 27
        LegionnairesHelm = 3005,                    // Level 39
        TacticalHeadgear = 3006,                    // Level 42
        VanguardFaceguard = 3007,                   // Level 43
        MonksWargear = 3008,                        // Level 44
        BrassguardCap = 3009,                       // Level 48
        RegalFootmansBandana = 3010,                // Level 49
        SagesEtherCirclet = 3011,                   // Level 49
        IroncladMask = 3012,                        // Level 50
        FederationCombatWrap = 3013,                // Level 53
        PrecisionCombatBandana = 3014,              // Level 53
        ReflexHeadband = 3015,                      // Level 54
        WoolenTacticalHat = 3016,                   // Level 55
        ScholarsCirclet = 3017,                     // Level 57
        BrassguardMask = 3018,                      // Level 57
        KampfschallerWarHelm = 3019,                // Level 57
        CenturionsWarVisor = 3020,                  // Level 57
        LegionnairesEtherCirclet = 3021,            // Level 57
        MercenaryCaptainsWargear = 3022,            // Level 57
        HoshikazuWarwrap = 3023,                    // Level 58
        EisenwarHelm = 3024,                        // Level 58
        ReinforcedBandana = 3025,                   // Level 59
        BoneforgedMask = 3026,                      // Level 60
        EruditesMindband = 3027,                    // Level 61
        EarthwovenHachimaki = 3028,                 // Level 61
        NoctshadeBeret = 3029,                      // Level 61
        CottonWeaveHeadband = 3030,                 // Level 62
        RegalSquiresHelm = 3031,                    // Level 62
        LizardscaleHelm = 3032,                     // Level 65
        CorrodedWarCap = 3033,                      // Level 67
        PaddedCombatCap = 3034,                     // Level 68
        CottonCombatWrap = 3035,                    // Level 69
        FortifiedGuardCap = 3036,                   // Level 70
        UnmarkedCombatCap = 3037,                   // Level 72
        GarrisonWarSallet = 3038,                   // Level 74
        CrimsonWarCap = 3039,                       // Level 74
        SilveredMask = 3040,                        // Level 74
        IroncladVisor = 3041,                       // Level 74
        IronMusketeersBattleArmet = 3042,           // Level 77
        LeatherboundBandana = 3043,                 // Level 79
        WarbornSallet = 3044,                       // Level 82
        MercenarysWarwrap = 3045,                   // Level 85
        ChitinPlatedMask = 3046,                    // Level 85
        ValkyrianMask = 3047,                       // Level 86
        ChitinPlatedMask2 = 3048,                   // Level 87
        WalkureMask = 3049,                         // Level 89
        BandedWarhelm = 3050,                       // Level 89
        CorsairsTricorne = 3051,                    // Level 91
        WoolenWarCap = 3052,                        // Level 93
        CottonfieldHeadgear = 3053,                 // Level 94
        RaptorstrikeHelm = 3054,                    // Level 95
        MythriteSallet = 3055,                      // Level 96
        ShinobiShadowguard = 3056,                  // Level 97
        SteelguardVisor = 3057,                     // Level 98
        ObsidianCrowBeret = 3058,                   // Level 100
        TacticiansArcaneHat = 3059,                 // Level 100
        ShockguardMask = 3060,                      // Level 100

        // Body Armor (3061-3109) - 49 recipes
        AurionAlloyHarness = 3061,                  // Level 2
        EtherwovenRobe = 3062,                      // Level 3
        LeatherCombatVest = 3063,                   // Level 12
        WarwovenKenpogi = 3064,                     // Level 16
        ReinforcedTunic = 3065,                     // Level 18
        BrassguardHarness = 3066,                   // Level 20
        ReinforcedDoublet = 3067,                   // Level 22
        TitanScaleMail = 3068,                      // Level 23
        EtherwovenLinenRobe = 3069,                 // Level 23
        LizardscaleJerkin = 3070,                   // Level 34
        BoneforgedHarness = 3071,                   // Level 35
        CottonWovenDogi = 3072,                     // Level 37
        ObsidianTunic = 3073,                       // Level 38
        ChitinPlatedHarness = 3074,                 // Level 42
        TitanChainmail = 3075,                      // Level 47
        CottonfieldDoublet = 3076,                  // Level 49
        BrassguardScaleMail = 3077,                 // Level 52
        WarbornKite = 3078,                         // Level 56 (Shield, moved to body for now)
        WarWovenWoolRobe = 3079,                    // Level 56
        Eisenbreastplate = 3080,                    // Level 56
        OraclesTunic = 3081,                        // Level 57
        NoctshadowDoublet = 3082,                   // Level 57
        EarthforgedGi = 3083,                       // Level 58
        ReinforcedStuddedVest = 3084,               // Level 62
        WarcastersCloak = 3085,                     // Level 65
        EtherwovenDoublet = 3086,                   // Level 66
        BishopsSanctifiedRobe = 3087,               // Level 67
        PaddedCombatArmor = 3088,                   // Level 69
        FortifiedGambison = 3089,                   // Level 69
        RegalVelvetRobe = 3090,                     // Level 73
        SilverguardMail = 3091,                     // Level 74
        IroncladScaleMail = 3092,                   // Level 74
        BattleHardenedCuirass = 3093,               // Level 77
        WarcastersCloak2 = 3094,                    // Level 79
        IronMusketeersWarGambison = 3095,           // Level 79
        RegalSquiresChainmail = 3096,               // Level 80
        RegalSquiresEtherRobe = 3097,               // Level 81
        TitanBreastplate = 3098,                    // Level 82
        DivineAegisBreastplate = 3099,              // Level 82
        WoolenCombatDoublet = 3100,                 // Level 87
        ReinforcedBrigandine = 3101,                // Level 87
        BandedWarMail = 3102,                       // Level 89
        ChitinPlatedHarness2 = 3103,                // Level 90
        WoolenWarGambison = 3104,                   // Level 95
        MythriteBreastplate = 3105,                 // Level 95
        ObsidianCrowJupon = 3106,                   // Level 97
        DinohideJerkin = 3107,                      // Level 99
        SteelguardScaleMail = 3108,                 // Level 99
        ShinobiShadowGi = 3109,                     // Level 100
        SanctifiedWhiteCloak = 3110,                // Level 98

        // Hands Armor (3111-3157) - 47 recipes
        ReinforcedCuffs = 3111,                     // Level 8
        AurionAlloyMittens = 3112,                  // Level 9
        LeatherCombatGloves = 3113,                 // Level 11
        BattleforgedTekko = 3114,                   // Level 14
        WarforgedMitts = 3115,                      // Level 15
        TitanScaleGauntlets = 3116,                 // Level 17
        BrassguardMittens = 3117,                   // Level 19
        EtherwovenCuffs = 3118,                     // Level 23
        CombatGloves = 3119,                        // Level 24
        BoneforgedMittens = 3120,                   // Level 29
        LizardscaleCombatGloves = 3121,             // Level 35
        CottonWovenTekko = 3122,                    // Level 35
        ChitinPlatedMittens = 3123,                 // Level 39
        SanctifiedWhiteMitts = 3124,                // Level 41
        CottonCombatGloves = 3125,                  // Level 48
        TitanChainMittens = 3126,                   // Level 51
        DevoteesSanctifiedMitts = 3127,             // Level 53
        BrassguardFingerGauntlets = 3128,           // Level 57
        NoctshadowGloves = 3129,                    // Level 57
        WoolenCombatCuffs = 3130,                   // Level 58
        EisenwroughtGauntlets = 3131,               // Level 58
        CombatReinforcedMittens = 3132,             // Level 59
        EarthforgedTekko = 3133,                    // Level 60
        OraclesMitts = 3134,                        // Level 61
        ReinforcedStuddedGloves = 3135,             // Level 61
        IroncladMittens = 3136,                     // Level 69
        SilverguardMittens = 3137,                  // Level 70
        EtherwovenMitts = 3138,                     // Level 71
        IroncladFingerGauntlets = 3139,             // Level 71
        CombatBracers = 3140,                       // Level 74
        LeatherboundGloves = 3141,                  // Level 74
        RegalVelvetCuffs = 3142,                    // Level 76
        TitanGauntlets = 3143,                      // Level 79
        WarcastersMitts = 3144,                     // Level 81
        IronMusketeersBattleGauntlets = 3145,       // Level 81
        SilverPlatedBangles = 3146,                 // Level 83
        ChitinPlatedMittens2 = 3147,                // Level 88
        InsulatedMufflers = 3148,                   // Level 92
        TideshellBangles = 3149,                    // Level 92
        RaptorstrikeGloves = 3150,                  // Level 93
        SteelguardFingerGauntlets = 3151,           // Level 93
        WoolenCombatBracers = 3152,                 // Level 96
        MythriteGauntlets = 3153,                   // Level 97
        ShinobiShadowTekko = 3154,                  // Level 97
        ObsidianMitts = 3155,                       // Level 97
        ObsidianCrowBracers = 3156,                 // Level 97
        TacticianMagiciansWarCuffs = 3157,          // Level 100

        // Feet Armor (3158-3203) - 46 recipes
        AshenStrideClogs = 3157,                    // Level 4
        AurionAlloyLeggings = 3158,                 // Level 7
        LeatherCombatHighboots = 3159,              // Level 11
        WarwovenKyahan = 3160,                      // Level 13
        EtherTouchedSoleas = 3161,                  // Level 16
        ReinforcedGaiters = 3162,                   // Level 19
        BrassguardLeggings = 3163,                  // Level 20
        TitanScaleGreaves = 3164,                   // Level 23
        VerdantHollyClogs = 3165,                   // Level 27
        LizardscaleLedelsens = 3166,                // Level 32
        BoneforgedLeggings = 3167,                  // Level 33
        CottonWovenKyahan = 3168,                   // Level 33
        MettleforgedLeggings = 3169,                // Level 35
        ChitinPlatedLeggings = 3170,                // Level 39
        ArcaneWeaversSandals = 3171,                // Level 42
        WingstrideBoots = 3172,                     // Level 48
        CottonWovenGaiters = 3173,                  // Level 49
        TitanWarGreaves = 3174,                     // Level 49
        BrassguardGreaves = 3175,                   // Level 55
        EarthforgedKyahan = 3176,                   // Level 55
        ZephyrSoleas = 3177,                        // Level 56
        EisensteelBoots = 3178,                     // Level 57
        OraclesPumps = 3179,                        // Level 58
        NoctshadowGaiters = 3180,                   // Level 58
        ReinforcedStuddedBoots = 3181,              // Level 58
        CombatLeggings = 3182,                      // Level 67
        EtherwovenShoes = 3183,                     // Level 69
        InsulatedSocks = 3184,                      // Level 70
        SilverguardGreaves = 3185,                  // Level 73
        LeatherboundHighboots = 3186,               // Level 73
        IroncladGreaves = 3187,                     // Level 75
        ObsidianSabots = 3188,                      // Level 76
        IronMusketeersBattleSabatons = 3189,        // Level 77
        PlateforgedLeggings = 3190,                 // Level 80
        WarcastersShoes = 3191,                     // Level 82
        InfernalSabots = 3192,                      // Level 85
        ChitinPlatedLeggings2 = 3193,               // Level 87
        WarbornSollerets = 3194,                    // Level 91
        RaptorstrikeLedelsens = 3195,               // Level 94
        SteelguardGreaves = 3196,                   // Level 96
        MythriteLeggings = 3197,                    // Level 97
        ObsidianCrowGaiters = 3198,                 // Level 97
        WoolenInsulatedSocks = 3199,                // Level 98
        ReinforcedMoccasins = 3200,                 // Level 98
        ShinobiShadowKyahan = 3201,                 // Level 100
        TacticianMagiciansWarPigaches = 3202,       // Level 100

        // Shield Armor (3203-3220) - 18 recipes
        VerdantGuard = 3203,                        // Level 5
        AbyssalBarrier = 3204,                      // Level 6
        ShellbackAegis = 3205,                      // Level 14
        TimberguardShield = 3206,                   // Level 16
        SentinelAegis = 3207,                       // Level 18
        ElmguardShield = 3208,                      // Level 26
        TitanTarge = 3209,                          // Level 32
        TropicWard = 3210,                          // Level 34
        LanternAegis = 3211,                        // Level 38
        TideshellShield = 3212,                     // Level 48
        WarbornKiteShield = 3213,                   // Level 56
        OakheartShield = 3214,                      // Level 72
        SylphicAegis = 3215,                        // Level 78
        RegalSquiresGuard = 3216,                   // Level 80
        EmberplateHeater = 3217,                    // Level 86
        WarbornBuckler = 3218,                      // Level 90
        LeathercladGuard = 3219,                    // Level 98
        RoyalKnightArmyBulwark = 3220,              // Level 100

        #endregion


        #region Engineering: 6001-9000

        // Rifles (6001-6009) - 9 recipes
        HakenbuechseRifle = 6001,           // Level 2
        VanguardMusket = 6002,              // Level 10
        MaraudersRifle = 6003,              // Level 20
        TanegashimaRifle = 6004,            // Level 30
        ArquebusRifle = 6005,               // Level 44
        CorsairsRifle = 6006,               // Level 57
        MarsHexRifle = 6007,                // Level 60
        DarksteelHexRifle = 6008,           // Level 74
        NegoroshikiRifle = 6009,            // Level 84
        SeadogRepeater = 6010,              // Level 100

        // Pistols (6011-6020) - 10 recipes
        ArcstrikePistol = 6011,             // Level 3
        LegionnairesRepeater = 6012,        // Level 21
        VanguardRepeater = 6013,            // Level 24
        BastionRepeater = 6014,             // Level 31
        ZamburakAuto = 6015,                // Level 61
        RikonodoStriker = 6016,             // Level 73
        HuntersFang = 6017,                 // Level 80
        TellsMarksman = 6018,               // Level 88
        TrackersSidearm = 6019,             // Level 91
        EtherboltArbalest = 6020,           // Level 97

        // Bows (6021-6034) - 14 recipes
        StrikerShortbow = 6021,             // Level 5
        VanguardLongbow = 6022,             // Level 13
        ArcwoodBow = 6023,                  // Level 17
        RegalArchersWarbow = 6024,          // Level 23
        SandusRecurve = 6025,               // Level 33
        PowerstrikeBow = 6026,              // Level 35
        WrappedWarbow = 6027,               // Level 51
        TitanGreatbow = 6028,               // Level 63
        ShadowpiercerBow = 6029,            // Level 71
        CompositeRecurve = 6030,            // Level 75
        BattleforgedBow = 6031,             // Level 83
        KamanStriker = 6032,                // Level 90
        WarbornBow = 6033,                  // Level 99

        #endregion


        #region Fabrication: 9001-12000

        // Ring recipes (9001-9030) - 30 recipes
        CopperforgeRing = 9001,                         // Level 1
        BrassguardRing = 9002,                          // Level 7
        EremitesFocusRing = 9003,                       // Level 17
        SentinelsSafeguardRing = 9004,                  // Level 20
        SanctifiedBand = 9005,                          // Level 20
        ReflexResonanceRing = 9006,                     // Level 25
        AmberResonanceRing = 9007,                      // Level 26
        EquilibriumRing = 9008,                         // Level 26
        ValorRing = 9009,                               // Level 26
        LapisArcaneRing = 9010,                         // Level 26
        EtherflowRing = 9011,                           // Level 27
        EnduranceRing = 9012,                           // Level 27
        ObsidianOnyxRing = 9013,                        // Level 29
        AmethystArcRing = 9014,                         // Level 30
        BeaconofHopeRing = 9015,                        // Level 30
        LeatherboundRing = 9016,                        // Level 31
        SilverguardRing = 9017,                         // Level 31
        BoneforgedRing = 9018,                          // Level 33
        ChitinPlatedRing = 9019,                        // Level 44
        MythriteBand = 9020,                            // Level 45
        MarksmansRing = 9021,                           // Level 57
        SmilodonFangRing = 9022,                        // Level 62
        DeftstrikeRing = 9023,                          // Level 69
        VerveEtherflowRing = 9024,                      // Level 70
        AlacrityResonanceRing = 9025,                   // Level 71
        HornedCrestRing = 9026,                         // Level 72
        OathkeepersLoyaltyRing = 9027,                  // Level 72
        SolaceGuardianRing = 9028,                      // Level 72
        PuissanceBattleRing = 9029,                     // Level 75
        ElectrumArcRing = 9030,                         // Level 77

        // Additional Ring recipes (9031-9040) - Continuing ring recipes
        SharpshootersRing = 9031,                       // Level 83
        WoodsmansTrackerRing = 9032,                    // Level 85
        AuricGoldRing = 9033,                           // Level 87
        PhalanxDefenderRing = 9034,                     // Level 97

        // Waist recipes (9035-9060) - 26 recipes
        BloodforgedStone = 9035,                        // Level 3
        WovenHekoObi = 9036,                            // Level 13
        LeatherboundBelt = 9037,                        // Level 17
        TitanPlateBelt = 9038,                          // Level 21
        LizardscaleBelt = 9039,                         // Level 31
        WarriorsWarBelt = 9040,                         // Level 32
        ArcaneInfusedBelt = 9041,                       // Level 35
        MohbwaWardenSash = 9042,                        // Level 39
        SilverthreadObi = 9043,                         // Level 40
        ReinforcedChainBelt = 9044,                     // Level 50
        ForceboundBelt = 9045,                          // Level 53
        GriotsResonanceBelt = 9046,                     // Level 55
        ShamansEtherBelt = 9047,                        // Level 57
        HojutsuCombatBelt = 9048,                       // Level 64
        AuricGoldObi = 9049,                            // Level 66
        SilverguardBelt = 9050,                         // Level 67
        ReinforcedCorsette = 9051,                      // Level 80
        TacticalWaistbelt = 9052,                       // Level 80
        QiqirnScoutsSash = 9053,                        // Level 82
        WarforgedSwordbelt = 9054,                      // Level 86
        LifewardenBelt = 9055,                          // Level 97

        // Back recipes (9061-9084) - 24 recipes  
        HarestrideMantle = 9061,                        // Level 5
        EtherwovenCape = 9062,                          // Level 13
        WayfarersMantle = 9063,                         // Level 25
        DhalmelhideMantle = 9064,                       // Level 36
        LizardscaleMantle = 9065,                       // Level 37
        CottonWovenCape = 9066,                         // Level 38
        NomadsTrailMantle = 9067,                       // Level 46
        BonzesEtherCape = 9068,                         // Level 50
        PrecisionMantle = 9069,                         // Level 51
        DirewolfMantle = 9070,                          // Level 56
        MidnightShadowCape = 9071,                      // Level 62
        ObsidianCloak = 9072,                           // Level 67
        RamguardMantle = 9073,                          // Level 73
        CavaliersWarMantle = 9074,                      // Level 73
        CrimsonDrape = 9075,                            // Level 83
        AuroraSkyMantle = 9076,                         // Level 88
        ShadowJaguarMantle = 9077,                      // Level 94
        RaptorstrikeMantle = 9078,                      // Level 99
        ArcaneVeilMantle = 9079,                        // Level 99
        TitanWarMantle = 9080,                          // Level 100
        SharpshootersMantle = 9081,                     // Level 100

        // Neck recipes (9082-9106) - 25 recipes
        LeatherboundGorget = 9082,                      // Level 9
        PlumeCollar = 9083,                             // Level 11
        JudicatorsBadge = 9084,                         // Level 15
        VerdantScarf = 9085,                            // Level 17
        TitanScaleGorget = 9086,                        // Level 21
        RangersPendant = 9087,                          // Level 27
        AvianWhistle = 9088,                            // Level 32
        FocusedResonanceCollar = 9089,                  // Level 35
        ObsidianSilkNeckerchief = 9090,                 // Level 38
        ChitinPlatedGorget = 9091,                      // Level 39
        VerdantGorget = 9092,                           // Level 40
        PredatorFangNecklace = 9093,                    // Level 44
        TigerfangStole = 9094,                          // Level 45
        WovenHempGorget = 9095,                         // Level 47
        BeastmastersWhistle = 9096,                     // Level 49
        TitanChainGorget = 9097,                        // Level 50
        SanctifiedPhial = 9098,                         // Level 50
        DirewolfGorget = 9099,                          // Level 57
        AegisShieldPendant = 9100,                      // Level 72
        MedievalWarCollar = 9101,                       // Level 73
        ReinforcedGorget = 9102,                        // Level 80
        MohbwaWardenScarf = 9103,                       // Level 81
        AzureGorget = 9104,                             // Level 91
        WarlordsNodowa = 9105,                          // Level 99
        MoonlitArcTorque = 9106,                        // Level 99

        // Additional Neck recipes (9107-9110)
        MythriteGorget = 9107,                          // Level 100
        RaptorBeakNecklace = 9108,                      // Level 100
        IntellectResonanceTorque = 9109,                // Level 100

        #endregion


        #region Synthesis: 15001-18000

        // Head accessories (15001-15013) - 13 recipes (all mystical head accessories)
        EtherTouchedCirclet = 15001,                    // Level 1
        MysticHairpin = 15002,                          // Level 12
        ArcaneBand = 15003,                             // Level 18
        CrystalCirclet = 15004,                         // Level 26
        ResonanceHeadband = 15005,                      // Level 34
        ElementalCrown = 15006,                         // Level 42
        MythriteTiara = 15007,                          // Level 50
        PsiCrystalCirclet = 15008,                      // Level 58
        VoidShadowDiadem = 15009,                       // Level 66
        QuantumForgedCrown = 15010,                     // Level 74
        TemporalFluxCirclet = 15011,                    // Level 82
        NanoEnchantedTiara = 15012,                     // Level 90
        EthertechMasterDiadem = 15013,                  // Level 97

        #endregion
    }
}
