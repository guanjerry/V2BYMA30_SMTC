using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mirle.SMTCV.Conveyor.Controller
{
    public class clsTool
    {
        public static TEnum funGetEnumValue<TEnum>(int EnumAsInt) where TEnum : struct
        {
            TEnum enumType = (TEnum)Enum.GetValues(typeof(TEnum)).GetValue(0);
            Enum.TryParse<TEnum>(EnumAsInt.ToString(), out enumType);
            return enumType;
        }

        public static void Signal_Show(bool bFlag, ref Label label)
        {
            if (bFlag == true) label.BackColor = Color.Yellow;
            else label.BackColor = Color.Transparent;
        }
    }
}
