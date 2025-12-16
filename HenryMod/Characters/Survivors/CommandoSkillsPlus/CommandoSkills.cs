namespace CustomSkillsTutorial
{
    [BepInPlugin("com.matterwoven.InfernusMod", "InfernusMod", "1.0.0")]
    [BepInDependency(R2API.ContentManagement.R2APIContentManager.PluginGUID)]
    [BepInDependency(LanguageAPI.PluginGUID)]
    public class InfernusPlugin : BaseUnityPlugin
    {
        public void Awake()
        {
            RegisterSkills();
        }

        private void RegisterSkills()
        {
            // Create Primary
            CreateSimpleBullet(skillSlot: SkillSlot.Primary);

            // Create Secondary
            CreateNapalm(skillSlot: SkillSlot.Secondary);

            // Create Utility
            CreateSimpleBullet(skillSlot: SkillSlot.Utility);

            // Create Special
            CreateSimpleBullet(skillSlot: SkillSlot.Special);
        }
    }
}