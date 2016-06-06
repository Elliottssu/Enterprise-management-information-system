using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class PageBar
    {
        /// <summary>
        /// 产生数字页码条。
        /// </summary>
        /// <param name="pageIndex">当前页码值</param>
        /// <param name="pageCount">总的页数</param>
        /// <returns></returns>
        public static string GetPageBar(int pageIndex, int pageCount)
        {
            if (pageCount == 1)//总页数为1 则不显示
            {
                return string.Empty;
            }
            int start = pageIndex - 5;//起始位置,要求页面上显示10个数字页码
            if (start < 1)
            {
                start = 1;
            }
            int end = start + 9;//终止位置
            if (end > pageCount)  //不能超了pagecount
            {
                end = pageCount;
            }
            StringBuilder sb = new StringBuilder();
            for (int i = start; i <= end; i++)
            {
                if (i == pageIndex)  //如何在当前页码值 则直接追加 不加超链接
                {
                    sb.Append(i);
                }
                else
                {
                    sb.Append(string.Format("<a href='?pageIndex={0}'>{0}</a>", i));//追加超链接
                }
            }
            return sb.ToString();
        }
    }
}
