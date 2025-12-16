using System;
using BepInEx;
using EntityStates;
using R2API;
using RoR2;
using RoR2.Skills;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace CustomSkillsTutorial
{
    [BepInDependency(R2API.ContentManagement.R2APIContentManager.PluginGUID)]
    [BepInDependency(LanguageAPI.PluginGUID)]

    [BepInPlugin(
        "com.matterwoven.InfernusMod",
        "InfernusMod",
        "1.0.0")]
    public class CustomSecondary : BaseUnityPlugin
    {
        public void Awake()
        {
            // First we must load our survivor's Body prefab. For this tutorial, we are making a skill for Commando
            // If you would like to load a different survivor, you can find the key for their Body prefab at the following link
            // https://xiaoxiao921.github.io/GithubActionCacheTest/assetPathsDump.html
            GameObject commandoBodyPrefab = Addressables.LoadAssetAsync<GameObject>("RoR2/Base/Commando/CommandoBody.prefab").WaitForCompletion();

            // We use LanguageAPI to add strings to the game, in the form of tokens
            // Please note that it is instead recommended that you use a language file.
            // More info in https://risk-of-thunder.github.io/R2Wiki/Mod-Creation/Assets/Localization/
            LanguageAPI.Add("COMMANDO_SECONDARY_NAPALM_NAME", "Napalm");
            LanguageAPI.Add("KEYWORD_NAPALM", "<style=cisNapalm>Napalm</style><style=cSub>Increases damage taken by 40%</style>");
            LanguageAPI.Add("COMMANDO_SECONDARY_NAPALM_DESCRIPTION", $"Spew an incendiary mixture, dealing <style=cIsDamage>30% damage</style>, applying <style=cIsUtility>Slow</style>, and coating targets in <style=cIsUtility>Napalm</style>.");
            // Now we must create a SkillDef
            SkillDef mySkillDef = ScriptableObject.CreateInstance<SkillDef>();

            //Check step 2 for the code of the CustomSkillsTutorial.MyEntityStates.SimpleBulletAttack class
            mySkillDef.activationState = new SerializableEntityStateType(typeof(CustomSecondary.MyEntityStates.SimpleBulletAttack));
            mySkillDef.activationStateMachineName = "Weapon";
            mySkillDef.baseMaxStock = 1;
            mySkillDef.baseRechargeInterval = 0f;
            mySkillDef.beginSkillCooldownOnSkillEnd = true;
            mySkillDef.canceledFromSprinting = false;
            mySkillDef.cancelSprintingOnActivation = true;
            mySkillDef.fullRestockOnAssign = true;
            mySkillDef.interruptPriority = InterruptPriority.Any;
            mySkillDef.isCombatSkill = true;
            mySkillDef.mustKeyPress = false;
            mySkillDef.rechargeStock = 1;
            mySkillDef.requiredStock = 1;
            mySkillDef.stockToConsume = 1;
            // For the skill icon, you will have to load a Sprite from your own AssetBundle
            mySkillDef.icon = null;
            mySkillDef.skillDescriptionToken = "COMMANDO_SECONDARY_NAPALM_DESCRIPTION";
            mySkillDef.skillName = "COMMANDO_SECONDARY_NAPALM_NAME";
            mySkillDef.skillNameToken = "COMMANDO_SECONDARY_NAPALM_NAME";

            // This adds our skilldef. If you don't do this, the skill will not work.
            ContentAddition.AddSkillDef(mySkillDef);

            // Now we add our skill to one of the survivor's skill families
            // You can change component.primary to component.secondary, component.utility and component.special
            SkillLocator skillLocator = commandoBodyPrefab.GetComponent<SkillLocator>();
            SkillFamily skillFamily = skillLocator.secondary.skillFamily;

            // If this is an alternate skill, use this code.
            // Here, we add our skill as a variant to the existing Skill Family.
            Array.Resize(ref skillFamily.variants, skillFamily.variants.Length + 1);
            skillFamily.variants[skillFamily.variants.Length - 1] = new SkillFamily.Variant
            {
                skillDef = mySkillDef,
                unlockableName = "",
                viewableNode = new ViewablesCatalog.Node(mySkillDef.skillNameToken, false, null)
            };
        }
    }
}