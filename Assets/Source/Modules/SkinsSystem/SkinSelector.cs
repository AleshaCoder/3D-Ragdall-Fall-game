using Analytics;
using Assets.Source.Structs.Scripts;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace SkinsSystem
{
    [Serializable]
    public class SkinSelector
    {
        [field: SerializeField] public Skin ActiveSkin { get; private set; }
        [SerializeField] private List<Skin> _skins;
        public event Action<Skin> OnSkinChanged;
        
        public void ChangeSkin(RagdollType ragdollType)
        {
            var templateSkin = _skins.FirstOrDefault(skin => skin.RagdollType == ragdollType);

            if (templateSkin == default)
                return;

            var root = ActiveSkin.Root;
           // var position = ActiveSkin.DefaultPositionChar;
            //var rotation = ActiveSkin.DefaultRotationChar;

            UnityEngine.Object.Destroy(root.gameObject);

            var newRoot = UnityEngine.Object.Instantiate(templateSkin.Root, ActiveSkin.DefaultPositionChar, ActiveSkin.DefaultRotationChar);

            var newskin = newRoot.GetComponent<Skin>();

            ActiveSkin = newskin;
            ActiveSkin.SetDefaultPositionAndRotation();

            AnalyticsSender.SelectSkin(ragdollType.ToString());
            OnSkinChanged?.Invoke(newskin);
        }
    }
}
