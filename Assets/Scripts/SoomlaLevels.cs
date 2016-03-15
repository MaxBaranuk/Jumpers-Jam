//using UnityEngine;
//using System.Collections;
//using Soomla.Levelup;
//using Soomla;
//using System.Collections.Generic;

//public class SoomlaLevels {

//    public World CreateInitialWorld() {

//        Score pointScore = new Score(
//      "pointScore_ID",                            // ID
//      "Point Score",                              // Name
//      true                                        // Higher is better
//    );

//        Reward medalReward = new BadgeReward(
//      "medalReward_ID",                           // ID
//      "Medal Reward"                              // Name
//    );

//        Mission level1 = new RecordMission(
//      "pointMission_ID",                          // ID
//      "Point Mission",                            // Name
//      new List<Reward>() { medalReward },         // Rewards
//      pointScore.ID,                              // Associated score
//      3                                           // Desired record
//    );

//        Mission level2 = new RecordMission(
//      "pointMission_ID",                          // ID
//      "Point Mission",                            // Name
//      new List<Reward>() { medalReward },        // Rewards
//      pointScore.ID,                              // Associated score
//      3                                           // Desired record
//    );

//        World mainWorld = new World(
//      "main_world", null, null, null,
//      new List<Mission>() { level1, level2}
//    );

//    //    mainWorld.BatchAddLevelsWithTemplates(
//    //  6,                                          // Number of levels
//    //  null,                                       // Gate template
//    //  new List<Score>() { pointScore },             // Score templates
//    //  null                                        // Mission templates
//    //);
//        return mainWorld;

//    }
//}
