﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace PureRadio.DataModel
{
    public class ContentIncrementalLoading<T> : ObservableCollection<T>, ISupportIncrementalLoading
    {
        // 是否正在异步加载中
        private bool _isBusy = false;
        // 防止数据重复
        private int? _lastIndex;

        // 提供数据的 Func
        // 第一个参数：增量加载的起始索引；第二个参数：需要获取的数据量；第三个参数：获取到的数据集合
        private Func<int, int, List<T>> _funcGetData;
        // 最大可显示的数据量
        private uint _totalCount = 0;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="totalCount">最大可显示的数据量</param>
        /// <param name="getDataFunc">提供数据的 Func</param>
        public ContentIncrementalLoading(uint totalCount, Func<int, int, List<T>> getDataFunc)
        {
            _funcGetData = getDataFunc;
            _totalCount = totalCount;
        }

        /// <summary>
        /// 是否还有更多的数据
        /// </summary>
        public bool HasMoreItems
        {
            get { return this.Count < _totalCount; }
        }

        /// <summary>
        /// 异步加载数据（增量加载）
        /// </summary>
        /// <param name="count">需要加载的数据量</param>
        /// <returns></returns>
        public IAsyncOperation<LoadMoreItemsResult> LoadMoreItemsAsync(uint count)
        {

            if (_isBusy)
            {
                throw new InvalidOperationException("忙着呢，先不搭理你");
            }
            _isBusy = true;

            var dispatcher = Window.Current.Dispatcher;

            return AsyncInfo.Run
            (
                (token) => Task.Run<LoadMoreItemsResult>
                (
                    async () =>
                    {
                        try
                        {
                            // 模拟长时任务
                            // await Task.Delay(1000);

                            // 增量加载的起始索引
                            var startIndex = this.Count / 12 + 1;

                            if (startIndex == _lastIndex)
                            {
                                _isBusy = false;
                                return new LoadMoreItemsResult { Count = 0 };
                            }

                            await dispatcher.RunAsync
                            (
                                 CoreDispatcherPriority.Normal,
                                 () =>
                                 {
                                     // 通过 Func 获取增量数据
                                     var items = _funcGetData(startIndex, (int)count);
                                     if (items == null || items.Count == 0)
                                     {
                                         _totalCount = (uint)this.Count;
                                         _isBusy = false;
                                         return;
                                     }
                                     foreach (var item in items)
                                     {
                                         this.Add(item);
                                     }
                                     _lastIndex = startIndex;
                                 }
                             );

                            // Count - 实际已加载的数据量
                            return new LoadMoreItemsResult { Count = (uint)this.Count / 12 };
                        }
                        finally
                        {
                            _isBusy = false;
                        }
                    },
                    token
                )
            );
        }
    }
}
