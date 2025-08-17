using System;
using System.Collections.Generic;


[System.Serializable]
public class HanZhangEffect : ExclusiveEffect
{
    private const float bonusRate = 0.05f;

    public HanZhangEffect()
    {
        IsPostiveEffect = true;
        Quality = Quality.Legendary;
        Name = "含章可贞";
    }

    public override string getDiscription()
    {
        return "每次使用调息时，攻击与防御提升5%。";
    }

    public override void ExcecuteSkill(ref Skill skill)
    {
        base.ExcecuteSkill(ref skill);

        // 判断是否为“调息”类型技能（可根据你项目里实际命名修改判断逻辑）
        if (skill.Name == "调息")
        {
            Loader.EnhanceAttackByMultiply(bonusRate);
            Loader.EnhanceDefenseByMultiply(bonusRate);

            
        }
    }
}


[System.Serializable]
public class ShieldByDefenseEffect : ExclusiveEffect
{
    public ShieldByDefenseEffect()
    {
        IsPostiveEffect = true;
        Quality = Quality.Rare;
        Name = "御盾"; 
    }

    public override string getDiscription()
    {
        return "回合开始时，获得等同于当前防御的护甲。";
    }

    public override void OnTurnStart()
    {
        base.OnTurnStart();
        int def = AttributeCalculator.GetEffectiveDefNum(Loader.Player);
        if (def > 0)
        {
            Loader.ModifyArmor(def);
        }
    }
}

[System.Serializable]
public class TurnStartHealDemo : ExclusiveEffect
{
    public TurnStartHealDemo()
    {
        IsPostiveEffect = true;
        Quality = Quality.Common;
        Name = "回息";
    }
    public override string getDiscription() => "回合开始时，回复自身2%生命。";
    public override void OnTurnStart()
    {
        base.OnTurnStart();
        int heal = (int)Math.Ceiling(Loader.Player.AttributePart.getMaxHealth() * 0.02);
        Loader.RecoverHealth(heal);
    }
}

[System.Serializable]
public class OpponentTurnStartManaStealDemo : ExclusiveEffect
{
    public OpponentTurnStartManaStealDemo()
    {
        IsPostiveEffect = true;
        Quality = Quality.Rare;
        Name = "夺气";
    }
    public override string getDiscription() => "对手回合开始时，夺取其5%内力。";
    public override void OpponentOnTurnStart()
    {
        base.OpponentOnTurnStart();
        var opp = Loader.GetOpponent();
        int steal = (int)Math.Ceiling(opp.Player.AttributePart.Manner * 0.05);
        opp.Player.AttributePart.Manner = Math.Max(0, opp.Player.AttributePart.Manner - steal);              // ← 替代 Mathf.Max
        int maxMana = Loader.Player.AttributePart.getMaxMana();
        Loader.Player.AttributePart.Manner = Math.Min(maxMana, Loader.Player.AttributePart.Manner + steal); // ← 替代 Mathf.Min
    }
}

[System.Serializable]
public class EndTurnManaRegenDemo : ExclusiveEffect
{
    public EndTurnManaRegenDemo()
    {
        IsPostiveEffect = true;
        Quality = Quality.Common;
        Name = "纳气";
    }
    public override string getDiscription() => "回合结束时，恢复100内力。";
    public override void OnTurnEnd()
    {
        base.OnTurnEnd();
        Loader.RecoverMana(100);
    }
}

[System.Serializable]
public class OppEndHealDemo : ExclusiveEffect
{
    public OppEndHealDemo()
    {
        IsPostiveEffect = true;
        Quality = Quality.Common;
        Name = "续命";
    }
    public override string getDiscription() => "对手回合结束时，回复自身2%生命。";
    public override void OpponentOnTurnEnd()
    {
        base.OpponentOnTurnEnd();
        int heal = (int)Math.Ceiling(Loader.Player.AttributePart.getMaxHealth() * 0.02);
        Loader.RecoverHealth(heal);
    }
}


[System.Serializable]
public class ArmorOnHitDemo : ExclusiveEffect
{
    public ArmorOnHitDemo()
    {
        IsPostiveEffect = true;
        Quality = Quality.Uncommon;
        Name = "铁骨";
    }
    public override string getDiscription() => "受到伤害后，获得100点护甲。";
    public override void BeHit(ref int num)
    {
        base.BeHit(ref num);
        Loader.ModifyArmor(100);
    }
}

[System.Serializable]
public class ExtraHealDemo : ExclusiveEffect
{
    public ExtraHealDemo()
    {
        IsPostiveEffect = true;
        Quality = Quality.Uncommon;
        Name = "护元";
    }
    public override string getDiscription() => "治疗量+20%。";
    public override void Recover(ref int num)
    {
        base.Recover(ref num);
        num = (int)Math.Ceiling(num * 1.2);
    }
}



[System.Serializable]
public class DodgeShieldDemo : ExclusiveEffect
{
    public DodgeShieldDemo()
    {
        IsPostiveEffect = true;
        Quality = Quality.Uncommon;
        Name = "回护";
    }
    public override string getDiscription() => "闪避时，获得1层【抵御】。";
    public override void OnDodge()
    {
        base.OnDodge();
        Loader.AddEffect(new DiYu(1));
    }
}

[System.Serializable]
public class ItemHealDemo : ExclusiveEffect
{
    public ItemHealDemo()
    {
        IsPostiveEffect = true;
        Quality = Quality.Common;
        Name = "摄元";
    }
    public override string getDiscription() => "使用道具时，回复300生命与150内力。";
    public override void ConsumeItem(Item item)
    {
        base.ConsumeItem(item);
        Loader.RecoverHealth(300);
        Loader.RecoverMana(150);
    }
}


[System.Serializable]
public class OpponentSkillArmorDemo : ExclusiveEffect
{
    public OpponentSkillArmorDemo()
    {
        IsPostiveEffect = true;
        Quality = Quality.Uncommon;
        Name = "御劲";
    }
    public override string getDiscription() => "对手施放技能时，我方获得120护甲。";
    public override void OpponentExcecuteSkill(ref Skill skill)
    {
        base.OpponentExcecuteSkill(ref skill);
        Loader.ModifyArmor(120);
    }
}
