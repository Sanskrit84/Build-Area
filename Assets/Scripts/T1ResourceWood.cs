using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Items/Tier 1 Resource")]
public class T1ResourceWood : Item
{
    [Range(1, 6)]
    public int Tier = 1;

    public int mc;
    public int sg;
    public int mor;
    public int moe;
    public int wml;
    public int ib;
    public int cpag;
    public int cpeg;
    public int spg;
    public int tpg;
    public int sh;

    public override string GetItemType()
    {
        sb.Length = 0;
        sb.Append("Tier 1 Resource - Wood");
        return sb.ToString();
    }

    public override string GetDescritpion()
    {
        sb.Length = 0;
        sb.Append("A Common wood.");
        sb.AppendLine();
        sb.Append("MC: " + mc.ToString());
        sb.AppendLine();
        sb.Append("SG: " + sg.ToString());
        sb.AppendLine();
        sb.Append("MOR: " + mor.ToString());
        sb.AppendLine();
        sb.Append("MOE: " + moe.ToString());
        sb.AppendLine();
        sb.Append("WML: " + wml.ToString());
        sb.AppendLine();
        sb.Append("IB: " + ib.ToString());
        sb.AppendLine();
        sb.Append("CPAG: " + cpag.ToString());
        sb.AppendLine();
        sb.Append("CPEG: " + cpeg.ToString());
        sb.AppendLine();
        sb.Append("SPG: " + spg.ToString());
        sb.AppendLine();
        sb.Append("TPG: " + tpg.ToString());
        sb.AppendLine();
        sb.Append("SH: " + sh.ToString());

        return sb.ToString();
    }
}
