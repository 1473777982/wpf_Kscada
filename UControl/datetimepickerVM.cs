//using Microsoft.Practices.Prism.Commands;

using Prism.Commands;

namespace R2R.UControl
{
    public class datetimepickerVM : ViewModelBase
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public datetimepickerVM()
        {
            TimeChangeCmd = new DelegateCommand(slect_from_influx);
        }
        #region 属性

        private string _DateTime_end;
        /// <summary>
        /// 结束时间
        /// </summary>
        public string DateTime_end
        {
            get { return _DateTime_end; }
            set
            {
                _DateTime_end = value;
                this.RaisePropertyChangedEvent("DateTime_end");
            }
        }
        private string _DateTime_start;
        /// <summary>
        /// 结束时间
        /// </summary>
        public string DateTime_start
        {
            get { return _DateTime_start; }
            set
            {
                _DateTime_start = value;
                this.RaisePropertyChangedEvent("DateTime_start");
            }
        }
        #endregion

        #region 命令
        /// <summary>
        /// 时间触发命令
        /// </summary>
        public DelegateCommand TimeChangeCmd { get; private set; }
        #endregion

        #region 方法
        private void slect_from_influx()
        {
            //MessageBox.Show("时间：" + this.MyDateTime);

        }

        #endregion
    }
}
