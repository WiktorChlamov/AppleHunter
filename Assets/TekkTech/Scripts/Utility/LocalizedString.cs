using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class LocalizedString
{
    public LanguageTags LanguageTag;
    public string text {
        get {
            return LocalizationManager.GetTextForTag(LanguageTag);
        }
        private set { }
    }
    private UnityAction callToSet;

    public LocalizedString(LanguageTags tag, UnityAction callToSetString = null)
    {
        LanguageTag = tag;
        callToSet = callToSetString;

        LocalizationManager.RegisterLanguageString(this);
    }

    public void SetAction(UnityAction callToSetString)
    {
        LocalizationManager.UnregisterLanguageString(this);
        callToSet = callToSetString;
        LocalizationManager.RegisterLanguageString(this);
    }

    public void LanguageSwitch()
    {
        text = LocalizationManager.GetTextForTag(LanguageTag);
        callToSet?.Invoke();
    }

    public static implicit operator string(LocalizedString t) => t.text;
    public static implicit operator LocalizedString(LanguageTags tag) => new LocalizedString(tag);

    public override string ToString() => text;

    public override bool Equals(object obj)
    {
        if (obj is LocalizedString)
        {
            if (this.LanguageTag.Equals(((LocalizedString)obj).LanguageTag))
            {
                return true;
            }
        }
        return false;
    }

    public override int GetHashCode()
    {
        return LanguageTag.GetHashCode();
    }
}
