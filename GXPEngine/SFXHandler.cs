using System.Collections.Generic;

namespace GXPEngine
{
    public class SFXHandler
    {
        private Dictionary<string, Sound> soundLibrary = new Dictionary<string, Sound>();
        private float volume;
        public SFXHandler(Dictionary<string, Sound> soundLibrary, float volume)
        {
            this.volume = volume;
            this.soundLibrary = soundLibrary;
        }

        public void PlaySound(string soundEffectName)
        {
            soundLibrary[soundEffectName].Play(false, 0,volume);
        }
        
        
    }
}