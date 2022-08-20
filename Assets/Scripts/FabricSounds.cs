using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FabricSounds : MonoBehaviour {
    [SerializeField] private GameObject mainCamera = null;

    [SerializeField] private ControlSoundMusic soundMonstrKillPrefab = null;
    [SerializeField] private ControlSoundMusic soundHitWallPrefab = null;
    [SerializeField] private ControlSoundMusic soundPlayerWinPrefab = null;

    public const string SOUND_MONSTR_KILL_PREFAB_CONST = "SOUND_MONSTR_KILL_PREFAB_CONST";
    public const string SOUND_HIT_WALL_PREFAB_CONST = "SOUND_HIT_WALL_PREFAB_CONST";
    public const string SOUND_PLAYER_WIN_GAME_PREFAB_CONST = "SOUND_PLAYER_WIN_GAME_PREFAB_CONST";

    public void CreateSound(string soundType) {
        if(SOUND_MONSTR_KILL_PREFAB_CONST.Trim() == soundType) {
            ControlSoundMusic script = Instantiate(soundMonstrKillPrefab);
            script.SetTargetCamera(mainCamera);
            return;
        }

        if(SOUND_HIT_WALL_PREFAB_CONST.Trim() == soundType) {
            ControlSoundMusic script = Instantiate(soundHitWallPrefab);
            script.SetTargetCamera(mainCamera);
            return;
        }

        if(SOUND_PLAYER_WIN_GAME_PREFAB_CONST.Trim() == soundType) {
            ControlSoundMusic script = Instantiate(soundPlayerWinPrefab);
            script.SetTargetCamera(mainCamera);
            return;
        }
    }
}
