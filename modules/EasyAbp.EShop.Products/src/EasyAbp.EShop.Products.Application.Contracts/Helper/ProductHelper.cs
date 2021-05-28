namespace EasyAbp.EShop.Products.Helpers
{
    using System;

    /// <summary>
    /// Defines the <see cref="ProductHelper" />.
    /// </summary>
    public static class ProductHelper
    {
        public static string ToCNNumberString(this int number)
        {
            int absNumber = Math.Abs(number);

            string result = string.Empty;

            string cnString = "零一二三四五六七八九";
            string unitString = " 十百千";
            string bigUnitString = " 万亿兆吉";
            string absNumberSrting = absNumber.ToString();
            // 分割数量
            int partitionCount = 4;
            // 开始分割索引
            int sIndex = 0;
            // 总分割次数
            int count = Convert.ToInt32(Math.Ceiling(absNumberSrting.Length * 1.0d / partitionCount));
            for (int i = 0; i < count; i++)
            {
                // 获取要转换的短数字字符串
                int tIndex = (count - i) * partitionCount;
                int subLength = partitionCount;
                if (tIndex > absNumberSrting.Length && i == 0)// 第一次进入，查询最后一页的数据条数
                {
                    subLength = partitionCount - (tIndex - absNumberSrting.Length);
                }
                string tempNumberString = Convert.ToInt32(absNumberSrting.Substring(sIndex, subLength)).ToString();
                sIndex += subLength;

                for (int j = 0; j < tempNumberString.Length; j++)
                {
                    if (i > 0 && i < count - 1)// 不是最后一页
                    {
                        if (j == 0 && tempNumberString.Length < partitionCount)
                        {
                            result += cnString[0];
                        }
                    }
                    else if (i == count - 1 && count > 1)
                    {
                        if (j == 0 && tempNumberString.Length < partitionCount)
                        {
                            if (result.Length > 0 && result[result.Length - 1].Equals(cnString[0]) == false)
                                result += cnString[0];
                        }
                    }
                    if (Convert.ToInt32(tempNumberString) == 0 && (result.Length <= 0 || result[result.Length - 1].Equals(cnString[0]) == false))
                    {
                        result += cnString[0];
                        break;
                    }
                    int unitIdx = -1;
                    // 不到最后一位数，计算小单位索引
                    if (tempNumberString.Length - (j + 1) != 0)// 小单位
                    {
                        int tempIdx = j;
                        if (tempNumberString.Length != partitionCount)
                        {
                            tempIdx += partitionCount - tempNumberString.Length;
                        }
                        unitIdx = (unitString.Length - 1) - tempIdx;
                    }
                    // 数字转中文
                    int cnIdx = Convert.ToInt32(tempNumberString[j].ToString());
                    result += $"{cnString[cnIdx]}{(unitIdx != -1 ? unitString[unitIdx].ToString() : string.Empty)}";
                    // 看看后面的字符串是否全是0，全是0这一页加个单位转换就结束了，不是的话，就加个零，然后把剩余的数字拿来重新骚整一下。
                    string tempString = tempNumberString.Substring(j + 1);
                    if (string.IsNullOrEmpty(tempString) == false)
                    {
                        int temp = Convert.ToInt32(tempString);
                        if (temp > 0)
                        {
                            if (tempString.Equals(temp.ToString()) == false)
                            {
                                result += cnString[0];
                                tempNumberString = temp.ToString();
                                j = -1;
                            }
                        }
                        else
                        {
                            // 加个单位退出循环
                            break;
                        }
                    }
                }
                if (i != count - 1)// 大单位
                {
                    result += bigUnitString[count - (i + 1)];
                }
                // 如果后面全是0，整个转换就完成了
                string surplusString = absNumberSrting.Substring(subLength);
                if (string.IsNullOrEmpty(surplusString) == false)
                {
                    if (surplusString.Length > 7 && Convert.ToInt64(surplusString) == 0)
                    {
                        break;
                    }
                    else if (surplusString.Length <= 7 && Convert.ToInt32(surplusString) == 0)
                    {
                        break;
                    }
                }
            }
            if (number < 0)
            {
                result = $"负{result}";
            }
            return result;
        }

    }
}