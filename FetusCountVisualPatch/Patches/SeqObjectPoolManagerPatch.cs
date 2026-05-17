using FetusCountVisualPatch.FetusSprite;
using HarmonyLib;
using MBMScripts;
using System.Collections.Generic;
using UnityEngine;

namespace FetusCountVisualPatch.Patches;

[HarmonyPatch(typeof(SeqObjectPoolManager), nameof(SeqObjectPoolManager.Initialize))]
public class SeqObjectPoolManagerPatch
{
    [HarmonyPostfix]
    public static void InitializePostfix()
    {
        var instance = SeqObjectPoolManager.Instance;
        Dictionary<string, List<PooledObject>> PooledObjectDictionary =
            Traverse.Create(instance)
            .Field<Dictionary<string, List<PooledObject>>>("m_PooledObjectDictionary")
            .Value;
        List<PooledObject> list0;
        List<GameObject> list = [];
        PooledObjectDictionary.TryGetValue("SlaveStatePlate".ToLower(), out list0);
        list.Add(list0[0].gameObject);
        PooledObjectDictionary.TryGetValue("SexStatePlate".ToLower(), out list0);
        list.Add(list0[0].gameObject);

        for (int i = 0; i < list.Count; i++)
        {
            // SlaveStatePlate/Object/StatePlate/State/ovum/FetusCount == 0/False
            // SexStatePlate/Object/StatePlate/State/ovum/slaveui_icon_ovum/False
            string[] inject = ["FetusCount == 0", "slaveui_icon_ovum"];
            var slaveuiIconFetusLayer = list[i].transform
                .Find("Object/StatePlate/State/ovum/" + inject[i] + "/False").gameObject;
            var FalseList = Traverse.Create(slaveuiIconFetusLayer.GetComponent<UpdaterGameObject>())
                .Field<List<GameObject>>("m_FalseGameObjectList")
                .Value;
            var template_fetus_3 = slaveuiIconFetusLayer.transform.Find("slaveui_icon_fetus_3").gameObject;
            //var enumType = Traverse.Create(template_fetus_3.GetComponent<ReferenceCharacterStatePlate>())
            //    .Field("m_DataType").GetValueType();

            #region fetus_4
            GameObject fetus_4 = GameObject.Instantiate(template_fetus_3, slaveuiIconFetusLayer.transform);
            FalseList.Add(fetus_4);

            fetus_4.name = "slaveui_icon_fetus_4";
            Traverse.Create(fetus_4.GetComponent<ReferenceCharacterStatePlate>())
                .Field("m_DataType").SetValue(ModEntry.fetus4EDataType);

            var sprite_fetus_4 = fetus_4.transform.GetChild(0).gameObject;
            sprite_fetus_4.name = "slaveui_icon_fetus_4";
            sprite_fetus_4.GetComponent<SpriteRenderer>().sprite = UIFetusSprite.SpriteFetus4;
            #endregion

            #region fetus_5
            GameObject fetus_5 = GameObject.Instantiate(template_fetus_3, slaveuiIconFetusLayer.transform);
            FalseList.Add(fetus_5);

            fetus_5.name = "slaveui_icon_fetus_5";
            Traverse.Create(fetus_5.GetComponent<ReferenceCharacterStatePlate>())
                .Field("m_DataType").SetValue(ModEntry.fetus5EDataType);

            var sprite_fetus_5 = fetus_5.transform.GetChild(0).gameObject;
            sprite_fetus_5.name = "slaveui_icon_fetus_5";
            sprite_fetus_5.GetComponent<SpriteRenderer>().sprite = UIFetusSprite.SpriteFetus5;
            #endregion
        }
    }
}