using System.ComponentModel;

namespace R2R.UControl
{
    public class ViewModelBase : INotifyPropertyChanged
    {
        /// <summary>
        /// 自定义通知属性事件
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;
        public virtual void RaisePropertyChangedEvent(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
