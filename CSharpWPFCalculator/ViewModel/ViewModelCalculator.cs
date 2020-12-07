using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace CSharpWPFCalculator.ViewModel {
    class ViewModelCalculator : INotifyPropertyChanged  {
        public ViewModelCalculator()
        {
            NumberButtonCmd = new RelayCommand(new Action<object>(NumberButtonPressed));
            EqualButtonCmd = new RelayCommand(new Action<object>(EqualButtonPressed));
        }

        private ICommand m_ButtonCommand1;
        private ICommand m_ButtonCommand2;
        private StringBuilder _SBCalculation = new StringBuilder();
        private string _Calculation;
        private Boolean doReset = false;

        public ICommand NumberButtonCmd
        {
            get
            {
                return m_ButtonCommand1;
            }
            set
            {
                m_ButtonCommand1 = value;
            }
        }

        public ICommand EqualButtonCmd
        {
            get
            {
                return m_ButtonCommand2;
            }
            set
            {
                m_ButtonCommand2 = value;
            }
        }

        public void NumberButtonPressed(object obj)
        {
            if (doReset)
            {
                _SBCalculation.Clear();
                doReset = false;
            }

            _SBCalculation.Append(obj.ToString());
            Calculation = _SBCalculation.ToString();
        }

        public void EqualButtonPressed(object obj)
        {
            try
            {
                DataTable dt = new DataTable();
                var v = dt.Compute(Calculation, "");
                Calculation = v.ToString();
            
            } 
            catch (Exception ex)
            {
                Calculation = "Error";

            } finally
            {
                doReset = true;
            }
            
        }

        #region properties

        public string Calculation {
            get { return _Calculation; }
            set { 
                _Calculation = value;
                OnPropertyChange("Calculation");
            }
        }

        #endregion



        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChange(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
