using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SFX
{
    Shot,
    Hit
}
public class SoundManager : Singleton<SoundManager>
{
    [SerializeField] private List<SFXObject> SFXList;

    private void Awake()
    {
        CheckIfShouldPopulateSFXList();
    }
    private void CheckIfShouldPopulateSFXList()
    {
        if (SFXList == null)
            SFXList = new List<SFXObject>();
        if (SFXList.Count == 0)
        {
            for (var i = 0; i < transform.childCount; i++)
            {
                var childGO = this.gameObject.transform.GetChild(i);
                SFXList.Add(new SFXObject(childGO.name, childGO.GetComponent<AudioSource>(), (childGO.GetComponent<SoundManagerSFXList>() == null) ? false : true));
            }
        }
    }
    public void PlaySFX(string name)
    {
        int index = SFXList.FindIndex((SFXObject sFXObject) => sFXObject.name == name);

        if (index < 0)
        {
            Debug.LogError("Index out of range. " + name + " not found. It's correct name of this sound?");
            return;
        }
        if (SFXList[index].isRandom)
            PlayRandom(index);
        else
            SFXList[index].audioSource.Play();

    }

    public AudioSource GetAudioSource(string name)
    {
        int index = SFXList.FindIndex((SFXObject sFXObject) => sFXObject.name == name);
        if (index < 0)
        {
            Debug.LogError("Index out of range. " + name + " not found. It's correct name of this sound? Creating totaly bland audio source");
            return new AudioSource();
        }
        return SFXList[index].audioSource;
    }

    public void StopSFX(string name)
    {
        int index = SFXList.FindIndex((SFXObject sFXObject) => sFXObject.name == name);

        if (index < 0)
        {
            Debug.LogError("Index out of range. " + name + " not found. It's correct name of this sound?");
            return;
        }
        SFXList[index].audioSource.Stop();
    }

    public void PlaySFXWithPitch(string name, float pitch)
    {
        int index = SFXList.FindIndex((SFXObject sFXObject) => sFXObject.name == name);

        if (index < 0)
        {
            Debug.LogError("Index out of range. " + name + " not found. It's correct name of this sound?");
            return;
        }
        if (SFXList[index].isRandom)
            PlayRandom(index);
        else
        {
            SFXList[index].audioSource.pitch = pitch;
            SFXList[index].audioSource.Play();
        }

    }

    public void PlaySFXOneShot(string name)
    {
        int index = SFXList.FindIndex((SFXObject sFXObject) => sFXObject.name == name);

        if (index < 0)
        {
            Debug.LogWarning("Index out of range. " + name + " not found. It's correct name of this sound?");
            return;
        }
        if (SFXList[index].isRandom)
            PlayOneShotRandom(index);
        else
            SFXList[index].audioSource.PlayOneShot(SFXList[index].audioSource.clip);
    }

    public void PlaySFXOneShot(string name, float volumeScale)
    {
        int index = SFXList.FindIndex((SFXObject sFXObject) => sFXObject.name == name);

        if (index < 0)
        {
            Debug.LogWarning("Index out of range. " + name + " not found. It's correct name of this sound?");
            return;
        }
        if (SFXList[index].isRandom)
            PlayOneShotRandom(index, volumeScale);
        else
            SFXList[index].audioSource.PlayOneShot(SFXList[index].audioSource.clip, volumeScale);
    }

    private void PlayOneShotRandom(int index)
    {
        var soundManagerSFXList = this.gameObject.transform.GetChild(index).GetComponent<SoundManagerSFXList>();

        Vector2 pitchRange = soundManagerSFXList.PitchRange;
        int sfxListLength = soundManagerSFXList.sfxlist.Length;
        SFXList[index].audioSource.pitch = Random.Range(pitchRange.x, pitchRange.y);
        SFXList[index].audioSource.PlayOneShot(soundManagerSFXList.sfxlist[Random.Range(0, sfxListLength)]);
    }

    private void PlayOneShotRandom(int index, float volumeScale)
    {
        SFXList[index].audioSource.volume = volumeScale;
        PlayOneShotRandom(index);
    }

    private void PlayRandom(int index)
    {
        var _SoundManagerSFXList = this.gameObject.transform.GetChild(index).GetComponent<SoundManagerSFXList>();

        Vector2 _PitchRange = _SoundManagerSFXList.PitchRange;
        int _SFXListLength = _SoundManagerSFXList.sfxlist.Length;
        SFXList[index].audioSource.clip = _SoundManagerSFXList.sfxlist[Random.Range(0, _SFXListLength)];
        SFXList[index].audioSource.pitch = Random.Range(_PitchRange.x, _PitchRange.y);
        SFXList[index].audioSource.Play();
    }

    public bool IsPlaying(string name)
    {
        int index = SFXList.FindIndex((SFXObject sFXObject) => sFXObject.name == name);

        if (index < 0)
        {
            Debug.LogWarning("Index out of range. " + name + " not found. It's correct name of this sound?");
            return false;
        }
        return SFXList[index].audioSource.isPlaying;
    }

}

[System.Serializable]
public class SFXObject
{
    public string name;
    public AudioSource audioSource;
    public bool isRandom;

    public SFXObject(string name, AudioSource audioSource, bool isRandom)
    {
        this.name = name;
        this.audioSource = audioSource;
        this.isRandom = isRandom;
    }
}

