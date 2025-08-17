using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class MyCharacterSpawnTweaks : IWorkshopCharacterModifier
{
    public List<Character> WorkShopModifyCharacters(List<Character> characters, string challengeCode)
    {
        if (challengeCode != GoldenTowerControl.GetChallengeModeName())
        {
            characters.Add(AddCeShiRenYuan());
        }      
        return characters;
    }




    static Character AddCeShiRenYuan()
    {
        // 创建角色对象
        Character character = new Character();

        character.AttributePart.Charm = 5.0f; // 高魅力，符合唐梦璃的特性
        character.AttributePart.Strength = 4.0f;
        character.AttributePart.Physique = 6.0f;
        character.AttributePart.Speed = 6.5f;
        character.AttributePart.Comprehension = 5.0f;
        character.AttributePart.Concentrate = 4.0f;

        // 设置角色个人数据
        character.PersonalData.FamilyName = "测试";
        character.PersonalData.SecondName = "人员";
        character.PersonalData.Gender =1; 
        character.PersonalData.SpriteName = character.PersonalData.GetFullName();
        // 将角色设置为玩家
        Controller.GameData.Player = character;

        character.Identifier = "CESHIRENYUAN";

        // 添加特性天赋
        Talent KeyTalent = new TalentFromEffect(new HanZhangEffect()); 
        KeyTalent.IsBuff = true;
        TalentManager.AddTalent(KeyTalent);

        return character;
    }

}

