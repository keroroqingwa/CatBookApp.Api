using System;
using System.Collections.Generic;
using System.Text;

namespace CatBookApp.BookStatistics.Dto
{
    /// <summary>
    /// 适用于有x轴和y轴的折线图
    /// </summary>
    public class XYAxisStatisticOutput
    {
        /// <summary>
        /// x轴
        /// </summary>
        public List<string> xAxis { get; set; }

        /// <summary>
        /// y轴
        /// </summary>
        public List<int> yAxis { get; set; }
    }
}
