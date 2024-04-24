using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFTemplate
{
    public class ViewModelFactory
    {
        private static readonly object _locker = new object();

        private readonly Dictionary<Type, object> _viewModels = new Dictionary<Type, object>();

        private static ViewModelFactory _instance;

        public static ViewModelFactory Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_locker)
                    {
                        _instance = new ViewModelFactory();
                    }
                }
                return _instance;
            }
        }

        public ViewModelFactory()
        {

        }

        public T CreateInstance<T>() where T : ViewModelBase
        {
            if (_viewModels.ContainsKey(typeof(T)))
            {
                return _viewModels[typeof(T)] as T;
            }
            var instance = Activator.CreateInstance<T>();
            _viewModels.Add(typeof(T), instance);

            return instance;
        }
    }
}
