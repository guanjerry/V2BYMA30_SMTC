using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace unitTest.Proc
{
    public class clsBufferTool
    {

        /// <summary>
        /// 將數值轉成列舉值
        /// </summary>
        /// <typeparam name="TEnum">列舉Type</typeparam>
        /// <param name="EnumAsString">列舉數值</param>
        /// <returns>傳回列舉值</returns>
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
