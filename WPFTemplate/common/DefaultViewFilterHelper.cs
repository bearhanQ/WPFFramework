using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFTemplate
{
    internal class DefaultViewFilterHelper
    {
        private ICollectionView _collectionView;
        private DataView _dataView;
        private Dictionary<string, string> _filterDic;
        public bool MatchWholeWord = true;

        public DefaultViewFilterHelper()
        {
            if (_filterDic == null)
            {
                _filterDic = new Dictionary<string, string>();
            }
        }

        public void SetView(ICollectionView collectionView)
        {
            this._collectionView = collectionView;
        }

        public void SetView(DataView dataView)
        {
            this._dataView = dataView;
        }

        public void ExecuteFilter(object parameter)
        {
            if (this._collectionView != null)
            {
                if (_filterDic.Count > 0)
                {
                    if (_filterDic.ContainsKey("stringlistmark"))
                    {
                        Predicate<object> filter = item =>
                        {
                            bool bo = true;
                            var value = _filterDic["stringlistmark"];
                            var dataItem = item.ToString();
                            bo = bo && MatchWholeWord ? dataItem.Equals(value) : dataItem.Contains(value);
                            return bo;
                        };
                        _collectionView.Filter = filter;
                    }
                    else
                    {
                        Predicate<object> filter = item =>
                        {
                            bool bo = true;
                            foreach (var keyValue in _filterDic)
                            {
                                var key = keyValue.Key;
                                var value = keyValue.Value;
                                var dataItem = item.GetType().GetProperty(key).GetValue(item).ToString();
                                bo = bo && MatchWholeWord ? dataItem.Equals(value) : dataItem.Contains(value);
                            }
                            return bo;

                        };
                        _collectionView.Filter = filter;
                    }
                }
                else
                {
                    _collectionView.Filter = null;
                }
            }
            else
            {
                if (_dataView != null)
                {
                    if (_filterDic.Count > 0)
                    {
                        var filtercollection = MatchWholeWord ? _filterDic.Select(x => $"{x.Key}='{x.Value}'").ToList() : _filterDic.Select(x => $"{x.Key} like '%{x.Value}%'").ToList();
                        string filter = string.Join(" and ", filtercollection);
                        _dataView.RowFilter = filter;
                    }
                    else
                    {
                        _dataView.RowFilter = null;
                    }
                }
            }
        }

        public bool CanExecuteFilter(object parameter)
        {
            if (parameter == null)
            {
                return false;
            }

            var mixparameter = parameter.ToString().Split('&');
            if (mixparameter.Length < 2)
            {
                return false;
            }
            var key = mixparameter[0];
            var value = mixparameter[1];

            if (_filterDic.TryGetValue(key, out string existingFilter))
            {
                if (value == "-")
                {
                    _filterDic.Remove(key);
                }
                else
                {
                    _filterDic[key] = value;
                }
            }
            else
            {
                _filterDic.Add(key, value);
            }

            return true;
        }

        public void ClearDefaultView()
        {
            _collectionView = null;
            _dataView = null;
        }
    }
}
