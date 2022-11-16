using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureRadio.Uwp.Models.Data.Radio
{
    /// <summary>
    /// 电台集合.
    /// </summary>
    /// <typeparam name="T">结果类型.</typeparam>
    public class RadioSet<T>
        where T : class
    {
        /// <summary>
        /// 创建 <see cref="RadioSet{T}"/> 的新实例.
        /// </summary>
        /// <param name="items">条目列表.</param>
        /// <param name="isEnd">是否已经获取完全部结果.</param>
        public RadioSet(IEnumerable<T> items, bool isEnd)
        {
            Items = items;
            IsEnd = isEnd;
        }

        /// <summary>
        /// 条目列表.
        /// </summary>
        public IEnumerable<T> Items { get; }

        /// <summary>
        /// 是否已经获取完全部结果.
        /// </summary>
        public bool IsEnd { get; }
    }
}
